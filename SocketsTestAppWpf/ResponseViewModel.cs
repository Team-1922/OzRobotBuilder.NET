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
        public ResponseViewModel()
        {
        }

        public ResponseViewModel(ResponseViewModel other)
        {
            _response.StatusCode = other._response.StatusCode;
            _response.Body = other._response.Body;
        }

        public void SetResponse(Response response)
        {
            _response = response;
            OnPropertyChanged("StatusCode");
            OnPropertyChanged("Body");
        }

        public string StatusCode
        {
            get
            {
                return _response.StatusCode.ToString();
            }
        }

        public string Body
        {
            get
            {
                return _response.Body;
            }
        }

        #region Private Fields
        private Response _response = new Response();
        #endregion
    }
}
