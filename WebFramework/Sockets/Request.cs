using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class Request
    {
        public string Text
        {
            get
            {
                return Utils.SerializeRequest(this);
            }
            
            set
            {
                var request = Utils.ParseRequest(value);
                Method = request.Method;
                Path = request.Path;
                Body = request.Body;
            }
        }
        public Protocall.Method Method { get; set; }
        public string Path { get; set; }
        public string Body { get; set; }
    }
}
