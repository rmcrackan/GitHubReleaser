using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dinah.Core;
using Dinah.Core.WindowsDesktop.Processes;

namespace GitHubReleaser
{
	public abstract class Project : Enumeration<Project>
    {
        public static Project LibationCli { get; } = new LibationCli_Project();
        public static Project HangoverWinForms { get; } = new HangoverWinForms_Project();
        public static Project HangoverAvalonia { get; } = new HangoverAvalonia_Project();

        public static Project LibationExe_Classic => LibationExe.Classic;
        public static Project LibationExe_Chardonnay => LibationExe.Chardonnay;

        public string CsProj { get; }
		public string Name { get; }
        protected virtual string SubDir => "";
		protected virtual string ProjDir => CsProj;
		public string Dir => $@"{SubDir}\{ProjDir}".Trim('\\');

        private static int i = 0;
		public Project(string csProj) : this(csProj, csProj) { }
        protected Project(string csProj, string name) : base(i++, csProj)
        {
            CsProj = ArgumentValidator.EnsureNotNullOrEmpty(csProj, nameof(csProj));
			Name = string.IsNullOrWhiteSpace(name) ? csProj : name;
        }

        private class LibationCli_Project : Project { public LibationCli_Project() : base(
            csProj: "LibationCli") { } }
        private class HangoverWinForms_Project : Project { public HangoverWinForms_Project() : base(
            csProj: "HangoverWinForms") { } }
        private class HangoverAvalonia_Project : Project { public HangoverAvalonia_Project() : base(
            csProj: "HangoverAvalonia") { } }

		public override string ToString()
			=> $"{GetType()}"
			+ $"|{nameof(CsProj)}:{CsProj}"
			+ $"|{nameof(Name)}:{Name}"
			+ $"|{nameof(Dir)}:{Dir}";
	}

	public abstract class LibationExe : Project
    {
        public static LibationExe Classic { get; } = new ClassicExe();
        public static LibationExe Chardonnay { get; } = new ChardonnayExe();

		public LibationExe(string csProj, string name) : base(csProj: csProj, name: name) { }

		private class ClassicExe : LibationExe { public ClassicExe() : base(
			csProj: "LibationWinForms",
			name: "classic") { } }
        private class ChardonnayExe : LibationExe { public ChardonnayExe() : base(
            csProj: "LibationAvalonia",
            name: "chardonnay") { }
        }
    }

	public abstract class ConfigApp : Project
    {
        public static ConfigApp WindowsConfigApp { get; } = new Windows_ConfigApp();
        public static ConfigApp LinuxConfigApp { get; } = new Linux_ConfigApp();
        public static ConfigApp MacOSConfigApp { get; } = new MacOS_ConfigApp();

        protected override string SubDir => @"LoadByOS";
		public ConfigApp(OperatingSystem os) : base(os.Name + "ConfigApp") { }

        private class Windows_ConfigApp : ConfigApp { public Windows_ConfigApp() : base(OperatingSystem.Windows) { } }
        private class Linux_ConfigApp : ConfigApp { public Linux_ConfigApp() : base(OperatingSystem.Linux) { } }
        private class MacOS_ConfigApp : ConfigApp { public MacOS_ConfigApp() : base(OperatingSystem.MacOS) { } }
	}

	public abstract class OperatingSystem : Enumeration<OperatingSystem>
	{
		public static OperatingSystem Windows { get; } = new WindowsOperatingSystem();
		public static OperatingSystem Linux { get; } = new LinuxOperatingSystem();
		public static OperatingSystem MacOS { get; } = new MacOSOperatingSystem();

		public string Name { get; }
		public string Abbrev { get; }

		private static int i = 0;
		public OperatingSystem(string name, string abbrev) : base(i++, name)
		{
			Name = name;
			Abbrev = abbrev;
		}

		private class WindowsOperatingSystem : OperatingSystem { public WindowsOperatingSystem() : base(
			name: "Windows",
			abbrev: "win") { } }
		private class LinuxOperatingSystem : OperatingSystem { public LinuxOperatingSystem() : base(
			name: "Linux",
			abbrev: "linux") { } }
		private class MacOSOperatingSystem : OperatingSystem { public MacOSOperatingSystem() : base(
			name: "MacOS",
			abbrev: "macos") { } }

        public override string ToString() => $"{nameof(OperatingSystem)}:{Name}";
    }

	class Release
	{
		public OperatingSystem OS { get; init; }
		public string BuildPrefix { get; init; } = "";

        /// <summary>order matters. First project is main/LibationExe</summary>
        public List<Project> Projects { get; init; } = new();
        /// <summary>LibationExe</summary>
        public Project MainExe => Projects.FirstOrDefault();
	}
	static class ReleaseExtensions
    {
		public const string BIN_PUBLISH = @"bin\Publish";

		public static string GetPublishDirRelative(this Release release) => Path.Combine(BIN_PUBLISH, $@"{release.OS.Abbrev}-{release.MainExe.Name}");

		public static string GetZipDir(this Release release) => $@"{release.BuildPrefix}Libation.#.#.#-{release.OS.Abbrev}-{release.MainExe.Name}";
	}

    internal class LibationSettings : ProjectSettings
	{
		// order matters. upload classic FIRST
		List<Release> releases { get; } = new()
		{
			// order matters:
			// * main exe must be FIRST
			// * build hangover LAST
			new()
			{
				OS = OperatingSystem.Windows,
				BuildPrefix = "Classic-",
				Projects = new(){ LibationExe.Classic, ConfigApp.WindowsConfigApp, Project.LibationCli, Project.HangoverWinForms },
			},
			new()
			{
				OS = OperatingSystem.Windows,
				Projects = new(){ LibationExe.Chardonnay, ConfigApp.WindowsConfigApp, Project.LibationCli, Project.HangoverAvalonia }
			},
			new()
			{
				OS = OperatingSystem.Linux,
                Projects = new(){ LibationExe.Chardonnay, ConfigApp.LinuxConfigApp, Project.LibationCli, Project.HangoverAvalonia }
			},
			new()
			{
				OS = OperatingSystem.MacOS,
                Projects = new(){ LibationExe.Chardonnay, ConfigApp.MacOSConfigApp, Project.LibationCli, Project.HangoverAvalonia }
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

		Action<string> logMe;
		public override async Task BuildAsync(Action<string> outputCallback)
		{
			logMe = outputCallback;

			foreach (var r in releases)
			{
				logMe($@"{r.OS}|{r.MainExe}");

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
