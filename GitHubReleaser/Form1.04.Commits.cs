using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dinah.Core;

namespace GitHubReleaser
{
    public partial class Form1
    {
        private async void LatestCommitsTab_VisibleChanged(object sender, EventArgs e)
        {
            if (!latestCommitsTab.Visible)
                return;

            // ensure local repo has commits that may only exist on the remote
            git.RunGitCommand("fetch");

            // use GitHub's latest release tag (same source as the Version tab), not git describe.
            // describe can pick up a newer local/remote tag that is not a published release.
            var latestTag = await getLastPublishedTagAsync();

            tagLbl.Text = latestTag;

            finalBodyTb.Clear();

            finalBodyTb.AppendText(selectedProject.Header);

            addCommitMessages(latestTag);

            var contributors = await findContributors(latestTag);
            addContributorsThankYou(contributors);

            finalBodyTb.AppendText(selectedProject.Footer);
        }

        private void addCommitMessages(string latestTag)
        {
            var lines = git.GetCommitsSinceTag(latestTag)
                .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length > 0)
                finalBodyTb.AppendText(string.Join("\r\n", lines.Select(s => $"* {s}")));
        }

        private async Task<List<string>> findContributors(string latestTag)
        {
            var contributors = new List<string>();
            //   git log <yourlasttag>..HEAD
            // %H == commit hash. https://git-scm.com/book/en/v2/Git-Basics-Viewing-the-Commit-History
            var commitHashes = git.RunGitCommand($"git log --pretty=%H {latestTag}..HEAD").Output;
            foreach (var hash in commitHashes.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries))
            {
                var commit = await gitHubRepository.Commit.Get(repoId, hash);
                var name = commit?.Author?.Login ?? commit?.Commit?.Committer?.Name;
                if (name is not null)
                {
                    if (!name.Trim().EqualsInsensitive("Robert McRackan") &&
                        !name.Trim().EqualsInsensitive("rmcrackan") &&
                        !name.Trim().EqualsInsensitive("dependabot[bot]") &&
                        !contributors.Contains(name))
                        contributors.Add(name);
                }
            }

            return contributors;
        }

        private void addContributorsThankYou(List<string> contributors)
        {
            if (contributors.Count > 0)
            {
                finalBodyTb.AppendText("\r\n\r\nThanks to " +
                    string.Join(", ", contributors.Select(s => $"@{s}")));
            }
        }
    }
}
