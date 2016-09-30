using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class SocketStreamReader : IDisposable
    {
        public void StartListener()
        {

        }

        public void StopListener()
        {            
            //avoid multiple starts
            if (null != _connectionDispatcherTask)
                return;

            var listener = Utils.MakeSocket();
            listener.Bind(new IPEndPoint(IPAddress.Any, _port));
            listener.Listen(backlog: 15);

            //reset the port just in case the port was assigned to something different
            _port = ((IPEndPoint)listener.LocalEndPoint).Port;

            if (_connectionDispatcherTask != null)
                return;
            _connectionDispatcherTask = Task.Run(() =>
            {
                Console.WriteLine("listener task started");
                while (_listeners.Count < _maxClients)
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
                    SocketConnectEvent?.Invoke(new PrimativeConnectionInfo() { IpAddress = ((IPEndPoint)client.RemoteEndPoint).Address.ToString(), Port = ((IPEndPoint)client.RemoteEndPoint).Port });
                }
                listener.Dispose();
                Console.WriteLine("Listener task closing");

            }, _cts.Token);
        }

        #region Private Fields
        public Socket _socket;
        public NetworkStream _networkStream;
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
                    _networkStream?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SocketStreamReader() {
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
