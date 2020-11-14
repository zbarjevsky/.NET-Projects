using BarometerBT.BlueMaestro;
using BarometerBT.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;

namespace BarometerBT.Bluetooth
{
    public class BluetoothConnection
    {
        // Create Bluetooth Listener
        private BluetoothLEAdvertisementWatcher _watcher = new BluetoothLEAdvertisementWatcher();
        private Dictionary<ushort, DeviceRecordVM> _records = new Dictionary<ushort, DeviceRecordVM>();
        private BMRecordAverages _averages;
        private BMRecordCurrent _current;

        public void StartBluetoothSearch(int OutOfRangeTimeout = 50000, int SamplingInterval = 2000)
        {
            _watcher.ScanningMode = BluetoothLEScanningMode.Active;

            // Only activate the watcher when we're recieving values >= -80
            _watcher.SignalStrengthFilter.InRangeThresholdInDBm = -80;

            // Stop watching if the value drops below -90 (user walked away)
            _watcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -90;

            // Register callback for when we see an advertisements
            _watcher.Received += OnAdvertisementReceived;

            // Wait 5 seconds to make sure the device is really out of range
            _watcher.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(OutOfRangeTimeout);
            _watcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(SamplingInterval);

            // Starting watching for advertisements
            _watcher.Start();
        }

        private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs e)
        {
            // Tell the user we see an advertisement and print some properties
            Debug.WriteLine(String.Format("Advertisement:"));
            Debug.WriteLine(String.Format("  BT_ADDR: {0}", e.BluetoothAddress));
            Debug.WriteLine(String.Format("  FR_NAME: {0}", e.Advertisement.LocalName));
            Debug.WriteLine(String.Format("  FR_TYPE: {0}", e.AdvertisementType));
            Debug.WriteLine("DATA COUNT: "+e.Advertisement.DataSections.Count);

            try
            {
                if(e.Advertisement.DataSections != null && e.Advertisement.DataSections.Count > 0)
                {
                    var dataSections = e.Advertisement.DataSections;
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

                        if(section.DataType == (byte)BleDataType.CompleteLocalName)
                        {
                            string name = Encoding.UTF8.GetString(data);
                            Debug.WriteLine("  NAME: " + name);
                        }
                    }
                }

                if (e.Advertisement.ManufacturerData != null)
                {
                    var manufacturerSections = e.Advertisement.ManufacturerData;
                    if (manufacturerSections.Count > 0)
                    {
                        Debug.WriteLine("  SECTIONS: " + manufacturerSections.Count);
                        foreach (BluetoothLEManufacturerData section in manufacturerSections)
                        {
                            // Only print the first one of the list
                            var data = new byte[section.Data.Length];
                            using (var reader = DataReader.FromBuffer(section.Data))
                            {
                                reader.ReadBytes(data);
                            }

                            //// Print the company ID + the raw data in hex format
                            string manufacturerDataString = string.Format("0x{0}: {1}",
                                section.CompanyId.ToString("X"),
                                BitConverter.ToString(data));
                            Debug.WriteLine(string.Format("  COMPANY({0}): {1}", data.Length, manufacturerDataString));

                            _records[section.CompanyId] = new DeviceRecordVM(e.Advertisement.LocalName, section.CompanyId, data);

                            if (BMRecordCurrent.IsManufacturerID(section.CompanyId))
                            {
                                DateTime date = DateTime.Now; // eventArgs.Timestamp.DateTime;
                                BluetoothDevice dev = new BluetoothDevice(
                                    e.Advertisement.LocalName,
                                    e.BluetoothAddress, 
                                    e.AdvertisementType.ToString());

                                if(_averages == null)
                                    _averages = new BMRecordAverages(dev, e.RawSignalStrengthInDBm, date, null);

                                if(_current == null)
                                    _current = new BMRecordCurrent(dev, e.RawSignalStrengthInDBm, date, null);

                                if (data.Length == 14)
                                {
                                    _current = BMDatabaseMap.INSTANCE.AddRecord(dev, e.RawSignalStrengthInDBm, date, data);
                                    
                                    MainWindow.SetChartData(BMDatabaseMap.INSTANCE.Map[dev.getAddress()]);
                                }
                                else if (data.Length == 25)
                                {
                                    _averages.Set_sData(data);
                                }
                                else
                                {
                                    Debug.WriteLine(" --- Unknown Length: " + data.Length);
                                }

                                MainWindow.SetInfo(_current.ToString() + _averages.ToString());
                            }

                            MainWindow.UpdateList(_records);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine("ERROR: "+err);
            }
            Debug.WriteLine("");
        }
    }
}
