using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

/*
 * This file is part of aIW Server Browser.
 *
 *    aIW Server Browser is free software: you can redistribute it and/or modify
 *    it under the terms of the GNU General Public License as published by
 *    the Free Software Foundation, either version 3 of the License, or
 *    (at your option) any later version.
 *
 *    aIW Server Browser is distributed in the hope that it will be useful,
 *    but WITHOUT ANY WARRANTY; without even the implied warranty of
 *    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *    GNU General Public License for more details.
 *
 *    You should have received a copy of the GNU General Public License
 *    along with aIW Server Browser.  If not, see <http://www.gnu.org/licenses/>.
 */


namespace aIWServerBrowser
{
    public delegate bool addFriend(string n, long g);
    public class FriendEdit : Form
    {
        private addFriend _cb;
        public FriendEdit(addFriend cb)
        {
            _cb = cb;

            InitializeComponent();
        }

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox guid;
        private Button addmate;
        private TextBox name;
    
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.guid = new System.Windows.Forms.TextBox();
            this.addmate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your Friend\'s Name and XUID:";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(56, 33);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(163, 20);
            this.name.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "GUID:";
            // 
            // guid
            // 
            this.guid.Location = new System.Drawing.Point(56, 66);
            this.guid.Name = "guid";
            this.guid.Size = new System.Drawing.Size(163, 20);
            this.guid.TabIndex = 4;
            // 
            // addmate
            // 
            this.addmate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addmate.Location = new System.Drawing.Point(126, 93);
            this.addmate.Name = "addmate";
            this.addmate.Size = new System.Drawing.Size(93, 28);
            this.addmate.TabIndex = 5;
            this.addmate.Text = "+ Add Friend";
            this.addmate.UseVisualStyleBackColor = true;
            this.addmate.Click += new System.EventHandler(this.button1_Click);
            // 
            // FriendEdit
            // 
            this.ClientSize = new System.Drawing.Size(231, 132);
            this.Controls.Add(this.addmate);
            this.Controls.Add(this.guid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FriendEdit";
            this.Text = "Add a Friend";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (name.Text.Length > 20)
            {
                MessageBox.Show("The Name is too long!", "Error");
                return;
            }

            long l;
            if (!long.TryParse(guid.Text, NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out l))
            {
                MessageBox.Show("The GUID is not valid! (hex format only)", "Error");
                return;
            }

            if (_cb(name.Text, l))
                this.Close();
            else
                MessageBox.Show("A friend with that name already exists!");
        }
    }
}
