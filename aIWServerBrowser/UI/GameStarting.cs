using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Globalization;
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
    class GameStartingUI : Form
    {
        private Label startInfo;
        private Action _callback;
        private Server _server;
        private ManualResetEvent _close_form_event;

        private volatile bool gameWait = false;
        private static uint _consoleInputAddress = 0x64FEEA8;
        private static uint _logOutputAddress = 0x64FEE9C;

        private IntPtr _logH;
        private IntPtr _cinH;

        public GameStartingUI(Server s, Action callback)
        {
            _callback = callback;
            _server = s;

            _close_form_event = new ManualResetEvent(false);
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
            this.startInfo.Size = new System.Drawing.Size(235, 59);
            this.startInfo.TabIndex = 0;
            this.startInfo.Text = "alterIWnet is starting...\n\nPress ESC to cancel";
            this.startInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameStartingUI
            // 
            this.ClientSize = new System.Drawing.Size(235, 59);
            this.ControlBox = false;
            this.Controls.Add(this.startInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameStartingUI";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Joining server";
            this.TopMost = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameStartingUI_KeyPress);
            this.ResumeLayout(false);

        }

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

        private static Process getIW4Process() // function to retrieve the iw4 process
        {
            foreach (Process p in Process.GetProcessesByName("iw4mp.dat"))
                return p;
            return null;
        }

        private static string dirname(string ins)
        {
            string[] s = ins.Split('\\');
            StringBuilder path = new StringBuilder();
            for (int i = 0; i < s.Length - 1; i++)
                path.Append(s[i] + "\\");
            
            return path.ToString();
        }

        private static Process startIW4()
        {
            // we use user32 "CreateProcess" instead of Process.Start, (the .dat extension could be assigned to start in another program)
            PROCESS_INFORMATION procInfStruct = new PROCESS_INFORMATION();
            STARTUPINFO startupInfStruct = new STARTUPINFO();

            string dirn = dirname(HelperFunctions.getGamePath());

            if (!CreateProcess(dirn + "iw4mp.dat", // dirname(string) always returns a path with a trailing /
                null,                              
                IntPtr.Zero,                       
                IntPtr.Zero,
                false,
                0,
                IntPtr.Zero,
                dirn,
                ref startupInfStruct,
                out procInfStruct))
                return null;
            
            return Process.GetProcessById(procInfStruct.dwProcessId);
        }

        private void waitTillDialogClosed()
        {
            while ((FindWindow("#32770", "Set Optimal Settings?")) != IntPtr.Zero)
            {
                editStatusText("Waiting for user to decide on a dialog...");
                Thread.Sleep(1000);
            }
            while (FindWindow("#32770", "Run In Safe Mode?") != IntPtr.Zero)
            {
                editStatusText("Waiting for user to decide on a dialog...");
                Thread.Sleep(1000);
            }
        }

        private void waitTillAttemptComplete()
        {
            if (this.InvokeRequired) // we assume that the logic is being performed within another thread.
            {
                _close_form_event.WaitOne();
                this.Invoke(new Action(waitTillAttemptComplete));
            }
            else
            {
                _callback();         // we do not want to block the UI thread though, so don't wait for anything to happen.
                this.Close();
            }
        }

        private int waitTillServerNotFull()
        {
            int attempts = 0;
            bool failed = false;

            do
            {
                _server.update(null);
                Thread.Sleep(1000); // wait a second so we don't costantly flood the server with ARE WE THERE YET?

                if (_server.queryStatus != Server.ServerQueryStatus.Successful)
                {
                    if (!failed) // haven't failed before? let's reset the counter AND GO HARDC0RE COUNTING HUUUUURRRR
                        attempts = 0;
                    failed = true;

                    if (attempts > 5)
                    {
                        editStatusText("Server timed out");
                        Thread.Sleep(1000);
                        _close_form_event.Set(); // tell the form to stop
                        return attempts;
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
            while (_server.serverNPlayers >= _server.serverMaxPlayers); // onoes, server is full

            return attempts;
        }

        private void doGameInit()
        {
            bool clean = false;
            
            Process iw4 = getIW4Process();
            editStatusText("Waiting on game to properly load...");

            if (iw4 == null)
            {
                iw4 = startIW4();
                while ((iw4 = getIW4Process()) == null)
                    Thread.Sleep(100);

                // from here we wait for the Input handle and the log handle to become available.
                waitInH();
                waitLogH();

                editStatusText("Waiting for game to authorise with LSP...\nPress enter to skip");
                gameWait = true;

                Match m;
                Regex playerInfoMatch = new Regex("Sent to LSP: HELLO: LSPXUID ([a-z0-9]*?) - GT \"(.*?)\"", RegexOptions.Compiled);
                while (!(m = playerInfoMatch.Match(readLogH())).Success && gameWait)
                    Thread.Sleep(1000);
                if (gameWait)
                {
                    if (m.Success)
                    {
                        try
                        {
                            long xuid = long.Parse(m.Groups[1].Value, NumberStyles.AllowHexSpecifier);

                            WebClient wc = new WebClient();
                            editStatusText("Waiting for the client to be clean...\n\nPress enter to skip");
                            while (!clean && gameWait)
                            {
                                // server.alteriw.net:13000/clean lets us check whether the xuid will be good enough to join a server.
                                clean = (wc.DownloadString("http://server.alteriw.net:13000/clean/" + xuid.ToString()) == "valid");
                                editStatusText("Waiting for the client to be clean...\n" + (clean ? "you are clean!" : "still unclean...Press enter to skip"));
                                Thread.Sleep(3000);
                            }
                        }
                        catch { }
                    }
                }
            }

            if (clean == false)
            {
                for (int secondsLeft = 20; secondsLeft > 0 && gameWait; secondsLeft--)
                {
                    waitTillDialogClosed(); // check if any of those popup dialogs are up...
                    editStatusText("Waiting a bit for the client to sort itself out...\n" + secondsLeft + " secs left\nPress enter to skip");
                    Thread.Sleep(1000);
                }
            }
            gameWait = false;
        }

        public string findLine(string search)
        {
            string[] lines = readLogH().Split('\n');

            foreach (string line in lines)
            {
                if (line.Contains(search))
                    return line;
            }
            return null;
        }

        public void startConnecting()
        {
            this.Show();
            // start.. TWO threads
            Thread t = new Thread(new ThreadStart(_gameLaunchProcedure));
            t.IsBackground = true;
            t.Start();

            Thread t2 = new Thread(new ThreadStart(waitTillAttemptComplete));
            t2.IsBackground = false;
            t2.Start();
        }

        private void _gameLaunchProcedure()
        {
            //Process iw4 = getIW4Process();
            //if (iw4 == null)
            //{
            //    iw4 = startIW4();

            //    if (iw4 == null)
            //        MessageBox.Show(Marshal.GetLastWin32Error().ToString());

            //    editStatusText("Waiting for the game to be started...");

            //    // wait for the process to become available.
            //    while (Process.GetProcessesByName("iw4mp.dat").Length == 0)
            //        Thread.Sleep(1000);
            //}

            doGameInit();
            waitInH();

            editStatusText("Waiting for a space... attempt 0");
            int attempts = waitTillServerNotFull();

            editStatusText(string.Format("Space found in server! ({0} attempts)", attempts));
            

            // now we connect!

            writeCommandH(string.Format("connect {0}", _server.ServerAddress.ToString()).Replace(";", ""));

            _close_form_event.Set();
        }

        private void closeSelf()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(closeSelf));
                return;
            }
            this.Close();
        }

        private void GameStartingUI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                _close_form_event.Set();
            }
            else if (gameWait && ((Keys)e.KeyChar == Keys.Enter))
            {
                gameWait = false; // we cancel it through this method.
            }
        }


        private static Int32 readInt32(IntPtr proc, uint loc)
        {
            uint read = 0;
            byte[] outbuf = new byte[4]; // int32 is 4 bytes
            if (ReadProcessMemory(proc, (IntPtr)loc, outbuf, 4, ref read))
                return BitConverter.ToInt32(outbuf, 0);
            else 
                return 0;
        }

        private void waitInH()
        {
            Process iw4;
            if ((iw4 = getIW4Process()) == null)
                return;

            while ((_cinH = (IntPtr)readInt32(iw4.Handle, _consoleInputAddress)) == IntPtr.Zero)
                Thread.Sleep(100);
        }

        private void waitLogH()
        {
            Process iw4;
            if ((iw4 = getIW4Process()) == null)
                return;

            while ((_logH = (IntPtr)readInt32(iw4.Handle, _logOutputAddress)) == IntPtr.Zero)
                Thread.Sleep(100);
        }

        private string readLogH()
        {
            int length = SendMessage(_logH, 0x0E, 0, null);
            StringBuilder lParam = new StringBuilder(length);
            SendMessage(_logH, 0x0D, length + 1, lParam);

            return lParam.ToString();
        }

        private void writeCommandH(string cmd)
        {
            SendMessage(_cinH, 0x0C, IntPtr.Zero, cmd);
            SendMessage(_cinH, 0x102, 0xD, 0);
        }

        [DllImport("user32.dll")]
        static extern Int32 SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, StringBuilder lParam);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String sClassName, String sAppName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, bool bInheritHandle, UInt32 dwProcessId);

        [DllImport("Kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UInt32 nSize, ref UInt32 lpNumberOfBytesRead);


        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public unsafe byte* lpSecurityDescriptor;
            public int bInheritHandle;
        }

        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(
            string lpApplicationName, 
            string lpCommandLine, 
            IntPtr lpProcessAttributes, 
            IntPtr lpThreadAttributes,
            bool bInheritHandles, 
            uint dwCreationFlags, 
            IntPtr lpEnvironment,
            string lpCurrentDirectory, 
            ref STARTUPINFO lpStartupInfo, 
            out PROCESS_INFORMATION lpProcessInformation);

    }
}
