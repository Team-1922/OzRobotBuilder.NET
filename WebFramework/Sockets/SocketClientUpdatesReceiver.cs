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
    /// A Socket client which connects to a socket updates server and leaves the connection
    /// open to receive socket updates from
    /// </summary>
    /// <remarks>
    /// This is still considered a client, becuase this class must initiate the connection, however afterwards
    /// no longer initiates any requests
    /// </remarks>
    public class SocketClientUpdatesReceiver : ISocketClient
    {
        /// <summary>
        /// Initializes a SocketCLientUpdatesReceiver from a <see cref="IRequestDelegator"/>
        /// </summary>
        /// <param name="requestDelegator">the request delegator to delegate incoming requests</param>
        public SocketClientUpdatesReceiver(IRequestDelegator requestDelegator)
        {
            _requestDelegator = requestDelegator;
        }
        
        /// <summary>
        /// Initializes a SocketClientUpdatesReceiver from a <see cref="IHierarchialAccessRoot"/> using the <see cref="RequestDelegator"/> class
        /// </summary>
        /// <param name="data">the data to update upon request</param>
        public SocketClientUpdatesReceiver(IHierarchialAccessRoot data)
        {
            // do not propagate incoming requests, becuase they are being sent from a server and we do not
            //      want to send the same requests back
            _requestDelegator = new RequestDelegator(data, false);
        }

        #region ISocketClient
        /// <summary>
        /// This data member is irrelevent for this class, for this class is receive only
        /// </summary>
        public bool SendOnly { get { return false; } }

        /// <summary>
        /// The local end point of the connection
        /// </summary>
        public IPEndPoint LocalEndPoint
        {
            get
            {
                return (IPEndPoint)_listenerSocket?.LocalEndPoint;
            }
        }

        /// <summary>
        /// The remote endpoint of the connection
        /// </summary>
        public IPEndPoint RemoteEndPoint
        {
            get
            {
                return (IPEndPoint)_listenerSocket?.RemoteEndPoint;
            }
        }

        /// <summary>
        /// Closes the connection to the updates server
        /// </summary>
        /// <returns></returns>
        public async Task CloseConnectionAsync()
        {
            _cts.Cancel();
            _listenerSocket?.Dispose();

            _listenerSocket = null;
            _cts = null;
        }
        /// <summary>
        /// Initiates the connection to the updates server
        /// </summary>
        /// <param name="hostName">the host name of the updates server</param>
        /// <param name="port">the port of the updates server</param>
        /// <returns></returns>
        public async Task OpenConnectionAsync(string hostName, int port)
        {
            if (_listenerSocket != null)
                throw new Exception("Client Already Connected!");

            IPHostEntry ipHost = await Dns.GetHostEntryAsync(hostName);
            IPAddress ipAddress = ipHost.AddressList.Where(address => address.AddressFamily == AddressFamily.InterNetwork).First();
            if (ipAddress == null)
            {
                Console.WriteLine("no IPv4 address");
                return;
            }
            _cts = new CancellationTokenSource();
            _listenerSocket = Utils.MakeSocket();
            _listenerSocket.Connect(ipAddress, port);

            //start the listener task
            _listenerTask = SocketListenerFactory.Instance.StartSocket(_listenerSocket, _requestDelegator, _cts.Token);
        }

        /// <summary>
        /// This should never be called becuase this client only receives requests
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Response> SendAsync(Request request)
        {
            throw new InvalidOperationException("Sending Requests is Not Supported by SocketClientUpdpatesReceiver; you may want to use SocketClient instead!");
        }
        #endregion

        #region Private Fields
        private CancellationTokenSource _cts;
        private Socket _listenerSocket;
        private Task _listenerTask;
        private IRequestDelegator _requestDelegator;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    CloseConnectionAsync().Wait();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SocketClientUpdatesReceiver() {
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
        #endregion
    }
}
