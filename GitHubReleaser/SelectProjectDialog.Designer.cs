namespace GitHubReleaser
{
    partial class SelectProjectDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.projectsCb = new System.Windows.Forms.ComboBox();
            this.okBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // projectsCb
            // 
            this.projectsCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.projectsCb.FormattingEnabled = true;
            this.projectsCb.Location = new System.Drawing.Point(12, 13);
            this.projectsCb.Name = "projectsCb";
            this.projectsCb.Size = new System.Drawing.Size(165, 23);
            this.projectsCb.TabIndex = 0;
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(199, 12);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // SelectProjectDialog
            // 
            this.AcceptButton = this.okBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 47);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.projectsCb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectProjectDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Project";
            this.Load += new System.EventHandler(this.SelectProject_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox projectsCb;
        private System.Windows.Forms.Button okBtn;
    }
}