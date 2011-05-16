using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;

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
    public delegate void updateFilter(Filter f);
    public partial class FilterCreateDlg : Form
    {
        private Filter changingFilter;
        private updateFilter cb;
        private bool newf;
        public FilterCreateDlg(Filter f, updateFilter callback, bool newf)
        {
            changingFilter = f;
            cb = callback;
            this.newf = newf;

            InitializeComponent();
            initUIWithFilter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void initUIWithFilter()
        {
            sMap.Items.Clear();
            sMap.Items.Add("(Any)");
            foreach (string m in HelperFunctions.getMaps())
                sMap.Items.Add(HelperFunctions.mapToFullname(m));

            sGametype.Items.Clear();
            sGametype.Items.Add("(Any)");
            foreach (string g in HelperFunctions.getGametypes())
                sGametype.Items.Add(HelperFunctions.gameTypeToFullname(g));
            
            sFull.CheckState = y2c(changingFilter.Full);
            sEmpty.CheckState = y2c(changingFilter.Empty);
            sPing.CheckState = y2c(changingFilter.AllowedPing);
            sFavourite.CheckState = y2c(changingFilter.Favourites);
            sHardcore.CheckState = y2c(changingFilter.Hardcore);
            sMods.CheckState = y2c(changingFilter.Mods);

            sName.Text = changingFilter.name;
            if (!string.IsNullOrEmpty(changingFilter.gametype.Trim()))
                sGametype.Text = HelperFunctions.gameTypeToFullname(changingFilter.gametype);
            if (!string.IsNullOrEmpty(changingFilter.map.Trim()))
            sMap.Text = HelperFunctions.mapToFullname(changingFilter.map);
            sMod.Text = changingFilter.mod;
            sFriend.Text = changingFilter.buddy;

            filterName.Text = changingFilter.filterName;
        }

        private Filter.YNA c2y(CheckState c)
        {
            if (c == CheckState.Checked)
                return Filter.YNA.Yes;
            else if (c == CheckState.Indeterminate)
                return Filter.YNA.All;
            else
                return Filter.YNA.No;
        }

        private CheckState y2c(Filter.YNA c)
        {
            if (c == Filter.YNA.Yes)
                return CheckState.Checked;
            else if (c == Filter.YNA.All)
                return CheckState.Indeterminate;
            else
                return CheckState.Unchecked;
        }
        
        private bool validateFilterName(string s)
        {
            foreach (char c in s.ToLower().ToCharArray())
                if (!('a' <= c && c <= 'z' || '0' >= c && c <= '9'))
                    return false;
            return true;
        }

        private bool saveFilter()
        {
            if (!validateFilterName(changingFilter.filterName))
            {
                MessageBox.Show("A filter name can only be alpha-numeric! (letters and numbers only)",
                    "Filter name invalid", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if ((File.Exists("filters/" + changingFilter.filterName.ToLower()) && newf) ||
                changingFilter.filterName.ToLower() == "all servers" || // these will always exist anyway :P
                changingFilter.filterName.ToLower() == "favourites only")
            {
                MessageBox.Show("A filter with that name already exists!",
                    "Filter already exists", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            FilterLoader.SaveFilterClass(changingFilter);
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changingFilter.Full = c2y(sFull.CheckState);
            changingFilter.Empty = c2y(sEmpty.CheckState);
            changingFilter.AllowedPing = c2y(sPing.CheckState);
            changingFilter.Favourites = c2y(sFavourite.CheckState);
            changingFilter.Hardcore = c2y(sHardcore.CheckState);
            changingFilter.Mods = c2y(sMods.CheckState);

            changingFilter.name = sName.Text;
            string gtstr = sGametype.Text;
            if (gtstr != "(Any)")
                changingFilter.gametype = HelperFunctions.FullNameToGametype(gtstr);
            gtstr = sMap.Text;
            if (gtstr != "(Any)")
                changingFilter.map = HelperFunctions.FullnameToMap(gtstr);
            changingFilter.mod = sMod.Text;
            changingFilter.buddy = sFriend.Text;

            changingFilter.filterName = filterName.Text;

            if (saveFilter())
                cb(changingFilter);
            this.Close();
        }
    }
}
