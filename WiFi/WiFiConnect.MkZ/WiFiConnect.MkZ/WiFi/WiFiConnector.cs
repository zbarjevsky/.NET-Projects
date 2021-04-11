using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Networking.Connectivity;

namespace WiFiConnect.MkZ.WiFi
{
    //https://docs.microsoft.com/en-us/samples/microsoft/windows-iotcore-samples/wifi-connector/
    //https://www.thomasclaudiushuber.com/2019/04/26/calling-windows-10-apis-from-your-wpf-application/
    //https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/desktop-to-uwp-enhance
    public class WiFiConnector
    {
        public WiFiAdapter firstAdapter { get; private set; }
        private List<ConnectionProfile> _connectionProfiles;

        public ObservableCollection<WiFiNetworkVM> ResultCollection { get; } = new ObservableCollection<WiFiNetworkVM>();

        public Action DiscoverySuccededAction = () => { };

        public async void DiscoverAllWiFiAdapters()
        {
            // RequestAccessAsync must have been called at least once by the app before using the API
            // Calling it multiple times is fine but not necessary
            // RequestAccessAsync must be called from the UI thread
            var access = await WiFiAdapter.RequestAccessAsync();
            if (access != WiFiAccessStatus.Allowed)
            {
                Debug.WriteLine("Access denied");
            }
            else
            {
                var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
                if (result.Count >= 1)
                {
                    firstAdapter = await WiFiAdapter.FromIdAsync(result[0].Id);
                    ScanWiFi(firstAdapter);
                    //var button = new Button();
                    //button.Content = string.Format("Scan Available Wifi Networks");
                    //button.Click += Button_Click;
                    //Buttons.Children.Add(button);
                }
                else
                {
                    firstAdapter = null;
                    Debug.WriteLine("No WiFi Adapters detected on this machine.");
                }
            }
        }

        private async void ScanWiFi(WiFiAdapter firstAdapter)
        {
            try
            {
                await firstAdapter.ScanAsync();
            }
            catch (Exception err)
            {
                Debug.WriteLine(String.Format("Error scanning WiFi adapter: 0x{0:X}: {1}", err.HResult, err.Message));
                return;
            }

            DisplayNetworkReport(firstAdapter.NetworkReport);
        }

        private void DisplayNetworkReport(WiFiNetworkReport report)
        {
            Debug.WriteLine(string.Format("Network Report Timestamp: {0}", report.Timestamp));

            ResultCollection.Clear();
            ConcurrentDictionary<string, WiFiNetworkVM> dictionary = new ConcurrentDictionary<string, WiFiNetworkVM>();

            List<ConnectionProfile> connectionProfiles = ConnectionProfiles;

            foreach (var network in report.AvailableNetworks)
            {
                var item = new WiFiNetworkVM(network, firstAdapter);
                if (!String.IsNullOrEmpty(network.Ssid))
                {
                    dictionary.TryAdd(network.Ssid, item);
                }
                else
                {
                    string bssid = network.Bssid.Substring(0, network.Bssid.LastIndexOf(":"));
                    dictionary.TryAdd(bssid, item);
                }
            }

            var values = dictionary.Values;
            foreach (var item in values)
            {
                item.Update();
                if (IsConnected(item.AvailableNetwork, connectionProfiles))
                {
                    ResultCollection.Insert(0, item);
                    //ResultsListView.SelectedItem = ResultsListView.Items[0];
                    //ResultsListView.ScrollIntoView(ResultsListView.SelectedItem);
                    //SwitchToItemState(item.AvailableNetwork, WifiConnectedState, false);
                }
                else
                {
                    ResultCollection.Add(item);
                }
            }

            DiscoverySuccededAction();
            //ResultsListView.Focus(FocusState.Pointer);
        }

        public List<ConnectionProfile> ConnectionProfiles
        {
            get
            {
                _connectionProfiles = new List<ConnectionProfile>(NetworkInformation.GetConnectionProfiles());
                return _connectionProfiles;
            }
        }

        public static bool IsConnected(WiFiAvailableNetwork network, List<ConnectionProfile> connectionProfiles)
        {
            if (network == null)
                return false;

            string profileName = GetCurrentWifiNetwork(connectionProfiles);
            if (!string.IsNullOrEmpty(network.Ssid) &&
                !string.IsNullOrEmpty(profileName) &&
                (network.Ssid == profileName))
            {
                return true;
            }

            return false;
        }

        public static string GetCurrentWifiNetwork(IReadOnlyList<ConnectionProfile> connectionProfiles)
        {
            //var connectionProfiles = NetworkInformation.GetConnectionProfiles();

            if (connectionProfiles.Count < 1)
            {
                return null;
            }

            var validProfiles = connectionProfiles.Where(profile =>
            {
                return (profile.IsWlanConnectionProfile && profile.GetNetworkConnectivityLevel() != NetworkConnectivityLevel.None);
            });

            if (validProfiles.Count() < 1)
            {
                return null;
            }

            ConnectionProfile firstProfile = validProfiles.First();

            return firstProfile.ProfileName;
        }

    }
}
