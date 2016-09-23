using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    public class SocketUpdatesServer : SocketUpdatesBase
    {
        public SocketUpdatesServer(ISocketServer receiver, ISocketServer senderDelegator) : base(receiver)
        {
            //server.SocketConnectEvent += _socketServer_SocketConnectEvent;
        }

        private async void _socketServer_SocketConnectEvent(PrimativeConnectionInfo connectionInfo)
        {
            await AddConnectionAsync(connectionInfo);
        }

        protected async Task AddConnectionAsync(PrimativeConnectionInfo connectionInfo)
        {
            if (ContainsConnection(connectionInfo))
                return;//if the connection info is the SAME, then do not reconnect

            //open a new client to connect to the server of the newly connected client
            var nextClient = new SocketClient();
            await nextClient.OpenConnectionAsync(connectionInfo.IpAddress, connectionInfo.Port);
            try
            {
                _clientsLock.EnterWriteLock();
                _clients.Add(connectionInfo, nextClient);//TODO: thread-saftey
            }
            finally
            {
                _clientsLock.ExitWriteLock();
            }

            // TODO: this should not be forced upon the client, but rather it should be requested by it.
            //when we connect to the other server, since they started the connection, give them our data
            /*await SendAsync(
                new Request()
                {
                    Method = Protocall.Method.Set,
                    Body = (await _socketServer.RequestDelegator.ProcessRequestAsync(
                        new Request()
                        {
                            Method = Protocall.Method.Get,
                            Body = ""
                        })).ToString(),
                    Path = ""
                });*/
        }

        public override async Task<IEnumerable<Response>> SendAsync(Request request)
        {
            List<Task<Response>> senders = new List<Task<Response>>();
            try
            {
                _clientsLock.EnterReadLock();
                foreach (var clientPair in _clients)
                {
                    senders.Add(clientPair.Value.SendAsync(request));
                }
            }
            finally
            {
                _clientsLock.ExitReadLock();
            }
            List<Response> responses = new List<Response>();
            foreach(var sender in senders)
            {
                responses.Add(await sender);
            }
            return responses;
        }

        #region Private Helper Methods
        private bool ContainsConnection(PrimativeConnectionInfo connectionInfo)
        {
            try
            {
                _clientsLock.EnterReadLock();
                return _clients.ContainsKey(connectionInfo);
            }
            finally
            {
                _clientsLock.ExitReadLock();
            }
        }
        #endregion

        #region Private Fields
        Dictionary<PrimativeConnectionInfo, SocketClient> _clients = new Dictionary<PrimativeConnectionInfo, SocketClient>();
        public ReaderWriterLockSlim _clientsLock = new ReaderWriterLockSlim();
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
                    foreach(var client in _clients)
                    {
                        client.Value?.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        #endregion
    }
}
