using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class SocketClient : ISocketClient
    {
        public SocketClient()
        {
        }

        #region ISocketClient
        public async Task CloseConnectionAsync()
        {
            await SendAsync(new Request() { Method = Protocall.Method.Close });

            _cts.Cancel();
            _netStream.Dispose();
            _client.Dispose();

            _client = null;
            _netStream = null;
            _cts = null;
        }
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
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _client.Connect(ipAddress, port);
            _netStream = new NetworkStream(_client);
            _netStream.ReadTimeout = 5000;
            _netStream.WriteTimeout = 5000;
        }

        //NOTE: this should ONLY BE CALLED BY ONE THREAD AT A TIME
        public async Task<Response> SendAsync(Request request)
        {
            if (null == _client)
                throw new Exception("Client Must Be Connected Before Sending");
            if (null == request)
                throw new ArgumentException("request", "Given Request was Null");

            var senderTask = Utils.SocketSendAsync(_netStream, request);
            return await Utils.SocketReceiveResponseAsync(_netStream);
        }
        #endregion

        #region Private Fields
        private CancellationTokenSource _cts;
        private Socket _client;
        private NetworkStream _netStream;
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
                    if(_netStream != null)
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
