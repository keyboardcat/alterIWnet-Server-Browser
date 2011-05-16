using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace aIWServerBrowser
{
    public class aIWMasterListQuery
    {
        protected byte[] packet;
        private IPEndPoint master_addr;
        public aIWMasterListQuery(IPEndPoint addr)
        {
            // instantiate class attrs
            packet = chars2bytes("\xff\xff\xff\xffgetservers IW4 142 full empty".ToCharArray());
            master_addr = addr;
        }

        //hacky, but does the job
        private byte[] chars2bytes(char[] chr)
        {
            byte[] b = new byte[chr.Length];
            for (int i = 0; i < chr.Length; i++)
                b[i] = (byte)chr[i];
            return b;
        }

        private int readPort(string data)
        {
            return (256 * (int)data[4] + (int)data[5]);
        }

        private IPAddress readIP(string data)
        {
            IPAddress ip = new IPAddress( 
                new byte[] { 
                (byte)data[0], 
                (byte)data[1], 
                (byte)data[2], 
                (byte)data[3] });
            return ip;
        }


        private List<Server> parseMasterResponse(byte[] data)
        {
            List<Server> addrList = new List<Server>();
            string strData = Encoding.UTF7.GetString(data);
            string[] addrSplit = strData.Split('\\');
            try
            {
                foreach (string addr in addrSplit)
                {
                    if (addr.ToLower().Contains("getserversresponse"))
                        continue;
                    if (addr.Contains("EOT"))
                        break;
                    if (addr.Length < 6)
                        continue;

                    addrList.Add(new Server(
                            new IPEndPoint(readIP(addr),
                            readPort(addr))));
                }
            }
            catch (Exception)
            {
                throw new InvalidDataException("Master response was invalid.");
            }
            return addrList;
        }

        public List<Server> getList()
        {
            UdpClient udpTransport = new UdpClient();
            udpTransport.Connect(master_addr);
            udpTransport.Client.ReceiveTimeout = 2000;
            udpTransport.Send(packet, packet.Length);

            // let the server send its payload
            List<byte[]> payloads = new List<byte[]>();
            List<Server> addrList = new List<Server>();
            bool foundEOT = false;
            IPEndPoint senderEP = new IPEndPoint(IPAddress.Any, 0);
            do
            {
                // some of these will throw an exception, this is fine. We will catch it somewhere else.
                byte[] data = udpTransport.Receive(ref senderEP);
                if (!senderEP.Address.Equals(master_addr.Address))
                    continue; // that was.. not what we wanted :(
                payloads.Add(data);
                if (ASCIIEncoding.ASCII.GetString(data).Contains("EOT"))
                    foundEOT = true;
            }
            while (!foundEOT);
            foreach (byte[] pl in payloads)
                addrList.AddRange(parseMasterResponse(pl));
            return addrList;
        }

    }
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
                                     //Saves having to constantly l0000p through everything to find the correct one...!
        // event to tell the sender when the server has updated.
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
            sqh = (ServerQueryHandler)threadContext;
            try
            {
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

                if (sqh.QueryCancelled) // derp, stop nao!
                {
                    _udp.Close();
                    return;
                }
                if (!serverIn.Address.Equals(serverAddr.Address))
                {
                    this.queryStatus = ServerQueryStatus.Failed;
                    throw new Exception("Got data from wrong host " +
                        serverIn.Address.ToString() + " expected from " +
                        serverAddr.Address.ToString());
                }
                _udp.Close();
                this.parseAndPopulate(data);
                this.queryStatus = ServerQueryStatus.Successful;
            }
            catch (SocketException)
            {
                this.queryStatus = ServerQueryStatus.TimedOut;
            }
            catch (Exception)
            {
                this.queryStatus = ServerQueryStatus.Failed;
                throw;
            }
            finally
            {
                if (!sqh.QueryCancelled) // derp, stop nao!
                {
                    try
                    {
                        sqh.queryFinished(); // tell the handler that we've finished our query
                        ServerUpdated(this);
                    }
                    catch { /* o noes! */ }
                }
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
