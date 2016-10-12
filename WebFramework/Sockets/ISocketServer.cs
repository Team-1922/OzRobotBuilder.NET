using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// A class which listens for connection requests and creates connections for each, still remaining open for new connection requests
    /// </summary>
    public interface ISocketServer : IDisposable
    {
        /// <summary>
        /// starts the connection listener
        /// </summary>
        void StartListener();
        /// <summary>
        /// stop the connection listener; this also should terminate any open connections
        /// </summary>
        void StopListener();

        /// <summary>
        /// The request delegator to handle the requests
        /// </summary>
        IRequestDelegator RequestDelegator { get; }
        /// <summary>
        /// The class which branches off a new task for each connection
        /// </summary>
        IDataSocketFactory SocketFactory { get; }

        /// <summary>
        /// the port the listener is on
        /// </summary>
        int Port { get; }
    }
}
