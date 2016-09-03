using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1922.MVVM.Framework;
using Team1922.WebFramework.Sockets;

namespace SocketsTestApp
{
    class SessionViewModel : BindableBase, IDisposable
    {
        public SessionViewModel(string connectedUri)
        {
            try
            {
                _connectedUri = new Uri(connectedUri);
            }
            catch(UriFormatException e)
            {
                throw new Exception("Improperly Formatted URI");
            }
        }

        public async Task Connect()
        {
            if(!_connected)
            {
                await _socketClient.OpenConnectionAsync(_connectedUri.Host, _connectedUri.Port);
                _connected = true;
            }
        }

        public ObservableCollection<PairViewModel> Requests { get; } = new ObservableCollection<PairViewModel>();

        /*public RequestViewModel ActiveRequest
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
        }*/

        public IEnumerable<string> MethodValues
        {
            get
            {
                return Enum.GetNames(typeof(Protocall.Method));
            }
        }

        public PairViewModel SelectedItem
        {
            get
            {
                return _activePair;
            }

            set
            {
                //make sure that we get a NEW version of the request when we look at the old one so we don't rewrite history
                SetProperty(ref _activePair, new PairViewModel(value));
            }
        }

        public string ConnectedUri
        {
            get
            {
                return _connectedUri.ToString();
            }
        }

        public async Task SendActiveRequest()
        {
            //TODO: actually make the request
            Requests.Add(_activePair);
            //this is like a copy-constructor
            SelectedItem = new PairViewModel(_activePair);
        }

        #region Private Fields
        private PairViewModel _activePair = new PairViewModel();
        private SocketClient _socketClient = new SocketClient();
        private bool _connected = false;
        private Uri _connectedUri;
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _socketClient?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SessionViewModel() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
