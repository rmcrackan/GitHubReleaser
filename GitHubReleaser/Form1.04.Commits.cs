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

            // fetch new tags which were created on github
            git.RunGitCommand("fetch");

            var latestTag = git.GetLatestTag();

            tagLbl.Text = latestTag;

            finalBodyTb.Clear();

            addCommitMessages(latestTag);

            var contributors = await findContributors(latestTag);
            addContributorsThankYou(contributors);

            finalBodyTb.AppendText(selectedProject.Footer);
        }

        private void addCommitMessages(string latestTag)
        {
            var commitMessages = git.GetCommitsSinceTag(latestTag);

            if (commitMessages.Any())
                finalBodyTb.AppendText(commitMessages.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => $"* {s}")
                    .Aggregate((a, b) => $"{a}\r\n{b}"));
        }

        private async Task<List<string>> findContributors(string latestTag)
        {
            var contributors = new List<string>();
            //   git log <yourlasttag>..HEAD
            // %H == commit hash. https://git-scm.com/book/en/v2/Git-Basics-Viewing-the-Commit-History
            var commitHashes = git.RunGitCommand($"git log --pretty=%H {latestTag}..HEAD").Output;
            foreach (var hash in commitHashes.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var commit = await gitHubRepository.Commit.Get(repoId, hash);
                var name = commit?.Author?.Login ?? commit?.Commit?.Committer?.Name;
                if (name is not null)
                {
                    if (!name.Trim().EqualsInsensitive("Robert McRackan") &&
                        !name.Trim().EqualsInsensitive("rmcrackan") &&
                        !contributors.Contains(name))
                        contributors.Add(name);
                }
            }

            return contributors;
        }

        private void addContributorsThankYou(List<string> contributors)
        {
            if (contributors.Any())
            {
                finalBodyTb.AppendText("\r\n\r\nThanks to " +
                    contributors
                    .Select(s => $"@{s}")
                    .Aggregate((a, b) => $"{a}, {b}"));
            }
        }
    }
}
