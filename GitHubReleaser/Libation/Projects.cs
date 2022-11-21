using System;
using System.Collections.Generic;
using System.Linq;
using Dinah.Core;

namespace GitHubReleaser.Libation
{
	public abstract class Project : Enumeration<Project>
    {
        public static Project LibationCli { get; } = new LibationCli_Project();
        public static Project HangoverWinForms { get; } = new HangoverWinForms_Project();
        public static Project HangoverAvalonia { get; } = new HangoverAvalonia_Project();

        public static Project LibationExe_Classic => LibationExe.Classic;
        public static Project LibationExe_Chardonnay => LibationExe.Chardonnay;

        public static Project ConfigApp_Windows => ConfigApp.Windows;
        public static Project ConfigApp_Linux => ConfigApp.Linux;
        public static Project ConfigApp_MacOS => ConfigApp.MacOS;

        public string CsProj { get; }
		public string Name { get; }
        protected virtual string SubDir => "";
		protected virtual string ProjDir => CsProj;
		public string Dir => $@"{SubDir}\{ProjDir}".Trim('\\');

        private static int i = 0;
		protected Project(string csProj, string name = null) : base(i++, csProj)
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
        public static ConfigApp Windows { get; } = new WindowsConfigApp();
        public static ConfigApp Linux { get; } = new LinuxConfigApp();
        public static ConfigApp MacOS { get; } = new MacOSConfigApp();

        protected override string SubDir => @"LoadByOS";
		public ConfigApp(OperatingSystem os) : base(os.Name + "ConfigApp") { }

        private class WindowsConfigApp : ConfigApp { public WindowsConfigApp() : base(OperatingSystem.Windows) { } }
        private class LinuxConfigApp : ConfigApp { public LinuxConfigApp() : base(OperatingSystem.Linux) { } }
        private class MacOSConfigApp : ConfigApp { public MacOSConfigApp() : base(OperatingSystem.MacOS) { } }
	}
}
