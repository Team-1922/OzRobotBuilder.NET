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
    public class SocketServer : ISocketServer
    {
        public SocketServer(ushort port, IRequestDelegator delegator)
        {
            _port = port;
            _requestDelegator = delegator;
        }

        #region Private Helper Methods
        private async Task ListenerAsync(Socket socket)
        {
            try
            {
                using (var stream = new NetworkStream(socket, ownsSocket: true))
                {
                    bool completed = false;
                    do
                    {
                        var request = await Utils.SocketReceiveRequestAsync(stream);
                        if(request.Method == Protocall.Method.Close)
                        {
                            completed = true;
                        }
                        await Utils.SocketSendAsync(stream, await _requestDelegator.ProcessRequestAsync(request));

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
        }        
        #endregion

        #region ISocketServer
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

                    _listeners.Add(ListenerAsync(client));

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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    StopListener();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SocketServer() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
