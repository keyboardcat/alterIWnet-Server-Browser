namespace aIWServerBrowser
{
    partial class InfoDlg
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.serverInfoView = new System.Windows.Forms.RichTextBox();
            this.singleServerJoinButton = new System.Windows.Forms.Button();
            this.singleServerRefreshButton = new System.Windows.Forms.Button();
            this.playerListView = new System.Windows.Forms.ListView();
            this.playerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.playerScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.playerPing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.serverInfoView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.singleServerJoinButton);
            this.splitContainer1.Panel2.Controls.Add(this.singleServerRefreshButton);
            this.splitContainer1.Panel2.Controls.Add(this.playerListView);
            this.splitContainer1.Size = new System.Drawing.Size(367, 384);
            this.splitContainer1.SplitterDistance = 172;
            this.splitContainer1.TabIndex = 0;
            // 
            // serverInfoView
            // 
            this.serverInfoView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.serverInfoView.BackColor = System.Drawing.SystemColors.Control;
            this.serverInfoView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.serverInfoView.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverInfoView.ForeColor = System.Drawing.SystemColors.ControlText;
            this.serverInfoView.Location = new System.Drawing.Point(12, 12);
            this.serverInfoView.Name = "serverInfoView";
            this.serverInfoView.ReadOnly = true;
            this.serverInfoView.Size = new System.Drawing.Size(339, 142);
            this.serverInfoView.TabIndex = 0;
            this.serverInfoView.Text = "No server selected.";
            // 
            // singleServerJoinButton
            // 
            this.singleServerJoinButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.singleServerJoinButton.Enabled = false;
            this.singleServerJoinButton.Location = new System.Drawing.Point(250, 170);
            this.singleServerJoinButton.Name = "singleServerJoinButton";
            this.singleServerJoinButton.Size = new System.Drawing.Size(101, 28);
            this.singleServerJoinButton.TabIndex = 2;
            this.singleServerJoinButton.Text = "Join Game";
            this.singleServerJoinButton.UseVisualStyleBackColor = true;
            this.singleServerJoinButton.Click += new System.EventHandler(this.singleServerJoinButton_Click);
            // 
            // singleServerRefreshButton
            // 
            this.singleServerRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.singleServerRefreshButton.Enabled = false;
            this.singleServerRefreshButton.Location = new System.Drawing.Point(143, 170);
            this.singleServerRefreshButton.Name = "singleServerRefreshButton";
            this.singleServerRefreshButton.Size = new System.Drawing.Size(101, 28);
            this.singleServerRefreshButton.TabIndex = 1;
            this.singleServerRefreshButton.Text = "Refresh";
            this.singleServerRefreshButton.UseVisualStyleBackColor = true;
            this.singleServerRefreshButton.Click += new System.EventHandler(this.singleServerRefreshButton_Click);
            // 
            // playerListView
            // 
            this.playerListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.playerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.playerName,
            this.playerScore,
            this.playerPing});
            this.playerListView.FullRowSelect = true;
            this.playerListView.Location = new System.Drawing.Point(12, 13);
            this.playerListView.Name = "playerListView";
            this.playerListView.Size = new System.Drawing.Size(339, 151);
            this.playerListView.TabIndex = 0;
            this.playerListView.UseCompatibleStateImageBehavior = false;
            this.playerListView.View = System.Windows.Forms.View.Details;
            // 
            // playerName
            // 
            this.playerName.Text = "Name";
            this.playerName.Width = 177;
            // 
            // playerScore
            // 
            this.playerScore.Text = "Score";
            this.playerScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.playerScore.Width = 80;
            // 
            // playerPing
            // 
            this.playerPing.Text = "Ping";
            this.playerPing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.playerPing.Width = 80;
            // 
            // InfoDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 384);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "No server selected.";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InfoDlg_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox serverInfoView;
        private System.Windows.Forms.ListView playerListView;
        private System.Windows.Forms.ColumnHeader playerName;
        private System.Windows.Forms.ColumnHeader playerScore;
        private System.Windows.Forms.ColumnHeader playerPing;
        private System.Windows.Forms.Button singleServerRefreshButton;
        private System.Windows.Forms.Button singleServerJoinButton;
    }
}