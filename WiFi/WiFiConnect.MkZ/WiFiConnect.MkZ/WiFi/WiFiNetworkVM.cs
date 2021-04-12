using MkZ.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Windows.Devices.WiFi;
using Windows.Foundation.Metadata;
using Windows.Networking.Connectivity;

namespace WiFiConnect.MkZ.WiFi
{
    public class WiFiNetworkVM
    {
        private WiFiAdapter _adapter;
        private static readonly string _assemblyName = Assembly.GetExecutingAssembly().GetName().Name; //"sD.WPF.MessageBox";

        public WiFiAvailableNetwork AvailableNetwork { get; private set; }

        public ILog Log { get; }

        public WiFiNetworkVM(WiFiAvailableNetwork availableNetwork, WiFiAdapter adapter, ILog log)
        {
            Log = log;
            AvailableNetwork = availableNetwork;
            this._adapter = adapter;
        }

        public void Update()
        {
            Func<Task> update = async () =>
            {
                UpdateWiFiImage();
                UpdateNetworkKeyVisibility();
                UpdateHiddenSsidTextBoxVisibility();
                await UpdateConnectivityLevelAsync();
                await UpdateWpsPushButtonAvailableAsync();
            };

            WPF_Helper.ExecuteOnUIThreadWPF(update);
        }

        private void UpdateHiddenSsidTextBoxVisibility()
        {
            IsHiddenNetwork = string.IsNullOrEmpty(AvailableNetwork.Ssid);
            OnPropertyChanged(nameof(IsHiddenNetwork));
        }

        private void UpdateNetworkKeyVisibility()
        {
            // Only show the password box if needed
            if ((AvailableNetwork.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Open80211 &&
                 AvailableNetwork.SecuritySettings.NetworkEncryptionType == NetworkEncryptionType.None) ||
                 IsEapAvailable)
            {
                NetworkKeyInfoVisibility = false;
            }
            else
            {
                NetworkKeyInfoVisibility = true;
            }
        }

        private void UpdateWiFiImage()
        {
            string imageFileNamePrefix = "secure";
            if (AvailableNetwork.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Open80211)
            {
                imageFileNamePrefix = "open";
            }

            string imageFileName = string.Format("Images/{0}_{1}bar.png", imageFileNamePrefix, AvailableNetwork.SignalBars);

            WiFiImage = WPF_Helper.GetResourceImage(imageFileName, _assemblyName);

            OnPropertyChanged(nameof(WiFiImage));
        }

        public async Task UpdateConnectivityLevelAsync()
        {
            string connectivityLevel = "Not Connected";
            string connectedSsid = null;

            try
            {
                ConnectionProfile connectedProfile = await _adapter.NetworkAdapter.GetConnectedProfileAsync();
                if (connectedProfile != null &&
                    connectedProfile.IsWlanConnectionProfile &&
                    connectedProfile.WlanConnectionProfileDetails != null)
                {
                    connectedSsid = connectedProfile.WlanConnectionProfileDetails.GetConnectedSsid();
                }

                if (!string.IsNullOrWhiteSpace(connectedSsid))
                {
                    if (connectedSsid.Equals(AvailableNetwork.Ssid) ||
                        connectedSsid.Equals(HiddenSsid))
                    {
                        NetworkConnectivityLevel level = WPF_Helper.ExecuteOnWorkerThread(() => connectedProfile.GetNetworkConnectivityLevel());
                        connectivityLevel = level.ToString();
                    }
                }
            }
            catch (Exception err)
            {
                Log.Log("UpdateConnectivityLevelAsync - {0} Err: {1}", connectedSsid, err.Message);
            }

            ConnectivityLevel = connectivityLevel;
            OnPropertyChanged(nameof(ConnectivityLevel));
        }

        public ConnectionProfile GetConnectedProfileAsync()
        {
            ConnectionProfile connectedProfile = null;
            Func<Task> get1 = async () =>
            {
                connectedProfile = await _adapter.NetworkAdapter.GetConnectedProfileAsync();
            };

            get1.Invoke();
            return connectedProfile;
        }

        public async Task UpdateWpsPushButtonAvailableAsync()
        {
            IsWpsPushButtonAvailable = await IsWpsPushButtonAvailableAsync();
            OnPropertyChanged(nameof(IsWpsPushButtonAvailable));
        }

        public void Disconnect()
        {
            _adapter.Disconnect();
        }

        public bool IsWpsPushButtonAvailable { get; set; }

        public bool NetworkKeyInfoVisibility { get; set; }

        public bool IsHiddenNetwork { get; set; }

        private bool usePassword = false;
        public bool UsePassword
        {
            get { return usePassword; }
            set { usePassword = value; OnPropertyChanged(); }
        }

        private bool connectAutomatically = false;
        public bool ConnectAutomatically
        {
            get { return connectAutomatically; }
            set { connectAutomatically = value; OnPropertyChanged(); }
        }

        public string Ssid => string.IsNullOrEmpty(AvailableNetwork.Ssid) ? "Hidden Network" : AvailableNetwork.Ssid;

        public string Bssid => AvailableNetwork.Bssid;

        public string ChannelCenterFrequency => string.Format("{0}kHz", AvailableNetwork.ChannelCenterFrequencyInKilohertz);

        public string Rssi => string.Format("{0}dBm", AvailableNetwork.NetworkRssiInDecibelMilliwatts);

        public string SecuritySettings => string.Format("Authentication: {0}; Encryption: {1}", AvailableNetwork.SecuritySettings.NetworkAuthenticationType, AvailableNetwork.SecuritySettings.NetworkEncryptionType);

        public string ConnectivityLevel
        {
            get;
            private set;
        }

        public BitmapImage WiFiImage
        {
            get;
            private set;
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string domain;
        public string Domain
        {
            get { return domain; }
            set { domain = value; OnPropertyChanged(); }
        }

        private string hiddenSsid;
        public string HiddenSsid
        {
            get { return hiddenSsid; }
            set { hiddenSsid = value; OnPropertyChanged(); }
        }

        public bool IsEapAvailable
        {
            get
            {
                return ((AvailableNetwork.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Rsna) ||
                    (AvailableNetwork.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Wpa));
            }
        }

        public async Task<bool> IsWpsPushButtonAvailableAsync()
        {
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5, 0))
            {
                var result = await _adapter.GetWpsConfigurationAsync(AvailableNetwork);
                if (result.SupportedWpsKinds.Contains(WiFiWpsKind.PushButton))
                    return true;
            }

            return false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            WPF_Helper.ExecuteOnUIThreadWPF(() => 
            { 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return 0;
            });
        }

        public override string ToString()
        {
            return "VM: " + Ssid;
        }
    }
}
