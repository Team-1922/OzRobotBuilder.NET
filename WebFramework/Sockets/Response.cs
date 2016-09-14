using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class Response
    {
        public Response() { }
        public Response(string content)
        {
            var split = Utils.SplitString(content, 2);

            int statusCode;
            if (!int.TryParse(split[0], out statusCode))
            {
                throw new ArgumentException("content", "Invalid HttpStatusCode");
            }
            StatusCode = (System.Net.HttpStatusCode)statusCode;

            Body = split[1];
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; } = "";

        public override string ToString()
        {
            return $"{(int)StatusCode} {Body}";
        }
    }
}
