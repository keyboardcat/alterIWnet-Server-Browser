using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

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
    // Lulzy name.
    public class firstWordIntItemComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public firstWordIntItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        public firstWordIntItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public int Compare(object x, object y) 
        {
            int returnVal = -1;
            string[] s1, s2;
            try
            {
                s1 = ((ListViewItem)x).SubItems[col].Text.Split(' ');
                s2 = ((ListViewItem)y).SubItems[col].Text.Split(' ');
            }
            catch { return -1; }
            //if (s1[0] == "--" || s2[0] == "--")
            //    return 1;
            int val;
            if (!int.TryParse(s1[0], out val))
                return -1;
            int val2;
            if (!int.TryParse(s2[0], out val2))
                return -1;
            returnVal = val - val2;
            
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }
    }

    public class StringItemComparer : IComparer
    {
        private int col;
        private SortOrder order;
        public StringItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        public StringItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;
            try
            {
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text.ToLower(),
                                    ((ListViewItem)y).SubItems[col].Text.ToLower());
            }
            catch { }
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }
    }
}
