using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// A socket manager factory which sends out propagation updates to the listeners
    /// </summary>
    public class SocketUpdatesFactory : IDataSocketFactory
    {
        #region Singleton Implementation
        /// <summary>
        /// The singleton instance of this class
        /// </summary>
        public static SocketUpdatesFactory Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new SocketUpdatesFactory();
                return _instance;
            }
        }
        private static SocketUpdatesFactory _instance = null;
        private SocketUpdatesFactory() { }
        #endregion

        /// <summary>
        /// Sends out the <see cref="IEventPropagator.Propagated"/> events to the connected client
        /// </summary>
        /// <param name="socket">the socket to manage the connections of</param>
        /// <param name="requestDelegator">the request delegator to get the data from</param>
        /// <param name="token">the cancellation token</param>
        /// <returns></returns>
        public async Task StartSocket(Socket socket, IRequestDelegator requestDelegator, CancellationToken token)
        {
            try
            {
                using (var stream = new NetworkStream(socket, ownsSocket: true))
                {
                    //add our event handler to the data's propagated event handlers to send out these events to the connected client
                    requestDelegator.Data.Propagated += (MVVM.Contracts.Events.EventPropagationEventArgs e) =>
                    {
                        var senderTask = Utils.SocketSendAsync(stream, new SocketMessage(new Request() { Path = e.PropertyName, Method = e.Method, Body = e.PropertyValue }), token);
                        Utils.SocketReceiveResponseAsync(stream, token); //TODO: how to allow for better handilng of this event handler
                    };
                    //TODO: how to handle these event handlers without closing the socket in the mean time.
                    while (socket.Connected && !token.IsCancellationRequested) ;
                }
                Console.WriteLine("closed stream and client socket");
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
