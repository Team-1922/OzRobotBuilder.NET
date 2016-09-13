using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class CompoundSocketServer : SocketServer
    {
        public CompoundSocketServer(IRequestDelegator delegator, string path, int port, int maxClients = 9001) : base(delegator, path, port, maxClients)
        {
        }

        public void AddServer(ISocketServer socketServer)
        {
            _socketServers.Add(socketServer);
        }

        #region SocketServer
        protected override async Task<Response> RouteMessageAsync(SocketMessage message)
        {
            var socket = (from server in _socketServers where server.Path == message.Header.SocketPath select server.RequestDelegator)?.ToList()?.First();
            if (socket == null)
                throw new Exception();

            return await socket.ProcessRequestAsync(message.ToRequest());
        }
        #endregion

        #region Private Fields
        private List<ISocketServer> _socketServers = new List<ISocketServer>();
        #endregion
    }
}
