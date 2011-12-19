namespace aIWServerBrowser
{
    partial class FriendsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FriendsList));
            this.friendView = new System.Windows.Forms.ListView();
            this.guidCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.addFriendBtn = new System.Windows.Forms.Button();
            this.joinGameBtn = new System.Windows.Forms.Button();
            this.delFriendbtn = new System.Windows.Forms.Button();
            this.checkFriendStatsbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // friendView
            // 
            this.friendView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.friendView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.guidCol,
            this.nameCol,
            this.statusCol});
            this.friendView.FullRowSelect = true;
            this.friendView.Location = new System.Drawing.Point(12, 45);
            this.friendView.MultiSelect = false;
            this.friendView.Name = "friendView";
            this.friendView.Size = new System.Drawing.Size(563, 315);
            this.friendView.TabIndex = 0;
            this.friendView.UseCompatibleStateImageBehavior = false;
            this.friendView.View = System.Windows.Forms.View.Details;
            this.friendView.SelectedIndexChanged += new System.EventHandler(this.friendView_SelectedIndexChanged);
            // 
            // guidCol
            // 
            this.guidCol.DisplayIndex = 1;
            this.guidCol.Text = "GUID";
            this.guidCol.Width = 157;
            // 
            // nameCol
            // 
            this.nameCol.DisplayIndex = 0;
            this.nameCol.Text = "Given Name";
            this.nameCol.Width = 250;
            // 
            // statusCol
            // 
            this.statusCol.Text = "Status";
            this.statusCol.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From this dialog, you can track your friends via XUID.\r\n";
            // 
            // addFriendBtn
            // 
            this.addFriendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addFriendBtn.Location = new System.Drawing.Point(12, 368);
            this.addFriendBtn.Name = "addFriendBtn";
            this.addFriendBtn.Size = new System.Drawing.Size(88, 29);
            this.addFriendBtn.TabIndex = 2;
            this.addFriendBtn.Text = "+  Add Friend";
            this.addFriendBtn.UseVisualStyleBackColor = true;
            this.addFriendBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // joinGameBtn
            // 
            this.joinGameBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.joinGameBtn.Enabled = false;
            this.joinGameBtn.Location = new System.Drawing.Point(491, 368);
            this.joinGameBtn.Name = "joinGameBtn";
            this.joinGameBtn.Size = new System.Drawing.Size(85, 29);
            this.joinGameBtn.TabIndex = 3;
            this.joinGameBtn.Text = "Join Game";
            this.joinGameBtn.UseVisualStyleBackColor = true;
            this.joinGameBtn.Click += new System.EventHandler(this.joinGameBtn_Click);
            // 
            // delFriendbtn
            // 
            this.delFriendbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.delFriendbtn.Enabled = false;
            this.delFriendbtn.Location = new System.Drawing.Point(106, 368);
            this.delFriendbtn.Name = "delFriendbtn";
            this.delFriendbtn.Size = new System.Drawing.Size(91, 29);
            this.delFriendbtn.TabIndex = 4;
            this.delFriendbtn.Text = "- Delete Friend";
            this.delFriendbtn.UseVisualStyleBackColor = true;
            this.delFriendbtn.Click += new System.EventHandler(this.delFriendbtn_Click);
            // 
            // checkFriendStatsbtn
            // 
            this.checkFriendStatsbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkFriendStatsbtn.Enabled = false;
            this.checkFriendStatsbtn.Location = new System.Drawing.Point(400, 368);
            this.checkFriendStatsbtn.Name = "checkFriendStatsbtn";
            this.checkFriendStatsbtn.Size = new System.Drawing.Size(85, 29);
            this.checkFriendStatsbtn.TabIndex = 5;
            this.checkFriendStatsbtn.Text = "Check Stats";
            this.checkFriendStatsbtn.UseVisualStyleBackColor = true;
            this.checkFriendStatsbtn.Click += new System.EventHandler(this.checkFriendStatsbtn_Click);
            // 
            // FriendsList
            // 
            this.AcceptButton = this.joinGameBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 407);
            this.Controls.Add(this.checkFriendStatsbtn);
            this.Controls.Add(this.delFriendbtn);
            this.Controls.Add(this.joinGameBtn);
            this.Controls.Add(this.addFriendBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.friendView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(407, 252);
            this.Name = "FriendsList";
            this.Text = "Buddy List";
            this.Load += new System.EventHandler(this.FriendsList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView friendView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addFriendBtn;
        private System.Windows.Forms.ColumnHeader nameCol;
        private System.Windows.Forms.ColumnHeader guidCol;
        private System.Windows.Forms.ColumnHeader statusCol;
        private System.Windows.Forms.Button joinGameBtn;
        private System.Windows.Forms.Button delFriendbtn;
        private System.Windows.Forms.Button checkFriendStatsbtn;
    }
}