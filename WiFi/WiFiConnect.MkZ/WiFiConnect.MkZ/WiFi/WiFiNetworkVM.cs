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
using System.Windows.Media;


using MkZ.Windows;
using MkZ.WPF;
using Windows.Foundation;

namespace WiFiConnect.MkZ.WiFi
{
    public class WiFiNetworkVM : NotifyPropertyChangedImpl
    {
        public static ILog Log { get; private set; }
        public WiFiAdapter Adapter { get; }
        private static readonly string _assemblyName = Assembly.GetExecutingAssembly().GetName().Name; //"sD.WPF.MessageBox";

        public WiFiAvailableNetwork Network { get; }


        public WiFiNetworkVM(WiFiAvailableNetwork availableNetwork, WiFiAdapter adapter, ILog log)
        {
            Log = log;
            Network = availableNetwork;
            this.Adapter = adapter;
        }

        public WiFiNetworkVM(WiFiNetworkVM vm) : this(vm.Network, vm.Adapter, Log)
        {
            WiFiImage = vm.WiFiImage;
            NetworkKeyInfoVisibility = vm.NetworkKeyInfoVisibility;
            ConnectivityLevel = vm.ConnectivityLevel;
            IsWpsPushButtonAvailable = vm.IsWpsPushButtonAvailable;
            IsHiddenNetwork = vm.IsHiddenNetwork;
            UsePassword = vm.UsePassword;
            UserName = vm.UserName;
            Password = vm.Password;
            Domain = vm.Domain;
            HiddenSsid = vm.HiddenSsid;
            ConnectAutomatically = vm.ConnectAutomatically;
        }

        public void Update()
        {
            Func<Task> update = async () =>
            {
                UpdateWiFiImage();
                UpdateNetworkKeyVisibility();
                UpdateHiddenSsidTextBoxVisibility();
                var info = await GetConnectivityLevelAsync(Adapter);
                UpdateConnectivityLevel(info);
                await UpdateWpsPushButtonAvailableAsync();
            };

            WPF_Helper.ExecuteOnUIThreadWPF(update);
        }

        private void UpdateHiddenSsidTextBoxVisibility()
        {
            IsHiddenNetwork = string.IsNullOrEmpty(Network.Ssid);
            NotifyPropertyChanged(nameof(IsHiddenNetwork));
        }

        private void UpdateNetworkKeyVisibility()
        {
            // Only show the password box if needed
            if ((Network.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Open80211 &&
                 Network.SecuritySettings.NetworkEncryptionType == NetworkEncryptionType.None) ||
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
            if (Network.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Open80211)
            {
                imageFileNamePrefix = "open";
            }

            string imageFileName = string.Format("Images/{0}_{1}bar.png", imageFileNamePrefix, Network.SignalBars);

            WiFiImage = WPF_Helper.GetResourceImage(imageFileName, _assemblyName);

            NotifyPropertyChanged(nameof(WiFiImage));
        }

        public class ConnectionInfo
        {
            public string Ssid = null;
            public NetworkConnectivityLevel Level = NetworkConnectivityLevel.None;
            public string sLevel
            {
                get
                {
                    if(Level == NetworkConnectivityLevel.None)
                        return "Not Connected";
                    return Level.ToString();
                }
            }

            public override string ToString()
            {
                return sLevel + " " + Ssid;
            }
        }

        public static async Task<ConnectionInfo> GetConnectivityLevelAsync(WiFiAdapter adapter)
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();

            try
            {
                ConnectionProfile connectedProfile = await adapter.NetworkAdapter.GetConnectedProfileAsync();
                if (connectedProfile != null &&
                    connectedProfile.IsWlanConnectionProfile &&
                    connectedProfile.WlanConnectionProfileDetails != null)
                {
                    connectionInfo.Ssid = connectedProfile.WlanConnectionProfileDetails.GetConnectedSsid();
                }

                if (!string.IsNullOrWhiteSpace(connectionInfo.Ssid))
                {
                    connectionInfo.Level = WPF_Helper.ExecuteOnWorkerThread(() => connectedProfile.GetNetworkConnectivityLevel());
                }
            }
            catch (Exception err)
            {
                Log.Log("UpdateConnectivityLevelAsync - {0} Err: {1}", connectionInfo.Ssid, err.Message);
            }

            return connectionInfo;
        }

        public void UpdateConnectivityLevel(ConnectionInfo activeConnectionInfo)
        {
            Background = Brushes.Transparent;
            ConnectivityLevel = "Not Connected";

            if (activeConnectionInfo != null && activeConnectionInfo.Ssid != null)
            {
                if (activeConnectionInfo.Ssid.Equals(Network.Ssid) || activeConnectionInfo.Ssid.Equals(HiddenSsid))
                {
                    Background = activeConnectionInfo.Level == NetworkConnectivityLevel.None ? Brushes.Transparent : Brushes.Navy;
                    ConnectivityLevel = activeConnectionInfo.sLevel;
                }
            }

            NotifyPropertyChanged(nameof(ConnectivityLevel));
        }

        public ConnectionProfile GetConnectedProfileAsync()
        {
            ConnectionProfile connectedProfile = null;
            Func<Task> get1 = async () =>
            {
                connectedProfile = await Adapter.NetworkAdapter.GetConnectedProfileAsync();
            };

            get1.Invoke();
            return connectedProfile;
        }

        public async Task UpdateWpsPushButtonAvailableAsync()
        {
            IsWpsPushButtonAvailable = await IsWpsPushButtonAvailableAsync();
            NotifyPropertyChanged(nameof(IsWpsPushButtonAvailable));
        }

        public void Disconnect()
        {
            Adapter.Disconnect();
        }

        Brush _background = Brushes.Transparent;
        public Brush Background { get { return _background; } private set { SetProperty(ref _background, value); } }

        public bool IsWpsPushButtonAvailable { get; set; }

        public bool NetworkKeyInfoVisibility { get; set; }

        public bool IsHiddenNetwork { get; set; }

        private bool usePassword = false;
        public bool UsePassword
        {
            get { return usePassword; }
            set { SetProperty(ref usePassword, value); }
        }

        private bool connectAutomatically = false;
        public bool ConnectAutomatically
        {
            get { return connectAutomatically; }
            set { SetProperty(ref connectAutomatically, value); }
        }

        public string Ssid => string.IsNullOrEmpty(Network.Ssid) ? "Hidden Network" : Network.Ssid;

        public string Bssid => Network.Bssid;

        public string ChannelCenterFrequency => string.Format("{0}kHz", Network.ChannelCenterFrequencyInKilohertz);

        public string Rssi => string.Format("{0}dBm", Network.NetworkRssiInDecibelMilliwatts);

        public string SecuritySettings => string.Format("Authentication: {0}; Encryption: {1}", Network.SecuritySettings.NetworkAuthenticationType, Network.SecuritySettings.NetworkEncryptionType);

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
            set { SetProperty(ref userName, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private string domain;
        public string Domain
        {
            get { return domain; }
            set { SetProperty(ref domain, value); }
        }

        private string hiddenSsid;
        public string HiddenSsid
        {
            get { return hiddenSsid; }
            set { SetProperty(ref hiddenSsid, value); }
        }

        public bool IsEapAvailable
        {
            get
            {
                return ((Network.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Rsna) ||
                    (Network.SecuritySettings.NetworkAuthenticationType == NetworkAuthenticationType.Wpa));
            }
        }

        public async Task<bool> IsWpsPushButtonAvailableAsync()
        {
            if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 5, 0))
            {
                var result = await Adapter.GetWpsConfigurationAsync(Network);
                if (result.SupportedWpsKinds.Contains(WiFiWpsKind.PushButton))
                    return true;
            }

            return false;
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    WPF_Helper.ExecuteOnUIThreadWPF(() => 
        //    { 
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //        return 0;
        //    });
        //}

        public override string ToString()
        {
            return "VM: " + Ssid;
        }
    }
}
