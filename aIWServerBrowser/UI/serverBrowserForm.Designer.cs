namespace aIWServerBrowser
{
    partial class serverBrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(serverBrowserForm));
            this.stopButton = new System.Windows.Forms.Button();
            this.taskProgress = new System.Windows.Forms.ProgressBar();
            this.taskDescription = new System.Windows.Forms.Label();
            this.joinGameButton = new System.Windows.Forms.Button();
            this.serverInfoButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.filterName = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.refreshListButton = new System.Windows.Forms.Button();
            this.serverBrowserView = new System.Windows.Forms.ListView();
            this.serverIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverFavourite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverPing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverPlayers = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverGametype = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverMap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverMod = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serverHC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.favouriteToggle = new System.Windows.Forms.Button();
            this.queryIPAddr = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopButton.Location = new System.Drawing.Point(1136, 12);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(51, 24);
            this.stopButton.TabIndex = 0;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // taskProgress
            // 
            this.taskProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.taskProgress.Location = new System.Drawing.Point(991, 13);
            this.taskProgress.Name = "taskProgress";
            this.taskProgress.Size = new System.Drawing.Size(139, 23);
            this.taskProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.taskProgress.TabIndex = 1;
            // 
            // taskDescription
            // 
            this.taskDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskDescription.Location = new System.Drawing.Point(252, 15);
            this.taskDescription.Name = "taskDescription";
            this.taskDescription.Size = new System.Drawing.Size(735, 20);
            this.taskDescription.TabIndex = 2;
            this.taskDescription.Text = "0 servers found.";
            this.taskDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // joinGameButton
            // 
            this.joinGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.joinGameButton.Enabled = false;
            this.joinGameButton.Location = new System.Drawing.Point(1093, 537);
            this.joinGameButton.Name = "joinGameButton";
            this.joinGameButton.Size = new System.Drawing.Size(94, 27);
            this.joinGameButton.TabIndex = 3;
            this.joinGameButton.Text = "Join Game";
            this.joinGameButton.UseVisualStyleBackColor = true;
            this.joinGameButton.Click += new System.EventHandler(this.joinGameButton_Click);
            // 
            // serverInfoButton
            // 
            this.serverInfoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.serverInfoButton.Location = new System.Drawing.Point(993, 537);
            this.serverInfoButton.Name = "serverInfoButton";
            this.serverInfoButton.Size = new System.Drawing.Size(94, 27);
            this.serverInfoButton.TabIndex = 4;
            this.serverInfoButton.Text = "Server Info";
            this.serverInfoButton.UseVisualStyleBackColor = true;
            this.serverInfoButton.Click += new System.EventHandler(this.serverInfoButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.filterButton.Location = new System.Drawing.Point(793, 537);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(94, 27);
            this.filterButton.TabIndex = 6;
            this.filterButton.Text = "Change Filter";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "FILTER:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // filterName
            // 
            this.filterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterName.Location = new System.Drawing.Point(76, 12);
            this.filterName.Name = "filterName";
            this.filterName.Size = new System.Drawing.Size(170, 23);
            this.filterName.TabIndex = 8;
            this.filterName.Text = "All servers";
            this.filterName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // authorLabel
            // 
            this.authorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(12, 537);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(82, 13);
            this.authorLabel.TabIndex = 11;
            this.authorLabel.Text = "by KeyboardCat";
            // 
            // refreshListButton
            // 
            this.refreshListButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshListButton.Location = new System.Drawing.Point(893, 537);
            this.refreshListButton.Name = "refreshListButton";
            this.refreshListButton.Size = new System.Drawing.Size(94, 27);
            this.refreshListButton.TabIndex = 12;
            this.refreshListButton.Text = "Refresh List";
            this.refreshListButton.UseVisualStyleBackColor = true;
            this.refreshListButton.Click += new System.EventHandler(this.refreshListButton_Click);
            // 
            // serverBrowserView
            // 
            this.serverBrowserView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.serverBrowserView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.serverIP,
            this.serverName,
            this.serverFavourite,
            this.serverPing,
            this.serverPlayers,
            this.serverGametype,
            this.serverMap,
            this.serverMod,
            this.serverHC});
            this.serverBrowserView.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverBrowserView.FullRowSelect = true;
            this.serverBrowserView.HideSelection = false;
            this.serverBrowserView.Location = new System.Drawing.Point(15, 42);
            this.serverBrowserView.MultiSelect = false;
            this.serverBrowserView.Name = "serverBrowserView";
            this.serverBrowserView.Size = new System.Drawing.Size(1172, 489);
            this.serverBrowserView.TabIndex = 10;
            this.serverBrowserView.UseCompatibleStateImageBehavior = false;
            this.serverBrowserView.View = System.Windows.Forms.View.Details;
            this.serverBrowserView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.serverBrowserView_ColumnClick);
            this.serverBrowserView.SelectedIndexChanged += new System.EventHandler(this.serverBrowserView_SelectedIndexChanged);
            this.serverBrowserView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.serverBrowserView_MouseDoubleClick);
            // 
            // serverIP
            // 
            this.serverIP.DisplayIndex = 7;
            this.serverIP.Text = "IP Address";
            this.serverIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.serverIP.Width = 178;
            // 
            // serverName
            // 
            this.serverName.DisplayIndex = 0;
            this.serverName.Text = "Server Name";
            this.serverName.Width = 250;
            // 
            // serverFavourite
            // 
            this.serverFavourite.DisplayIndex = 1;
            this.serverFavourite.Text = "Favourite";
            this.serverFavourite.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.serverFavourite.Width = 75;
            // 
            // serverPing
            // 
            this.serverPing.DisplayIndex = 2;
            this.serverPing.Text = "Ping (Max)";
            this.serverPing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.serverPing.Width = 100;
            // 
            // serverPlayers
            // 
            this.serverPlayers.DisplayIndex = 3;
            this.serverPlayers.Text = "# Players";
            this.serverPlayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.serverPlayers.Width = 89;
            // 
            // serverGametype
            // 
            this.serverGametype.DisplayIndex = 4;
            this.serverGametype.Text = "Game Type";
            this.serverGametype.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.serverGametype.Width = 148;
            // 
            // serverMap
            // 
            this.serverMap.DisplayIndex = 5;
            this.serverMap.Text = "Map";
            this.serverMap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.serverMap.Width = 120;
            // 
            // serverMod
            // 
            this.serverMod.DisplayIndex = 8;
            this.serverMod.Text = "Mod";
            this.serverMod.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.serverMod.Width = 147;
            // 
            // serverHC
            // 
            this.serverHC.DisplayIndex = 6;
            this.serverHC.Text = "HC";
            this.serverHC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // favouriteToggle
            // 
            this.favouriteToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.favouriteToggle.Enabled = false;
            this.favouriteToggle.Location = new System.Drawing.Point(445, 537);
            this.favouriteToggle.Name = "favouriteToggle";
            this.favouriteToggle.Size = new System.Drawing.Size(206, 27);
            this.favouriteToggle.TabIndex = 13;
            this.favouriteToggle.Text = "Add/Remove Server from Favourites";
            this.favouriteToggle.UseVisualStyleBackColor = true;
            this.favouriteToggle.Click += new System.EventHandler(this.favouriteToggle_Click);
            // 
            // queryIPAddr
            // 
            this.queryIPAddr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.queryIPAddr.Location = new System.Drawing.Point(657, 537);
            this.queryIPAddr.Name = "queryIPAddr";
            this.queryIPAddr.Size = new System.Drawing.Size(130, 27);
            this.queryIPAddr.TabIndex = 14;
            this.queryIPAddr.Text = "Query a single address";
            this.queryIPAddr.UseVisualStyleBackColor = true;
            this.queryIPAddr.Click += new System.EventHandler(this.button1_Click);
            // 
            // serverBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 575);
            this.Controls.Add(this.queryIPAddr);
            this.Controls.Add(this.favouriteToggle);
            this.Controls.Add(this.refreshListButton);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.serverBrowserView);
            this.Controls.Add(this.filterName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.serverInfoButton);
            this.Controls.Add(this.joinGameButton);
            this.Controls.Add(this.taskDescription);
            this.Controls.Add(this.taskProgress);
            this.Controls.Add(this.stopButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(890, 613);
            this.Name = "serverBrowserForm";
            this.Text = "alterIWnet Server Browser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.serverBrowserForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.serverBrowserForm_FormClosed);
            this.Load += new System.EventHandler(this.serverBrowserForm_Load);
            this.LostFocus += new System.EventHandler(this.serverBrowserForm_LostFocus);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.ProgressBar taskProgress;
        private System.Windows.Forms.Label taskDescription;
        private System.Windows.Forms.Button joinGameButton;
        private System.Windows.Forms.Button serverInfoButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label filterName;
        private System.Windows.Forms.ListView serverBrowserView;
        private System.Windows.Forms.ColumnHeader serverName;
        private System.Windows.Forms.ColumnHeader serverFavourite;
        private System.Windows.Forms.ColumnHeader serverPing;
        private System.Windows.Forms.ColumnHeader serverGametype;
        private System.Windows.Forms.ColumnHeader serverMap;
        private System.Windows.Forms.ColumnHeader serverPlayers;
        private System.Windows.Forms.ColumnHeader serverIP;
        private System.Windows.Forms.ColumnHeader serverMod;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Button refreshListButton;
        private System.Windows.Forms.ColumnHeader serverHC;
        private System.Windows.Forms.Button favouriteToggle;
        private System.Windows.Forms.Button queryIPAddr;
    }
}