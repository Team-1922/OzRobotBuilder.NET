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
                return _text;
            }
            
            set
            {
                _text = value;
                var splitText = _text.Split(new char[] { '\n' }, StringSplitOptions.None);
                if (splitText.Length != 3)
                    throw new ArgumentException("Invalid Number of Parameters for Request");
                if (!Enum.TryParse(splitText[0], out _method))
                    throw new ArgumentException("Invalid Method");
                //TODO: make these SAFER
                Path = splitText[1];
                Body = splitText[2];
            }
        }
        public Protocall.Method Method
        {
            get
            {
                return _method;
            }

            private set
            {
                _method = value;
            }
        }
        public string Path { get; private set; }
        public string Body { get; private set; }

        private string _text = "";
        private Protocall.Method _method;
    }
}
