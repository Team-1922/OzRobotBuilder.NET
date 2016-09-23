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
    public class SocketListenerFactory : IDataSocketFactory
    {
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
