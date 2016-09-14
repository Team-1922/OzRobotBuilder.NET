using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class Request
    {
        public Request() { }
        public Request(string content)
        {
            var split = Utils.SplitString(content, 3);

            Protocall.Method method;
            if (!Enum.TryParse(split[0], out method))
            {
                throw new ArgumentException("content", "Invalid Protocall.Method");
            }
            Method = method;

            Path = split[1];
            Body = split[2];
        }

        public Protocall.Method Method { get; set; }
        public string Path { get; set; } = "";
        public string Body { get; set; } = "";

        public override string ToString()
        {
            return $"{Method} {Path} {Body}";
        }
    }
}
