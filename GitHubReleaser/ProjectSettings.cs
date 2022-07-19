using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dinah.Core;
using Dinah.Core.Processes;

namespace GitHubReleaser
{
    public abstract class ProjectSettings : Enumeration<ProjectSettings>
	{
		public static ProjectSettings Libation { get; } = new LibationSettings();
		public static ProjectSettings XstitchXcel { get; } = new XstitchXcelSettings();

		public string Name => DisplayName;
		public string GitDirectory { get; init; }
		public string SourceDirectory { get; init; }

		public string ProjectWithVersion { get; init; }
		public string Solution { get; init; }

		public string ReleaseDirectory { get; init; }
		public string VersionDirectory { get; init; }

		public string Footer { get; init; }
		public Func<bool> ReleaseIsComplete;

		private static int i = 0;
		public ProjectSettings(string displayName) : base(i++, displayName) { }

		public virtual Task BuildAsync() => BuildSolutionAsync();
		private async Task BuildSolutionAsync()
			=> await ProcessRunner.RunHiddenAsync(
				@"C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe",
				$@"  ""{Solution}"" /build ""Release|AnyCPU""  ".Trim());

		public override string ToString() => Name;

        private class LibationSettings : ProjectSettings
		{
			public LibationSettings() : base("Libation")
			{
				GitDirectory = Path.Combine(SECRETS.VS2022ProjectsDirectory, Name);
				SourceDirectory = Path.Combine(GitDirectory, "Source");

				ProjectWithVersion = Path.Combine(SourceDirectory, @"AppScaffolding\AppScaffolding.csproj");
				Solution = Path.Combine(SourceDirectory, "Libation.sln");
				//// per FolderProfile.pubxml in EACH PROJECT
				//// <Project>
				////   <PropertyGroup>
				////     <PublishDir>..\bin\publish\</PublishDir>
				//// publishes to: C:\Dropbox\DinahsFolder\coding\_NET\Visual Studio 2022\Libation\Source\bin\publish
				//ReleaseDirectory = Path.Combine(SourceDirectory, @"bin\publish");

				ReleaseDirectory = Path.Combine(SourceDirectory, @"bin\Release");
				VersionDirectory = Path.Combine(SourceDirectory, @"bin\Libation."); // final dot IS intentional

				Footer =
					"\r\n\r\n" +
					@$"
[Libation](https://github.com/rmcrackan/Libation) is a free, open source audible library manager for Windows. Decrypt, backup, organize, and search your audible library

I intend to keep Libation free and open source, but if you want to [leave a tip](https://paypal.me/mcrackan?locale.x=en_us), who am I to argue?
".Trim();
				ReleaseIsComplete = () =>
				{
					var exe = Path.Combine(ReleaseDirectory, "Libation.exe");
					var spanishDir = Path.Combine(ReleaseDirectory, "es");

					return
                        File.Exists(exe) &&
                        Directory.Exists(spanishDir) &&
                        Directory.EnumerateFiles(spanishDir).Count() > 0;
				};
			}

			//// publish
			//public override async Task BuildAsync()
			//{
			//	var projects = new[] { "LibationWinForms", "LibationCli", "Hangover" };
			//	foreach (var project in projects)
			//	{
			//		await ProcessRunner.RunHiddenAsync(new ProcessStartInfo
			//		{
			//			FileName = "dotnet",
			//			WorkingDirectory = SourceDirectory,
			//			Arguments = $@"publish -c Release {project}\{project}.csproj -p:PublishProfile={project}\Properties\PublishProfiles\FolderProfile.pubxml",
			//		});
			//	}
			//}
        }

		private class XstitchXcelSettings : ProjectSettings
		{
			public XstitchXcelSettings() : base("XstitchXcel")
			{
				GitDirectory = Path.Combine(SECRETS.VS2022ProjectsDirectory, Name);
				SourceDirectory = GitDirectory;

				ProjectWithVersion = Path.Combine(SourceDirectory, @"XstitchXcel\XstitchXcel.csproj");
				Solution = Path.Combine(SourceDirectory, "XstitchXcel.sln");
				ReleaseDirectory = Path.Combine(SourceDirectory, @"XstitchXcel\bin\Release");
				VersionDirectory = Path.Combine(SourceDirectory, @"XstitchXcel\bin\XstitchXcel."); // final dot IS intentional

				Footer =
					"\r\n\r\n" +
					@$"
After unzipping, double-click on `XstitchXcel.exe`

[XstitchXcel](https://github.com/rmcrackan/XstitchXcel) is free, open source cross stitch tools for patterns drafted in Excel

As long as I'm maintaining this software, it will remain free and open source., but if you want to [leave a tip](https://paypal.me/mcrackan?locale.x=en_us), who am I to argue?
".Trim();
				ReleaseIsComplete = () =>
				{
					var exe = Path.Combine(ReleaseDirectory, "XstitchXcel.exe");
					var spanishDir = Path.Combine(ReleaseDirectory, "es");

					return
						File.Exists(exe) &&
                        Directory.Exists(spanishDir) &&
                        Directory.EnumerateFiles(spanishDir).Count() > 16;
				};
			}
        }
	}
}
