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


using WiFiConnect.MkZ.WiFi;

namespace WiFiConnect.MkZ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WiFiConnector WiFiConnector = new WiFiConnector();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResultsListView.ItemsSource = WiFiConnector.ResultCollection;

            WiFiConnector.DiscoverAllWiFiAdapters();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            WiFiConnector.ResultCollection.Clear();
            WiFiConnector.DiscoverAllWiFiAdapters();
        }

        private void ResultsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedNetwork = ResultsListView.SelectedItem as WiFiNetworkVM;
            if (selectedNetwork == null)
            {
                return;
            }

            foreach (var item in e.RemovedItems)
            {
                SwitchToItemState(item, "WifiInitialState", true);
            }

            foreach (var item in e.AddedItems)
            {
                var network = item as WiFiNetworkVM;
                SetSelectedItemState(network);
            }
        }

        private ListViewItem SwitchToItemState(object dataContext, string templateKey, bool forceUpdate)
        {
            DataTemplate dataTemplate = FindResource(templateKey) as DataTemplate;

            if (forceUpdate)
            {
                ResultsListView.UpdateLayout();
            }
            var item = ResultsListView.ItemContainerGenerator.ContainerFromItem(dataContext) as ListViewItem;
            if (item != null)
            {
                item.ContentTemplate = dataTemplate;
            }
            return item;
        }

        private void SetSelectedItemState(WiFiNetworkVM network)
        {
            if (network == null)
                return;

            if (WiFiConnector.IsConnected(network.AvailableNetwork))
            {
                SwitchToItemState(network, "WifiConnectedState", true);
            }
            else
            {
                SwitchToItemState(network, "WifiConnectState", true);
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PushButtonConnect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
