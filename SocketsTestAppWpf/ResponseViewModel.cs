using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.WebFramework.Sockets;

namespace SocketsTestApp
{
    class ResponseViewModel : BindableBase
    {

        public HttpStatusCode StatusCode
        {
            get
            {
                return _response.StatusCode;
            }

            set
            {
                var temp = StatusCode;
                SetProperty(ref temp, value);
                _response.StatusCode = temp;
            }
        }

        public string Body
        {
            get
            {
                return _response.Body;
            }
            
            set
            {
                var temp = Body;
                SetProperty(ref temp, value);
                _response.Body = temp;
            }
        }

        #region Private Fields
        private Response _response = new Response();
        #endregion
    }
}
