using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

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
}
