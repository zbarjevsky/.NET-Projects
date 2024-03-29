﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;


using MkZ.Tools;
using MkZ.WPF.MessageBox;
using MkZ.BlueMaestroLib;
using MkZ.WeatherStation.Utils;
using MkZ.Weather;

namespace MkZ.Bluetooth
{
    //https://stackoverflow.com/questions/37333179/is-there-any-way-to-use-bluetooth-le-from-a-c-sharp-desktop-app-in-windows-10
    //https://github.com/CarterAppleton/Win10Win32Bluetooth
    //C:\Program Files (x86)\Windows Kits\10\UnionMetadata\Windows.winmd
    //C:\Program Files(x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5\System.Runtime.WindowsRuntime.dll
    public class BluetoothWatcher
    {
        public const int OUT_OF_RANGE_TIMEOUT = 50000;

        public Action<string> OnBMDeviceMsgReceivedAction = (info) => { };
        public Action<double> OnBMDeviceCheckAction = (elapsed) => { };
        public Action<double> OnBLEDeviceCheckAction = (elapsed) => { };
        public Action<double> OnTimerSaveAction = (elapsed) => { };

        // Create Bluetooth Listener
        private BluetoothLEAdvertisementWatcher _watcherBLE = new BluetoothLEAdvertisementWatcher();
        private Dictionary<ushort, DeviceRecordVM> _records = new Dictionary<ushort, DeviceRecordVM>();
        private Dictionary<ulong, BluetoothLEDevice> _blueMaestroDevices = new Dictionary<ulong, BluetoothLEDevice>();

        private double _stopperBM = 0;
        private double _watchDog = 0;
        private double _saveCounter = 0;

        private BMRecordAverages _averages;
        private BMRecordCurrent _current;

        private DispatcherTimer _timer;

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

        public BluetoothWatcher()
        {
            //I need watch dog - to check bluetooth connection status once a 1 minute
            _timer = new DispatcherTimer(DispatcherPriority.Background);
            _timer.Interval = TimeSpan.FromSeconds(1);//.FromMilliseconds(OUT_OF_RANGE_TIMEOUT);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();

            _stopperBM++;
            _watchDog++;
            _saveCounter++;

            if(_saveCounter > 3600) //save every 1 hour
            {
                OnTimerSaveAction(_saveCounter);
                _saveCounter = 0;
            }

            OnBMDeviceCheckAction(_stopperBM);
            OnBLEDeviceCheckAction(_stopperBM);

            //if no message more than one minute - restart BluetoothLEAdvertisementWatcher
            if (_watchDog > 60)
            {
                try
                {
                    Log.d("Watch Dog Elapsed: " + _watchDog);
                    var result = CommonTools.ErrorMessage("Watcher is Stuck!\nOk to Restart?\n" + _watchDog);
                    if(result == MessageBoxResult.OK)
                    {
                        _watchDog = 0;
                        StopBluetoothSearch();
                        StartBluetoothSearch();
                    }
                    else //wait 2 minutes
                    {
                        _watchDog = -60;
                    }
                }
                catch (Exception err)
                {
                    Log.e("Watch Dog Exception: " + err);
                    CommonTools.ErrorMessage("Exception on Stuck: " + err);
                }
            }

            _timer.Start();
        }

        public void StartBluetoothSearch(int OutOfRangeTimeout = OUT_OF_RANGE_TIMEOUT, int SamplingInterval = 2000)
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
            _watchDog = 0; //message received

            try
            {
                //PrintDataSections(e);

                MData sections = new MData(e.Advertisement.ManufacturerData);
                //PrintManufacturerData(e, sections);
                ProcessManufacturerData(e, sections);
            }
            catch (Exception err)
            {
                Log.e("OnAdvertisementReceived::ERROR: " + err);
                CommonTools.ErrorMessage(err.ToString(), "OnAdvertisementReceived");
            }
        }

        private string _lastAverage = "", _lastCurrent = "";
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
                        _stopperBM = 0;
                    }
                }
            }

            if (_blueMaestroDevices.ContainsKey(e.BluetoothAddress))
            {
                BluetoothLEDeviceDisplay display = new BluetoothLEDeviceDisplay(_blueMaestroDevices[e.BluetoothAddress].DeviceInformation);
                Log.d("*BT* " + display.ToString());
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

                            if (section.Buffer.Length == 14) //current
                            {
                                if (WeatherDataManager.INSTANCE.BMDatabaseMap.Contains(dev.Address))
                                {
                                    int count = WeatherDataManager.INSTANCE.BMDatabaseMap[dev.Address].Records.Count;
                                    if (count > 0)
                                    {
                                        //time between readings
                                        TimeSpan tsElapsed = DateTime.Now - WeatherDataManager.INSTANCE.BMDatabaseMap[dev.Address].Records.Last().Date;
                                        _lastCurrent = ", Curr Updated: " + tsElapsed.TotalSeconds.ToString("0s");
                                    }
                                }
                                    
                                _current = WeatherDataManager.INSTANCE.AddMeteorologicalData(dev, e.RawSignalStrengthInDBm, date, section.Buffer);
                            }
                            else if (section.Buffer.Length == 25) //24hr averages
                            {
                                //just update time
                                TimeSpan tsElapsed = DateTime.Now - _averages.Date;
                                _lastAverage = ", Avg Updated: " + tsElapsed.TotalSeconds.ToString("0s");

                                _averages.Set_sData(section.Buffer);
                            }
                            else
                            {
                                Log.e(" --- Unknown Length: " + section.Buffer.Length);
                            }

                            string recordsCount = "";
                            if (WeatherDataManager.INSTANCE.BMDatabaseMap.Contains(dev.Address))
                            {
                                int count = WeatherDataManager.INSTANCE.BMDatabaseMap[dev.Address].Records.Count;
                                recordsCount = "Records: " + count + " \n";
                            }

                            string message = recordsCount;
                            message += "Total: " + CommonTools.TimeSpanToString(TimeSpan.FromSeconds(_stopperBM)) + _lastAverage + _lastCurrent + " \n";
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

            Log.d("BluetoothLEAdvertisementWatcher stopped");
            //CommonTools.InfoMessage("BluetoothLEAdvertisementWatcher stopped");
        }

        private void PrintDataSections(BluetoothLEAdvertisementReceivedEventArgs e)
        {
            // Tell the user we see an advertisement and print some properties
            Log.d("--------------------------------------------------------------------");
            Log.d(String.Format("Advertisement:"));
            Log.d(String.Format("  BT_ADDR: {0} -- {1}", e.BluetoothAddress, BitConverter.ToString(BitConverter.GetBytes(e.BluetoothAddress).Reverse().ToArray())));
            Log.d(String.Format("  FR_NAME: {0}", e.Advertisement.LocalName));
            Log.d(String.Format("  FR_TYPE: {0}", e.AdvertisementType));

            IList<BluetoothLEAdvertisementDataSection> dataSections = e.Advertisement.DataSections;
            if (dataSections != null && dataSections.Count > 0)
            {
                Log.d("DATA COUNT: " + dataSections.Count);
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
                    Log.d(string.Format("  DATA({0}): {1}", data.Length, manufacturerDataString));

                    if (section.DataType == (byte)BleDataType.CompleteLocalName)
                    {
                        string name = Encoding.UTF8.GetString(data);
                        Log.d("  NAME: " + name);
                    }
                }
            }
        }

        private void PrintManufacturerData(BluetoothLEAdvertisementReceivedEventArgs e, MData m)
        {
            {
                if (m.ManufacturerData.Count > 0)
                {
                    Log.d("  SECTIONS: " + m.ManufacturerData.Count);
                    foreach (MData.Section section in m.ManufacturerData)
                    {
                        //// Print the company ID + the raw data in hex format
                        string manufacturerDataString = string.Format("0x{0}: {1}",
                            section.CompanyId.ToString("X"),
                            BitConverter.ToString(section.Buffer));
                        Log.d(string.Format("  COMPANY({0}): {1}", section.Buffer.Length, manufacturerDataString));

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
