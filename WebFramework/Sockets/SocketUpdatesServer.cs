using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    public class SocketUpdatesServer : SocketUpdatesBase
    {
        public SocketUpdatesServer(ISocketServer server) : base(server)
        {
        }

        protected override async Task AddConnectionAsync(PrimativeConnectionInfo connectionInfo)
        {
            if (ContainsConnection(connectionInfo))
                return;//if the connection info is the SAME, then do not reconnect

            //open a new client to connect to the server of the newly connected client
            var nextClient = new SocketClient();
            await nextClient.OpenConnectionAsync(connectionInfo.IpAddress, connectionInfo.Port);
            _clients.Add(connectionInfo, nextClient);//TODO: thread-saftey

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
            foreach(var clientPair in _clients)
            {
                senders.Add(clientPair.Value.SendAsync(request));
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
            return _clients.ContainsKey(connectionInfo);
        }
        #endregion

        #region Private Fields
        Dictionary<PrimativeConnectionInfo, SocketClient> _clients = new Dictionary<PrimativeConnectionInfo, SocketClient>();
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
