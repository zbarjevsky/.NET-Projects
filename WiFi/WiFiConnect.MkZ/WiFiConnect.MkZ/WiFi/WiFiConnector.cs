using MkZ.WPF;
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
    public enum SortOrder
    {
        Ascending,
        Descending,
        Secured,
        Open,
        Strength,
    }

    //https://docs.microsoft.com/en-us/samples/microsoft/windows-iotcore-samples/wifi-connector/
    //https://www.thomasclaudiushuber.com/2019/04/26/calling-windows-10-apis-from-your-wpf-application/
    //https://docs.microsoft.com/en-us/windows/apps/desktop/modernize/desktop-to-uwp-enhance
    public class WiFiConnector
    {
        public WiFiAdapter _firstAdapter { get; private set; }
        private List<ConnectionProfile> _connectionProfiles;
        public ILog Log { get; }

        public ObservableCollection<WiFiNetworkVM> ResultCollection { get; private set; }

        public Action DiscoverySuccededAction = () => { };

        public Action<WiFiAdapter, object> AvailableNetworksChanged = (adapter, o) => { };

        public WiFiConnector(ILog log)
        {
            Log = log;
        }

        public async void DiscoverAllWiFiAdapters()
        {
            if (ResultCollection != null)
                ResultCollection.Clear();

            // RequestAccessAsync must have been called at least once by the app before using the API
            // Calling it multiple times is fine but not necessary
            // RequestAccessAsync must be called from the UI thread
            var access = await WiFiAdapter.RequestAccessAsync();
            if (access != WiFiAccessStatus.Allowed)
            {
                Log.Log("DiscoverAllWiFiAdapters: Access denied");
            }
            else
            {
                //var result = await Windows.Devices.Enumeration.DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
                await Task.Factory.StartNew(async () =>
                {
                    var result = await WiFiAdapter.FindAllAdaptersAsync();
                    if (result.Count >= 1)
                    {
                        if (_firstAdapter != null)
                            _firstAdapter.AvailableNetworksChanged -= FirstAdapter_AvailableNetworksChanged;

                        //_firstAdapter = await WiFiAdapter.FromIdAsync(result[0].Id);
                        _firstAdapter = result[0];

                        _firstAdapter.AvailableNetworksChanged += FirstAdapter_AvailableNetworksChanged;

                        ScanWiFi(_firstAdapter);
                    }
                    else
                    {
                        _firstAdapter = null;
                        Debug.WriteLine("No WiFi Adapters detected on this machine.");
                    }
                });
            }
        }

        private void FirstAdapter_AvailableNetworksChanged(WiFiAdapter sender, object args)
        {
            //Log.Log("AvailableNetworksChanged");

            if (ResultCollection == null)
                return;

            AvailableNetworksChanged(sender, args);
        }

        private async void ScanWiFi(WiFiAdapter firstAdapter)
        {
            try
            {
                await firstAdapter.ScanAsync();
            }
            catch (Exception err)
            {
                Log.Log("Error scanning WiFi adapter: 0x{0:X}: {1}", err.HResult, err.Message);
                return;
            }

            DisplayNetworkReport(firstAdapter.NetworkReport);
        }

        private void DisplayNetworkReport(WiFiNetworkReport report)
        {
            Log.Log("Network Report Timestamp: {0}", report.Timestamp);

            ResultCollection = new ObservableCollection<WiFiNetworkVM>();
            ConcurrentDictionary<string, WiFiNetworkVM> dictionary = new ConcurrentDictionary<string, WiFiNetworkVM>();

            List<ConnectionProfile> connectionProfiles = ConnectionProfiles;

            foreach (var network in report.AvailableNetworks)
            {
                var item = new WiFiNetworkVM(network, _firstAdapter, Log);
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
                if (IsConnected(item.Network, connectionProfiles))
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

        public void SortResults(SortOrder order)
        {
            if (ResultCollection == null || ResultCollection.Count == 0)
                return;

            List<WiFiNetworkVM> vms = new List<WiFiNetworkVM>(ResultCollection);
            vms.RemoveAt(0); //sort all but first - active
            switch (order)
            {
                case SortOrder.Ascending:
                    vms.Sort((n1, n2) => string.Compare(n1.Ssid, n2.Ssid, true));
                    break;
                case SortOrder.Descending:
                    vms.Sort((n1, n2) => string.Compare(n2.Ssid, n1.Ssid, true));
                    break;
                case SortOrder.Secured:
                    break;
                case SortOrder.Open:
                    break;
                case SortOrder.Strength:
                    break;
                default:
                    break;
            }

            vms.Insert(0, ResultCollection[0]);
            ResultCollection.Clear();

            foreach (WiFiNetworkVM vm in vms)
            {
                ResultCollection.Add(vm);
            }
        }

        public List<ConnectionProfile> ConnectionProfiles
        {
            get
            {
                _connectionProfiles = WPF_Helper.ExecuteOnWorkerThread(() =>
                {
                    return new List<ConnectionProfile>(NetworkInformation.GetConnectionProfiles());
                });
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

            return WPF_Helper.ExecuteOnUIThreadWPF(() =>
            {
                var validProfiles = connectionProfiles.Where(profile =>
                {
                    NetworkConnectivityLevel level = WPF_Helper.ExecuteOnWorkerThread(() => { return profile.GetNetworkConnectivityLevel(); });
                    return (profile.IsWlanConnectionProfile && level != NetworkConnectivityLevel.None);
                });
                return validProfiles.FirstOrDefault()?.ProfileName;
            });
        }
    }
}
