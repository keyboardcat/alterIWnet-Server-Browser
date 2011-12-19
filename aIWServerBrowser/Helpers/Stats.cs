using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
    /* --
     * le python
    
    while 1:
	    h = eval('"\\x' + raw_input("hex: ").replace(' ', '\\x') + '"')
	    print struct.unpack("i", h)
    */

    public class StatsReader
    {
        private readonly int start_address = 0x00000800;
        private Stream stat_file;
        private bool close_after_read = false;

        #region Stat values
        public int prestige { get; set; }
        public int score { get; set; }
        public int xp { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public int ties { get; set; }
        public int kills { get; set; }
        public int headshots { get; set; }
        public int assists { get; set; }
        public int streak { get; set; }
        public int deaths { get; set; }
        public int time { get; set; }
        public int total_shots { get; set; }
        public int last_play { get; set; }
        public int suicides { get; set; }
        public int time_played_allies { get; set; }
        public int time_played_opfor { get; set; }
        public int time_played_other { get; set; }
        public int win_streak { get; set; }
        public int hits { get; set; }
        public int misses { get; set; } 
        #endregion

        public StatsReader(Stream stat, bool closeAfterRead)
        {
            this.stat_file = stat;
            this.close_after_read = closeAfterRead;
            read();
        }

        public StatsReader(Stream stat) :
            this(stat, false) { }

        private void read()
        {
            byte[] mem = new byte[4];
            long pos = stat_file.Seek(start_address + 4, SeekOrigin.Begin);

            // wins
            stat_file.Read(mem, 0, 4);
            this.wins = BitConverter.ToInt32(mem, 0);

            // ??
            stat_file.Seek(4, SeekOrigin.Current);

            // xp
            stat_file.Read(mem, 0, 4);
            this.xp = BitConverter.ToInt32(mem, 0);

            // ??
            stat_file.Seek(4, SeekOrigin.Current);

            // prestige?
            stat_file.Read(mem, 0, 4);
            this.prestige = BitConverter.ToInt32(mem, 0);

            // some shit
            stat_file.Seek(4, SeekOrigin.Current);

            // sc0re
            stat_file.Read(mem, 0, 4);
            this.score = BitConverter.ToInt32(mem, 0);

            // kills
            stat_file.Read(mem, 0, 4);
            this.kills = BitConverter.ToInt32(mem, 0);

            // streak
            stat_file.Read(mem, 0, 4);
            this.streak = BitConverter.ToInt32(mem, 0);

            // deaths
            stat_file.Read(mem, 0, 4);
            this.deaths = BitConverter.ToInt32(mem, 0);

            // ?
            stat_file.Seek(4, SeekOrigin.Current);

            // assists
            stat_file.Read(mem, 0, 4);
            this.assists = BitConverter.ToInt32(mem, 0);

            // headshots
            stat_file.Read(mem, 0, 4);
            this.headshots = BitConverter.ToInt32(mem, 0);

            // ?
            stat_file.Seek(4, SeekOrigin.Current);

            // suicides
            stat_file.Read(mem, 0, 4);
            this.suicides = BitConverter.ToInt32(mem, 0);

            // time played on allied teams
            stat_file.Read(mem, 0, 4);
            this.time_played_allies = BitConverter.ToInt32(mem, 0);

            // time played on opfor
            stat_file.Read(mem, 0, 4);
            this.time_played_opfor = BitConverter.ToInt32(mem, 0);

            // time played on other teams
            stat_file.Read(mem, 0, 4);
            this.time_played_other = BitConverter.ToInt32(mem, 0);

            // time played total
            stat_file.Read(mem, 0, 4);
            this.time = BitConverter.ToInt32(mem, 0);

            // k/d ratio, which isn't needed
            stat_file.Seek(4, SeekOrigin.Current);

            // wins
            stat_file.Read(mem, 0, 4);
            this.wins = BitConverter.ToInt32(mem, 0);

            // losses
            stat_file.Read(mem, 0, 4);
            this.losses = BitConverter.ToInt32(mem, 0);

            // ties
            stat_file.Read(mem, 0, 4);
            this.ties = BitConverter.ToInt32(mem, 0);

            // win streak
            stat_file.Read(mem, 0, 4);
            this.win_streak = BitConverter.ToInt32(mem, 0);

            // ??
            stat_file.Seek(4, SeekOrigin.Current);

            // w/l ratio, which isn't needed
            stat_file.Seek(4, SeekOrigin.Current);

            // hits
            stat_file.Read(mem, 0, 4);
            this.hits = BitConverter.ToInt32(mem, 0);

            // misses
            stat_file.Read(mem, 0, 4);
            this.misses = BitConverter.ToInt32(mem, 0);
            
            // total shots
            stat_file.Read(mem, 0, 4);
            this.total_shots = BitConverter.ToInt32(mem, 0);

            // accuracy, which is pretty un-needed
            stat_file.Seek(4, SeekOrigin.Current);

            // ??
            stat_file.Seek(4, SeekOrigin.Current);

            // ??
            stat_file.Seek(4, SeekOrigin.Current);

            // last play time
            stat_file.Read(mem, 0, 4);
            this.last_play = BitConverter.ToInt32(mem, 0);

            if (close_after_read)
                stat_file.Close();
        }
    }
}
