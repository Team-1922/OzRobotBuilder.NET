using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// A socket factory for listener connections
    /// </summary>
    public class SocketListenerFactory : IDataSocketFactory
    {
        #region Singleton Implementation
        /// <summary>
        /// The singleton instance of this class
        /// </summary>
        public static SocketListenerFactory Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new SocketListenerFactory();
                return _instance;
            }
        }
        private static SocketListenerFactory _instance = null;
        private SocketListenerFactory() { }
        #endregion

        /// <summary>
        /// Starts a new connection manager for listening connections
        /// </summary>
        /// <param name="socket">the socket to listen on</param>
        /// <param name="requestDelegator">the request delegator to delegate requests from the socket</param>
        /// <param name="token">the cancellation token</param>
        /// <returns></returns>
        public async Task StartSocket(Socket socket, IRequestDelegator requestDelegator, CancellationToken token)
        {
            try
            {
                using (var stream = new NetworkStream(socket, ownsSocket: true))
                {
                    bool completed = false;
                    do
                    {
                        var message = await Utils.SocketReceiveAsync(stream, token);
                        var request = message.ToRequest();
                        if (request.Method == Protocall.Method.Close)
                        {
                            completed = true;
                        }

                        Response response = null;
                        try
                        {
                            response = await requestDelegator.ProcessRequestAsync(message.ToRequest());
                        }
                        catch (Exception)
                        {
                            response = new Response();
                            response.StatusCode = HttpStatusCode.NotFound;
                        }
                        await Utils.SocketSendAsync(stream,
                            new SocketMessage(response), token);

                    } while (!completed && socket.Connected);
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
