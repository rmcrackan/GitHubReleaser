using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHubReleaser
{
    public partial class SelectProjectDialog : Form
    {
        public ProjectSettings SelectedProject { get; private set; }

        public SelectProjectDialog()
        {
            InitializeComponent();
        }

        private void SelectProject_Load(object sender, EventArgs e)
        {
            foreach (var projectSettings in ProjectSettings.GetAll())
                this.projectsCb.Items.Add(projectSettings);
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            SelectedProject = (ProjectSettings)this.projectsCb.SelectedItem;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
