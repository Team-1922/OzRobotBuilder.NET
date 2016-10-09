using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    /// <summary>
    /// A pair with the request and response
    /// </summary>
    public class RequestResponsePair
    {
        /// <summary>
        /// The Request of the pair
        /// </summary>
        public Request Request { get; set; } = new Request();
        /// <summary>
        /// The Response of the pair
        /// </summary>
        public Response Response { get; set; } = new Response();
    }
}
