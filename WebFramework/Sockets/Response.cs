using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class Response
    {
        public string Text
        {
            get
            {
                return Utils.SerializeResponse(this);
            }

            set
            {
                var response = Utils.ParseResponse(value);
                StatusCode = response.StatusCode;
                Body = response.Body;
            }
        }
        public HttpStatusCode StatusCode { get; set; }
        public string Body { get; set; }
    }
}
