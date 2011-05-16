using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
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
    public delegate void BlankCallback();

    #region Helper Function statics
    public static class HelperFunctions
    {
        // *NOT* quick and easy way to retrieve a country for each server.
        public static string getCountryFromAddress(IPAddress ip)
        {
            return new WebClient().DownloadString("http://api.hostip.info/country.php?ip=" + ip.ToString());
        }

        public static IPEndPoint GetMasterAddr()
        {
            IPAddress[] addrList = Dns.GetHostAddresses("server.alteriw.net");
            return new IPEndPoint(addrList[0], 20810); // 20810 is the default port for listings
        }

        public static string removeColourCodes(string s)
        {
            for (int i = 0; i < 9; i++)
                s = s.Replace("^" + i, "");
            return s;
        }

        public static string mapToFullname(string short_mp)
        {
            switch (short_mp)
            {
                case "mp_derail":
                    return "Derail";
                case "mp_highrise":
                    return "Highrise";
                case "mp_invasion":
                    return "Invasion";
                case "mp_afghan":
                    return "Afghan";
                case "mp_checkpoint":
                    return "Karachi";
                case "mp_estate":
                    return "Estate";
                case "mp_complex":
                    return "Bailout";
                case "mp_abandon":
                    return "Carnival";
                case "mp_crash":
                    return "Crash";
                case "mp_favela":
                    return "Favela";
                case "mp_fuel2":
                    return "Fuel";
                case "mp_overgrown":
                    return "Overgrown";
                case "mp_quarry":
                    return "Quarry";
                case "mp_storm":
                    return "Storm";
                case "mp_rust":
                    return "Rust";
                case "mp_compact":
                    return "Salvage";
                case "mp_subbase":
                    return "Sub Base";
                case "mp_nightshift":
                    return "Skidrow";
                case "mp_rundown":
                    return "Rundown";
                case "mp_strike":
                    return "Strike";
                case "mp_terminal":
                    return "Terminal";
                case "mp_trailerpark":
                    return "Trailer Park";
                case "mp_boneyard":
                    return "Scrapyard";
                case "mp_brecourt":
                    return "Wasteland";
                case "mp_underpass":
                    return "Underpass";
                case "mp_vacant":
                    return "Vacant";
                default:
                    return short_mp;
            }
        }

        public static string gameTypeToFullname(string short_gt)
        {
            switch (short_gt)
            {
                case "sd":
                    return "Search & Destroy";
                case "dm":
                    return "Free-For-All";
                case "war":
                    return "Team Deathmatch";
                case "sab":
                    return "Sabotage";
                case "vip":
                    return "VIP";
                case "dom":
                    return "Domination";
                case "koth":
                    return "Headquarters";
                case "arena":
                    return "Arena";
                case "dd":
                    return "Demolition";
                case "ctf":
                    return "Capture the Flag";
                case "oneflag":
                    return "One Flag CTF";
                case "gtnw":
                    return "Global Thermo-Nuclear War";
                case "ss":
                    return "Sharpshooter";
                case "gg":
                    return "Gungame";
                default:
                    return short_gt;
            }
        }

        public static string getGamePath()
        {
            return ((string)Registry.GetValue("HKEY_CLASSES_ROOT\\aiw\\DefaultIcon", "", "")).Split(',')[0];
        }

        public static string FullnameToMap(string gtstr)
        {
            switch (gtstr)
            {
                case "Derail":
                    return "mp_derail";
                case "Highrise":
                    return "mp_highrise";
                case "Invasion":
                    return "mp_invasion";
                case "Afghan":
                    return "mp_afghan";
                case "Karachi":
                    return "mp_checkpoint";
                case "Estate":
                    return "mp_estate";
                case "Bailout":
                    return "mp_complex";
                case "Carnival":
                    return "mp_abandon";
                case "Crash":
                    return "mp_crash";
                case "Favela":
                    return "mp_favela";
                case "Fuel":
                    return "mp_fuel2";
                case "Overgrown":
                    return "mp_overgrown";
                case "Quarry":
                    return "mp_quarry";
                case "Storm":
                    return "mp_storm";
                case "Rust":
                    return "mp_rust";
                case "Salvage":
                    return "mp_compact";
                case "Sub Base":
                    return "mp_subbase";
                case "Skidrow":
                    return "mp_nightshift";
                case "Rundown":
                    return "mp_rundown";
                case "Strike":
                    return "mp_strike";
                case "Terminal":
                    return "mp_terminal";
                case "Trailer Park":
                    return "mp_trailerpark";
                case "Scrapyard":
                    return "mp_boneyard";
                case "Wasteland":
                    return "mp_brecourt";
                case "Underpass":
                    return "mp_underpass";
                case "Vacant":
                    return "mp_vacant";
                default:
                    return gtstr;
            }
        }

        public static string FullNameToGametype(string gtstr)
        {
            switch (gtstr)
            {
                case "Search & Destroy":
                    return "sd";
                case "Free-For-All":
                    return "dm";
                case "Team Deathmatch":
                    return "war";
                case "Sabotage":
                    return "sab";
                case "VIP":
                    return "vip";
                case "Domination":
                    return "dom";
                case "Headquarters":
                    return "koth";
                case "Arena":
                    return "arena";
                case "Demolition":
                    return "dd";
                case "Capture the Flag":
                    return "ctf";
                case "One Flag CTF":
                    return "oneflag";
                case "Global Thermo-Nuclear War":
                    return "gtnw";
                case "Sharpshooter":
                    return "ss";
                case "Gungame":
                    return "gg";
                default:
                    return gtstr;
            }
        }
        public static List<string> getMaps()
        {
            List<string> maps = new List<string>();
            maps.Add("mp_derail");
            maps.Add("mp_highrise");
            maps.Add("mp_invasion");
            maps.Add("mp_afghan");
            maps.Add("mp_checkpoint");
            maps.Add("mp_estate");
            maps.Add("mp_complex");
            maps.Add("mp_abandon");
            maps.Add("mp_crash");
            maps.Add("mp_favela");
            maps.Add("mp_fuel2");
            maps.Add("mp_overgrown");
            maps.Add("mp_quarry");
            maps.Add("mp_storm");
            maps.Add("mp_rust");
            maps.Add("mp_compact");
            maps.Add("mp_subbase");
            maps.Add("mp_nightshift");
            maps.Add("mp_rundown");
            maps.Add("mp_strike");
            maps.Add("mp_terminal");
            maps.Add("mp_trailerpark");
            maps.Add("mp_boneyard");
            maps.Add("mp_brecourt");
            maps.Add("mp_underpass");
            maps.Add("mp_vacant");
            return maps; // that took fucking ages
        }

        public static List<string> getGametypes()
        {
            List<string> gt = new List<string>();
            gt.Add("sd");
            gt.Add("dm");
            gt.Add("war");
            gt.Add("sab");
            gt.Add("vip");
            gt.Add("dom");
            gt.Add("koth");
            gt.Add("arena");
            gt.Add("dd");
            gt.Add("ctf");
            gt.Add("oneflag");
            gt.Add("gtnw");
            gt.Add("ss");
            gt.Add("gg");
            return gt; // this also took fucking ages
        }
    }

    public class FavouritesMngr
    {
        private List<Server> flist;
        public FavouritesMngr()
        {
            flist = _getFavourites();
        }
        private List<Server> _getFavourites()
        {
            List<Server> favList = new List<Server>();
            string[] fLines;
            try
            {
                fLines = File.ReadAllLines("favourites.lsv");
            }
            catch (FileNotFoundException)
            {
                return favList;
            }

            foreach (string line in fLines)
            {
                try
                {
                    string[] addrPair = line.Split(':');
                    favList.Add(
                        new Server(
                        new IPEndPoint(
                            IPAddress.Parse(addrPair[0]), int.Parse(addrPair[1]))));
                }
                catch
                {
                    // some fool messed about with the file and did not insert a favourite correctly.
                    throw new Exception("You fool! You messed around with the favourites. (format: ip:port and newline)");
                }
            }
            return favList;
        }

        public List<Server> getFavourites()
        {
            return flist;
        }

        public void saveFavourites(List<Server> s)
        {
            string[] fLines = new string[s.Count];
            for (int i = 0; i < s.Count; i++)
                fLines[i] = s[i].ServerAddress.ToString();
            File.WriteAllLines("favourites.lsv", fLines);
            flist = _getFavourites();
        }

        public bool isFavourite(Server s)
        {
            return (getFavourites().Contains(s));
        }
    }
#endregion

    public delegate void gameLaunchError(string message);
    public class GameLauncher
    {
        protected Thread _thread;
        private string server;
        private gameLaunchError gle;
        private string gamepath;
        public GameLauncher(string server, gameLaunchError gle)
        {
            this.server = server;
            this.gle = gle;
            this.gamepath = HelperFunctions.getGamePath();
            _thread = new Thread(new ThreadStart(Run));
            _thread.Start();
        }

        protected virtual void Run()
        {
            if (Process.GetProcessesByName("iw4mp.dat").Length == 0)
            {
                try
                {
                    Process.Start(this.gamepath);
                }
                catch (Exception e)
                {
                    gle(e.Message);
                }
                Thread.Sleep(15000);
            }
            try
            {
                Process.Start("aiw://connect/" + server);
            }
            catch (Exception e)
            {
                gle(e.Message);
            }
        }
    }
}
