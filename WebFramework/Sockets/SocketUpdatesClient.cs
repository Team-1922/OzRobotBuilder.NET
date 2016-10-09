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
    /// The branch of socket updates which connects to a socket server and sends updates from
    /// its clients to the server
    /// </summary>
    public class SocketUpdatesClient
    {
        /// <summary>
        /// Creates a socket updates client with the given request delegator
        /// </summary>
        /// <param name="requestDelegator">the request delegator to handle requests incoming from the socket updates server</param>
        public SocketUpdatesClient(IRequestDelegator requestDelegator)
        {
            _clientReceiver = new SocketClientUpdatesReceiver(requestDelegator);
            requestDelegator.Data.Propagated += Data_Propagated;
        }

        private async void Data_Propagated(MVVM.Contracts.Events.EventPropagationEventArgs e)
        {
            await _client.SendAsync(new Request() { Body = e.PropertyValue, Method = e.Method, Path = e.PropertyName });
        }

        /// <summary>
        /// Opens a connection to a socket updates server
        /// </summary>
        /// <param name="hostName">the host name of the updates server</param>
        /// <param name="receivingPort">the port on the updates server which this class sends information to</param>
        /// <param name="sendingPort">the port on the updates server which this class receives information from</param>
        /// <returns></returns>
        public async Task OpenConnectionAsync(string hostName, int receivingPort, int sendingPort)
        {
            //open the client connection to the receiving port
            _client = new SocketClient();
            await _client.OpenConnectionAsync(hostName, receivingPort);

            //open the client connection to the sending port
            await _client.OpenConnectionAsync(hostName, sendingPort);

            /*_cts = new CancellationTokenSource();
            var listenerSocket = Utils.MakeSocket();
            listenerSocket.Connect(ipAddress, port);
            _clientNetStream = new NetworkStream(_client);
            var server = new SocketServer(_requestDelegator, )*/
        }

        #region Private Fields
        private SocketClient _client = null;
        private SocketClientUpdatesReceiver _clientReceiver = null;
        #endregion

        #region IDisposable
        private bool disposedValue = false; // To detect redundant calls

        protected void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _client?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        #endregion
    }
}
