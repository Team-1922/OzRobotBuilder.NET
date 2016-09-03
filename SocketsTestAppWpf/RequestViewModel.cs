using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.WebFramework.Sockets;

namespace SocketsTestApp
{
    class RequestViewModel : BindableBase
    {

        public string Method
        {
            get
            {
                return _request.Method.ToString();
            }

            set
            {
                var temp = Method;
                SetProperty(ref temp, value);
                _request.Method = (Protocall.Method)Enum.Parse(typeof(Protocall.Method),temp);
            }
        }

        public string Path
        {
            get
            {
                return _request.Path;
            }

            set
            {
                var temp = Path;
                SetProperty(ref temp, value);
                _request.Path = temp;
            }
        }

        public string Body
        {
            get
            {
                return _request.Body;
            }

            set
            {
                var temp = Body;
                SetProperty(ref temp, value);
                _request.Body = temp;
            }
        }

        #region Private Fields
        private Request _request = new Request();
        #endregion
    }
}
