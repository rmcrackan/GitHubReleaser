using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace GitHubReleaser
{
    public partial class Form1
    {
        private async void VersionTab_VisibleChanged(object sender, EventArgs e)
        {
            if (!versionTab.Visible)
                return;

            // if (tabControl1.SelectedTab == versionTab) ...

            var published = await getLastPublishedVersionAsync();
            var current = getCurrentVersion();

            lastPublishedVerValueLbl.Text = $"{published}";
            currVersionValueLbl.Text = $"{current}";

            // warn if not greater. compare without revision
            var published3 = new Version(published.Major, published.Minor, published.Build);
            var current3 = new Version(current.Major, current.Minor, current.Build);

            versionCanRelease = published3 < current3;

            if (versionCanRelease)
            {
                // eg: 1.0.0 , 1.2.0 , 1.2.3
                finalVersionTb.Text = $"v{current3}";

                var v3 = current3.ToString();
                while (v3.EndsWith(".0"))
                {
                    // remove final 2 char
                    v3 = v3[..^2];
                }

                // eg: 1 , 1.2 , 1.2.3
                finalTitleTb.Text = $"{selectedProject.Name} {v3}";
            }
        }

        private async Task<Version> getLastPublishedVersionAsync()
        {
            // https://octokitnet.readthedocs.io/en/latest/releases/
            var releases = await gitHubRepository.Release.GetAll("rmcrackan", selectedProject.Name);
            var latest = releases.First(r => !r.Draft && !r.Prerelease);
            var latestVersionString = latest.TagName.Trim('v');
            return Version.Parse(latestVersionString);
        }

        private Version getCurrentVersion()
        {
            var xPath = "/Project/PropertyGroup/Version/text()";
            var value = new XPathDocument(selectedProject.ProjectWithVersion)
                .CreateNavigator()
                .SelectSingleNode(xPath)
                .Value;
            return Version.Parse(value);
        }

        private void verRefreshBtn_Click(object sender, EventArgs e)
        {
            var current = getCurrentVersion();
            currVersionValueLbl.Text = $"{current}";
        }
    }
}
