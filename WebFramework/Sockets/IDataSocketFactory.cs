using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public interface IDataSocketFactory
    {
        Task StartSocket(Socket socket, IRequestDelegator requestDelegator, CancellationToken token);
    }
}
