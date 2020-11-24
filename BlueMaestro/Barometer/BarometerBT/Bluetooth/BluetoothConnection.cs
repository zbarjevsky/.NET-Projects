using BarometerBT.BlueMaestro;
using BarometerBT.Utils;
using SDKTemplate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;

namespace BarometerBT.Bluetooth
{
    //https://stackoverflow.com/questions/37333179/is-there-any-way-to-use-bluetooth-le-from-a-c-sharp-desktop-app-in-windows-10
    //https://github.com/CarterAppleton/Win10Win32Bluetooth
    //C:\Program Files (x86)\Windows Kits\10\UnionMetadata\Windows.winmd
    //C:\Program Files(x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5\System.Runtime.WindowsRuntime.dll
    public class BluetoothConnection
    {
        public Action<string> OnBMDeviceMsgReceivedAction = (info) => { };
        public Action<TimeSpan> OnBMDeviceCheckAction = (elapsed) => { };
        public Action<TimeSpan> OnBLEDeviceCheckAction = (elapsed) => { };

        // Create Bluetooth Listener
        private BluetoothLEAdvertisementWatcher _watcherBLE = new BluetoothLEAdvertisementWatcher();
        private Dictionary<ushort, DeviceRecordVM> _records = new Dictionary<ushort, DeviceRecordVM>();
        private Dictionary<ulong, BluetoothLEDevice> _blueMaestroDevices = new Dictionary<ulong, BluetoothLEDevice>();

        private Stopwatch _stopperBM = new Stopwatch();
        private Stopwatch _watchDog = new Stopwatch();

        private BMRecordAverages _averages;
        private BMRecordCurrent _current;

        private System.Threading.Timer _timer;

        public class MData
        {
            public class Section
            {
                public ushort CompanyId { get; }
                public byte[] Buffer;

                public Section(BluetoothLEManufacturerData section)
                {
                    CompanyId = section.CompanyId;
                    Buffer = new byte[section.Data.Length];
                    using (var reader = DataReader.FromBuffer(section.Data))
                    {
                        reader.ReadBytes(Buffer);
                    }
                }
            }

            public List<Section> ManufacturerData = new List<Section>();

            public MData(IList<BluetoothLEManufacturerData> manufacturerSections)
            {
                foreach (BluetoothLEManufacturerData section in manufacturerSections)
                {
                    ManufacturerData.Add(new Section(section));
                }
            }

            public int FindManufacturerId(ushort id)
            {
                return ManufacturerData.FindIndex((s) => s.CompanyId == id);
            }
        }

        public BluetoothConnection()
        {
            //I need watch dog - to check bluetooth connection status once a 1 minute
            _timer = new Timer((o) => 
            {
                TimeSpan tsBM = _stopperBM.Elapsed;
                TimeSpan tsWD = _watchDog.Elapsed;

                OnBMDeviceCheckAction(tsBM);
                OnBLEDeviceCheckAction(tsWD);

                //if no message more than one minute - restart BluetoothLEAdvertisementWatcher
                if (tsWD.TotalMinutes > 1.0)
                {
                    try
                    {
                        Debug.WriteLine("Watch Dog Elapsed: " + tsWD);
                        CommonTools.ErrorMessage("Restarting, Stuck: " + tsWD);
                        StopBluetoothSearch();
                        StartBluetoothSearch();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine("Watch Dog Exception: " + err);
                        CommonTools.ErrorMessage("Exception on Stuck: " + err);
                    }
                }
            }, null, 10000, 60000);
        }

        public void StartBluetoothSearch(int OutOfRangeTimeout = 5000, int SamplingInterval = 2000)
        {
            _watcherBLE.ScanningMode = BluetoothLEScanningMode.Active;

            // Only activate the watcher when we're recieving values >= -80
            _watcherBLE.SignalStrengthFilter.InRangeThresholdInDBm = -80;

            // Stop watching if the value drops below -90 (user walked away)
            _watcherBLE.SignalStrengthFilter.OutOfRangeThresholdInDBm = -90;

            // Register callback for when we see an advertisements
            _watcherBLE.Received += OnAdvertisementReceived;

            _watcherBLE.Stopped += OnWatcherStopped;

            // Wait 5 seconds to make sure the device is really out of range
            _watcherBLE.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(OutOfRangeTimeout);
            _watcherBLE.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(SamplingInterval);

            // Starting watching for advertisements
            _watcherBLE.Start();
        }

        public void StopBluetoothSearch()
        {
            _watcherBLE.Stop();
        }

        private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs e)
        {
            _watchDog.Restart(); //message received

            try
            {
                //PrintDataSections(e);

                MData sections = new MData(e.Advertisement.ManufacturerData);
                //PrintManufacturerData(e, sections);
                ProcessManufacturerData(e, sections);
            }
            catch (Exception err)
            {
                Debug.WriteLine("ERROR: "+err);
                CommonTools.ErrorMessage(err.ToString(), "OnAdvertisementReceived");
            }
        }

        private async void ProcessManufacturerData(BluetoothLEAdvertisementReceivedEventArgs e, MData m)
        {
            int idx = m.FindManufacturerId(BMRecordBase.MANUFACTURER_ID);
            if (idx < 0)
                return;

            if (!_blueMaestroDevices.ContainsKey(e.BluetoothAddress))
            {
                BluetoothLEDevice device = await BluetoothLEDevice.FromBluetoothAddressAsync(e.BluetoothAddress);
                lock (_blueMaestroDevices)
                {
                    if (device != null)
                    {
                        _blueMaestroDevices[e.BluetoothAddress] = device;
                        _stopperBM.Restart();
                    }
                }
            }

            if (_blueMaestroDevices.ContainsKey(e.BluetoothAddress))
            {
                BluetoothLEDeviceDisplay display = new BluetoothLEDeviceDisplay(_blueMaestroDevices[e.BluetoothAddress].DeviceInformation);
                Debug.WriteLine("*BT* " + display.ToString());
            }

            if (m.ManufacturerData.Count > 0)
            {
                lock (_blueMaestroDevices)
                {
                    foreach (MData.Section section in m.ManufacturerData)
                    {
                        if (BMRecordCurrent.IsManufacturerID(section.CompanyId))
                        {
                            DateTime date = DateTime.Now; // eventArgs.Timestamp.DateTime;
                            BluetoothDevice dev = new BluetoothDevice(
                                e.Advertisement.LocalName,
                                e.BluetoothAddress,
                                e.AdvertisementType.ToString());

                            if (_averages == null)
                                _averages = new BMRecordAverages(dev, e.RawSignalStrengthInDBm, date, null);

                            if (_current == null)
                                _current = new BMRecordCurrent(dev, e.RawSignalStrengthInDBm, date, null);

                            if (section.Buffer.Length == 14)
                            {
                                _current = BMDatabaseMap.INSTANCE.AddRecord(dev, e.RawSignalStrengthInDBm, date, section.Buffer);
                            }
                            else if (section.Buffer.Length == 25)
                            {
                                _averages.Set_sData(section.Buffer);
                            }
                            else
                            {
                                Debug.WriteLine(" --- Unknown Length: " + section.Buffer.Length);
                            }

                            string recordsCount = "";
                            if(BMDatabaseMap.INSTANCE.Map.Count > 0)
                                recordsCount = "Records: " + BMDatabaseMap.INSTANCE.Map.First().Value.Records.Count + " \n";

                            string message = recordsCount;
                            message += "Elapsed: " + _stopperBM.Elapsed.ToString(@"d\.hh\:mm\:ss") + " \n";
                            message += "Timestamp: " + date.ToString("MMM dd, HH:mm:ss") + " \n";
                            message += _current.ToString() + _averages.ToString();
                            
                            OnBMDeviceMsgReceivedAction(message);
                        }
                    }
                }
            }
        }

        private void OnWatcherStopped(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementWatcherStoppedEventArgs args)
        {
            _watcherBLE.Received -= OnAdvertisementReceived;
            _watcherBLE.Stopped -= OnWatcherStopped;

            CommonTools.InfoMessage("BluetoothLEAdvertisementWatcher stopped");
        }

        private void PrintDataSections(BluetoothLEAdvertisementReceivedEventArgs e)
        {
            // Tell the user we see an advertisement and print some properties
            Debug.WriteLine("--------------------------------------------------------------------");
            Debug.WriteLine(String.Format("Advertisement:"));
            Debug.WriteLine(String.Format("  BT_ADDR: {0} -- {1}", e.BluetoothAddress, BitConverter.ToString(BitConverter.GetBytes(e.BluetoothAddress).Reverse().ToArray())));
            Debug.WriteLine(String.Format("  FR_NAME: {0}", e.Advertisement.LocalName));
            Debug.WriteLine(String.Format("  FR_TYPE: {0}", e.AdvertisementType));

            IList<BluetoothLEAdvertisementDataSection> dataSections = e.Advertisement.DataSections;
            if (dataSections != null && dataSections.Count > 0)
            {
                Debug.WriteLine("DATA COUNT: " + dataSections.Count);
                foreach (BluetoothLEAdvertisementDataSection section in dataSections)
                {
                    var data = new byte[section.Data.Length];
                    using (var reader = DataReader.FromBuffer(section.Data))
                    {
                        reader.ReadBytes(data);
                    }

                    string manufacturerDataString = string.Format("0x{0}: {1}",
                       section.DataType.ToString("X"),
                       BitConverter.ToString(data));
                    Debug.WriteLine(string.Format("  DATA({0}): {1}", data.Length, manufacturerDataString));

                    if (section.DataType == (byte)BleDataType.CompleteLocalName)
                    {
                        string name = Encoding.UTF8.GetString(data);
                        Debug.WriteLine("  NAME: " + name);
                    }
                }
            }
        }

        private void PrintManufacturerData(BluetoothLEAdvertisementReceivedEventArgs e, MData m)
        {
            {
                if (m.ManufacturerData.Count > 0)
                {
                    Debug.WriteLine("  SECTIONS: " + m.ManufacturerData.Count);
                    foreach (MData.Section section in m.ManufacturerData)
                    {
                        //// Print the company ID + the raw data in hex format
                        string manufacturerDataString = string.Format("0x{0}: {1}",
                            section.CompanyId.ToString("X"),
                            BitConverter.ToString(section.Buffer));
                        Debug.WriteLine(string.Format("  COMPANY({0}): {1}", section.Buffer.Length, manufacturerDataString));

                        if (!_records.ContainsKey(section.CompanyId))
                            _records[section.CompanyId] = new DeviceRecordVM(e.Advertisement.LocalName, section.CompanyId, section.Buffer);

                        _records[section.CompanyId].Buffer = section.Buffer;
                        if (!string.IsNullOrWhiteSpace(e.Advertisement.LocalName))
                            _records[section.CompanyId].Name = e.Advertisement.LocalName;
                    }
                }
            }
        }
    }
}
