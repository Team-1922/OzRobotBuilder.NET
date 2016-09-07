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
    public abstract class SocketUpdatesBase
    {
        public SocketUpdatesBase(ISocketServer server)
        {
            _socketServer = server;
            server = null;

            _socketServer.SocketConnectEvent += _socketServer_SocketConnectEvent;
        }

        private async void _socketServer_SocketConnectEvent(PrimativeConnectionInfo connectionInfo)
        {
            await AddConnectionAsync(connectionInfo);
        }
        
        protected abstract Task AddConnectionAsync(PrimativeConnectionInfo connectionInfo);
        protected abstract Task<IEnumerable<Response>> SendAsync(Request request);
        
        #region Private Fields
        protected ISocketServer _socketServer;
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
                    _socketServer?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SocketUpdatesReceivable() {
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
