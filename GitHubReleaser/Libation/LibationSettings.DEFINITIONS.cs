using System.Collections.Generic;

namespace GitHubReleaser.Libation
{
    internal partial class LibationSettings
    {
        // order matters. upload classic FIRST
        List<Release> releases { get; } =
        [
			// order matters:
			// * main exe must be FIRST
			// * build hangover LAST
			new()
            {
                OS = OperatingSystem.Windows,
                BuildPrefix = "Classic-",
                Projects = [LibationExe.Classic, ConfigApp.Windows, Project.LibationCli, Project.HangoverWinForms],
            },
            new()
            {
                OS = OperatingSystem.Windows,
                Projects = [LibationExe.Chardonnay, ConfigApp.Windows, Project.LibationCli, Project.HangoverAvalonia]
            },
            new()
            {
                OS = OperatingSystem.Linux,
                Projects = [LibationExe.Chardonnay, ConfigApp.Linux, Project.LibationCli, Project.HangoverAvalonia]
            },
            new()
            {
                OS = OperatingSystem.MacOS,
                Projects = [LibationExe.Chardonnay, ConfigApp.MacOS, Project.LibationCli, Project.HangoverAvalonia]
            }
        ];
    }
}
