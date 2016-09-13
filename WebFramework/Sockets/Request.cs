using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class Request
    {
        public Protocall.Method Method { get; set; }
        public string Path { get; set; } = "";
        public string Body { get; set; } = "";

        public override string ToString()
        {
            return $"{Method} {Path} {Body}";
        }
    }
}
