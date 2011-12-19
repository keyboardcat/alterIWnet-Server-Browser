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
    partial class FiltersDlg
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
            this.filterList = new System.Windows.Forms.ListBox();
            this.newFilter = new System.Windows.Forms.Button();
            this.editFilter = new System.Windows.Forms.Button();
            this.delFilter = new System.Windows.Forms.Button();
            this.useFilter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filterList
            // 
            this.filterList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterList.FormattingEnabled = true;
            this.filterList.ItemHeight = 19;
            this.filterList.Items.AddRange(new object[] {
            "All servers",
            "Favourites",
            "Friends"});
            this.filterList.Location = new System.Drawing.Point(119, 11);
            this.filterList.Name = "filterList";
            this.filterList.Size = new System.Drawing.Size(186, 156);
            this.filterList.TabIndex = 0;
            this.filterList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // newFilter
            // 
            this.newFilter.Location = new System.Drawing.Point(7, 44);
            this.newFilter.Name = "newFilter";
            this.newFilter.Size = new System.Drawing.Size(102, 29);
            this.newFilter.TabIndex = 1;
            this.newFilter.Text = "New";
            this.newFilter.UseVisualStyleBackColor = true;
            this.newFilter.Click += new System.EventHandler(this.newFilter_Click);
            // 
            // editFilter
            // 
            this.editFilter.Enabled = false;
            this.editFilter.Location = new System.Drawing.Point(7, 79);
            this.editFilter.Name = "editFilter";
            this.editFilter.Size = new System.Drawing.Size(102, 29);
            this.editFilter.TabIndex = 2;
            this.editFilter.Text = "Edit";
            this.editFilter.UseVisualStyleBackColor = true;
            this.editFilter.Click += new System.EventHandler(this.editFilter_Click);
            // 
            // delFilter
            // 
            this.delFilter.Enabled = false;
            this.delFilter.Location = new System.Drawing.Point(7, 114);
            this.delFilter.Name = "delFilter";
            this.delFilter.Size = new System.Drawing.Size(102, 29);
            this.delFilter.TabIndex = 3;
            this.delFilter.Text = "Delete";
            this.delFilter.UseVisualStyleBackColor = true;
            this.delFilter.Click += new System.EventHandler(this.delFilter_Click);
            // 
            // useFilter
            // 
            this.useFilter.Enabled = false;
            this.useFilter.Location = new System.Drawing.Point(7, 9);
            this.useFilter.Name = "useFilter";
            this.useFilter.Size = new System.Drawing.Size(102, 29);
            this.useFilter.TabIndex = 4;
            this.useFilter.Text = "Select";
            this.useFilter.UseVisualStyleBackColor = true;
            this.useFilter.Click += new System.EventHandler(this.useFilter_Click);
            // 
            // FiltersDlg
            // 
            this.AcceptButton = this.useFilter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 189);
            this.Controls.Add(this.useFilter);
            this.Controls.Add(this.delFilter);
            this.Controls.Add(this.editFilter);
            this.Controls.Add(this.newFilter);
            this.Controls.Add(this.filterList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FiltersDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Filters";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FiltersDlg_FormClosing);
            this.Load += new System.EventHandler(this.FiltersDlg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox filterList;
        private System.Windows.Forms.Button newFilter;
        private System.Windows.Forms.Button editFilter;
        private System.Windows.Forms.Button delFilter;
        private System.Windows.Forms.Button useFilter;
    }
}