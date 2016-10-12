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
    /// <summary>
    /// A socket client which connects to a server and sends requests to update its data
    /// </summary>
    public class SocketClient : ISocketClient
    {
        #region ISocketClient
        /// <summary>
        /// The local endpoint of the connection
        /// </summary>
        public IPEndPoint LocalEndPoint
        {
            get
            {
                return (IPEndPoint)_client?.LocalEndPoint;
            }
        }

        /// <summary>
        /// The remote endpoint of the connection
        /// </summary>
        public IPEndPoint RemoteEndPoint
        {
            get
            {
                return (IPEndPoint)_client?.RemoteEndPoint;
            }
        }

        /// <summary>
        /// Whether or not the client is connected
        /// </summary>
        public bool Connected
        {
            get
            {
                return _client.Connected;
            }
        }

        /// <summary>
        /// Closes the connection to the server
        /// </summary>
        /// <returns></returns>
        public async Task CloseConnectionAsync()
        {
            if (null == _clientNetStream)
                return;

            await SendAsync(new Request() { Method = Protocall.Method.Close });

            _cts.Cancel();
            _clientNetStream.Dispose();
            _client?.Dispose();

            _client = null;
            _clientNetStream = null;
            _cts = null;
        }
        /// <summary>
        /// Opens the connection to the server to send requests to
        /// </summary>
        /// <param name="hostName">the host name of the server</param>
        /// <param name="port">the port of the server to connect to</param>
        /// <returns></returns>
        public async Task OpenConnectionAsync(string hostName, int port)
        {
            if (_client != null)
                throw new Exception("Client Already Connected!");

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
        }
        #endregion

        /// <summary>
        /// Sends a given request to the server
        /// </summary>
        /// <remarks>
        /// This should only be called by one thread at a time
        /// </remarks>
        /// <param name="request">the request to send</param>
        /// <returns>a response from the server of the given request</returns>
        public async Task<Response> SendAsync(Request request)
        {
            if (null == _client)
                throw new Exception("Client Must Be Connected Before Sending");
            if (null == request)
                throw new ArgumentException("request", "Given Request was Null");

            var senderTask = Utils.SocketSendAsync(_clientNetStream, new SocketMessage(request), _cts.Token);
            return await Utils.SocketReceiveResponseAsync(_clientNetStream, _cts.Token);
        }

        #region Private Fields
        private CancellationTokenSource _cts;
        private Socket _client;
        private NetworkStream _clientNetStream;
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
        #endregion
    }
}
