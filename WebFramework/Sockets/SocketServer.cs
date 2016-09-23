﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Team1922.MVVM.Contracts;

namespace Team1922.WebFramework.Sockets
{
    public class PrimativeConnectionInfo : IEquatable<PrimativeConnectionInfo>
    {
        public string IpAddress = "";
        public int Port = -1;

        public bool Equals(PrimativeConnectionInfo other)
        {
            return (other.IpAddress == IpAddress && other.Port == Port);
        }
    }
    public delegate void SocketConnectEvent(PrimativeConnectionInfo connectionInfo);

    public class SocketServer : ISocketServer
    {
        public SocketServer(IRequestDelegator delegator, int port = 0, int maxClients = 9001)
        {
            _requestDelegator = delegator;
            _port = port;
            _maxClients = maxClients;
        }

        public SocketServer(RequestDelegator delegator, IDataSocketFactory socketFactory, int port = 0, int maxClients = 9001) : this(delegator, port, maxClients)
        {
            if (socketFactory == null)
                throw new ArgumentNullException("socketFactory", "Socket Factory Must Not Be Null");
            SocketFactory = socketFactory;
        }
        
        #region Private Helper Methods

        #endregion

        #region ISocketServer
        public IRequestDelegator RequestDelegator
        {
            get
            {
                return _requestDelegator;
            }
        }
        public event SocketConnectEvent SocketConnectEvent;
        public void StartListener()
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
                    
                    _listeners.Add(SocketFactory.StartSocket(client, _requestDelegator, _cts.Token));
                    //SocketConnectEvent?.Invoke(new PrimativeConnectionInfo() { IpAddress = ((IPEndPoint)client.RemoteEndPoint).Address.ToString(), Port = ((IPEndPoint)client.RemoteEndPoint).Port });
                }
                listener.Dispose();
                Console.WriteLine("Listener task closing");

            }, _cts.Token);

        }
        public void StopListener()
        {
            _cts.Cancel();
        }
        public int Port { get { return _port; } }
        public IDataSocketFactory SocketFactory { get; }
        #endregion

        #region Private Fields
        private CancellationTokenSource _cts = new CancellationTokenSource();
        private List<Task> _listeners = new List<Task>();
        private Task _connectionDispatcherTask;
        private IRequestDelegator _requestDelegator;
        private int _port;
        private int _maxClients;
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
