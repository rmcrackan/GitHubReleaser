using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dinah.Core;

namespace GitHubReleaser
{
    public class ProjectSettings : Enumeration<ProjectSettings>
	{
		public static ProjectSettings Libation { get; } = new LibationSettings();
		public static ProjectSettings XstitchXcel { get; } = new XstitchXcelSettings();

		public string Name => DisplayName;
		public string GitDirectory { get; init; }

		public string ProjectWithVersion { get; init; }
		public string Solution { get; init; }

		public string ReleaseDirectory { get; init; }
		public string VersionDirectory { get; init; }

		public string Footer { get; init; }
		public Func<bool> ReleaseIsComplete;

		private static int i = 0;
		public ProjectSettings(string displayName) : base(i++, displayName) { }

		public override string ToString() => Name;

        private class LibationSettings : ProjectSettings
		{
			public LibationSettings() : base("Libation")
			{
				GitDirectory = Path.Combine(SECRETS.VS2022ProjectsDirectory, Name);

				ProjectWithVersion = Path.Combine(GitDirectory, @"AppScaffolding\AppScaffolding.csproj");
				Solution = Path.Combine(GitDirectory, "Libation.sln");
				ReleaseDirectory = Path.Combine(GitDirectory, @"LibationWinForms\bin\Release");
				VersionDirectory = Path.Combine(GitDirectory, @"LibationWinForms\bin\Libation."); // final dot IS intentional

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
                        Directory.EnumerateFiles(spanishDir).Count() > 16;
				};
			}
		}

		private class XstitchXcelSettings : ProjectSettings
		{
			public XstitchXcelSettings() : base("XstitchXcel")
			{
				GitDirectory = Path.Combine(SECRETS.VS2022ProjectsDirectory, Name);

				ProjectWithVersion = Path.Combine(GitDirectory, @"XstitchXcel\XstitchXcel.csproj");
				Solution = Path.Combine(GitDirectory, "XstitchXcel.sln");
				ReleaseDirectory = Path.Combine(GitDirectory, @"XstitchXcel\bin\Release");
				VersionDirectory = Path.Combine(GitDirectory, @"XstitchXcel\bin\XstitchXcel."); // final dot IS intentional

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
