using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Team1922.MVVM.Framework;
using Team1922.WebFramework.Sockets;

namespace SocketsTestApp
{
    class ViewModel : BindableBase
    {
        public ObservableCollection<SessionViewModel> Sessions { get; } = new ObservableCollection<SessionViewModel>();
        public string NextConnectionUri
        {
            get
            {
                return _nextConnectionUri;
            }

            set
            {
                SetProperty(ref _nextConnectionUri, value);
            }
        }

        public async Task OpenConnection()
        {
            SessionViewModel nextSession = null;
            try
            {
                nextSession = new SessionViewModel(NextConnectionUri);                
                await nextSession.Connect();
                Sessions.Add(nextSession);
                
            }
            catch (Exception e)
            {
                nextSession.Dispose();
                MessageBox.Show($"Failed To Connect: {e.Message}");
            }
        }

        #region Private Fields
        private string _nextConnectionUri = "";
        #endregion
    }
}
