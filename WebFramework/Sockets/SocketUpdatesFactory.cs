using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class SocketUpdatesFactory : IDataSocketFactory
    {
        public async Task StartSocket(Socket socket, IRequestDelegator requestDelegator, CancellationToken token)
        {
            try
            {
                using (var stream = new NetworkStream(socket, ownsSocket: true))
                {
                    requestDelegator.Data.Propagated += (MVVM.Contracts.Events.EventPropagationEventArgs e) =>
                    {
                        var senderTask = Utils.SocketSendAsync(stream, new SocketMessage(new Request() { Path = e.PropertyName, Method = e.Method, Body = e.PropertyValue }), token);
                        Utils.SocketReceiveResponseAsync(stream, token); //TODO: how to allow for better handilng of this event handler
                    };
                    //TODO: how to handle these event handlers without closing the socket in the mean time.
                    while (socket.Connected) ;
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
