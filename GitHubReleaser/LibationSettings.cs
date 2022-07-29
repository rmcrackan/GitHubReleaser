﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dinah.Core;
using Dinah.Core.Processes;

namespace GitHubReleaser
{
	// from: AppScaffolding.LibationScaffolding.ReleaseIdentifier
	enum ReleaseIdentifier
	{
		WindowsClassic,
		WindowsAvalonia,
		LinuxAvalonia,
		MacOSAvalonia
	}

	public abstract class LibationExe : Enumeration<LibationExe>
	{
		public static LibationExe Classic { get; } = new ClassicExe();
		public static LibationExe Chardonnay { get; } = new ChardonnayExe();

		public string Name { get; }
		public string CsProj { get; }

		private static int i = 0;
		public LibationExe(string name, string csProj) : base(i++, name)
		{
			Name = name;
			CsProj = csProj;
		}

		private class ClassicExe : LibationExe { public ClassicExe() : base("classic", "LibationWinForms") { } }
		private class ChardonnayExe : LibationExe { public ChardonnayExe() : base("chardonnay", "LibationAvalonia") { } }
	}

	enum DotnetAction { build, publish }

	enum Project { LibationCli, Hangover }

	public abstract class OperatingSystems : Enumeration<OperatingSystems>
	{
		public static OperatingSystems Windows { get; } = new WindowsOperatingSystem();
		public static OperatingSystems Linux { get; } = new LinuxOperatingSystem();
		public static OperatingSystems MacOS { get; } = new MacOperatingSystem();

		public string Name { get; }
		public string Abbrev { get; }

		private static int i = 0;
		public OperatingSystems(string name, string abbrev) : base(i++, name)
		{
			Name = name;
			Abbrev = abbrev;
		}

		private class WindowsOperatingSystem : OperatingSystems { public WindowsOperatingSystem() : base("Windows", "win") { } }
		private class LinuxOperatingSystem : OperatingSystems { public LinuxOperatingSystem() : base("Linux", "linux") { } }
		private class MacOperatingSystem : OperatingSystems { public MacOperatingSystem() : base("MacOS", "macos") { } }
	}

	class Release
	{
		public ReleaseIdentifier Identifier { get; init; }
		public OperatingSystems OS { get; init; }
		public LibationExe LibationExe { get; init; }
		public DotnetAction Action { get; init; } = DotnetAction.publish;
		public string BuildPrefix { get; init; } = "";

		// order matters
		public List<Project> OtherProjects { get; init; } = new List<Project>();

		public List<string> Projects
		{
			get
			{
				var list = new List<string>();

				if (LibationExe is not null)
					list.Add(LibationExe.CsProj.ToString());

				list.AddRange(OtherProjects.Select(p => $"{p}"));
				return list;
			}
		}
	}
	static class ReleaseExtensions
    {
		public static string BinPublish = @"bin\Publish";

		public static string GetPublishDirRelative(this Release release) => Path.Combine(BinPublish, $@"{release.OS.Abbrev}-{release.LibationExe.Name}");

		public static string GetZipDir(this Release release) => $@"{release.BuildPrefix}Libation.#.#.#-{release.OS.Abbrev}-{release.LibationExe.Name}";
	}

    internal class LibationSettings : ProjectSettings
	{
		// order matters. upload classic FIRST
		List<Release> releases { get; } = new()
		{
			new()
			{
				Identifier = ReleaseIdentifier.WindowsClassic,

				OS = OperatingSystems.Windows,
				LibationExe = LibationExe.Classic,
				Action = DotnetAction.build,
				BuildPrefix = "Classic-",
				// order matters. build hangover LAST
				OtherProjects = new(){ Project.LibationCli, Project.Hangover },
			},
			new()
			{
				Identifier = ReleaseIdentifier.WindowsAvalonia,

				OS = OperatingSystems.Windows,
				LibationExe = LibationExe.Chardonnay,
				Action = DotnetAction.build,
				// order matters. build hangover LAST
				OtherProjects = new(){ Project.LibationCli, Project.Hangover }
			},
			new()
			{
				Identifier = ReleaseIdentifier.LinuxAvalonia,

				OS = OperatingSystems.Linux,
				LibationExe = LibationExe.Chardonnay,
				OtherProjects = new(){ Project.LibationCli }
			},
			new()
			{
				Identifier = ReleaseIdentifier.MacOSAvalonia,

				OS = OperatingSystems.MacOS,
				LibationExe = LibationExe.Chardonnay,
				OtherProjects = new(){ Project.LibationCli }
			}
		};

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

		public override async Task BuildAsync()
		{
			foreach (var r in releases)
			{
				foreach (var project in r.Projects)
				{
					var csproj = $@"{project}\{project}.csproj";
					var pubxml = $@"{project}\Properties\PublishProfiles\{r.OS.Name}Profile.pubxml";

					var args = $@"{r.Action} -c Release -o {r.GetPublishDirRelative()} {csproj} -p:PublishProfile={pubxml}";

                    await ProcessRunner.RunHiddenAsync(new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        WorkingDirectory = SourceDirectory,
                        Arguments = args,
                    });
                }
			}
		}

        public override IEnumerable<string> RenameReleaseDirectories(string ver)
		{
			foreach (var r in releases)
			{
				var source = Path.Combine(SourceDirectory, r.GetPublishDirRelative());

				var destDir = r.GetZipDir().Replace("#.#.#", ver);
				var dest = Path.Combine(SourceDirectory, ReleaseExtensions.BinPublish, destDir);

				RenameDir(source, dest);

				yield return dest;
			}
		}
    }
}