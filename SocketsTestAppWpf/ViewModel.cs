using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.WebFramework.Sockets;

namespace SocketsTestApp
{
    class ViewModel : BindableBase
    {
        public ObservableCollection<PairViewModel> Requests { get; } = new ObservableCollection<PairViewModel>();

        public RequestViewModel ActiveRequest
        {
            get
            {
                return _activePair.Request;
            }
        }

        public ResponseViewModel ActiveResponse
        {
            get
            {
                return _activePair.Response;
            }
        }

        public IEnumerable<string> MethodValues
        {
            get
            {
                return Enum.GetNames(typeof(Protocall.Method));
            }
        }

        public async Task SendActiveRequest()
        {
            //TODO: actually make the request
            Requests.Add(_activePair);
        }

        #region Private Fields
        PairViewModel _activePair = new PairViewModel();
        #endregion
    }
}
