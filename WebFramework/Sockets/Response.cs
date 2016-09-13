using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; } = "";

        public override string ToString()
        {
            return $"{(int)StatusCode} {Body}";
        }
    }
}
