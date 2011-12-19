using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    public partial class FriendStats : Form
    {
        public FriendStats(string name, StatsReader r)
        {
            InitializeComponent();

            this.nameval.Text = "\"" + name + "\"";
            //this.plyrlvl.Text = r.
            this.assistsval.Text = r.assists.ToString();
            this.deathsval.Text = r.deaths.ToString();
            this.headshotsval.Text = r.headshots.ToString();
            this.hitsval.Text = r.hits.ToString();
            this.killsval.Text = r.kills.ToString();
            this.lasttplayedval.Text = new DateTime(1970, 1, 1).AddSeconds(r.last_play).ToString();
            this.lossesval.Text = r.losses.ToString();
            this.missesval.Text = r.misses.ToString();
            this.prestigelvl.Text = r.prestige.ToString();
            this.scoreval.Text = r.score.ToString();
            this.streakval.Text = r.streak.ToString();
            this.suicidesval.Text = r.suicides.ToString();
            this.tiesval.Text = r.ties.ToString();
            this.timepval.Text = TimeSpan.FromSeconds(r.time).ToString();
            this.winsval.Text = r.wins.ToString();
            this.xpval.Text = r.xp.ToString();
            this.kdrval.Text = Math.Round(((double)r.kills / (double)(r.deaths == 0 ? 1 : r.deaths)), 2).ToString();
            this.wlrval.Text = Math.Round((double)r.wins / (double)(r.losses == 0 ? 1 : r.losses), 2).ToString();
            this.accuracyval.Text = (Math.Round(((double)r.hits / (double)(r.total_shots == 0 ? 1 : r.total_shots)), 2) * 100).ToString() + "%";
        }
    }
}
