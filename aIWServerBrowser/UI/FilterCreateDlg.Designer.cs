
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
    partial class FilterCreateDlg
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
            this.sEmpty = new System.Windows.Forms.CheckBox();
            this.sFull = new System.Windows.Forms.CheckBox();
            this.sHardcore = new System.Windows.Forms.CheckBox();
            this.sPing = new System.Windows.Forms.CheckBox();
            this.sFavourite = new System.Windows.Forms.CheckBox();
            this.sMods = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.sName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sGametype = new System.Windows.Forms.ComboBox();
            this.sMap = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sMod = new System.Windows.Forms.TextBox();
            this.sFriend = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.filterName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // sEmpty
            // 
            this.sEmpty.AutoSize = true;
            this.sEmpty.Checked = true;
            this.sEmpty.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.sEmpty.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sEmpty.Location = new System.Drawing.Point(12, 81);
            this.sEmpty.Name = "sEmpty";
            this.sEmpty.Size = new System.Drawing.Size(66, 22);
            this.sEmpty.TabIndex = 2;
            this.sEmpty.Text = "Empty";
            this.sEmpty.ThreeState = true;
            this.sEmpty.UseVisualStyleBackColor = true;
            // 
            // sFull
            // 
            this.sFull.AutoSize = true;
            this.sFull.Checked = true;
            this.sFull.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.sFull.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sFull.Location = new System.Drawing.Point(12, 41);
            this.sFull.Name = "sFull";
            this.sFull.Size = new System.Drawing.Size(50, 22);
            this.sFull.TabIndex = 1;
            this.sFull.Text = "Full";
            this.sFull.ThreeState = true;
            this.sFull.UseVisualStyleBackColor = true;
            // 
            // sHardcore
            // 
            this.sHardcore.AutoSize = true;
            this.sHardcore.Checked = true;
            this.sHardcore.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.sHardcore.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sHardcore.Location = new System.Drawing.Point(108, 41);
            this.sHardcore.Name = "sHardcore";
            this.sHardcore.Size = new System.Drawing.Size(83, 22);
            this.sHardcore.TabIndex = 3;
            this.sHardcore.Text = "Hardcore";
            this.sHardcore.ThreeState = true;
            this.sHardcore.UseVisualStyleBackColor = true;
            // 
            // sPing
            // 
            this.sPing.AutoSize = true;
            this.sPing.Checked = true;
            this.sPing.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.sPing.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sPing.Location = new System.Drawing.Point(108, 81);
            this.sPing.Name = "sPing";
            this.sPing.Size = new System.Drawing.Size(93, 22);
            this.sPing.TabIndex = 4;
            this.sPing.Text = "Ping < Max";
            this.sPing.ThreeState = true;
            this.sPing.UseVisualStyleBackColor = true;
            // 
            // sFavourite
            // 
            this.sFavourite.AutoSize = true;
            this.sFavourite.Checked = true;
            this.sFavourite.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.sFavourite.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sFavourite.Location = new System.Drawing.Point(222, 41);
            this.sFavourite.Name = "sFavourite";
            this.sFavourite.Size = new System.Drawing.Size(92, 22);
            this.sFavourite.TabIndex = 5;
            this.sFavourite.Text = "Favourites";
            this.sFavourite.ThreeState = true;
            this.sFavourite.UseVisualStyleBackColor = true;
            // 
            // sMods
            // 
            this.sMods.AutoSize = true;
            this.sMods.Checked = true;
            this.sMods.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.sMods.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sMods.Location = new System.Drawing.Point(222, 81);
            this.sMods.Name = "sMods";
            this.sMods.Size = new System.Drawing.Size(61, 22);
            this.sMods.TabIndex = 6;
            this.sMods.Text = "Mods";
            this.sMods.ThreeState = true;
            this.sMods.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(232, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(151, 269);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // sName
            // 
            this.sName.Location = new System.Drawing.Point(125, 124);
            this.sName.Name = "sName";
            this.sName.Size = new System.Drawing.Size(186, 20);
            this.sName.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Server name (contains):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Gametype:";
            // 
            // sGametype
            // 
            this.sGametype.FormattingEnabled = true;
            this.sGametype.Location = new System.Drawing.Point(125, 153);
            this.sGametype.Name = "sGametype";
            this.sGametype.Size = new System.Drawing.Size(186, 21);
            this.sGametype.TabIndex = 10;
            this.sGametype.Text = "(Any)";
            // 
            // sMap
            // 
            this.sMap.FormattingEnabled = true;
            this.sMap.Location = new System.Drawing.Point(125, 180);
            this.sMap.Name = "sMap";
            this.sMap.Size = new System.Drawing.Size(186, 21);
            this.sMap.TabIndex = 12;
            this.sMap.Text = "(Any)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Map:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Mod (contains):";
            // 
            // sMod
            // 
            this.sMod.Location = new System.Drawing.Point(125, 207);
            this.sMod.Name = "sMod";
            this.sMod.Size = new System.Drawing.Size(186, 20);
            this.sMod.TabIndex = 14;
            // 
            // sFriend
            // 
            this.sFriend.Location = new System.Drawing.Point(125, 233);
            this.sFriend.Name = "sFriend";
            this.sFriend.Size = new System.Drawing.Size(186, 20);
            this.sFriend.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 48);
            this.label5.TabIndex = 15;
            this.label5.Text = "Friend Search:\r\n(case insensitive)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "Name:";
            // 
            // filterName
            // 
            this.filterName.Location = new System.Drawing.Point(65, 9);
            this.filterName.MaxLength = 64;
            this.filterName.Name = "filterName";
            this.filterName.Size = new System.Drawing.Size(246, 20);
            this.filterName.TabIndex = 0;
            // 
            // FilterCreateDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 304);
            this.Controls.Add(this.filterName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.sFriend);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sMod);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sMap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sGametype);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sName);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sMods);
            this.Controls.Add(this.sFavourite);
            this.Controls.Add(this.sPing);
            this.Controls.Add(this.sHardcore);
            this.Controls.Add(this.sFull);
            this.Controls.Add(this.sEmpty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterCreateDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Filter";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox sEmpty;
        private System.Windows.Forms.CheckBox sFull;
        private System.Windows.Forms.CheckBox sHardcore;
        private System.Windows.Forms.CheckBox sPing;
        private System.Windows.Forms.CheckBox sFavourite;
        private System.Windows.Forms.CheckBox sMods;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox sName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sGametype;
        private System.Windows.Forms.ComboBox sMap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox sMod;
        private System.Windows.Forms.TextBox sFriend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox filterName;
    }
}