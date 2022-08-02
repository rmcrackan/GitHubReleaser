
namespace GitHubReleaser
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.stopDropboxTab = new System.Windows.Forms.TabPage();
            this.stopDropboxBtn = new System.Windows.Forms.Button();
            this.gitTab = new System.Windows.Forms.TabPage();
            this.gitStatusLbl = new System.Windows.Forms.Label();
            this.versionTab = new System.Windows.Forms.TabPage();
            this.verRefreshBtn = new System.Windows.Forms.Button();
            this.currVersionValueLbl = new System.Windows.Forms.Label();
            this.currVersionDescLbl = new System.Windows.Forms.Label();
            this.lastPublishedVerValueLbl = new System.Windows.Forms.Label();
            this.lastPublishedVerDescLbl = new System.Windows.Forms.Label();
            this.latestCommitsTab = new System.Windows.Forms.TabPage();
            this.tagLbl = new System.Windows.Forms.Label();
            this.latestGitTagLbl = new System.Windows.Forms.Label();
            this.buildReleaseTab = new System.Windows.Forms.TabPage();
            this.buildReleaseOutputTb = new System.Windows.Forms.TextBox();
            this.buildReleaseBtn = new System.Windows.Forms.Button();
            this.publishToGithubTab = new System.Windows.Forms.TabPage();
            this.publishPreReleaseCb = new System.Windows.Forms.CheckBox();
            this.publishBtn = new System.Windows.Forms.Button();
            this.restartDropboxTab = new System.Windows.Forms.TabPage();
            this.restartDropboxBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.prevBtn = new System.Windows.Forms.Button();
            this.finalGb = new System.Windows.Forms.GroupBox();
            this.finalZipTb = new System.Windows.Forms.TextBox();
            this.finalZipLbl = new System.Windows.Forms.Label();
            this.finalBodyTb = new System.Windows.Forms.TextBox();
            this.finalTitleTb = new System.Windows.Forms.TextBox();
            this.finalTitleLbl = new System.Windows.Forms.Label();
            this.finalVersionTb = new System.Windows.Forms.TextBox();
            this.finalVersionLbl = new System.Windows.Forms.Label();
            this.warningsLbl = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.stopDropboxTab.SuspendLayout();
            this.gitTab.SuspendLayout();
            this.versionTab.SuspendLayout();
            this.latestCommitsTab.SuspendLayout();
            this.buildReleaseTab.SuspendLayout();
            this.publishToGithubTab.SuspendLayout();
            this.restartDropboxTab.SuspendLayout();
            this.finalGb.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.stopDropboxTab);
            this.tabControl1.Controls.Add(this.gitTab);
            this.tabControl1.Controls.Add(this.versionTab);
            this.tabControl1.Controls.Add(this.latestCommitsTab);
            this.tabControl1.Controls.Add(this.buildReleaseTab);
            this.tabControl1.Controls.Add(this.publishToGithubTab);
            this.tabControl1.Controls.Add(this.restartDropboxTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(435, 397);
            this.tabControl1.TabIndex = 0;
            // 
            // stopDropboxTab
            // 
            this.stopDropboxTab.Controls.Add(this.stopDropboxBtn);
            this.stopDropboxTab.Location = new System.Drawing.Point(4, 24);
            this.stopDropboxTab.Name = "stopDropboxTab";
            this.stopDropboxTab.Padding = new System.Windows.Forms.Padding(3);
            this.stopDropboxTab.Size = new System.Drawing.Size(427, 369);
            this.stopDropboxTab.TabIndex = 0;
            this.stopDropboxTab.Text = "Stop Dropbox";
            this.stopDropboxTab.UseVisualStyleBackColor = true;
            // 
            // stopDropboxBtn
            // 
            this.stopDropboxBtn.Location = new System.Drawing.Point(6, 6);
            this.stopDropboxBtn.Name = "stopDropboxBtn";
            this.stopDropboxBtn.Size = new System.Drawing.Size(140, 23);
            this.stopDropboxBtn.TabIndex = 1;
            this.stopDropboxBtn.Text = "Stop Dropbox";
            this.stopDropboxBtn.UseVisualStyleBackColor = true;
            this.stopDropboxBtn.Click += new System.EventHandler(this.stopDropboxBtn_Click);
            // 
            // gitTab
            // 
            this.gitTab.Controls.Add(this.gitStatusLbl);
            this.gitTab.Location = new System.Drawing.Point(4, 24);
            this.gitTab.Name = "gitTab";
            this.gitTab.Padding = new System.Windows.Forms.Padding(3);
            this.gitTab.Size = new System.Drawing.Size(427, 369);
            this.gitTab.TabIndex = 2;
            this.gitTab.Text = "Git checked in";
            this.gitTab.UseVisualStyleBackColor = true;
            // 
            // gitStatusLbl
            // 
            this.gitStatusLbl.AutoSize = true;
            this.gitStatusLbl.Location = new System.Drawing.Point(6, 3);
            this.gitStatusLbl.Name = "gitStatusLbl";
            this.gitStatusLbl.Size = new System.Drawing.Size(62, 15);
            this.gitStatusLbl.TabIndex = 0;
            this.gitStatusLbl.Text = "Git status: ";
            // 
            // versionTab
            // 
            this.versionTab.Controls.Add(this.verRefreshBtn);
            this.versionTab.Controls.Add(this.currVersionValueLbl);
            this.versionTab.Controls.Add(this.currVersionDescLbl);
            this.versionTab.Controls.Add(this.lastPublishedVerValueLbl);
            this.versionTab.Controls.Add(this.lastPublishedVerDescLbl);
            this.versionTab.Location = new System.Drawing.Point(4, 24);
            this.versionTab.Name = "versionTab";
            this.versionTab.Padding = new System.Windows.Forms.Padding(3);
            this.versionTab.Size = new System.Drawing.Size(427, 369);
            this.versionTab.TabIndex = 1;
            this.versionTab.Text = "New version #";
            this.versionTab.UseVisualStyleBackColor = true;
            // 
            // verRefreshBtn
            // 
            this.verRefreshBtn.Location = new System.Drawing.Point(7, 36);
            this.verRefreshBtn.Name = "verRefreshBtn";
            this.verRefreshBtn.Size = new System.Drawing.Size(129, 23);
            this.verRefreshBtn.TabIndex = 5;
            this.verRefreshBtn.Text = "Refresh current";
            this.verRefreshBtn.UseVisualStyleBackColor = true;
            this.verRefreshBtn.Click += new System.EventHandler(this.verRefreshBtn_Click);
            // 
            // currVersionValueLbl
            // 
            this.currVersionValueLbl.AutoSize = true;
            this.currVersionValueLbl.Location = new System.Drawing.Point(142, 18);
            this.currVersionValueLbl.Name = "currVersionValueLbl";
            this.currVersionValueLbl.Size = new System.Drawing.Size(38, 15);
            this.currVersionValueLbl.TabIndex = 3;
            this.currVersionValueLbl.Text = "label3";
            // 
            // currVersionDescLbl
            // 
            this.currVersionDescLbl.AutoSize = true;
            this.currVersionDescLbl.Location = new System.Drawing.Point(6, 18);
            this.currVersionDescLbl.Name = "currVersionDescLbl";
            this.currVersionDescLbl.Size = new System.Drawing.Size(91, 15);
            this.currVersionDescLbl.TabIndex = 2;
            this.currVersionDescLbl.Text = "Current version:";
            // 
            // lastPublishedVerValueLbl
            // 
            this.lastPublishedVerValueLbl.AutoSize = true;
            this.lastPublishedVerValueLbl.Location = new System.Drawing.Point(142, 3);
            this.lastPublishedVerValueLbl.Name = "lastPublishedVerValueLbl";
            this.lastPublishedVerValueLbl.Size = new System.Drawing.Size(38, 15);
            this.lastPublishedVerValueLbl.TabIndex = 1;
            this.lastPublishedVerValueLbl.Text = "label2";
            // 
            // lastPublishedVerDescLbl
            // 
            this.lastPublishedVerDescLbl.AutoSize = true;
            this.lastPublishedVerDescLbl.Location = new System.Drawing.Point(6, 3);
            this.lastPublishedVerDescLbl.Name = "lastPublishedVerDescLbl";
            this.lastPublishedVerDescLbl.Size = new System.Drawing.Size(130, 15);
            this.lastPublishedVerDescLbl.TabIndex = 0;
            this.lastPublishedVerDescLbl.Text = "Last published version: ";
            // 
            // latestCommitsTab
            // 
            this.latestCommitsTab.Controls.Add(this.tagLbl);
            this.latestCommitsTab.Controls.Add(this.latestGitTagLbl);
            this.latestCommitsTab.Location = new System.Drawing.Point(4, 24);
            this.latestCommitsTab.Name = "latestCommitsTab";
            this.latestCommitsTab.Padding = new System.Windows.Forms.Padding(3);
            this.latestCommitsTab.Size = new System.Drawing.Size(427, 369);
            this.latestCommitsTab.TabIndex = 3;
            this.latestCommitsTab.Text = "Latest commits";
            this.latestCommitsTab.UseVisualStyleBackColor = true;
            // 
            // tagLbl
            // 
            this.tagLbl.AutoSize = true;
            this.tagLbl.Location = new System.Drawing.Point(93, 3);
            this.tagLbl.Name = "tagLbl";
            this.tagLbl.Size = new System.Drawing.Size(38, 15);
            this.tagLbl.TabIndex = 2;
            this.tagLbl.Text = "label2";
            // 
            // latestGitTagLbl
            // 
            this.latestGitTagLbl.AutoSize = true;
            this.latestGitTagLbl.Location = new System.Drawing.Point(6, 3);
            this.latestGitTagLbl.Name = "latestGitTagLbl";
            this.latestGitTagLbl.Size = new System.Drawing.Size(81, 15);
            this.latestGitTagLbl.TabIndex = 0;
            this.latestGitTagLbl.Text = "Latest git tag: ";
            // 
            // buildReleaseTab
            // 
            this.buildReleaseTab.Controls.Add(this.buildReleaseOutputTb);
            this.buildReleaseTab.Controls.Add(this.buildReleaseBtn);
            this.buildReleaseTab.Location = new System.Drawing.Point(4, 24);
            this.buildReleaseTab.Name = "buildReleaseTab";
            this.buildReleaseTab.Padding = new System.Windows.Forms.Padding(3);
            this.buildReleaseTab.Size = new System.Drawing.Size(427, 369);
            this.buildReleaseTab.TabIndex = 4;
            this.buildReleaseTab.Text = "Build Release";
            this.buildReleaseTab.UseVisualStyleBackColor = true;
            // 
            // buildReleaseOutputTb
            // 
            this.buildReleaseOutputTb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buildReleaseOutputTb.Location = new System.Drawing.Point(6, 35);
            this.buildReleaseOutputTb.Multiline = true;
            this.buildReleaseOutputTb.Name = "buildReleaseOutputTb";
            this.buildReleaseOutputTb.ReadOnly = true;
            this.buildReleaseOutputTb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.buildReleaseOutputTb.Size = new System.Drawing.Size(415, 328);
            this.buildReleaseOutputTb.TabIndex = 1;
            // 
            // buildReleaseBtn
            // 
            this.buildReleaseBtn.Location = new System.Drawing.Point(3, 6);
            this.buildReleaseBtn.Name = "buildReleaseBtn";
            this.buildReleaseBtn.Size = new System.Drawing.Size(116, 23);
            this.buildReleaseBtn.TabIndex = 0;
            this.buildReleaseBtn.Text = "Build Release";
            this.buildReleaseBtn.UseVisualStyleBackColor = true;
            this.buildReleaseBtn.Click += new System.EventHandler(this.buildReleaseBtn_Click);
            // 
            // publishToGithubTab
            // 
            this.publishToGithubTab.Controls.Add(this.publishPreReleaseCb);
            this.publishToGithubTab.Controls.Add(this.publishBtn);
            this.publishToGithubTab.Location = new System.Drawing.Point(4, 24);
            this.publishToGithubTab.Name = "publishToGithubTab";
            this.publishToGithubTab.Size = new System.Drawing.Size(427, 369);
            this.publishToGithubTab.TabIndex = 5;
            this.publishToGithubTab.Text = "Publish to github";
            this.publishToGithubTab.UseVisualStyleBackColor = true;
            // 
            // publishPreReleaseCb
            // 
            this.publishPreReleaseCb.AutoSize = true;
            this.publishPreReleaseCb.Location = new System.Drawing.Point(3, 35);
            this.publishPreReleaseCb.Name = "publishPreReleaseCb";
            this.publishPreReleaseCb.Size = new System.Drawing.Size(140, 19);
            this.publishPreReleaseCb.TabIndex = 1;
            this.publishPreReleaseCb.Text = "Publish as Pre-release";
            this.publishPreReleaseCb.UseVisualStyleBackColor = true;
            this.publishPreReleaseCb.CheckedChanged += new System.EventHandler(this.publishPreReleaseCb_CheckedChanged);
            // 
            // publishBtn
            // 
            this.publishBtn.Location = new System.Drawing.Point(3, 6);
            this.publishBtn.Name = "publishBtn";
            this.publishBtn.Size = new System.Drawing.Size(116, 23);
            this.publishBtn.TabIndex = 0;
            this.publishBtn.Text = "Publish to github";
            this.publishBtn.UseVisualStyleBackColor = true;
            this.publishBtn.Click += new System.EventHandler(this.publishBtn_Click);
            // 
            // restartDropboxTab
            // 
            this.restartDropboxTab.Controls.Add(this.restartDropboxBtn);
            this.restartDropboxTab.Location = new System.Drawing.Point(4, 24);
            this.restartDropboxTab.Name = "restartDropboxTab";
            this.restartDropboxTab.Padding = new System.Windows.Forms.Padding(3);
            this.restartDropboxTab.Size = new System.Drawing.Size(427, 369);
            this.restartDropboxTab.TabIndex = 6;
            this.restartDropboxTab.Text = "Restart Dropbox";
            this.restartDropboxTab.UseVisualStyleBackColor = true;
            // 
            // restartDropboxBtn
            // 
            this.restartDropboxBtn.Location = new System.Drawing.Point(6, 6);
            this.restartDropboxBtn.Name = "restartDropboxBtn";
            this.restartDropboxBtn.Size = new System.Drawing.Size(140, 23);
            this.restartDropboxBtn.TabIndex = 0;
            this.restartDropboxBtn.Text = "Restart Dropbox";
            this.restartDropboxBtn.UseVisualStyleBackColor = true;
            this.restartDropboxBtn.Click += new System.EventHandler(this.restartDropboxBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextBtn.Location = new System.Drawing.Point(372, 415);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(75, 23);
            this.nextBtn.TabIndex = 1;
            this.nextBtn.Text = "Next";
            this.nextBtn.UseVisualStyleBackColor = true;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // prevBtn
            // 
            this.prevBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prevBtn.Location = new System.Drawing.Point(12, 415);
            this.prevBtn.Name = "prevBtn";
            this.prevBtn.Size = new System.Drawing.Size(75, 23);
            this.prevBtn.TabIndex = 2;
            this.prevBtn.Text = "Prev";
            this.prevBtn.UseVisualStyleBackColor = true;
            this.prevBtn.Click += new System.EventHandler(this.prevBtn_Click);
            // 
            // finalGb
            // 
            this.finalGb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finalGb.Controls.Add(this.finalZipTb);
            this.finalGb.Controls.Add(this.finalZipLbl);
            this.finalGb.Controls.Add(this.finalBodyTb);
            this.finalGb.Controls.Add(this.finalTitleTb);
            this.finalGb.Controls.Add(this.finalTitleLbl);
            this.finalGb.Controls.Add(this.finalVersionTb);
            this.finalGb.Controls.Add(this.finalVersionLbl);
            this.finalGb.Location = new System.Drawing.Point(453, 12);
            this.finalGb.Name = "finalGb";
            this.finalGb.Size = new System.Drawing.Size(383, 332);
            this.finalGb.TabIndex = 3;
            this.finalGb.TabStop = false;
            this.finalGb.Text = "Final";
            // 
            // finalZipTb
            // 
            this.finalZipTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finalZipTb.Location = new System.Drawing.Point(6, 264);
            this.finalZipTb.Multiline = true;
            this.finalZipTb.Name = "finalZipTb";
            this.finalZipTb.ReadOnly = true;
            this.finalZipTb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.finalZipTb.Size = new System.Drawing.Size(371, 62);
            this.finalZipTb.TabIndex = 6;
            // 
            // finalZipLbl
            // 
            this.finalZipLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.finalZipLbl.AutoSize = true;
            this.finalZipLbl.Location = new System.Drawing.Point(6, 246);
            this.finalZipLbl.Name = "finalZipLbl";
            this.finalZipLbl.Size = new System.Drawing.Size(27, 15);
            this.finalZipLbl.TabIndex = 5;
            this.finalZipLbl.Text = "Zip:";
            // 
            // finalBodyTb
            // 
            this.finalBodyTb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finalBodyTb.Location = new System.Drawing.Point(6, 74);
            this.finalBodyTb.Multiline = true;
            this.finalBodyTb.Name = "finalBodyTb";
            this.finalBodyTb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.finalBodyTb.Size = new System.Drawing.Size(371, 169);
            this.finalBodyTb.TabIndex = 4;
            // 
            // finalTitleTb
            // 
            this.finalTitleTb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finalTitleTb.Location = new System.Drawing.Point(87, 45);
            this.finalTitleTb.Name = "finalTitleTb";
            this.finalTitleTb.Size = new System.Drawing.Size(290, 23);
            this.finalTitleTb.TabIndex = 3;
            // 
            // finalTitleLbl
            // 
            this.finalTitleLbl.AutoSize = true;
            this.finalTitleLbl.Location = new System.Drawing.Point(6, 48);
            this.finalTitleLbl.Name = "finalTitleLbl";
            this.finalTitleLbl.Size = new System.Drawing.Size(32, 15);
            this.finalTitleLbl.TabIndex = 2;
            this.finalTitleLbl.Text = "Title:";
            // 
            // finalVersionTb
            // 
            this.finalVersionTb.Location = new System.Drawing.Point(87, 16);
            this.finalVersionTb.Name = "finalVersionTb";
            this.finalVersionTb.Size = new System.Drawing.Size(69, 23);
            this.finalVersionTb.TabIndex = 1;
            // 
            // finalVersionLbl
            // 
            this.finalVersionLbl.AutoSize = true;
            this.finalVersionLbl.Location = new System.Drawing.Point(6, 19);
            this.finalVersionLbl.Name = "finalVersionLbl";
            this.finalVersionLbl.Size = new System.Drawing.Size(75, 15);
            this.finalVersionLbl.TabIndex = 0;
            this.finalVersionLbl.Text = "New version:";
            // 
            // warningsLbl
            // 
            this.warningsLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.warningsLbl.AutoSize = true;
            this.warningsLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.warningsLbl.Location = new System.Drawing.Point(453, 347);
            this.warningsLbl.Name = "warningsLbl";
            this.warningsLbl.Size = new System.Drawing.Size(65, 15);
            this.warningsLbl.TabIndex = 5;
            this.warningsLbl.Text = "[warnings]";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 450);
            this.Controls.Add(this.warningsLbl);
            this.Controls.Add(this.finalGb);
            this.Controls.Add(this.prevBtn);
            this.Controls.Add(this.nextBtn);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Release";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.stopDropboxTab.ResumeLayout(false);
            this.gitTab.ResumeLayout(false);
            this.gitTab.PerformLayout();
            this.versionTab.ResumeLayout(false);
            this.versionTab.PerformLayout();
            this.latestCommitsTab.ResumeLayout(false);
            this.latestCommitsTab.PerformLayout();
            this.buildReleaseTab.ResumeLayout(false);
            this.buildReleaseTab.PerformLayout();
            this.publishToGithubTab.ResumeLayout(false);
            this.publishToGithubTab.PerformLayout();
            this.restartDropboxTab.ResumeLayout(false);
            this.finalGb.ResumeLayout(false);
            this.finalGb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage stopDropboxTab;
		private System.Windows.Forms.TabPage versionTab;
		private System.Windows.Forms.TabPage gitTab;
		private System.Windows.Forms.Button nextBtn;
		private System.Windows.Forms.Label lastPublishedVerValueLbl;
		private System.Windows.Forms.Label lastPublishedVerDescLbl;
		private System.Windows.Forms.Label currVersionValueLbl;
		private System.Windows.Forms.Label currVersionDescLbl;
		private System.Windows.Forms.Button verRefreshBtn;
		private System.Windows.Forms.Button prevBtn;
		private System.Windows.Forms.TabPage latestCommitsTab;
		private System.Windows.Forms.TabPage buildReleaseTab;
		private System.Windows.Forms.Label gitStatusLbl;
		private System.Windows.Forms.Label tagLbl;
		private System.Windows.Forms.Label latestGitTagLbl;
		private System.Windows.Forms.GroupBox finalGb;
		private System.Windows.Forms.TextBox finalVersionTb;
		private System.Windows.Forms.Label finalVersionLbl;
		private System.Windows.Forms.Label warningsLbl;
		private System.Windows.Forms.TextBox finalTitleTb;
		private System.Windows.Forms.Label finalTitleLbl;
		private System.Windows.Forms.TextBox finalBodyTb;
		private System.Windows.Forms.Button buildReleaseBtn;
		private System.Windows.Forms.Label finalZipLbl;
		private System.Windows.Forms.TextBox finalZipTb;
		private System.Windows.Forms.TabPage publishToGithubTab;
		private System.Windows.Forms.Button publishBtn;
        private System.Windows.Forms.Button stopDropboxBtn;
        private System.Windows.Forms.TabPage restartDropboxTab;
        private System.Windows.Forms.Button restartDropboxBtn;
        private System.Windows.Forms.CheckBox publishPreReleaseCb;
        private System.Windows.Forms.TextBox buildReleaseOutputTb;
    }
}

