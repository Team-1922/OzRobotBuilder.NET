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
            SocketServer = server;
        }

        public abstract Task<IEnumerable<Response>> SendAsync(Request request);
        
        protected ISocketServer SocketServer
        {
            get
            {
                return _socketServer;
            }

            set
            {
                if (ValidateSocketServer(_socketServer))
                {
                    _socketServer.RequestDelegator.Data.Propagated -= Data_Propagated;
                }
                _socketServer = value;
                if(ValidateSocketServer(value))
                {
                    _socketServer.RequestDelegator.Data.Propagated += Data_Propagated;
                }
            }
        }

        #region Private Fields
        protected ISocketServer _socketServer = null;
        #endregion

        #region Private Methods
        private async void Data_Propagated(MVVM.Contracts.Events.EventPropagationEventArgs e)
        {
            await SendAsync(new Request() { Method = e.Method, Body = e.PropertyValue, Path = e.PropertyName });
        }
        private static bool ValidateSocketServer(ISocketServer server)
        {
            return null != server && null != server?.RequestDelegator && null != server?.RequestDelegator?.Data;
        }
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
