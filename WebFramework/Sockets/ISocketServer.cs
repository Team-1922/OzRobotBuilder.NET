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
        ushort Port { get; }
    }
}
