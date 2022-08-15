using System;
using System.Collections.Generic;
using System.Linq;

namespace GitHubReleaser.Libation
{
    internal partial class LibationSettings
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
                Projects = new(){ LibationExe.Classic, ConfigApp.Windows, Project.LibationCli, Project.HangoverWinForms },
            },
            new()
            {
                OS = OperatingSystem.Windows,
                Projects = new(){ LibationExe.Chardonnay, ConfigApp.Windows, Project.LibationCli, Project.HangoverAvalonia }
            },
            new()
            {
                OS = OperatingSystem.Linux,
                Projects = new(){ LibationExe.Chardonnay, ConfigApp.Linux, Project.LibationCli, Project.HangoverAvalonia }
            },
            new()
            {
                OS = OperatingSystem.MacOS,
                Projects = new(){ LibationExe.Chardonnay, ConfigApp.MacOS, Project.LibationCli, Project.HangoverAvalonia }
            }
        };
    }
}
