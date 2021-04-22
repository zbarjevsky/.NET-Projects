using MkZ.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Windows.Threading;
using WiFiConnect.MkZ.Controls;
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
    public partial class MainWindow : Window, ILog
    {
        private WiFiConnector WiFiConnector { get; }
        private FileSystemWatchHelper _fileSystemWatch { get; }

        private List<Controls.ChartPoint> _bufferPings = new List<Controls.ChartPoint>();

        public enum eWifiState
        {
            WifiInitialState,
            WifiConnectState,
            WifiConnectingState,
            WifiConnectedState,
        }

        private DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Background);

        public MainWindow()
        {
            InitializeComponent();

            WiFiConnector = new WiFiConnector(this);

            _fileSystemWatch = new FileSystemWatchHelper(this, @"c:\Windows\System32\drivers\", "*");
            //_fileSystemWatch = new FileSystemWatchHelper(@"c:\Windows\SysNative\drivers\", "*");

            WiFiConnector.DiscoverySuccededAction = () => 
            {
                WPF_Helper.ExecuteOnUIThreadWPF(() =>
                {
                    cmbSort_SelectionChanged(this, null);
                    ResultsListView.ItemsSource = WiFiConnector.ResultCollection;
                    ResultsListView.SelectedIndex = 0;

                    if (WiFiConnector.ResultCollection.Count > 0)
                        Title = WiFiConnector.ResultCollection[0].Ssid + " - WiFi Connect";
                    else
                        Title = "WiFi Connect";

                    animatedImage.Visibility = Visibility.Collapsed;

                    return 0;
                });
            };

            WiFiConnector.AvailableNetworksChanged = (adapter, o) =>
            {
                WPF_Helper.ExecuteOnUIThreadWPF(() =>
                {
                    WiFiNetworkVM selectedNetwork = UpdateStatus();
                    Log("AvailableNetworksChanged: {0}", selectedNetwork?.Ssid);
                    return 0;
                });
            };

            _timer.Interval = TimeSpan.FromSeconds(3);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
            PingNetwork("www.amazon.com");

            if(_disconnectCount>2)
            {
                _disconnectCount = 0;
                if (WiFiConnector.ResultCollection.Count > 0)
                {
                    WiFiNetworkVM selectedNetwork = WiFiConnector.ResultCollection[0];
                    Log("Ping not responding more than 2 times: {0} on {1}", "www.amazon.com", selectedNetwork.Ssid);

                    if (selectedNetwork.ConnectivityLevel == "InternetAccess")
                    {
                        Disconnect(selectedNetwork);
                        DoWifiConnect(selectedNetwork, false);
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                UpdateItemState(network);
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

            UpdateConnectedItem(dataContext as WiFiNetworkVM, templateKey);

            return item;
        }

        private void UpdateConnectedItem(WiFiNetworkVM vm, eWifiState templateKey)
        {
            DataTemplate dataTemplate = FindResource(templateKey.ToString()) as DataTemplate;

            _connectedItem.UpdateLayout();
            _connectedItem.ContentTemplate = dataTemplate;
            _connectedItem.DataContext = new WiFiNetworkVM(vm);
            _connectedItem.UpdateLayout();
            vm.NotifyPropertyChangedAll();
        }

        private void UpdateItemState(WiFiNetworkVM network)
        {
            if (network == null)
                return;

            List<ConnectionProfile> connectionProfiles = WiFiConnector.ConnectionProfiles;
            if (WiFiConnector.IsConnected(network.Network, connectionProfiles))
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
            WiFiNetworkVM selectedNetwork = ResultsListView.SelectedItem as WiFiNetworkVM;
            DoWifiConnect(selectedNetwork, false);
        }

        private void WpsButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            WiFiNetworkVM selectedNetwork = ResultsListView.SelectedItem as WiFiNetworkVM;
            DoWifiConnect(selectedNetwork, true);
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            WiFiNetworkVM selectedNetwork = ResultsListView.SelectedItem as WiFiNetworkVM;
            Disconnect(selectedNetwork);
        }

        private void Disconnect(WiFiNetworkVM network)
        {
            if (network == null || WiFiConnector._firstAdapter == null)
            {
                Log("Network not selected");
                return;
            }

            network.Disconnect();
            Thread.Sleep(500);
            UpdateItemState(network);
        }

        private async void DoWifiConnect(WiFiNetworkVM selectedNetwork, bool pushButtonConnect)
        {
            if (selectedNetwork == null || WiFiConnector._firstAdapter == null)
            {
                Log("Network not selected");
                return;
            }

            var ssid = selectedNetwork.Network.Ssid;
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
                    didConnect = WiFiConnector._firstAdapter.ConnectAsync(selectedNetwork.Network, reconnectionKind, null, string.Empty, WiFiConnectionMethod.WpsPushButton).AsTask();
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
                    didConnect = WiFiConnector._firstAdapter.ConnectAsync(selectedNetwork.Network, reconnectionKind, credential, ssid).AsTask();
                }
                else
                {
                    didConnect = WiFiConnector._firstAdapter.ConnectAsync(selectedNetwork.Network, reconnectionKind, credential).AsTask();
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
                    WiFiConnector._firstAdapter.Disconnect();
                }
                Log(string.Format("Could not connect to {0}. Error: {1}", selectedNetwork.Ssid, (result != null ? result.ConnectionStatus : WiFiConnectionStatus.UnspecifiedFailure)));
                
                SwitchToItemState(selectedNetwork, eWifiState.WifiConnectState, false);
            }

            // Since a connection attempt was made, update the connectivity level displayed for each
            foreach (var network in WiFiConnector.ResultCollection)
            {
                var task = network.UpdateConnectivityLevelAsync();
            }
        }

        public void Log(string format, params object[] args)
        {
            string time = DateTime.Now.ToString("HH:mm:ss.fff");
            string log = time + " - " + string.Format(format, args) + "\n";

            Debug.Write(log);

            WPF_Helper.ExecuteOnUIThreadWPF(() => 
            { 
                txtLog.Text = log + txtLog.Text;
                if (txtLog.Text.Length > 16000)
                    txtLog.Text = txtLog.Text.Substring(0, 16000);
                return 0; 
            });
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WiFiConnector == null)
                return;

            if (cmbSort.SelectedItem is ComboBoxItem item)
                if (item.Content is SortOrder order)
                {
                    WiFiConnector.SortResults(order);
                    ResultsListView.ItemsSource = WiFiConnector.ResultCollection;
                }
        }

        private void btnUpdateList_Click(object sender, RoutedEventArgs e)
        {
            animatedImage.Visibility = Visibility.Visible;
            WiFiConnector.DiscoverAllWiFiAdapters();
        }

        private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            WiFiNetworkVM selectedNetwork = UpdateStatus();
            Log("btnUpdateStatus_Click: {0}", selectedNetwork?.Ssid);
        }

        private WiFiNetworkVM UpdateStatus()
        {
            if (WiFiConnector.ResultCollection == null)
                return null;

            foreach (WiFiNetworkVM network in WiFiConnector.ResultCollection)
            {
                var task = network.UpdateConnectivityLevelAsync();
            }

            WiFiNetworkVM selectedNetwork = ResultsListView.SelectedItem as WiFiNetworkVM;
            UpdateItemState(selectedNetwork);

            return selectedNetwork;
        }


        private int _disconnectCount = 0;
        private bool _isInPing = false;
        public void PingNetwork(string hostNameOrAddress)
        {
            Task.Factory.StartNew(() =>
            {
                if (_isInPing)
                    return;
                _isInPing = true;

                bool pingStatus = false;
                string status = "";
                Controls.ChartPoint pingPoint = new Controls.ChartPoint(DateTime.Now, 0);

                using (Ping p = new Ping())
                {
                    byte[] buffer = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                    const int timeout = 3000; // 3s

                    try
                    {
                        PingReply reply = p.Send(hostNameOrAddress, timeout, buffer);
                        pingStatus = (reply.Status == IPStatus.Success);
                        status = string.Format("Ping: '{0}' Status: {1}, Time: {2} ms", hostNameOrAddress, reply.Status, reply.RoundtripTime);
                        if (!pingStatus)
                        {
                            pingPoint.Value = timeout;
                            status = string.Format("Ping: '{0}' Status: {1}: {2}", hostNameOrAddress, reply.Status, timeout);
                        }
                        else
                        {
                            pingPoint.Value = reply.RoundtripTime;
                        }

                        WPF_Helper.ExecuteOnUIThread(() => { _txtStatus.Text = status; return 0; });
                    }
                    catch (Exception err)
                    {
                        status = string.Format("Ping: '{0}' Error: {1}", hostNameOrAddress, err.Message);
                        if (err.InnerException != null)
                            status += ", " + err.InnerException.Message;

                        Log(status);
                        WPF_Helper.ExecuteOnUIThread(() => { _txtStatus.Text = status; return 0; });
                        pingStatus = false;
                        pingPoint.Value = timeout;
                    }
                }

                if (pingStatus)
                {
                    if (_disconnectCount > 0)
                    {
                        if (_disconnectCount > 1)
                            Log(status);

                        _disconnectCount = 0;
                    }
                }
                else
                {
                    _disconnectCount++;
                    if(_disconnectCount>1)
                        Log("Error {0}, count: {1}", status, _disconnectCount);
                }

                UpdateChart(pingPoint);

                _isInPing = false;
            });

        }

        private void UpdateChart(ChartPoint pingPoint)
        {
            WPF_Helper.ExecuteOnUIThreadWPF(() =>
            {
                _bufferPings.Add(pingPoint);
                _chart.UpdateChart(_bufferPings, "Pings", System.Drawing.Color.Blue, "ms");

                return 0;
            });
        }

        private void ChkTimer_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (_chkTimer.IsChecked.Value)
                _timer.Start();
            else
                _timer.Stop();
        }
    }
}
