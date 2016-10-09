using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// A factory which produces a task that manages a socket connection
    /// </summary>
    public interface IDataSocketFactory
    {
        /// <summary>
        /// Starts a new connection managing task
        /// </summary>
        /// <param name="socket">the socket to manage the communications of</param>
        /// <param name="requestDelegator">the request delegator to use for socket updates</param>
        /// <param name="token">the cancellation token</param>
        /// <returns></returns>
        Task StartSocket(Socket socket, IRequestDelegator requestDelegator, CancellationToken token);
    }
}
