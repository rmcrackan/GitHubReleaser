using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dinah.Core.WindowsDesktop;
using Dinah.Core.WindowsDesktop.Processes;

namespace GitHubReleaser
{
    public partial class Form1 : Form
	{
		private Service dropboxWindowsService { get; } = new("DbxSvc");
		private Executable dropboxDesktop { get; } = new(@"C:\Program Files (x86)\Dropbox\Client\Dropbox.exe");

		Octokit.IRepositoriesClient gitHubRepository;
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
		List<string> zipFiles = new();

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

			gitHubRepository = new Octokit.GitHubClient(new Octokit.ProductHeaderValue($"{selectedProject.Name}_Releaser")) { Credentials = new(SECRETS.GithubApiToken) }.Repository;

            var repo = await gitHubRepository.Get("rmcrackan", selectedProject.Name);
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
	}
}
