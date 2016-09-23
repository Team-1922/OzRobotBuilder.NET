using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    public class SocketUpdatesClient : SocketUpdatesBase
    {
        public SocketUpdatesClient(IRequestDelegator requestDelegator) : base(null)
        {
            _requestDelegator = requestDelegator;
        }

        public async Task OpenConnectionAsync(string hostName, int receivingPort, int sendingPort)
        {
            _client = new SocketClient();
            await _client.OpenConnectionAsync(hostName, receivingPort);

            _cts = new CancellationTokenSource();
            var listenerSocket = Utils.MakeSocket();
            listenerSocket.Connect(ipAddress, port);
            _clientNetStream = new NetworkStream(_client);
            var server = new SocketServer(_requestDelegator, )
        }

        public override async Task<IEnumerable<Response>> SendAsync(Request request)
        {
            return new List<Response>() { await _client.SendAsync(request) };
        }

        #region Private Fields
        private SocketClient _client = null;
        IRequestDelegator _requestDelegator = null;
        #endregion

        #region IDisposable
        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _client?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        #endregion
    }
}
