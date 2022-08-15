using System;
using System.Collections.Generic;
using System.Linq;

namespace GitHubReleaser
{
    public partial class Form1
    {
        private void GitTab_VisibleChanged(object sender, EventArgs e)
        {
            if (!gitTab.Visible)
                return;

            var results = git.GetSyncStatusesRecrusive();

            this.gitStatuses = results;

            gitStatusLbl.Text
                = results.Any()
                ? $"Git status: out of sync.\r\n{results.Aggregate((a, b) => $"{a}\r\n{b}")}"
                : $"Git status: clean";
        }
    }
}
