using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHubReleaser
{
    public partial class Form1
    {
        private void buildLog(string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
                s = $"{DateTime.Now}: {s}";
            buildReleaseOutputTb.AppendText(s + Environment.NewLine);
        }

        private async void buildReleaseBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(finalVersionTb.Text))
            {
                MessageBox.Show("Must have version #");
                return;
            }

            try
            {
                buildReleaseBtn.Enabled = false;
                buildReleaseOutputTb.Clear();

                buildLog("Begin build");

                var success = await buildRelease();
                if (!success)
                    throw new Exception("Build release timed out");

                buildLog("build steps complete. Begin zip");

                var ver = finalVersionTb.Text.Trim('v');
                var verDirs = selectedProject.RenameReleaseDirectories(ver).ToList();

                await zipReleaseAsync(verDirs);

                buildLog("Build success");
            }
            catch (Exception ex)
            {
                buildLog("ERROR");
                buildLog(ex.Message);
                buildLog(ex.StackTrace);
            }
            finally
            {
                buildReleaseBtn.Enabled = true;
            }
        }

        private async Task<bool> buildRelease()
        {
            var start = DateTime.Now;

            await selectedProject.BuildAsync(buildLog);

            // overkill. should take < 30 sec
            var _2_minute_Timeout = new TimeSpan(0, 2, 0);
            while (true)
            {
                if (selectedProject.ReleaseIsComplete())
                    return true;

                if (DateTime.Now - start > _2_minute_Timeout)
                    return false;

                await Task.Delay(100);
            }
        }

        private async Task zipReleaseAsync(List<string> verDirs)
        {
            zipFiles.Clear();

            foreach (var verDir in verDirs)
            {
                var zip = verDir.Trim('\\') + ".zip";

                await Task.Run(() => System.IO.Compression.ZipFile.CreateFromDirectory(verDir, zip));

                zipFiles.Add(zip);

                Directory.Delete(verDir, true);
            }

            finalZipTb.Text = zipFiles.Aggregate((a, b) => $"{a}\r\n{b}");
        }
    }
}
