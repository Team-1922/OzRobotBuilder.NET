using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class ConnectionManager : IConnectionManager
    {
        public ConnectionManager(ushort port, IRequestDelegator delegator)
        {
            _port = port;
            _requestDelegator = delegator;
        }
        
        private Task CommunicateWithClientUsingSocketAsync(Socket socket)
        {
            return Task.Run(() =>
            {
                try
                {
                    using (socket)
                    {

                        bool completed = false;
                        do
                        {
                            try
                            {
                                //read the incoming buffer
                                byte[] readBuffer = new byte[1024];
                                int read = socket.Receive(readBuffer, 0, 1024, SocketFlags.None);
                                string fromClient = Encoding.UTF8.GetString(readBuffer, 0, read);

                                Console.WriteLine($"read {read} bytes: {fromClient}");

                                //is this the close command?
                                if (string.Compare(fromClient, "close", ignoreCase: true) == 0)
                                {
                                    //if so, terminate the connection
                                    completed = true;
                                }

                                var request = new Request();
                                request.Text = fromClient;

                                SendResponse(socket, _requestDelegator.ProcessRequestAsync(request.Method, request.Path, request.Body).Result);
                            }
                            catch(Exception e)
                            {
                                SendResponse(socket, new Response() { StatusCode = HttpStatusCode.InternalServerError, Body = e.Message });
                            }
                        } while (!completed);
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
            });
        }
        private static int SendResponse(Socket socket, Response response)
        {
            if (socket == null)
                return -1;
            if (response.Body == null)
                return -1;
            byte[] writeBuffer = Encoding.UTF8.GetBytes(response.Text);
            return socket.Send(writeBuffer);
        }

        #region IConnectionManager
        public void StartListener()
        {
            var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.ReceiveTimeout = 5000; // receive timout 5 seconds
            listener.SendTimeout = 5000; // send timeout 5 seconds 

            listener.Bind(new IPEndPoint(IPAddress.Any, _port));
            listener.Listen(backlog: 15);
            
            if (_connectionDispatcherTask != null)
                return;
            _connectionDispatcherTask = Task.Run(() =>
            {
                Console.WriteLine("listener task started");
                while (true)
                {
                    if (_cts.Token.IsCancellationRequested)
                    {
                        _cts.Token.ThrowIfCancellationRequested();
                        break;
                    }
                    Console.WriteLine("waiting for accept");
                    Socket client = listener.Accept();
                    if (!client.Connected)
                    {
                        Console.WriteLine("not connected");
                        continue;
                    }
                    Console.WriteLine($"client connected local address {((IPEndPoint)client.LocalEndPoint).Address} and port {((IPEndPoint)client.LocalEndPoint).Port}, remote address {((IPEndPoint)client.RemoteEndPoint).Address} and port {((IPEndPoint)client.RemoteEndPoint).Port}");

                    Task t = CommunicateWithClientUsingSocketAsync(client);

                }
                listener.Dispose();
                Console.WriteLine("Listener task closing");

            }, _cts.Token);

        }
        public void StopListener()
        {
            _cts.Cancel();
        }
        public ushort Port { get { return _port; } }
        #endregion

        #region Private Fields
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private List<Task> _listeners = new List<Task>();
        private Task _connectionDispatcherTask;
        private ushort _port;
        private IRequestDelegator _requestDelegator;
        #endregion
    }
}
