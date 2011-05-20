using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
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
    class GameStartingUI : Form
    {
        private Label startInfo;
        private joinServerFinished _callback;
        private Server server;
        private bool completed = false;
        private GameLauncher glauncher;

        public GameStartingUI(Server s, joinServerFinished callback)
        {
            _callback = callback;
            server = s;
            glauncher = new GameLauncher(server.ServerAddress.ToString(), new gameLaunchError(dlg));

            _connectAttemptComplete = new ManualResetEvent(false);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.startInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startInfo
            // 
            this.startInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startInfo.Location = new System.Drawing.Point(0, 0);
            this.startInfo.Name = "startInfo";
            this.startInfo.Size = new System.Drawing.Size(263, 50);
            this.startInfo.TabIndex = 0;
            this.startInfo.Text = "alterIWnet is starting...\n\nPress ESC to cancel";
            this.startInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameStartingUI
            // 
            this.ClientSize = new System.Drawing.Size(263, 50);
            this.ControlBox = false;
            this.Controls.Add(this.startInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameStartingUI";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "alterIWnet is starting...";
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameStartingUI_KeyPress);
            this.ResumeLayout(false);

        }

        private ManualResetEvent _connectAttemptComplete;


        delegate void editStatusText_Invoke(string s);
        private void editStatusText(string s)
        {
            if (startInfo.InvokeRequired)
            {
                this.Invoke(new editStatusText_Invoke(editStatusText), new object[] { s });
                return;
            }
            startInfo.Text = s + "\n\nPress ESC to cancel";
        }

        private void tryConnect()
        {
            glauncher.Connect();
        }

        private void waitTillAttemptComplete()
        {
            if (this.InvokeRequired)
            {
                _connectAttemptComplete.WaitOne();
                completed = true;
                this.Invoke(new BlankCallback(waitTillAttemptComplete));
            }
            else
            {
                this.Close();
            }
        }

        private void waitTillServerNotFull()
        {
            int attempts = 0;
            bool failed = false;

            do
            {
                server.update(null);
                Thread.Sleep(1000); // wait 3/4 of a second so we don't costantly flood the server with ARE WE THERE YET?

                if (server.queryStatus != Server.ServerQueryStatus.Successful)
                {
                    if (!failed) // haven't failed before? let's reset the counter AND GO HARDC0RE COUNTING HUUUUURRRR
                        attempts = 0;
                    failed = true;

                    if (attempts > 5)
                    {
                        editStatusText("Server timed out");
                        Thread.Sleep(1000);
                        _connectAttemptComplete.Set(); // tell the form to stop
                        return;
                    }

                    // reset the counter, and count to 5- if the server fails to respond. we're boned.
                    editStatusText("Server is not responding... attempt " + (attempts++));
                }
                else
                {
                    if (failed)
                    {
                        failed = false;
                        attempts = 0;
                    }
                    editStatusText("Waiting for new space... attempt " + (attempts++)); // concatenating string like this is.. urrghghgh
                }
            }
            while (server.serverNPlayers >= server.serverMaxPlayers); // onoes, server is full
        }

        public void startConnecting()
        {
            this.Show();
            // start.. TWO threads
            Thread t = new Thread(new ThreadStart(_startConnecting));
            t.IsBackground = true;
            t.Start();

            Thread t2 = new Thread(new ThreadStart(waitTillAttemptComplete));
            t2.IsBackground = false;
            t2.Start();
        }

        private void _startConnecting()
        {
            glauncher.Launch();

            editStatusText("Waiting for a space... attempt 0");
            waitTillServerNotFull();

            if (completed)
                return;

            editStatusText("Space found!");
            tryConnect();
            _connectAttemptComplete.Set();
        }

        private void closeSelf()
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new BlankCallback(closeSelf));
                }
                catch { }
                return;
            }
            this.Close();
        }

        private void dlg(string err)
        {
            MessageBox.Show(err);
        }

        private void GameStartingUI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
                _connectAttemptComplete.Set();
        }
    }
}
