using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public interface ISocketClient : IDisposable
    {
        Task OpenConnectionAsync(string hostName, int port);
        Task CloseConnectionAsync();
        Task<Response> SendAsync(Request request);

        IPEndPoint LocalEndPoint { get; }
        IPEndPoint RemoteEndPoint { get; }
    }
}
