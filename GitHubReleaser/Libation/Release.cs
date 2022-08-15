using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GitHubReleaser.Libation
{
    internal class Release
    {
        public OperatingSystem OS { get; init; }
        public string BuildPrefix { get; init; } = "";

        /// <summary>order matters. First project is main/LibationExe</summary>
        public List<Project> Projects { get; init; } = new();
        /// <summary>LibationExe</summary>
        public Project MainExe => Projects.FirstOrDefault();
    }

    internal static class ReleaseExtensions
    {
        public const string BIN_PUBLISH = @"bin\Publish";

        public static string GetPublishDirRelative(this Release release) => Path.Combine(BIN_PUBLISH, $@"{release.OS.Abbrev}-{release.MainExe.Name}");

        public static string GetZipDir(this Release release) => $@"{release.BuildPrefix}Libation.#.#.#-{release.OS.Abbrev}-{release.MainExe.Name}";
    }
}
