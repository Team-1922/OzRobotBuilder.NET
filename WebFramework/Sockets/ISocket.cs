using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public interface ISocket : IDisposable
    {
        IPEndPoint LocalEndPoint { get; }
        IPEndPoint RemoteEndPoint { get; }
    }
}
