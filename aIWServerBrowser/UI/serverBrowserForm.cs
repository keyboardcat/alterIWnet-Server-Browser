using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Diagnostics;
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
    public delegate void JoiningCallback(bool b);
    public partial class serverBrowserForm : Form
    {
        private List<Server> serverList;
        private Filter currentFilter;
        private bool taskInProgress = false;
        private ServerQueryHandler qhandler;
        private FiltersDlg filtersDlg;
        private FavouritesMngr favMngr;
        private FriendsMngr friendMngr;
        public bool connectingToServer = false;

        public serverBrowserForm()
        {
            InitializeComponent();
            currentFilter = Filter.getDefaultFilter();
            serverList = new List<Server>();
            infoDlg = new InfoDlg(new doUpdateServer(refreshSingleServer), new JoiningCallback(onJoiningGame));
            filtersDlg = new FiltersDlg(new onFilterChanged(filterChanged));
            favMngr = new FavouritesMngr();
            friendMngr = new FriendsMngr();
        }

        #region Query functions
        private void doNewMasterQuery()
        {
            if (taskInProgress)
                return;
            clearUp();
            taskInProgress = true;
            refreshListButton.Enabled = false;
            filterButton.Enabled = false;
            queryIPAddr.Enabled = false;
            stopButton.Enabled = true;
            new MasterQueryHandler(HelperFunctions.GetMasterAddr(), new MasterQueryCompleted(populateListWithServers));
        }

        private void doNewQuery(List<Server> servers)
        {
            if (taskInProgress)
                return;

            friendMngr.updateServers();

            taskInProgress = true;
            refreshListButton.Enabled = false;
            filterButton.Enabled = false;
            queryIPAddr.Enabled = false;
            stopButton.Enabled = true;
            qhandler = new ServerQueryHandler(servers, 
                new QueryProgress(onProgressUpdate),
                new Action(onServerQueryBatchCompleted));
        }

        private void refreshSingleServer(Server e)
        {
            if (taskInProgress)
                return;
            List<Server> server = new List<Server>();
            server.Add(e);

            addToServerList(e);
            doNewQuery(server);
        }
        #endregion

        #region UI - Functions

        private void checkForUpdate()
        {
            /*Version ver = new Version(Application.ProductVersion);
            try
            {
                WebClient wc = new WebClient();
                string[] dl = wc.DownloadString("http://kc.betaforce.com/serverlist/version.txt").Split('|');
                Version latestver = new Version(dl[0]);
                if (ver < latestver)
                {
                    askUserToUpdate(dl[1], latestver);
                }
            }
            catch
            {
                return;
            }*/
        }

        delegate void askUserTUCallback(string link, Version latestver);
        private void askUserToUpdate(string link, Version latestver)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new askUserTUCallback(askUserToUpdate), new object[] { link, latestver });
                return;
            }
            DialogResult updateChoice = MessageBox.Show(this, "A new update to version " + latestver.ToString() +
                    " is available!\nWould you like to go to the topic to get it?", "Update!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (updateChoice == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Process.Start(link);
                }
                catch
                {
                    DialogResult dr = MessageBox.Show("Could not open the link! Would you like it on your clipboard?",
                        "Failed!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                        Clipboard.SetText(link, TextDataFormat.Text);
                }
            }
        }

        delegate void addToServerListCallback(Server s);
        private void addToServerList(Server server)
        {
            if (serverList.Contains(server))
                if (server.rowItem != null)
                    if (serverBrowserView.Items.Contains(server.rowItem))
                        return;

            if (serverBrowserView.InvokeRequired)
            {
                addToServerListCallback s = new addToServerListCallback(addToServerList);
                this.Invoke(s, new object[] { server });
                return;
            }

            serverList.Add(server);
            server.ServerUpdated += new ServerUpdated(onServerUpdated);

            ListViewItem lvi = serverBrowserView.Items.Add(server.ServerAddress.ToString());
            if (server.queryStatus != Server.ServerQueryStatus.NothingYet &&
                server.queryStatus != Server.ServerQueryStatus.Failed &&
                server.queryStatus != Server.ServerQueryStatus.TimedOut)
            {
                lvi.SubItems.Add(HelperFunctions.removeColourCodes(server.serverName));
                lvi.SubItems.Add(favMngr.isFavourite(server) ? "X" : "-");
                lvi.SubItems.Add(string.Format("{0} ({1})", server.serverPing.ToString(), server.serverDvars["sv_maxPing"]));
                lvi.SubItems.Add(string.Format("{0} / {1}", server.serverNPlayers, server.serverMaxPlayers));
                lvi.SubItems.Add(HelperFunctions.gameTypeToFullname(server.serverGametype));
                lvi.SubItems.Add(HelperFunctions.mapToFullname(server.serverMap));
                lvi.SubItems.Add(server.serverMod);
                lvi.SubItems.Add(server.serverDvars["g_hardcore"].Equals("1") ? "X" : "-");
            }
            else // not queried yet, so we just add it as a blank server :<
            {
                lvi.SubItems.Add("--");
                lvi.SubItems.Add(favMngr.isFavourite(server) ? "X" : "-");
                lvi.SubItems.Add("--");
                lvi.SubItems.Add("--");
                lvi.SubItems.Add("--");
                lvi.SubItems.Add("--");
                lvi.SubItems.Add("--");
                lvi.SubItems.Add("--");
            }

            server.rowItem = lvi;
        }


        delegate void progressDescCallback(string desc);
        private void setProgressDesc(string desc)
        {
            if (taskDescription.InvokeRequired)
            {
                progressDescCallback oqpc = new progressDescCallback(setProgressDesc);
                this.Invoke(oqpc, new object[] { desc });
            }
            else
            {
                taskDescription.Text = desc;
            }
        }

        
        private List<Server> getSelectedServers()
        {
            ListView.SelectedListViewItemCollection lvis = serverBrowserView.SelectedItems;
            List<Server> serversSelected = new List<Server>();
            for (int i = 0; i < serverList.Count; i++)
                if (lvis.IndexOf(serverList[i].rowItem) >= 0)
                    serversSelected.Add(serverList[i]);
            return serversSelected;
        }

        private void launchGameOnAddress(Server s)
        {
            new GameStartingUI(s, new Action(serverFinishedLaunch)).startConnecting();
        }

        private void ShowErrorDlg(string msg)
        {
            MessageBox.Show(msg, "WTFError", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void clearUp()
        {
            this.serverBrowserView.Items.Clear();
            this.serverList.Clear();
            qhandler = null;
            GC.Collect();
        }
        #endregion

        #region UI - Backend Callbacks

        private void onJoiningGame(bool doingit)
        {
            connectingToServer = doingit;
            joinGameButton.Enabled = !doingit;

            if (doingit)
            {
                if (infoDlg != null)
                    infoDlg.Hide();
                WindowState = FormWindowState.Minimized;
            }
        }


        private void onServerQueryBatchCompleted()
        {
            if (stopButton.InvokeRequired)
            {
                this.Invoke(new Action(onServerQueryBatchCompleted));
                return;
            }
            taskInProgress = false;
            stopButton.Enabled = false;
            refreshListButton.Enabled = true;
            filterButton.Enabled = true;
            queryIPAddr.Enabled = true;

            if ((serverList.Count - serverBrowserView.Items.Count) != 0)
                setProgressDesc(serverBrowserView.Items.Count + " servers found (" + (serverList.Count - serverBrowserView.Items.Count) + " failed or filtered)...");
            else
                setProgressDesc(serverBrowserView.Items.Count + " servers found...");
        }


        delegate void populateCallback(MasterQueryResult mqr);
        private void populateListWithServers(MasterQueryResult mqr)
        {
            if (taskProgress.InvokeRequired)
            {
                populateCallback p = new populateCallback(populateListWithServers);
                this.Invoke(p, new object[] { mqr });
                return;
            }
            if (mqr.Code == MasterQueryResult.StatusCodes.CriticalStop)
            {
                taskInProgress = false;
                MessageBox.Show(mqr.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                foreach (Server serv in mqr.ServerList)
                    addToServerList(serv);
                setProgressDesc("Querying " + serverList.Count + " servers...");

                // Create the serverqueryhandler directly as we're already in a task - doQuery is there to create the task, not to continue it.
                qhandler = new ServerQueryHandler(serverList, new QueryProgress(onProgressUpdate), new Action(onServerQueryBatchCompleted));
            }
        }


        delegate void onProgressUpdate_Invoke(int done, int total);
        private void onProgressUpdate(int done, int total)
        {
            if (!taskInProgress)
                return;
            if (taskProgress.InvokeRequired)
            {
                this.Invoke(new onProgressUpdate_Invoke(onProgressUpdate), new object[] { done, total });
                return;
            }

            // fairly horrible but works
            taskProgress.Value = (int)(((float)done / (float)total) * 100.0);
            taskDescription.Text = (total - done) + " servers left out of " + total + " (" + (total - serverBrowserView.Items.Count) + " failed or filtered)...";
        }


        private void filterChanged(Filter f, string name)
        {
            filterName.Text = name;
            currentFilter = f;
        }

        delegate void onServerUpdatedCallback(Server server);
        private void onServerUpdated(Server server)
        {
            if (!taskInProgress)
                return;
            if (serverBrowserView.InvokeRequired)
            {
                onServerUpdatedCallback c = new onServerUpdatedCallback(onServerUpdated);
                this.Invoke(c, new object[] { server });
                return;
            }

            if (server.queryStatus == Server.ServerQueryStatus.Failed)
            {
                if (currentFilter.Favourites != Filter.YNA.Yes)
                    serverBrowserView.Items.Remove(server.rowItem);
                else
                    server.rowItem.BackColor = Color.OrangeRed;
                return;
            }
            else if (server.queryStatus == Server.ServerQueryStatus.TimedOut)
            {
                if (currentFilter.Favourites != Filter.YNA.Yes)
                    serverBrowserView.Items.Remove(server.rowItem);
                else
                    server.rowItem.BackColor = Color.Gray;
                return;
            }
            else if (server.queryStatus == Server.ServerQueryStatus.Successful &&
                !currentFilter.serverMatchesFilter(server, favMngr, friendMngr))
            {
                serverBrowserView.Items.Remove(server.rowItem);
            }
            else if (server.queryStatus == Server.ServerQueryStatus.Successful)
            {
                // update that individual list item
                if (server.rowItem.SubItems.Count != 9)
                {
                    server.rowItem.SubItems.Clear();
                    server.rowItem.SubItems.Add(HelperFunctions.removeColourCodes(server.serverName));
                    server.rowItem.SubItems.Add(favMngr.isFavourite(server) ? "X" : "-");
                    server.rowItem.SubItems.Add(string.Format("{0} ({1})", server.serverPing.ToString(), server.serverDvars["sv_maxPing"]));
                    server.rowItem.SubItems.Add(string.Format("{0} / {1}", server.serverNPlayers, server.serverMaxPlayers));
                    server.rowItem.SubItems.Add(HelperFunctions.gameTypeToFullname(server.serverGametype));
                    server.rowItem.SubItems.Add(HelperFunctions.mapToFullname(server.serverMap));
                    server.rowItem.SubItems.Add(server.serverMod);
                    server.rowItem.SubItems.Add(server.serverDvars["g_hardcore"].Equals("1") ? "X" : "-");
                }
                else
                {
                    server.rowItem.SubItems[1].Text = HelperFunctions.removeColourCodes(server.serverName);
                    server.rowItem.SubItems[2].Text = favMngr.isFavourite(server) ? "X" : "-";
                    server.rowItem.SubItems[3].Text = string.Format("{0} ({1})", server.serverPing.ToString(), server.serverDvars["sv_maxPing"]);
                    server.rowItem.SubItems[4].Text = string.Format("{0} / {1}", server.serverNPlayers, server.serverMaxPlayers);
                    server.rowItem.SubItems[5].Text = HelperFunctions.gameTypeToFullname(server.serverGametype);
                    server.rowItem.SubItems[6].Text = HelperFunctions.mapToFullname(server.serverMap);
                    server.rowItem.SubItems[7].Text = server.serverMod;
                    if (server.serverDvars.ContainsKey("g_hardcore"))
                        server.rowItem.SubItems[8].Text = (server.serverDvars["g_hardcore"].Equals("1") ? "X" : "-");
                    else
                        server.rowItem.SubItems[8].Text = "";
                }
            }
            if (infoDlg.currentServer == server)
                infoDlg.updateDlg(server); // if the info dlg exists, update it with the correct info

            serverBrowserView.Sort();
        }


        #endregion

        #region UI - Events
        
        private void serverFinishedLaunch()
        {
            joinGameButton.Enabled = true;
            onJoiningGame(false);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            new AddServerUI(new addServerCallback(refreshSingleServer)).ShowDialog();
        }

        private void serverBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void serverBrowserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void serverBrowserView_ColumnClick(object sender,
                           System.Windows.Forms.ColumnClickEventArgs e)
        {
            if (/*taskInProgress || */serverBrowserView.Items.Count == 0)
                return;

            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                sortColumn = e.Column;
                serverBrowserView.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (serverBrowserView.Sorting == SortOrder.Ascending)
                    serverBrowserView.Sorting = SortOrder.Descending;
                else
                    serverBrowserView.Sorting = SortOrder.Ascending;
            }
            serverBrowserView.Sort();

            // firstWordIntItemComparer - Long name, but takes the first word of the column and converts it to an int- then it sorts.
            // ugly as fuck, but.. it works?
            if (sortColumn == 3 || sortColumn == 4) // ping row and player count.
                this.serverBrowserView.ListViewItemSorter = new firstWordIntItemComparer(e.Column,
                                                              serverBrowserView.Sorting);
            else
                this.serverBrowserView.ListViewItemSorter = new StringItemComparer(e.Column,
                                                              serverBrowserView.Sorting);
        }
        
        private void joinGameButton_Click(object sender, EventArgs e)
        {
            if (connectingToServer)
                return;
            List<Server> serversSelected = getSelectedServers();
            if (serversSelected.Count > 0)
            {
                onJoiningGame(true);
                launchGameOnAddress(serversSelected[0]);
            }
        }

        private InfoDlg infoDlg = null;
        private void serverInfoButton_Click(object sender, EventArgs e)
        {
            if (infoDlg == null)
                infoDlg = new InfoDlg(new doUpdateServer(refreshSingleServer), new JoiningCallback(onJoiningGame));
            infoDlg.Show();
        }

        private void refreshServerButton_Click(object sender, EventArgs e)
        {
            if (taskInProgress)
                return;
            taskDescription.Text = "Refreshing server...";
            doNewQuery(getSelectedServers());
        }

        private void quickRefreshButton_Click(object sender, EventArgs e)
        {
            if (serverList.Count > 0)
                doNewQuery(serverList);
        }

        private void refreshListButton_Click(object sender, EventArgs e)
        {
            if (taskInProgress)
                return;
            clearUp();

            if (currentFilter.Favourites != Filter.YNA.Yes)
            {
                if (currentFilter.Friends == Filter.YNA.Yes)
                {
                    taskDescription.Text = "Querying friends' servers...";
                    foreach (IPEndPoint ep in friendMngr.getFriendServers())
                        addToServerList(new Server(ep));
                    if (serverList.Count == 0)
                        taskDescription.Text = "No friends, or none online.";
                    else
                        doNewQuery(serverList);
                    return;
                }

                taskDescription.Text = "Getting Master list...";
                doNewMasterQuery();
            }
            else
            {
                taskDescription.Text = "Querying favourite servers...";
                // retrieve favourites list and add it to the server listview (and serverList private attr).
                foreach (Server serv in favMngr.getFavourites())
                    addToServerList(serv);
                if (serverList.Count == 0)
                    taskDescription.Text = "No favourites found.";
                else
                    doNewQuery(serverList);
            }
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            filtersDlg.ShowDialog();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (!taskInProgress)
                return;
            qhandler.cancelQuery();

            stopButton.Enabled = false;
            setProgressDesc("Stopped query...");
        }

        private int sortColumn = -1;
        private void serverBrowserView_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Server> serversSelected = getSelectedServers();
            if (serversSelected.Count > 0)
            {
                infoDlg.updateDlg(serversSelected[0]);
            }
            joinGameButton.Enabled = true;
            favouriteToggle.Enabled = true;
        }

        private void serverBrowserView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (taskInProgress)
                return;
            List<Server> serversSelected = getSelectedServers();
            taskDescription.Text = "Refreshing server...";
            doNewQuery(serversSelected);
        }

        private void serverBrowserForm_LostFocus(object sender, System.EventArgs e)
        {
            infoDlg.Hide();
        }

        private void favouriteToggle_Click(object sender, EventArgs e)
        {
            List<Server> serversSelected = getSelectedServers();
            List<Server> sl = favMngr.getFavourites();
            if (favMngr.isFavourite(serversSelected[0]))
                sl.Remove(serversSelected[0]);
            else
                sl.Add(serversSelected[0]);
            favMngr.saveFavourites(sl);
            serversSelected[0].rowItem.SubItems[2].Text = (favMngr.isFavourite(serversSelected[0]) ? "X" : "-");
        }

        private void serverBrowserForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("filters/"))
                Directory.CreateDirectory("filters");
            

            Version ver = new Version(Application.ProductVersion);
            authorLabel.Text = "aIW Server Browser v" + ver.ToString() + " by KeyboardCat";
        }

        #endregion

        private void mngFriendsBtn_Click(object sender, EventArgs e)
        {
            new FriendsList().ShowDialog();
        }

        private void updateLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://alteriw.net/viewtopic.php?f=35&t=64408");
        }
    }
}
