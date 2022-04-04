using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;
using Dinah.Core;
using Dinah.Core.Processes;

namespace GitHubReleaser
{
    public partial class Form1 : Form
	{
		private WindowsService dropboxWindowsService { get; } = new WindowsService("DbxSvc");
		private WindowsProgram dropboxDesktop { get; } = new WindowsProgram(@"C:\Program Files (x86)\Dropbox\Client\Dropbox.exe");

		Octokit.GitHubClient gitHubClient;
		long repoId;

		List<string> _gitStatuses = new();
		List<string> gitStatuses
		{
			get => _gitStatuses;
			set
			{
				_gitStatuses = value ?? new();
				updateWarnings();
			}
		}

		bool _versionCanRelease;
		bool versionCanRelease
		{
			get => _versionCanRelease;
			set
			{
				if (_versionCanRelease == value)
					return;
				_versionCanRelease = value;
				updateWarnings();
			}
		}

		ProjectSettings selectedProject;
		GitClient git;

		public Form1()
		{
			InitializeComponent();

			if (this.DesignMode)
				return;

//hideTabs(tabControl1);

			gitTab.VisibleChanged += GitTab_VisibleChanged;
			versionTab.VisibleChanged += VersionTab_VisibleChanged;
			latestCommitsTab.VisibleChanged += LatestCommitsTab_VisibleChanged;
		}

		private static void hideTabs(TabControl tabControl)
		{
			tabControl.Appearance = TabAppearance.FlatButtons;
			tabControl.ItemSize = new Size(0, 1);
			tabControl.SizeMode = TabSizeMode.Fixed;
			foreach (TabPage tab in tabControl.TabPages)
				tab.Text = "";
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			if (this.DesignMode)
				return;

			var selectProjectDialog = new SelectProjectDialog();
			if (selectProjectDialog.ShowDialog() != DialogResult.OK)
			{
				MessageBox.Show("ERROR. No project selected");
				Environment.Exit(0);
				return;
			}

			await setProjectAsync(selectProjectDialog.SelectedProject);
			updateWarnings();
		}

		private async Task setProjectAsync(ProjectSettings projectSettings)
        {
            selectedProject = projectSettings;

			this.Text = $"Release {selectedProject.Name}";

			git = new(new DirectoryInfo(selectedProject.GitDirectory));

            gitHubClient = new(new Octokit.ProductHeaderValue($"{selectedProject.Name}_Releaser")) { Credentials = new(SECRETS.GithubApiToken) };

            var repo = await gitHubClient.Repository.Get("rmcrackan", selectedProject.Name);
            repoId = repo.Id;
        }

        private void updateWarnings()
		{
			var warnings = new List<string>();

			warnings.AddRange(gitStatuses);

			if (!versionCanRelease)
				warnings.Add("Need bigger current version");


			warningsLbl.Text = warnings.Any() ? warnings.Aggregate((a, b) => $"{a}\r\n{b}") : "";
		}

		#region next/prev buttons
		private void nextBtn_Click(object sender, EventArgs e) => tabControl1.SelectedIndex = tabIncr % tabControl1.TabCount;
		private void prevBtn_Click(object sender, EventArgs e) => tabControl1.SelectedIndex = tabDecr % tabControl1.TabCount;
		private int tabIncr => tabControl1.SelectedIndex + 1;
		private int tabDecr => tabControl1.SelectedIndex - 1 + tabControl1.TabCount;
		#endregion

		#region Tab: Stop Dropbox
		private void stopDropboxBtn_Click(object sender, EventArgs e)
		{
			// order is not important
			dropboxWindowsService.Stop();
			dropboxDesktop.Kill();
		}
		#endregion

		#region Tab: Git
		private void GitTab_VisibleChanged(object sender, EventArgs e)
		{
			if (!gitTab.Visible)
				return;

			var results = git.GetSyncStatusesRecrusive();

			this.gitStatuses = results;

			gitStatusLbl.Text
				= results.Any()
				? $"Git status: out of sync.\r\n{results.Aggregate((a, b) => $"{a}\r\n{b}")}"
				: $"Git status: clean";
		}
		#endregion

		#region Tab: Version #
		private async void VersionTab_VisibleChanged(object sender, EventArgs e)
		{
			if (!versionTab.Visible)
				return;

			// if (tabControl1.SelectedTab == versionTab) ...

			var published = await getLastPublishedVersionAsync();
			var current = getCurrentVersion();

			lastPublishedVerValueLbl.Text = $"{published}";
			currVersionValueLbl.Text = $"{current}";

			// warn if not greater. compare without revision
			var published3 = new Version(published.Major, published.Minor, published.Build);
			var current3 = new Version(current.Major, current.Minor, current.Build);

			versionCanRelease = published3 < current3;

			if (versionCanRelease)
			{
				finalVersionTb.Text = $"v{current3}";
				finalTitleTb.Text = $"{selectedProject.Name} {current3}";
			}
		}

		private async Task<Version> getLastPublishedVersionAsync()
		{
			// https://octokitnet.readthedocs.io/en/latest/releases/
			var releases = await gitHubClient.Repository.Release.GetAll("rmcrackan", selectedProject.Name);
			var latest = releases.First(r => !r.Draft && !r.Prerelease);
			var latestVersionString = latest.TagName.Trim('v');
			return Version.Parse(latestVersionString);
		}

		private Version getCurrentVersion()
		{
			var xPath = "/Project/PropertyGroup/Version/text()";
			var value = new XPathDocument(selectedProject.ProjectWithVersion)
				.CreateNavigator()
				.SelectSingleNode(xPath)
				.Value;
			return Version.Parse(value);
		}

		private void verRefreshBtn_Click(object sender, EventArgs e)
		{
			var current = getCurrentVersion();
			currVersionValueLbl.Text = $"{current}";
		}
		#endregion

		#region Tab: Build Release
		private async void buildReleaseBtn_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(finalVersionTb.Text))
			{
				MessageBox.Show("Must have version #");
				return;
			}

			var success = await buildRelease();
			if (!success)
				throw new Exception("Build release timed out");

			var verDir = renameReleaseDir(finalVersionTb.Text.Trim('v'));

			zipRelease(verDir);
		}

		private async Task<bool> buildRelease()
		{
			var start = DateTime.Now;
			await ProcessRunner.RunHiddenAsync(
				@"C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe",
				$@"  ""{selectedProject.Solution}"" /build ""Release|AnyCPU""  ".Trim());

			// overkill. should take < 30 sec
			var _2_minute_Timeout = new TimeSpan(0, 2, 0);
			while (true)
			{
				if (DateTime.Now - start > _2_minute_Timeout)
					return false;

				if (selectedProject.ReleaseIsComplete())
				{
					var ts_debug = DateTime.Now - start;
					var ts_str_debug = $"{ts_debug}";

					// again: overkill
					await Task.Delay(100);
					return true;
				}

				await Task.Delay(100);
			}
		}

		private string renameReleaseDir(string ver)
		{
			var verDir = $"{selectedProject.VersionDirectory}{ver}";
			if (Directory.Exists(verDir))
				throw new Exception($"Directory already exists:\r\n{verDir}");
            Directory.Move(selectedProject.ReleaseDirectory, verDir);
			System.Threading.Thread.Sleep(100);
			return verDir;
		}

		private void zipRelease(string verDir)
		{
			var zip = verDir.Trim('\\') + ".zip";

			System.IO.Compression.ZipFile.CreateFromDirectory(verDir, zip);
			System.Threading.Thread.Sleep(100);
			finalZipTb.Text = zip;

            Directory.Delete(verDir, true);
			System.Threading.Thread.Sleep(100);
		}
		#endregion Tab: Build Release

		#region Tab: Latest commits
		private void LatestCommitsTab_VisibleChanged(object sender, EventArgs e)
		{
			if (!latestCommitsTab.Visible)
				return;

			// fetch new tags which were created on github
			git.RunGitCommand("fetch");

			var latestTag = git.GetLatestTag();

			tagLbl.Text = latestTag;
			var commits = git.GetCommitsSinceTag(latestTag);

			if (commits.Any())
				finalBodyTb.Text = commits.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
					.Select(s => $"* {s}")
					.Aggregate((a, b) => $"{a}\r\n{b}")
					+ selectedProject.Footer;
		}
		#endregion

		#region Tab: Publish to github
		private async void publishBtn_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(finalVersionTb.Text)) throw new Exception(nameof(finalVersionTb));
			if (string.IsNullOrWhiteSpace(finalTitleTb.Text)) throw new Exception(nameof(finalTitleTb));
			if (string.IsNullOrWhiteSpace(finalBodyTb.Text)) throw new Exception(nameof(finalBodyTb));
			if (string.IsNullOrWhiteSpace(finalZipTb.Text)) throw new Exception(nameof(finalZipTb));

			var client = gitHubClient;

			finalVersionTb.Text = finalVersionTb.Text.Trim();
			finalTitleTb.Text = finalTitleTb.Text.Trim();
			finalBodyTb.Text = finalBodyTb.Text.Trim();

			// https://octokitnet.readthedocs.io/en/latest/releases/
			// - create pre-release
			// - attach asset/s
			// - update release to live status

			var newRelease = new Octokit.NewRelease(finalVersionTb.Text)
			{
				Name = finalTitleTb.Text,
				Body = finalBodyTb.Text,
				Draft = true
			};
			var release = await client.Repository.Release.Create(repoId, newRelease);

			var zipPath = finalZipTb.Text;
			var zipFileName = Path.GetFileName(zipPath);

			using var archiveContents = File.OpenRead(zipPath);
            var assetUpload = new Octokit.ReleaseAssetUpload()
			{
                FileName = zipFileName,
                ContentType = "application/zip",
                RawData = archiveContents
			};
            var asset = await client.Repository.Release.UploadAsset(release, assetUpload);

            var updateRelease = release.ToUpdate();
			updateRelease.Draft = false;
			var updateResult = await client.Repository.Release.Edit(repoId, release.Id, updateRelease);

			await Task.Delay(100);
            File.Delete(zipPath);
		}
		#endregion

		#region Tab: Restart Dropbox
		private void restartDropboxBtn_Click(object sender, EventArgs e)
		{
			var _30_second_Timeout = new TimeSpan(0, 0, 30);

			var completedSuccessfully = Task
				.Run(() =>
				{
					dropboxWindowsService.Start();
					// this is the reason for the timeout. blocks and never returns even though the work is complete
					dropboxDesktop.Start("/home");
				})
				.Wait(_30_second_Timeout);
			// completedSuccessfully is always false. thread terminate though
		}
		#endregion
	}
}
