using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

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
    public delegate void joinServerFinished();
    public delegate void doUpdateServer(Server e);
    public partial class InfoDlg : Form
    {
        private doUpdateServer update;
        public InfoDlg(doUpdateServer updateFunc)
        {
            update = updateFunc;
            InitializeComponent();
        }

        public Server currentServer;
        public void updateDlg(Server server)
        {
            playerListView.Items.Clear();
            if (server.queryStatus == Server.ServerQueryStatus.Successful)
            {
                serverInfoView.Text = "";
                int startLen = 0;
                string info;
                info = string.Format("Server Name: \t{0}", HelperFunctions.removeColourCodes(server.serverName));
                serverInfoView.Text = info;

                serverInfoView.SelectionStart = startLen;
                serverInfoView.SelectionLength = "Server Name: ".Length;
                serverInfoView.SelectionFont = new Font("Calibri", 11F, FontStyle.Bold, GraphicsUnit.Point);

                // TODO: Fix the positioning of the colour codes :/

                startLen = serverInfoView.TextLength;
                info = "\n\nAddress: \t" + server.ServerAddress.ToString();
                serverInfoView.AppendText(info);

                serverInfoView.SelectionStart = startLen;
                serverInfoView.SelectionLength = "Address: ".Length;
                serverInfoView.SelectionFont = new Font("Calibri", 11F, FontStyle.Bold, GraphicsUnit.Point);

                /*
                serverInfoView.SelectionStart = startLen;
                serverInfoView.SelectionLength = info.Length;
                serverInfoView.SelectionColor = Color.White;
                */

                startLen = serverInfoView.TextLength;
                info = string.Format("\nPlayers: \t{0}/{1}", server.serverNPlayers, server.serverMaxPlayers);
                serverInfoView.AppendText(info);
                serverInfoView.SelectionStart = startLen;
                serverInfoView.SelectionLength = "Players: ".Length;
                serverInfoView.SelectionFont = new Font("Calibri", 11F, FontStyle.Bold, GraphicsUnit.Point);


                startLen = serverInfoView.TextLength;
                info = "\nGameType: \t" + HelperFunctions.gameTypeToFullname(server.serverGametype);
                serverInfoView.AppendText(info);
                serverInfoView.SelectionStart = startLen;
                serverInfoView.SelectionLength = "GameType: ".Length;
                serverInfoView.SelectionFont = new Font("Calibri", 11F, FontStyle.Bold, GraphicsUnit.Point);

                startLen = serverInfoView.TextLength;
                info = "\nMap: \t\t" + HelperFunctions.mapToFullname(server.serverMap);
                serverInfoView.AppendText(info);
                serverInfoView.SelectionStart = startLen;
                serverInfoView.SelectionLength = "Map: ".Length;
                serverInfoView.SelectionFont = new Font("Calibri", 11F, FontStyle.Bold, GraphicsUnit.Point);

                startLen = serverInfoView.TextLength;
                info = "\nMod: \t\t" + server.serverMod;
                serverInfoView.AppendText(info);
                serverInfoView.SelectionStart = startLen;
                serverInfoView.SelectionLength = "Mod: ".Length;
                serverInfoView.SelectionFont = new Font("Calibri", 11F, FontStyle.Bold, GraphicsUnit.Point);

                playerListView.Items.Clear();
                foreach (Player p in server.serverPlayerList)
                {
                    ListViewItem lvi = playerListView.Items.Add(HelperFunctions.removeColourCodes(p.name));
                    lvi.SubItems.Add(p.score.ToString());
                    lvi.SubItems.Add(p.ping.ToString());
                }
                this.Text = HelperFunctions.removeColourCodes(server.serverName);
                singleServerRefreshButton.Enabled = true;
                singleServerJoinButton.Enabled = true;
            }
            else
            {
                if (server.queryStatus == Server.ServerQueryStatus.TimedOut)
                    serverInfoView.Text = "Server timed out...";
                else if (server.queryStatus == Server.ServerQueryStatus.NothingYet)
                    serverInfoView.Text = "Server is being queried...";
                else
                    serverInfoView.Text = "Server query failed...";
                singleServerRefreshButton.Enabled = false;
                singleServerJoinButton.Enabled = false;
            }
            currentServer = server;
        }

        private void InfoDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void singleServerRefreshButton_Click(object sender, EventArgs e)
        {
            if (currentServer != null)
                update(currentServer);
        }

        private void singleServerJoinButton_Click(object sender, EventArgs e)
        {
            singleServerJoinButton.Enabled = false;
            this.Hide();

            new GameStartingUI(currentServer, new joinServerFinished(joinServerDone)).Show();
        }

        public void joinServerDone()
        {
            singleServerJoinButton.Enabled = true;
        }
    }
}
