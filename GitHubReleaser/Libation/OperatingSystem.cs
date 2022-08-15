using System;
using System.Collections.Generic;
using System.Linq;
using Dinah.Core;

namespace GitHubReleaser.Libation
{
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
}
