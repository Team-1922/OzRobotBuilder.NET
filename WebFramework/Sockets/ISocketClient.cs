using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// The interface for classes which initiate a socket connection
    /// </summary>
    public interface ISocketClient : IDisposable
    {
        /// <summary>
        /// Opens the connection to the server
        /// </summary>
        /// <param name="hostName">the host name of the server</param>
        /// <param name="port">the port of the server to connect to</param>
        /// <returns></returns>
        Task OpenConnectionAsync(string hostName, int port);
        /// <summary>
        /// Closes the connection to the server
        /// </summary>
        /// <returns></returns>
        Task CloseConnectionAsync();

        /// <summary>
        /// The local endpoint of the connection
        /// </summary>
        IPEndPoint LocalEndPoint { get; }
        /// <summary>
        /// The remote endpoint of the connection
        /// </summary>
        IPEndPoint RemoteEndPoint { get; }
    }
}
