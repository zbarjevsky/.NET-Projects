using Microsoft.Win32;
using MkZ.WPF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
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

        private Settings Settings { get; } = new Settings();

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
                UpdateStatus();
            };

            Settings.Load();

            _timer.Interval = TimeSpan.FromSeconds(3);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    UpdateBuffers(new PingPoint("", DateTime.Now, 0, 0, "Resume"));
                    Thread.Sleep(2000);
                    _timer.Start();
                    break;
                case PowerModes.StatusChange:
                    break;
                case PowerModes.Suspend:
                    _timer.Stop();
                    UpdateBuffers(new PingPoint("", DateTime.Now, 0, 0, "Sleep"));
                    break;
                default:
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WiFiConnector.DiscoverAllWiFiAdapters();
        }

        private string _pingServerUrl = "www.google.com";
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
            PingNetwork(_pingServerUrl);

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

            WiFiNetworkVM.ConnectionInfo connectionInfo = await WiFiNetworkVM.GetConnectivityLevelAsync(WiFiConnector._firstAdapter);

            // Since a connection attempt was made, update the connectivity level displayed for each
            foreach (var network in WiFiConnector.ResultCollection)
            {
                network.UpdateConnectivityLevel(connectionInfo);
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
            UpdateStatus();
        }

        private string ConnectedSsid = "";

        private void UpdateStatus()
        {
            if (WiFiConnector.ResultCollection == null)
                return;

            Task.Factory.StartNew(async () =>
            {
                WiFiNetworkVM.ConnectionInfo connectionInfo = await WiFiNetworkVM.GetConnectivityLevelAsync(WiFiConnector._firstAdapter);

                if(connectionInfo != null)
                {
                    if(ConnectedSsid != connectionInfo.Ssid)
                        Log("Connected Network Change: new: {0}  old: {1}", connectionInfo.Ssid, ConnectedSsid);
                    ConnectedSsid = connectionInfo.Ssid;
                }
                else
                {
                    ConnectedSsid = "";
                }

                foreach (WiFiNetworkVM network in WiFiConnector.ResultCollection)
                {
                    network.UpdateConnectivityLevel(connectionInfo);
                }

                WPF_Helper.ExecuteOnUIThreadWPF(() =>
                {
                    WiFiNetworkVM selectedNetwork = ResultsListView.SelectedItem as WiFiNetworkVM;
                    UpdateItemState(selectedNetwork);
                    return 0;
                });
            });
        }

        private int _disconnectCount = 0;
        private bool _isInPing = false;
        private int[] _timeouts = new int [] { 3000, 4000, 5000 };
        public void PingNetwork(string hostNameOrAddress)
        {
            Task.Factory.StartNew(() =>
            {
                if (_isInPing)
                    return;
                _isInPing = true;

                bool pingStatus = false;
                string status = "";
                int timeout = _timeouts[_disconnectCount]; //

                Controls.PingPoint pingPoint = new Controls.PingPoint(_pingServerUrl, DateTime.Now, 0, timeout);

                using (Ping p = new Ping())
                {
                    byte[] buffer = Encoding.ASCII.GetBytes(hostNameOrAddress);

                    try
                    {
                        PingReply reply = p.Send(hostNameOrAddress, timeout, buffer);
                        pingStatus = (reply.Status == IPStatus.Success);
                        status = "N/A";
                        if (!pingStatus)
                        {
                            pingPoint.Error = timeout;
                            pingPoint.Message = reply.Status.ToString();
                            
                            status = string.Format("Ping: '{0}' Status: {1}: {2}", hostNameOrAddress, reply.Status, timeout);
                            UpdateChart(pingPoint, status);
                            UpdateChart(new PingPoint(_pingServerUrl, DateTime.Now, 0, timeout, pingPoint.Message), status);
                        }
                        else //ping ok
                        {
                            pingPoint.Value = reply.RoundtripTime;
                            pingPoint.Error = 0;

                            status = string.Format("Ping: '{0}' Status: {1}, Time: {2} ms", hostNameOrAddress, reply.Status, reply.RoundtripTime);
                            UpdateChart(pingPoint, status);
                        }
                    }
                    catch (Exception err)
                    {
                        status = string.Format("Ping: '{0}' Error: {1}", hostNameOrAddress, err.Message);
                        if (err.InnerException != null)
                            status += ", " + err.InnerException.Message;

                        Log(status);

                        pingStatus = false;
                        pingPoint.Error = timeout;
                        pingPoint.Message = err.Message;
                        UpdateChart(pingPoint, status);
                        UpdateChart(new PingPoint(_pingServerUrl, DateTime.Now, 0, timeout, err.Message), status);
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

                _isInPing = false;
            });

        }

        private void UpdateChart(PingPoint pingPoint, string status)
        {
            WPF_Helper.ExecuteOnUIThreadWPF(() =>
            {
                if (cmbBufferIdx == null)
                    return 0;

                DateTime now = DateTime.Now;

                UpdateBuffers(pingPoint);

                List<PingPoint> buffer = Settings.GetBuffer(cmbBufferIdx.SelectedIndex == 0);
                System.Drawing.Color color = cmbBufferIdx.SelectedIndex == 0 ? System.Drawing.Color.Blue : System.Drawing.Color.Goldenrod;

                _chart.UpdateChart(buffer, "Pings", color, "ms");

                TimeSpan delta = DateTime.Now - now;

                _txtStatus.Text = status;
                _txtStatus.Text += string.Format(", Processing time: {0:0.000} ms, Points: {1}", 
                    delta.TotalMilliseconds, buffer.Count);

                //Debug.WriteLine(string.Format("Update time: {0:0.000} ms", delta.TotalMilliseconds));

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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Settings.BufferPings.Clear();
            _chart.UpdateChart(Settings.GetBuffer(cmbBufferIdx.SelectedIndex == 0), "Pings", System.Drawing.Color.Blue, "ms");
        }

        private void ComboBoxBufferSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart(null, "Update");
        }

        private TimeSpan _maxBufferSize = TimeSpan.FromHours(1);
        private void UpdateBuffers(PingPoint ping)
        {
            int hours = (int)Math.Pow(2, cmbBufferSize.SelectedIndex);
            _maxBufferSize = TimeSpan.FromHours(hours);

            Settings.UpdateBuffers(ping, _maxBufferSize);
        }

        private void cmbServerUrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            _pingServerUrl = cmbServerUrl.Text;
        }
    }
}
