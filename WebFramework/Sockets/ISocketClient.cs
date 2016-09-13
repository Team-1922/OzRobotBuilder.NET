using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public interface ISocketClient : IDisposable
    {
        Task OpenConnectionAsync(string hostName, string path, int port);
        Task CloseConnectionAsync();
        Task<Response> SendAsync(Request request);

        IPEndPoint LocalEndPoint { get; }
        IPEndPoint RemoteEndPoint { get; }

        /// <summary>
        /// Whether this client just sends, or also receives updates as well
        /// </summary>
        bool SendOnly { get; }
    }
}
