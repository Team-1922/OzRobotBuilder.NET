using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// The part of the socket updates system which receives requests from clients for updates, but also sends out 
    /// propagated events from the <see cref="ISocketServer.RequestDelegator.Data"/> to all of the connected clients
    /// </summary>
    public class SocketUpdatesServer : IDisposable
    {
        /// <summary>
        /// Starts a socket updates server from a <see cref="ISocketServer"/> to handle clients connecting for read/write access
        /// and clients connecting for updates
        /// </summary>
        /// <param name="delegator">The requests delegator to use to handle incoming requests</param>
        /// <param name="receivingPort">The port to receiving incoming requests on</param>
        /// <param name="senderDelegatorPort">The port to listen for update requests one; note this is not the port which sends the updates themselves</param>
        public SocketUpdatesServer(IRequestDelegator delegator, int receivingPort, int senderDelegatorPort)
        {
            _receiver = new SocketServer(delegator, SocketListenerFactory.Instance, receivingPort);
            _senderDelegator = new SocketServer(delegator, SocketUpdatesFactory.Instance, senderDelegatorPort);

        }

        /// <summary>
        /// Starts the listeners for the receiving and updates sending servers
        /// </summary>
        public void StartListener()
        {
            _receiver?.StartListener();
            _senderDelegator?.StartListener();
        }

        /// <summary>
        /// Stops the listeners for the receiving and updates sending servers
        /// </summary>
        public void StopListener()
        {
            _receiver?.StopListener();
            _senderDelegator?.StopListener();
        }

        #region Private Fields
        ISocketServer _senderDelegator;
        ISocketServer _receiver;
        #endregion

        #region IDisposable
        private bool disposedValue = false; // To detect redundant calls

        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    /*foreach(var client in _clients)
                    {
                        client.Value?.Dispose();
                    }*/
                    StopListener();
                    _senderDelegator?.Dispose();
                    _receiver?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ViewModelBase() {
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
