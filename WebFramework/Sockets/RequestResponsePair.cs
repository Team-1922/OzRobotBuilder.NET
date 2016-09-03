using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class RequestResponsePair
    {
        public Request Request { get; set; } = new Request();
        public Response Response { get; set; } = new Response();
    }
}
