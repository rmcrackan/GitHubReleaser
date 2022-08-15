using System;
using System.Collections.Generic;
using System.Linq;

namespace GitHubReleaser
{
    public partial class Form1
    {
        private void stopDropboxBtn_Click(object sender, EventArgs e)
        {
            // order is not important
            dropboxWindowsService.Stop();
            dropboxDesktop.Kill();
        }
    }
}
