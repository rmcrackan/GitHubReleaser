using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GitHubReleaser.XstitchXcel
{
	internal class XstitchXcelSettings : ProjectSettings
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
