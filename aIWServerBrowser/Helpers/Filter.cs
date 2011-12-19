using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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
    public static class FilterLoader
    {
        public static Filter LoadFilterClass(string filtername)
        {
            if (!Directory.Exists("filters/"))
                Directory.CreateDirectory("filters");
            FileStream stream = File.Open("filters/" + filtername.ToLower() + ".osl", FileMode.Open, FileAccess.Read);
            BinaryFormatter bformatter = new BinaryFormatter();
            Filter sclass = (Filter)bformatter.Deserialize(stream);
            stream.Close();

            return sclass; // return deserialized filter
        }

        public static void SaveFilterClass(Filter f)
        {
            if (!Directory.Exists("filters/"))
                Directory.CreateDirectory("filters");
            FileStream stream = File.Open("filters/" + f.filterName.ToLower() + ".osl", FileMode.Create, FileAccess.Write);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, f);
            stream.Close();
        }
    }

    [Serializable()]
    public class Filter
    {
        public enum YNA { Yes, No, All };
        private YNA full;
        private YNA empty;
        private YNA hardcore;
        private YNA secure;
        private YNA pingLowerThanMax;
        private YNA favourites;
        private YNA mods;
        private YNA friends;

        public string map;
        public string gametype;
        public string mod;
        public string name;
        public string buddy;

        public string filterName;

        public Filter() { }

        public Filter(SerializationInfo info, StreamingContext ctxt)
        {
            full = (YNA)info.GetValue("full", typeof(YNA));
            empty = (YNA)info.GetValue("empty", typeof(YNA));
            hardcore = (YNA)info.GetValue("hardcore", typeof(YNA));
            secure = (YNA)info.GetValue("secure", typeof(YNA));
            pingLowerThanMax = (YNA)info.GetValue("pingLowerThanMax", typeof(YNA));
            favourites = (YNA)info.GetValue("favourites", typeof(YNA));
            mods = (YNA)info.GetValue("mods", typeof(YNA));
            friends = (YNA)info.GetValue("friends", typeof(YNA));

            map = info.GetString("map");
            gametype = info.GetString("gametype");
            mod = info.GetString("mod");
            name = info.GetString("name");
            buddy = info.GetString("buddy");

            filterName = info.GetString("filterName");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("full", full);
            info.AddValue("empty", empty);
            info.AddValue("hardcore", hardcore);
            info.AddValue("secure", secure);
            info.AddValue("pingLowerThanMax", pingLowerThanMax);
            info.AddValue("favourites", favourites);
            info.AddValue("mods", mods);
            info.AddValue("friends", friends);
            info.AddValue("map", map);
            info.AddValue("gametype", gametype);
            info.AddValue("mod", mod);
            info.AddValue("name", name);
            info.AddValue("buddy", buddy);
            info.AddValue("filterName", filterName);
        }

        public static Filter getDefaultFilter()
        {
            Filter f = new Filter();
            f.AllowedPing = YNA.All;
            f.Empty = YNA.All;
            f.Full = YNA.All;
            f.Favourites = YNA.All;
            f.Hardcore = YNA.All;
            f.Mods = YNA.All;
            f.Friends = YNA.All;

            f.map = "";
            f.gametype = "";
            f.mod = "";
            f.name = "";
            f.buddy = "";
            f.filterName = "All servers";
            return f;
        }

        public bool serverMatchesFilter(Server s, FavouritesMngr favMngr, FriendsMngr frndMngr)
        {
            bool b = frndMngr.friendInServer(s.ServerAddress);
            if (!(friends == YNA.All || // let it through if the filter == All
                (b && friends == YNA.Yes) || // let it through if the server contains a friend and set to Yes 
                (!b && friends == YNA.No))) // let it through if the server contains a friend and set to No
                return false;

            if (!(full == YNA.All || // let it through if the filter == All
                (s.serverNPlayers == s.serverMaxPlayers && full == YNA.Yes) || // let it through if it's full and set to Yes 
                (s.serverNPlayers != s.serverMaxPlayers && full == YNA.No))) // let it through if it isn't full and set to No
                return false;
            if (!(empty == YNA.All || // let it through if the filter == All
                (s.serverNPlayers == 0 && empty == YNA.Yes) || // let it through if it's empty and set to Yes 
                (s.serverNPlayers != 0 && empty == YNA.No))) // let it through if it isn't empty and set to No
                return false;
            if (!(hardcore == YNA.All || // let it through if the filter == All
                (s.serverDvars["g_hardcore"].Equals("1") && hardcore == YNA.Yes) || // let it through if it's hardcore and set to Yes 
                (s.serverDvars["g_hardcore"].Equals("0") && hardcore == YNA.No))) // let it through if it isn't hardcore and set to No
                return false;
            try
            {
                if (!(secure == YNA.All || // let it through if the filter == All
                    (float.Parse(s.serverDvars["shortversion"]) >= 0.3 && secure == YNA.Yes) || // let it through if it's secure and set to Yes 
                    (float.Parse(s.serverDvars["shortversion"]) < 0.3 && secure == YNA.No))) // let it through if it isn't secure and set to No
                    return false;
            }
            catch { if (!s.serverDvars.ContainsKey("shortversion") && secure != YNA.All) { return false; } } // shortversion was invalid! :(
            try
            {
                if (!(pingLowerThanMax == YNA.All || // let it through if the filter == All
                    (int.Parse(s.serverDvars["sv_maxPing"]) > s.serverPing && pingLowerThanMax == YNA.Yes) || // let it through if it's low enough and set to Yes 
                    (int.Parse(s.serverDvars["sv_maxPing"]) <= s.serverPing && pingLowerThanMax == YNA.No))) // let it through if it isn't low enough and set to No
                    return false;
            }
            catch { return false; } // sv_maxPing was invalid! :(
            if (!(favourites == YNA.All || // let it through if the filter == All
                (favMngr.isFavourite(s) && favourites == YNA.Yes) || // let it through if it's a favourite and set to Yes 
                (!favMngr.isFavourite(s) && favourites == YNA.No))) // let it through if it isn't a favourite and set to No
                return false;
            if (!(mods == YNA.All || // let it through if the filter == All
                (s.serverDvars.ContainsKey("fs_game") && mods == YNA.Yes) || // let it through if it's got a mod and set to Yes 
                (!s.serverDvars.ContainsKey("fs_game") && mods == YNA.No))) // let it through if it isn't modded and set to No
                return false;

            if (!(string.IsNullOrEmpty(name) || // let it through if the search == null
                (s.serverName.Contains(name) && !string.IsNullOrEmpty(name)))) // let it through if it contains a portion of the search and isn't null
                return false;
            if (!(string.IsNullOrEmpty(map) || // let it through if the search == null
                (s.serverMap.Contains(map) && !string.IsNullOrEmpty(map)))) // let it through if it contains a portion of the search and isn't null
                return false;
            if (!(string.IsNullOrEmpty(gametype) || // let it through if the search == null
                (s.serverGametype.Contains(gametype) && !string.IsNullOrEmpty(gametype)))) // let it through if it contains a portion of the search and isn't null
                return false;

            /*if (!string.IsNullOrEmpty(buddy)) // let it through if it contains a portion of the search and isn't null
            {
                bool found = false; // we have to do it like this because there is no way to skip the return statement after the loop
                foreach (Player p in s.serverPlayerList)
                {
                    if (p.name.ToLower().Equals(buddy.ToLower()))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return false;
            }*/

            if (!(string.IsNullOrEmpty(mod) || // let it through if the search == null
                (s.serverMod.Contains(mod) && !string.IsNullOrEmpty(mod)))) // let it through if it contains a portion of the search and isn't null
                return false;
            return true;
        }

        public YNA Full
        {
            get
            {
                return full;
            }
            set
            {
                full = value;
            }
        }
        public YNA Empty
        {
            get
            {
                return empty;
            }
            set
            {
                empty = value;
            }
        }
        public YNA Hardcore
        {
            get
            {
                return hardcore;
            }
            set
            {
                hardcore = value;
            }
        }
        public YNA AllowedPing
        {
            get
            {
                return pingLowerThanMax;
            }
            set
            {
                pingLowerThanMax = value;
            }
        }
        public YNA Favourites
        {
            get
            {
                return favourites;
            }
            set
            {
                favourites = value;
            }
        }
        public YNA Mods
        {
            get
            {
                return mods;
            }
            set
            {
                mods = value;
            }
        }

        public YNA Friends
        {
            get
            {
                return friends;
            }
            set
            {
                friends = value;
            }
        }
    }
}
