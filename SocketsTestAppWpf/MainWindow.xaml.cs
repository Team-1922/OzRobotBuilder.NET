using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SocketsTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel() { NextConnectionUri = "http://localhost:8082" };
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DisableSessionUI();

            try
            {
                //get the selected item
                await ((sender as Button)?.DataContext as SessionViewModel)?.SendActiveRequest();
            }
            finally
            {
                EnableSessionUI();
            }
        }
        private void DisableSessionUI()
        {
            tvSessions.IsEnabled = false;
        }
        private void EnableSessionUI()
        {
            tvSessions.IsEnabled = true;
        }

        private async void ConnectNext(object sender, RoutedEventArgs e)
        {
            DisableConnectionUI();
            try
            {
                await (DataContext as ViewModel).OpenConnection();
            }
            finally
            {
                EnableConnectionUI();
            }
        }
        private void DisableConnectionUI()
        {
            tbConnectUri.IsEnabled = false;
            btnConnect.IsEnabled = false;
        }
        private void EnableConnectionUI()
        {
            tbConnectUri.IsEnabled = true;
            btnConnect.IsEnabled = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox)
            {
                var lb = sender as ListBox;
                if (lb.DataContext is SessionViewModel)
                {
                    var session = lb.DataContext as SessionViewModel;
                    session.SelectedItem = e.AddedItems[0] as PairViewModel;
                }
            }
        }
    }
}
