using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public interface ISocketClient : ISocket
    {
        Task OpenConnectionAsync(string hostName, string path, int port);
        Task CloseConnectionAsync();
        Task<Response> SendAsync(Request request);

        /// <summary>
        /// Whether this client just sends, or also receives updates as well
        /// </summary>
        bool SendOnly { get; }
    }
}
