using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

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

    // Class for an individual server
    //  -- Can be queried to populate info.
    // will include events to tell the GUI to update that specific server.
    public delegate void ServerUpdated(Server e);
    public class Server
    {
        public enum ServerQueryStatus { Successful, TimedOut, Failed, NothingYet };
        public ServerQueryStatus queryStatus = ServerQueryStatus.NothingYet;

        protected IPEndPoint serverAddr; // most of these are just Dvars..
        public int serverNPlayers;   // but what the heck we'll organise them anyway.
        public int serverMaxPlayers;
        public int serverPing = 999; // classic old 999ing
        public string serverGametype;
        public string serverMap;
        public string serverMod;
        public string serverName;
        public List<Player> serverPlayerList;
        public Dictionary<string, string> serverDvars;
        public ListViewItem rowItem; // the GUI stores the ListViewItem unique to this class. 
        //Saves having to constantly loop through everything to find the correct one...!
        
        // event - when the server has updated.
        public event ServerUpdated ServerUpdated;

        private ServerQueryHandler sqh;

        // UDP socket
        private UdpClient _udp;
        private byte[] status_packet;

        public Server(IPEndPoint addr)
        {
            this.serverAddr = addr;
            this.status_packet = chars2bytes("\xff\xff\xff\xffgetstatus".ToCharArray());
        }

        private byte[] chars2bytes(char[] chr) // using anything else appears to give dodgy bytes
        {
            byte[] b = new byte[chr.Length];
            for (int i = 0; i < chr.Length; i++)
                b[i] = (byte)chr[i];
            return b;
        }

        public void update(Object threadContext)
        {
            if (threadContext != null)
                sqh = (ServerQueryHandler)threadContext;
            try
            {
                if (threadContext != null)
                    if (sqh.QueryCancelled)
                        return;
                _udp = new UdpClient();
                _udp.Client.ReceiveTimeout = 2000;
                _udp.Client.SendTimeout = 2000;

                _udp.Connect(serverAddr);

                int now = Environment.TickCount;
                _udp.Send(status_packet, status_packet.Length);

                IPEndPoint serverIn = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = _udp.Receive(ref serverIn);
                this.serverPing = Environment.TickCount - now; // grab the ping ASAP

                if (threadContext != null)
                {
                    if (sqh.QueryCancelled)
                    {
                        this.queryStatus = ServerQueryStatus.Failed;
                        _udp.Close();

                        ServerUpdated(this);
                        return;
                    }
                }

                if (queryStatus != ServerQueryStatus.Failed)
                {
                    if (!serverIn.Address.Equals(serverAddr.Address))
                    {
                        this.queryStatus = ServerQueryStatus.Failed;
                        throw new Exception("Got data from wrong host " +
                            serverIn.Address.ToString() + " expected from " +
                            serverAddr.Address.ToString());
                    }
                    
                    this.parseAndPopulate(data);
                    this.queryStatus = ServerQueryStatus.Successful;
                }
                _udp.Close();
            }
            catch (SocketException)
            {
                this.queryStatus = ServerQueryStatus.TimedOut;
            }
            catch (Exception)
            {
                this.queryStatus = ServerQueryStatus.Failed;
            }
            finally
            {
                try
                {
                    if (threadContext != null)
                        sqh.queryFinished(); // tell the handler that we've finished our query
                    ServerUpdated(this);
                }
                catch { /* o noes! */ }
            }
        }

        // Populate the class with the parsed packet data.
        private void parseAndPopulate(byte[] data)
        {
            string dataString = ASCIIEncoding.ASCII.GetString(data, 4, data.Length - 4);
            string[] tokData = dataString.Split('\\');
            Dictionary<string, string> dVars = new Dictionary<string, string>();
            for (int i = 1; i < tokData.Length; i += 2)
            {
                try
                {
                    dVars[tokData[i]] = tokData[i + 1];
                }
                catch
                { } // WHAT DO YOU MEAN THERE'S NO SECOND VALUE?
            }
            tokData = dataString.Split('\n');

            string[] pTok;
            List<Player> playerList = new List<Player>();
            for (int i = 2; i < tokData.Length; i++)
            {
                pTok = tokData[i].Split(' ');
                if (pTok.Length < 2)
                    continue;
                playerList.Add(
                    new Player(tokData[i].Split('"')[1],
                    int.Parse(pTok[0]),
                    int.Parse(pTok[1])));
            }

            // populate the class with what we've got.
            this.serverGametype = dVars["g_gametype"];
            this.serverMap = dVars["mapname"];
            this.serverMaxPlayers = int.Parse(dVars["sv_maxclients"]);
            this.serverNPlayers = playerList.Count;
            this.serverMod = "None";
            if (dVars.ContainsKey("fs_game"))
                this.serverMod = dVars["fs_game"];
            this.serverName = dVars["sv_hostname"];
            this.serverDvars = dVars;
            this.serverPlayerList = playerList;
        }

        public IPEndPoint ServerAddress
        {
            get
            {
                return serverAddr;
            }
        }

        public static bool operator ==(Server x, Server y)
        {
            try
            {
                return x.ServerAddress.ToString().Equals(y.ServerAddress.ToString());
            }
            catch
            {
                return false;
            }
        }

        public static bool operator !=(Server x, Server y)
        {
            try
            {
                return !x.ServerAddress.ToString().Equals(y.ServerAddress.ToString());
            }
            catch
            {
                return true;
            }
        }

        public override bool Equals(object o)
        {
            try
            {
                return this.ServerAddress.ToString().Equals(((Server)o).ServerAddress.ToString());
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return serverAddr.GetHashCode();
        }
    }

    // basic struct for a player in a server.
    public struct Player
    {
        public string name;
        public int score;
        public int ping;

        public Player(string name, int score, int ping)
        {
            this.name = name;
            this.score = score;
            this.ping = ping;
        }
    }
}
