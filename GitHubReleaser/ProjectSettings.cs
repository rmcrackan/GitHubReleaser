using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dinah.Core;
using Dinah.Core.WindowsDesktop.Processes;
using GitHubReleaser.Libation;
using GitHubReleaser.XstitchXcel;

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
		public Func<bool> ReleaseIsComplete = () => true;

		private static int i = 0;
		public ProjectSettings(string displayName) : base(i++, displayName) { }

		// default: solution-level build
		public virtual async Task BuildAsync(Action<string> outputCallback)
			=> await Runner.RunHiddenAsync(
				@"C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe",
				$@"  ""{Solution}"" /build ""Release|AnyCPU""  ".Trim());

		public virtual IEnumerable<string> RenameReleaseDirectories(string ver)
		{
			var verDir = $"{VersionDirectory}{ver}";

			RenameDir(ReleaseDirectory, verDir);
			yield return verDir;
		}

		protected static void RenameDir(string sourceDir, string destDir)
		{
			if (Directory.Exists(destDir))
				throw new Exception($"Directory already exists:\r\n{destDir}");
			Directory.Move(sourceDir, destDir);
		}

		public override string ToString() => Name;
	}
}
