using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public interface ISocketServer : IDisposable
    {
        void StartListener();
        void StopListener();
        IRequestDelegator RequestDelegator { get; }
        event SocketConnectEvent SocketConnectEvent;
        IDataSocketFactory SocketFactory { get; }

        int Port { get; }
    }
}
