using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dinah.Core;

namespace GitHubReleaser
{
    public partial class Form1
    {
        private void publishPreReleaseCb_CheckedChanged(object sender, EventArgs e) => this.finalTitleTb.Text += " - Pre-release";

        private async void publishBtn_Click(object sender, EventArgs e)
        {
            ArgumentValidator.EnsureNotNullOrWhiteSpace(finalVersionTb.Text, nameof(finalVersionTb));
            ArgumentValidator.EnsureNotNullOrWhiteSpace(finalTitleTb.Text, nameof(finalTitleTb));
            ArgumentValidator.EnsureNotNullOrWhiteSpace(finalBodyTb.Text, nameof(finalBodyTb));
            if (!zipFiles.Any()) throw new Exception(nameof(zipFiles));

            finalVersionTb.Text = finalVersionTb.Text.Trim();
            finalTitleTb.Text = finalTitleTb.Text.Trim();
            finalBodyTb.Text = finalBodyTb.Text.Trim();

            // https://octokitnet.readthedocs.io/en/latest/releases/
            // - create pre-release
            // - attach asset/s
            // - update release to live status

            var newRelease = new Octokit.NewRelease(finalVersionTb.Text)
            {
                Name = finalTitleTb.Text,
                Body = finalBodyTb.Text,
                Draft = true,
                Prerelease = publishPreReleaseCb.Checked
            };
            var release = await gitHubRepository.Release.Create(repoId, newRelease);

            // upload IN ORDER. don't do something clever which doesn't guarantee order
            foreach (var zipPath in zipFiles)
                await uploadZipAsync(release, zipPath);

            finalZipTb.Text = "";
            warningsLbl.Text = "Publish success";
        }

        private async Task uploadZipAsync(Octokit.Release release, string zipPath)
        {
            var zipFileName = Path.GetFileName(zipPath);

            using var archiveContents = File.OpenRead(zipPath);
            var assetUpload = new Octokit.ReleaseAssetUpload()
            {
                FileName = zipFileName,
                ContentType = "application/zip",
                RawData = archiveContents
            };
            var asset = await gitHubRepository.Release.UploadAsset(release, assetUpload);

            var updateRelease = release.ToUpdate();
            updateRelease.Draft = false;
            var updateResult = await gitHubRepository.Release.Edit(repoId, release.Id, updateRelease);

            File.Delete(zipPath);
        }
    }
}
