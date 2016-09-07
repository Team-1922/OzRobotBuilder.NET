using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    public class SocketUpdatesClient : SocketUpdatesBase
    {
        public SocketUpdatesClient(ISocketServer server) : base(server)
        {
        }

        protected override async Task AddConnectionAsync(PrimativeConnectionInfo connectionInfo)
        {
            //don't connect if we are already connected to one; more than one client should not be connecting to our server
            if (null != _client)
                throw new Exception("Multiple Connections To SocketUpdatesClient");

            _client = new SocketClient();
            await _client.OpenConnectionAsync(connectionInfo.IpAddress, connectionInfo.Port);
        }

        protected override async Task<IEnumerable<Response>> SendAsync(Request request)
        {
            return new List<Response>() { await _client.SendAsync(request) };
        }

        #region Private Fields
        private SocketClient _client = null;
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
