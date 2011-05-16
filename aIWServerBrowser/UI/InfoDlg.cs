using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace aIWServerBrowser
{
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
            new GameLauncher(currentServer.ServerAddress.ToString(), ShowErrorDlg);
            singleServerJoinButton.Enabled = false;
            this.Hide();

            new GameStartingUI().Show();
            new Thread(new ThreadStart(StartGWServer)).Start();
        }

        delegate void startGWCallback();
        private void StartGWServer()
        {
            if (singleServerJoinButton.InvokeRequired)
            {
                Server s = currentServer;
                new GameLauncher(s.ServerAddress.ToString(), ShowErrorDlg);

                Thread.Sleep(15000);
                this.Invoke(new startGWCallback(StartGWServer));
            }
            else
                singleServerJoinButton.Enabled = true;
        }

        private void ShowErrorDlg(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}
