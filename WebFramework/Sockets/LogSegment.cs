using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team1922.WebFramework.Sockets
{
    public class LogSegment
    {
        public LogSegment() { }
        public LogSegment(string content)
        {
            Text = content;
        }
        
        public string Text { get; set; } = "";

        public override string ToString()
        {
            return Text;
        }
    }
}
