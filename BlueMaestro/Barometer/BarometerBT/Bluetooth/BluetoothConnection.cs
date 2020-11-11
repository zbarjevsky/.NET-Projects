using BarometerBT.BlueMaestro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;

namespace BarometerBT.Bluetooth
{
    public class BluetoothConnection
    {
        // Create Bluetooth Listener
        private BluetoothLEAdvertisementWatcher _watcher = new BluetoothLEAdvertisementWatcher();

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

        private void OnAdvertisementReceived(BluetoothLEAdvertisementWatcher watcher, BluetoothLEAdvertisementReceivedEventArgs eventArgs)
        {
            // Tell the user we see an advertisement and print some properties
            Debug.WriteLine(String.Format("Advertisement:"));
            Debug.WriteLine(String.Format("  BT_ADDR: {0}", eventArgs.BluetoothAddress));
            Debug.WriteLine(String.Format("  FR_NAME: {0}", eventArgs.Advertisement.LocalName));

            try
            {
                if (eventArgs.Advertisement.ManufacturerData != null)
                {
                    var manufacturerSections = eventArgs.Advertisement.ManufacturerData;
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
                            Debug.WriteLine("  COMPANY: " + manufacturerDataString);

                            if(BlueMaestroDevice.IsManufacturerID(section.CompanyId))
                            {
                                BlueMaestroDevice dev = new BlueMaestroDevice(data);
                            }
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
