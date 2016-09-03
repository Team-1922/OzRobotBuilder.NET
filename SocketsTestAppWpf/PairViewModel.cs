using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;

namespace SocketsTestApp
{
    class PairViewModel : BindableBase
    {
        public PairViewModel() { }
        public PairViewModel(PairViewModel other)
        {
            Request = new RequestViewModel(other.Request);
            Response = new ResponseViewModel(other.Response);
        }
        public RequestViewModel Request { get; } = new RequestViewModel();
        public ResponseViewModel Response { get; } = new ResponseViewModel();
    }
}
