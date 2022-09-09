using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dinah.Core.WindowsDesktop.Processes;

namespace GitHubReleaser.Libation
{
    internal partial class LibationSettings : ProjectSettings
	{
        public LibationSettings() : base("Libation")
		{
			GitDirectory = Path.Combine(SECRETS.VS2022ProjectsDirectory, Name);
			SourceDirectory = Path.Combine(GitDirectory, "Source");
			ProjectWithVersion = Path.Combine(SourceDirectory, @"AppScaffolding\AppScaffolding.csproj");

			Footer =
				"\r\n\r\n" +
				@$"
[Libation](https://github.com/rmcrackan/Libation) is a free, open source audible library manager for Windows. Decrypt, backup, organize, and search your audible library

I intend to keep Libation free and open source, but if you want to [leave a tip](https://paypal.me/mcrackan?locale.x=en_us), who am I to argue?
".Trim();
		}

		Action<string> logMe;
		public override async Task BuildAsync(Action<string> outputCallback)
		{
			logMe = outputCallback;

            logMe("");
            foreach (var r in releases)
			{
				logMe($@"{r.OS}|{r.MainExe}");

                // dotnet clean: if no proj or sln specified then it will run all proj.s and sln.s found in WorkingDirectory.
				// in our case: Libation.sln
                await runDotNetAsync("clean -c Release");

				foreach (var project in r.Projects)
				{
					logMe($"* project:{project}");

					var csproj = $@"{project.Dir}\{project.CsProj}.csproj";
					var pubxml = $@"{project.Dir}\Properties\PublishProfiles\{r.OS.Name}Profile.pubxml";

					await runDotNetAsync($"publish -c Release -o {r.GetPublishDirRelative()} {csproj} -p:PublishProfile={pubxml}");
				}

				logMe("");
			}
		}

		private async Task runDotNetAsync(string args)
		{
			logMe($@"  {args}");
			await Runner.RunHiddenAsync(new ProcessStartInfo
			{
				FileName = "dotnet",
				WorkingDirectory = SourceDirectory,
				Arguments = args,
			});
		}

        public override IEnumerable<string> RenameReleaseDirectories(string ver)
		{
			foreach (var r in releases)
			{
				var source = Path.Combine(SourceDirectory, r.GetPublishDirRelative());

				var destDir = r.GetZipDir().Replace("#.#.#", ver);
				var dest = Path.Combine(SourceDirectory, ReleaseExtensions.BIN_PUBLISH, destDir);

				RenameDir(source, dest);

				yield return dest;
			}
		}
    }
}
