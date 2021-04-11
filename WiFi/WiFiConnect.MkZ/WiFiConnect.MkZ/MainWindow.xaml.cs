using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using Windows.Devices.WiFi;
using Windows.Foundation.Metadata;
using Windows.Networking.Connectivity;
using Windows.Security.Credentials;

namespace WiFiConnect.MkZ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WiFiConnector WiFiConnector = new WiFiConnector();

        public enum eWifiState
        {
            WifiInitialState,
            WifiConnectState,
            WifiConnectingState,
            WifiConnectedState,
        }

        public MainWindow()
        {
            InitializeComponent();

            WiFiConnector.DiscoverySuccededAction = () => 
            { 
                ResultsListView.SelectedIndex = 0;
                if (WiFiConnector.ResultCollection.Count > 0)
                    Title = WiFiConnector.ResultCollection[0].Ssid + " - WiFi Connect";
                else
                    Title = "WiFi Connect";
            };
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
                SwitchToItemState(item, eWifiState.WifiInitialState, true);
            }

            foreach (var item in e.AddedItems)
            {
                var network = item as WiFiNetworkVM;
                UpdateSelectedItemState(network);
            }
        }

        private ListViewItem SwitchToItemState(object dataContext, eWifiState templateKey, bool forceUpdate)
        {
            DataTemplate dataTemplate = FindResource(templateKey.ToString()) as DataTemplate;

            if (forceUpdate)
            {
                ResultsListView.UpdateLayout();
            }
            
            var item = ResultsListView.ItemContainerGenerator.ContainerFromItem(dataContext) as ListViewItem;
            if (item != null)
            {
                item.ContentTemplate = dataTemplate;
            }

            if (forceUpdate)
            {
                ResultsListView.UpdateLayout();
            }
            
            return item;
        }

        private void UpdateSelectedItemState(WiFiNetworkVM network)
        {
            if (network == null)
                return;

            List<ConnectionProfile> connectionProfiles = WiFiConnector.ConnectionProfiles;
            if (WiFiConnector.IsConnected(network.AvailableNetwork, connectionProfiles))
            {
                SwitchToItemState(network, eWifiState.WifiConnectedState, true);
            }
            else
            {
                SwitchToItemState(network, eWifiState.WifiConnectState, true);
            }
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            DoWifiConnect(false);
        }

        private void PushButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            DoWifiConnect(true);
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            var selectedNetwork = ResultsListView.SelectedItem as WiFiNetworkVM;
            if (selectedNetwork == null || WiFiConnector.firstAdapter == null)
            {
                Log("Network not selected");
                return;
            }

            selectedNetwork.Disconnect();
            Thread.Sleep(1000);
            UpdateSelectedItemState(selectedNetwork);
        }

        private async void DoWifiConnect(bool pushButtonConnect)
        {
            var selectedNetwork = ResultsListView.SelectedItem as WiFiNetworkVM;
            if (selectedNetwork == null || WiFiConnector.firstAdapter == null)
            {
                Log("Network not selected");
                return;
            }

            var ssid = selectedNetwork.AvailableNetwork.Ssid;
            if (string.IsNullOrEmpty(ssid))
            {
                if (string.IsNullOrEmpty(selectedNetwork.HiddenSsid))
                {
                    Log("Ssid required for connection to hidden network.");
                    return;
                }
                else
                {
                    ssid = selectedNetwork.HiddenSsid;
                }
            }

            WiFiReconnectionKind reconnectionKind = WiFiReconnectionKind.Manual;
            if (selectedNetwork.ConnectAutomatically)
            {
                reconnectionKind = WiFiReconnectionKind.Automatic;
            }

            Task<WiFiConnectionResult> didConnect = null;
            WiFiConnectionResult result = null;
            if (pushButtonConnect)
            {
                if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5, 0))
                {
                    didConnect = WiFiConnector.firstAdapter.ConnectAsync(selectedNetwork.AvailableNetwork, reconnectionKind, null, string.Empty, WiFiConnectionMethod.WpsPushButton).AsTask();
                }
            }
            else
            {
                PasswordCredential credential = new PasswordCredential();
                if (selectedNetwork.IsEapAvailable && selectedNetwork.UsePassword)
                {
                    if (!String.IsNullOrEmpty(selectedNetwork.Domain))
                    {
                        credential.Resource = selectedNetwork.Domain;
                    }

                    credential.UserName = selectedNetwork.UserName ?? "";
                    credential.Password = selectedNetwork.Password ?? "";
                }
                else if (!String.IsNullOrEmpty(selectedNetwork.Password))
                {
                    credential.Password = selectedNetwork.Password;
                }

                if (selectedNetwork.IsHiddenNetwork)
                {
                    // Hidden networks require the SSID to be supplied
                    didConnect = WiFiConnector.firstAdapter.ConnectAsync(selectedNetwork.AvailableNetwork, reconnectionKind, credential, ssid).AsTask();
                }
                else
                {
                    didConnect = WiFiConnector.firstAdapter.ConnectAsync(selectedNetwork.AvailableNetwork, reconnectionKind, credential).AsTask();
                }
            }

            SwitchToItemState(selectedNetwork, eWifiState.WifiConnectingState, false);

            if (didConnect != null)
            {
                result = await didConnect;
            }

            if (result != null && result.ConnectionStatus == WiFiConnectionStatus.Success)
            {
                Log(string.Format("Successfully connected to {0}.", selectedNetwork.Ssid));

                // refresh the webpage
                //webViewGrid.Visibility = Visibility.Visible;
                //toggleBrowserButton.Content = "Hide Browser Control";
                //refreshBrowserButton.Visibility = Visibility.Visible;

                WiFiConnector.ResultCollection.Remove(selectedNetwork);
                WiFiConnector.ResultCollection.Insert(0, selectedNetwork);
                ResultsListView.SelectedItem = ResultsListView.Items[0];
                ResultsListView.ScrollIntoView(ResultsListView.SelectedItem);

                SwitchToItemState(selectedNetwork, eWifiState.WifiConnectedState, false);
            }
            else
            {
                // Entering the wrong password may cause connection attempts to timeout
                // Disconnecting the adapter will return it to a non-busy state
                if (result.ConnectionStatus == WiFiConnectionStatus.Timeout)
                {
                    WiFiConnector.firstAdapter.Disconnect();
                }
                Log(string.Format("Could not connect to {0}. Error: {1}", selectedNetwork.Ssid, (result != null ? result.ConnectionStatus : WiFiConnectionStatus.UnspecifiedFailure)));
                
                SwitchToItemState(selectedNetwork, eWifiState.WifiConnectState, false);
            }

            // Since a connection attempt was made, update the connectivity level displayed for each
            foreach (var network in WiFiConnector.ResultCollection)
            {
                network.UpdateConnectivityLevelAsync();
            }
        }

        private void Log(string log)
        {
            Debug.WriteLine(log);
            txtLog.Text += (log + "\n");
        }
    }
}
