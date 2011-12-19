using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;

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
    public partial class FriendsList : Form
    {
        private FriendsMngr friends;
        private List<FriendItem> friends_inUI = new List<FriendItem>();

        public FriendsList()
        {
            friends = new FriendsMngr();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FriendEdit(new addFriend(addNewFriend)).ShowDialog();
        }

        private void FriendsList_Load(object sender, EventArgs e)
        {
            //loadFriendsIntoUI();

            Thread t = new Thread(new ThreadStart(checkFriendsThread));
            t.IsBackground = false;
            t.Start();
        }

        private void checkFriendsThread()
        {
            loadFriendsIntoUI();

            while (true)
            {
                foreach (FriendItem f in friends_inUI)
                    f.update();
                Thread.Sleep(10000);
            }
        }

        private void loadFriendsIntoUI()
        {
            if (friendView.InvokeRequired)
            {
                this.Invoke(new Action(loadFriendsIntoUI));
                return;
            }

            friendView.Items.Clear();

            Dictionary<long, string> flist = friends.getFriends();
            foreach (long k in flist.Keys)
            {
                ListViewItem lvi = friendView.Items.Add(k.ToString("x"));
                FriendItem f = new FriendItem(flist[k], k, lvi);
                f.updateItem();
                friends_inUI.Add(f);
                f.update();
            }
        }
        
        private bool addNewFriend(string n, long l)
        {
            Dictionary<long, string> d = friends.getFriends();

            foreach (string b in d.Values)
                if (b.ToLower() == n.ToLower())
                    return false;

            d[l] = n;
            friends.saveFriends(d);

            ListViewItem lvi = friendView.Items.Add(l.ToString("x"));
            FriendItem f = new FriendItem(n, l, lvi);
            friends_inUI.Add(f);
            f.update();

            return true;
        }

        private FriendItem GetFriendItemFromIndex(int idx)
        {
            foreach (FriendItem f in friends_inUI)
                if (f.ListViewItem.Index == idx)
                    return f;
            return null;
        }

        private void joinGameBtn_Click(object sender, EventArgs e)
        {
            if (friendView.SelectedItems.Count < 0)
                return;

            IPEndPoint ep = GetFriendItemFromIndex(friendView.SelectedItems[0].Index).Address;
            if (ep == null)
                return;

            new GameStartingUI(new Server(ep), new Action(delegate() { })).startConnecting();
        }

        private void delFriendbtn_Click(object sender, EventArgs e)
        {
            if (friendView.SelectedItems.Count < 0)
                return;

            Dictionary<long, string> d = friends.getFriends();
            ListViewItem lvi = friendView.SelectedItems[0];
            long k = long.Parse(lvi.Text, System.Globalization.NumberStyles.AllowHexSpecifier);
            d.Remove(k);
            friends.saveFriends(d);

            lvi.Remove();
        }

        private void friendView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.joinGameBtn.Enabled = 
            this.checkFriendStatsbtn.Enabled = 
            this.delFriendbtn.Enabled = 
                friendView.SelectedItems.Count > 0;
        }

        private void checkFriendStatsbtn_Click(object sender, EventArgs e)
        {
            FriendItem f = GetFriendItemFromIndex(friendView.SelectedItems[0].Index);
            StatsReader sr = f.GetStatistics();
            if (sr == null)
            {
                MessageBox.Show("Could not download statistics for this player!", 
                    "Retrieval error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new FriendStats(f.Name, sr).ShowDialog();
        }
    }

    public class FriendItem
    {
        private ListViewItem listViewItem;
        private string givenName;
        private long guid;
        private string status;
        private IPEndPoint current_server;

        public ListViewItem ListViewItem
        {
            get
            {
                return listViewItem;
            }
        }

        public string Name
        {
            get
            {
                return givenName;
            }
        }

        public FriendItem(string name, long guid, ListViewItem lvi)
        {
            this.givenName = name;
            this.guid = guid;
            this.listViewItem = lvi;
        }

        public void updateItem()
        {
            if (listViewItem.ListView == null)
                return;

            if (listViewItem.ListView.InvokeRequired)
            {
                listViewItem.ListView.Invoke(new Action(delegate()
                {
                    this.updateItem();
                }));
                return;
            }
            listViewItem.SubItems.Clear();
            listViewItem.Text = guid.ToString("x");
            listViewItem.SubItems.Add(givenName);
            listViewItem.SubItems.Add(status);
        }

        public void update()
        {
            if (listViewItem.ListView == null)
                return;

            try
            {
                WebClient wc = new WebClient();
                wc.Proxy = new WebProxy();
                string Gstatus = wc.DownloadString("http://server.alteriw.net:13000/cleanExt/" + guid.ToString());
                string serverIP = wc.DownloadString("http://server.alteriw.net:13000/server/" + guid.ToString());
                if (!Gstatus.StartsWith("valid")) // Oh you!
                {
                    this.status = "Offline";
                }
                else if (serverIP != "no server")
                {
                    string[] bits = serverIP.Split(':');
                    if (bits.Length < 1 || bits.Length > 2)
                        throw new InvalidDataException();

                    IPAddress address;
                    if (!IPAddress.TryParse(bits[0], out address))
                        throw new InvalidDataException();

                    int port = 28960;
                    if (bits.Length > 1)
                        if (!int.TryParse(bits[1], out port))
                            throw new InvalidDataException();

                    current_server = new IPEndPoint(address, port);
                    status = "Playing in: " + current_server.ToString();
                }
                else if (serverIP == "no server")
                {
                    status = "Online, not playing.";
                }
                else
                {
                    status = serverIP;
                }
            }
            catch (WebException)
            {
                status = "Offline";
            }
            catch (SocketException)
            {
                status = "Offline";
            }
            catch (InvalidDataException)
            {
                status = "Illegible data from server";
                return;
            }
            catch (Exception)
            {
                status = "Generic error";
            }

            this.updateItem();
        }

        public IPEndPoint Address
        {
            get
            {
                return current_server;
            }
        }

        public StatsReader GetStatistics()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Proxy = new WebProxy();
                MemoryStream ms = new MemoryStream(wc.DownloadData("http://server.alteriw.net:13000/pstats/" + guid.ToString()));
                return new StatsReader(ms, true);
            }
            catch { return null; }
        }
    }

    public class FriendsMngr
    {
        private Dictionary<long, string> flist;
        private Dictionary<long, IPEndPoint> flist_servers;

        public FriendsMngr()
        {
            flist = _getFriends();
            flist_servers = new Dictionary<long, IPEndPoint>();
        }
        private Dictionary<long, string> _getFriends()
        {
            Dictionary<long, string> frList = new Dictionary<long, string>();
            string[] fLines;
            try
            {
                fLines = File.ReadAllLines("friends.lsv");
            }
            catch (FileNotFoundException)
            {
                return frList;
            }

            foreach (string line in fLines)
            {
                try
                {
                    string[] derp = line.Split('\t');
                    frList.Add(long.Parse(derp[1]), derp[0]); // one line should consist of a decimal xuid.
                }
                catch
                {
                    // some fool messed about with the file.
                    MessageBox.Show("friends.lsv is illegible! (file format is: guid (dec format) delimited by \\n)");
                    return frList;
                }
            }
            return frList;
        }

        public Dictionary<long, string> getFriends()
        {
            return flist;
        }

        public bool friendInServer(IPEndPoint ep)
        {
            return (flist_servers.ContainsValue(ep));
        }

        public void updateServers()
        {
            foreach (long guid in flist.Keys)
            {
                try
                {
                    WebClient wc = new WebClient();
                    wc.Proxy = new WebProxy();
                    string serverIP = wc.DownloadString("http://server.alteriw.net:13000/server/" + guid.ToString());

                    if (serverIP != "no server")
                    {
                        string[] bits = serverIP.Split(':');
                        if (bits.Length < 1 || bits.Length > 2)
                            throw new InvalidDataException();

                        IPAddress address;
                        if (!IPAddress.TryParse(bits[0], out address))
                            throw new InvalidDataException();

                        int port = 28960;
                        if (bits.Length > 1)
                            if (!int.TryParse(bits[1], out port))
                                throw new InvalidDataException();

                        flist_servers[guid] = new IPEndPoint(address, port);
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        public void saveFriends(Dictionary<long, string> s)
        {
            int i = -1;
            string[] fLines = new string[s.Count];
            foreach (long key in s.Keys)
                fLines[i += 1] = string.Format("{0}\t{1}", s[key], key);
            File.WriteAllLines("friends.lsv", fLines);
            flist = _getFriends();
        }

        public bool isFriend(long s)
        {
            return getFriends().ContainsKey(s);
        }

        public IEnumerable<IPEndPoint> getFriendServers()
        {
            updateServers();
            return flist_servers.Values;
        }
    }
}
