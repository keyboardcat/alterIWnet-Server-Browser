using System;
using System.Net;
using System.Threading;
using System.Collections.Generic;
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

    public class MasterQueryResult
    {
        protected List<Server> _mlist;
        public enum StatusCodes { Success, CriticalStop };

        protected String _message;
        protected StatusCodes _code;

        public String Message
        {
            get
            {
                return _message;
            }
        }

        public StatusCodes Code
        {
            get
            {
                return _code;
            }
        }

        public MasterQueryResult(List<Server> servers)
        {
            _code = StatusCodes.Success;
            _message = "";
            _mlist = servers;
        }

        public MasterQueryResult(StatusCodes code, String message)
        {
            _message = message;
            _code = code;
        }

        public List<Server> ServerList
        {
            get
            {
                return _mlist;
            }
        }
    }

    public delegate void QueryProgress(int done, int total); // for a progress count
    public delegate void MasterQueryCompleted(MasterQueryResult result);
 
    public class MasterQueryHandler
    {
        protected Thread _thread;
        private MasterQueryCompleted _callback;
        private IPEndPoint _master_addr;
        public MasterQueryHandler(IPEndPoint master_addr, MasterQueryCompleted callback)
        {
            _callback = callback;
            _master_addr = master_addr;

            _thread = new Thread(new ThreadStart(Run));
            _thread.IsBackground = true;
            _thread.Start();
        }

        protected void Run()
        {
            List<Server> mslist;
            try
            {
                aIWMasterListQuery mlist = new aIWMasterListQuery(_master_addr);
                mslist = mlist.getList();
            }
            catch (Exception e)
            {
                _callback(new MasterQueryResult(MasterQueryResult.StatusCodes.CriticalStop, e.Message));
                return;
            }

            _callback(new MasterQueryResult(mslist));
        }
    }

    public class ServerQueryHandler
    {
        protected Thread _thread;
        private List<Server> serversToQuery; 
        private Action _finishCallback;
        private QueryProgress _progressCallback;
        public ManualResetEvent _doneEvent = new ManualResetEvent(false);

        private volatile bool _queryCancelled = false; // volatile because it can be accessed from a different thread. may not be necessary though.
        private volatile int _threadsLeft; // count how many threads we've queued and decrement them every time one does it's job

        public bool QueryCancelled
        {
            get
            {
                return _queryCancelled;
            }
        } // read only var.

        public ServerQueryHandler(
            List<Server> servers,
            QueryProgress progressCallback,
            Action finishCallback)
        {
            serversToQuery = servers;
            _finishCallback = finishCallback;
            _progressCallback = progressCallback;

            _thread = new Thread(new ThreadStart(Run));
            _thread.IsBackground = true; // don't let this thread stop the app closing!
            _thread.Start();
        }
        
        protected void Run()
        {
            ThreadPool.SetMaxThreads(100, 75); // max number of threads is 100, can be increased to speed stuff up but will use lots of p3rf0rm4nc3
            _threadsLeft = serversToQuery.Count;
            
            for (int i = 0; i < serversToQuery.Count; i++)
            {
                Server s = serversToQuery[i];
                try
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(s.update), (object)this); // pass the handler object to the class 
                                                                                            // so it can be updated if the user wants to stop something
                }
                catch
                {
                    _threadsLeft--; // the thread isn't starting- so why count it in _threadsLeft?
                    continue;
                }
            }
            _doneEvent.WaitOne(); // wait until the _threadsLeft decrements to 0 or lower
            _finishCallback(); // tell the UI we've finished
        }

        public void queryFinished()
        {
            _threadsLeft--;
            _progressCallback(serversToQuery.Count - _threadsLeft, serversToQuery.Count);
            if (_threadsLeft <= 0)
                _doneEvent.Set();
        }

        public void cancelQuery()
        {
            _queryCancelled = true; // when this is set to True, all remaining servers refreshing won't parse or send any events back.
            _threadsLeft = 0;
            _doneEvent.Set();
        }
    }
}