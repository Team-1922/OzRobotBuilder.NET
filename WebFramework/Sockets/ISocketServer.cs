using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public interface ISocketServer : ISocket
    {
        void StartListener();
        void StopListener();
        IRequestDelegator RequestDelegator { get; }
        
        string Path { get; }
    }
}
