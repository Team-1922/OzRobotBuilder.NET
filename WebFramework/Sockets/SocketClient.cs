using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    public class SocketClient : ISocketClient
    {
        public SocketClient(IHierarchialAccessRoot data = null, bool sendOnly = false)
        {
            SendOnly = sendOnly;
            _data = data;
        }

        #region ISocketClient
        public bool SendOnly { get; }

        public IPEndPoint LocalEndPoint
        {
            get
            {
                return (IPEndPoint)_client?.LocalEndPoint;
            }
        }

        public IPEndPoint RemoteEndPoint
        {
            get
            {
                return (IPEndPoint)_client?.RemoteEndPoint;
            }
        }

        public async Task CloseConnectionAsync()
        {
            if (null == _clientNetStream)
                return;

            await SendAsync(new Request() { Method = Protocall.Method.Close });

            _cts.Cancel();
            _clientNetStream.Dispose();
            _client?.Dispose();

            _server?.Dispose();

            _client = null;
            _clientNetStream = null;
            _server = null;
            _cts = null;
        }
        public async Task OpenConnectionAsync(string hostName, int port)
        {
            if (_client != null)
                throw new Exception("Client Already Connected!");

            //before even setting up this socket, make sure we have a server which can be connected to
            if(!SendOnly)
            {
                _server = new SocketServer(new RequestDelegator(_data), "");//TODO: should the server be given to us?  SHould the request delegator be given to us?
            }

            IPHostEntry ipHost = await Dns.GetHostEntryAsync(hostName);
            IPAddress ipAddress = ipHost.AddressList.Where(address => address.AddressFamily == AddressFamily.InterNetwork).First();
            if (ipAddress == null)
            {
                Console.WriteLine("no IPv4 address");
                return;
            }
            _cts = new CancellationTokenSource();
            _client = Utils.MakeSocket();
            _client.Connect(ipAddress, port);
            _clientNetStream = new NetworkStream(_client);

            //TODO: listen for their path

            if(!SendOnly)
            {
                //tell the connecting server what our path is
                await Utils.SocketSendAsync(_clientNetStream, new SocketMessage())
            }
        }

        //NOTE: this should ONLY BE CALLED BY ONE THREAD AT A TIME
        public async Task<Response> SendAsync(Request request)
        {
            if (null == _client)
                throw new Exception("Client Must Be Connected Before Sending");
            if (null == request)
                throw new ArgumentException("request", "Given Request was Null");

            var senderTask = Utils.SocketSendAsync(_clientNetStream, request);
            return await Utils.SocketReceiveResponseAsync(_clientNetStream);
        }
        #endregion

        #region Private Fields
        private CancellationTokenSource _cts;
        private Socket _client;
        private NetworkStream _clientNetStream;
        private SocketServer _server;
        private IHierarchialAccessRoot _data;
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if(_clientNetStream != null)
                        CloseConnectionAsync().Wait();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SocketClient() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        public Task OpenConnectionAsync(string hostName, string path, int port)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
