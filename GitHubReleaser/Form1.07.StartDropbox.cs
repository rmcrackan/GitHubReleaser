using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubReleaser
{
    public partial class Form1
    {
        private void restartDropboxBtn_Click(object sender, EventArgs e)
        {
            var _30_second_Timeout = new TimeSpan(0, 0, 30);

            var completedSuccessfully = Task
                .Run(() =>
                {
                    dropboxWindowsService.Start();
                    // this is the reason for the timeout. blocks and never returns even though the work is complete
                    dropboxDesktop.Start("/home");
                })
                .Wait(_30_second_Timeout);
            // completedSuccessfully is always false. thread terminate though
        }
    }
}
