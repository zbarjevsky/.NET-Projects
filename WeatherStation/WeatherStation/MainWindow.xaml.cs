using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;


using MkZ.WeatherStation.BlueMaestro;
using MkZ.WeatherStation.BlueMaestro.UX;
using MkZ.Bluetooth;
using MkZ.WeatherStation.Utils;
using MkZ.WPF.PropertyGrid;
using MkZ.BlueMaestroLib;
using MkZ.Bluetooth.Sample;
using MkZ.Weather.RadexOne;
using MkZ.RadexOne;
using MkZ.Weather;
using MkZ.Physics;

namespace MkZWeatherStation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BluetoothWatcher _btWatcher = new BluetoothWatcher();

        private readonly ObservableCollection<BMDeviceRecordVM> _devices = new ObservableCollection<BMDeviceRecordVM>();

        private readonly RadexOneDeviceManager _radexDevice = new RadexOneDeviceManager();

        private static Settings Settings { get; } = new Settings();
        
        public UnitsDescriptor Units { get; set; } = new UnitsDescriptor();

        public MainWindow()
        {
            InitializeComponent();

            _listDevices.ItemsSource = _devices;

            _cmbTemperatureUnits.ItemsSource = Units.TemperatureUnits.GetEnum();
            _cmbAirPressureUnits.ItemsSource = Units.AirPressureUnits.GetEnum();
            
            _cmbTemperatureUnits.SelectedIndex = 0;
            _cmbAirPressureUnits.SelectedIndex = 0;

            ClearChart();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BMDatabaseMap.INSTANCE.Load();
            WeatherDataManager.INSTANCE.Load();
            WeatherDataManager.INSTANCE.Merge(BMDatabaseMap.INSTANCE.Databases[0]);
            //add empty point at the end - to show disconnection
            WeatherDataManager.INSTANCE.weatherDB.RadiationDataPoints.Add(new RadiationDataPoint());

            UpdateDeviceList();
            UpdateChart();

            _btWatcher.OnBMDeviceMsgReceivedAction = (info) => { SetInfo(info); };
            _btWatcher.OnBMDeviceCheckAction = (elapsed) => { /*UpdateDeviceList();*/ };
            _btWatcher.OnTimerSaveAction = (elapsed) => { BMDatabaseMap.INSTANCE.Save(); WeatherDataManager.INSTANCE.Save(); };
            _btWatcher.StartBluetoothSearch();

            BMDatabaseMap.INSTANCE.OnRecordAddedAction = (device, record) => { UpdateAllAsync(device, record); };

            _radexDevice.Init();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _btWatcher.StopBluetoothSearch();
            _radexDevice.Dispose();

            BMDatabaseMap.INSTANCE.Save();

            //add empty point at the end - to show disconnection
            WeatherDataManager.INSTANCE.weatherDB.RadiationDataPoints.Add(new RadiationDataPoint());
            WeatherDataManager.INSTANCE.Save();
        }

        private void _listDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BMDatabase db = GetSelectedDB();
            if (db != null)
                UpdateChartFromSelectedDB(db);
            else //no selection
                ClearChart();
        }

        public static void SetInfo(string info)
        {
            CommonTools.ExecuteOnUiThreadBeginInvoke(() =>
            {
                MainWindow wnd = (Application.Current.MainWindow as MainWindow);
                if (wnd != null)
                    wnd._txtInfo.Text = info;
            });
        }

        public static void UpdateAllAsync(BluetoothDevice device, BMRecordCurrent record)
        {
            WeatherDataManager.INSTANCE.weatherDB.BMRecords.Add(record);

            CommonTools.ExecuteOnUiThreadBeginInvoke(() =>
            {
                MainWindow wnd = (Application.Current.MainWindow as MainWindow);
                if (wnd != null)
                {
                    if(wnd._chkAutoUpdate.IsChecked.Value)
                    {
                        wnd.UpdateDeviceList();
                        wnd.UpdateChart(device);
                    }
                }
            });
        }

        private BMDatabase GetSelectedDB()
        {
            BMDeviceRecordVM selected = _listDevices.SelectedItem as BMDeviceRecordVM;
            if (selected != null && selected.Database != null)
                return selected.Database;
            return null;
        }

        private void UpdateDeviceList()
        {
            CommonTools.ExecuteOnUiThreadInvoke(() => 
            { 
                foreach (BMDatabase db in BMDatabaseMap.INSTANCE.Databases)
                {
                    UpdateOrAddBMDeviceRecordVM(db);
                }

                if(_devices.Count > 0)
                {
                    if (_listDevices.SelectedItem == null)
                        _listDevices.SelectedIndex = 0; //select first by default
                }
            });
        }

        private void UpdateOrAddBMDeviceRecordVM(BMDatabase db)
        {
            BMDeviceRecordVM dev = _devices.FirstOrDefault(d => d.DeviceAddress == db.Device.Address);
            if(dev == null)
            {
                dev = new BMDeviceRecordVM(db, Units);
                _devices.Add(dev);
            }
            dev.Index = _devices.IndexOf(dev);
            dev.Update(db, Units);
        }

        public static void UpdateChartData()
        {
            CommonTools.ExecuteOnUiThreadBeginInvoke(() =>
            {
                MainWindow wnd = (Application.Current.MainWindow as MainWindow);
                if (wnd != null)
                {
                    if (!wnd._chkAutoUpdate.IsChecked.Value)
                        return;

                    wnd.UpdateChart(null);
                }
            });
        }

        private void UpdateChart(BluetoothDevice device = null)
        {
            BMDatabase selected = GetSelectedDB();
            if (selected != null)
            {
                if(device == null || selected.Device.Address == device.Address)
                    UpdateChartFromSelectedDB(selected);
            }
            UpdateRadiationChart();
        }

        private void UpdateRadiationChart()
        {
            List<RadiationDataPoint> points = _radexDevice.GetLog();
            WeatherDataManager.INSTANCE.Merge(points);

            double days = double.Parse(((ComboBoxItem)_cmbDays.SelectedItem).Tag.ToString());
            TimeSpan daysBack = TimeSpan.FromDays(days);

            List<RadiationDataPoint> recordsIn = WeatherDataManager.INSTANCE.weatherDB.GetLastRecords(daysBack);
            if(recordsIn == null || recordsIn.Count == 0)
            {
                //_chart4?.Clea
            }
            else
            {
                double bucketIntervalInSec = GetSelectedIntervalInSeconds(recordsIn.First().Date, recordsIn.Last().Date, recordsIn.Count);

                List<IDataPoint> recordsOut = IDataPoint.ThinningByTime<RadiationDataPoint>(recordsIn, bucketIntervalInSec, eBucketingType.Maximum, true).ToList<IDataPoint>();

                _chart4?.UpdateChartRadiation(recordsOut, Units.RadiationUnits, true);

            }
        }

        private bool _isInUpdate = false;
        private void UpdateChartFromSelectedDB(BMDatabase db)
        {
            if (_isInUpdate)
                return;
            _isInUpdate = true;

            DateTime startUpdateTime = DateTime.Now;

            this.Title = "Weather - " + db.Device;

            this.Cursor = Cursors.AppStarting;

            Units.TemperatureUnits.Units = (eTemperatureUnits)_cmbTemperatureUnits.SelectedItem;
            Units.AirPressureUnits.Units = (eAirPressureUnits)_cmbAirPressureUnits.SelectedItem;

            double days = double.Parse(((ComboBoxItem)_cmbDays.SelectedItem).Tag.ToString());
            TimeSpan daysBack = TimeSpan.FromDays(days);

            List<BMRecordCurrent> recordsIn = db.GetLastRecords(daysBack);

            //List<BMRecordCurrent> recordsOut;
            //bool isIntervalZoom = false;
            //if (isIntervalZoom)
            //{
            //    if (_chkAutoZoom.IsChecked.Value)
            //    {
            //        int bucketSize = db.Records.Count / 1000; //1000 records after zoom
            //        _sliderDillute.Value = Math.Ceiling(Math.Pow(bucketSize, 0.5));
            //    }

            //    int zoom = (int)Math.Pow(2, _sliderDillute.Value);
            //    _txtDilluteValue.Text = string.Format("Dillute x{0:0.0}", zoom);

            //    recordsOut = db.DilluteByPointAndConvertUnits(recordsIn, zoom);
            //}
            //else //dillute by time
            //{
                double bucketIntervalInSec = GetSelectedIntervalInSeconds(recordsIn.First().Date, recordsIn.Last().Date, recordsIn.Count);

                ////////
                //
                List<IDataPoint> recordsOut = IDataPoint.ThinningByTime<BMRecordCurrent>(recordsIn, bucketIntervalInSec, eBucketingType.Average, false).ToList<IDataPoint>();
            //}

            BMDeviceRecordVM dev = _listDevices.SelectedItem as BMDeviceRecordVM;
            _chart1.UpdateChartTemperature(recordsOut, Units.TemperatureUnits, dev.IsActive);
            _chart2.UpdateChartHumidity(recordsOut, Units.RelativeHumidityUnits, dev.IsActive);
            _chart3.UpdateChartAirPressure(recordsOut, Units.AirPressureUnits, dev.IsActive);

            TimeSpan tsElapsed = DateTime.Now - startUpdateTime;
            _txtDilluteResult.Text = string.Format("Total: {0:###,###} -> {1:###,###} -> {2} ({3:0.0} ms)", 
                db.Records.Count, recordsIn.Count, recordsOut.Count, tsElapsed.TotalMilliseconds);

            this.Cursor = Cursors.Arrow;

            _isInUpdate = false;
        }

        private double GetSelectedIntervalInSeconds(DateTime firstDate, DateTime lastDate, int recordsCount)
        {
            double bucketIntervalInSec = 60 * double.Parse(((ComboBoxItem)_cmbInterval.SelectedItem).Tag.ToString());
            if (_chkAutoZoom.IsChecked.Value)
            {
                bucketIntervalInSec = 15;
                //_cmbInterval.SelectedIndex = 0; //all
                if (recordsCount >= IDataPoint.MIN_RECORDS_TO_FILTER)
                {
                    TimeSpan range = lastDate - firstDate;
                    bucketIntervalInSec = range.TotalSeconds / 1000; //interval in seconds to get 1000 points
                    bucketIntervalInSec = GetClosestIntervalAndSetSelectInComboBox(bucketIntervalInSec);
                }
            }
            return bucketIntervalInSec;
        }

        private double GetClosestIntervalAndSetSelectInComboBox(double interval)
        {
            List<double> intervals = _cmbInterval.Items.Cast<ComboBoxItem>().Select(item => 60.0 * double.Parse(item.Tag.ToString())).ToList();
            
            int index = intervals.FindIndex(x => x > interval);

            _cmbInterval.SelectedIndex = index;

            return intervals[index];
        }

        private void ClearChart()
        {
            if (_isInUpdate)
                return;
            _isInUpdate = true;

            _chart1.UpdateChartTemperature(null, Units.TemperatureUnits, false);
            _chart2.UpdateChartHumidity(null, Units.RelativeHumidityUnits, false);
            _chart3.UpdateChartAirPressure(null, Units.AirPressureUnits, false);
            _chart4.UpdateChartRadiation(null, Units.RadiationUnits, false);

            _isInUpdate = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            foreach (BMDatabase db in BMDatabaseMap.INSTANCE.Databases)
            {
                db.SaveAs(db.GenerateFileName(), 1);
            }
            WeatherDataManager.INSTANCE.SaveAs(WeatherDataManager.GenerateFileName(), 1);
            this.Cursor = Cursors.Arrow;
        }

        private void SaveDillutedButton_Click(object sender, RoutedEventArgs e)
        {
            BMDatabase db = GetSelectedDB();
            if (db == null)
                return;

            double bucketIntervalInSec = 60 * double.Parse(((ComboBoxItem)_cmbInterval.SelectedItem).Tag.ToString());
            SaveDatabaseAs(db, bucketIntervalInSec);
        }

        private void SaveDatabaseAs(BMDatabase db, double bucketIntervalInSec)
        {
            SaveFileDialog _saveFileDialog = new SaveFileDialog()
            {
                RestoreDirectory = true,
                Filter = "All supported files|*.xml|All files (*.*)|*.*",
                FileName = db.GenerateFileName()
                //InitialDirectory = _openDirectory
            };

            if (_saveFileDialog.ShowDialog(this).Value)
            {
                this.Cursor = Cursors.Wait;
                db.SaveAs(_saveFileDialog.FileName, bucketIntervalInSec);
                this.Cursor = Cursors.Arrow;
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog()
            {
                Multiselect = true,
                RestoreDirectory = true,
                Filter = "All supported files|*.xml|All files (*.*)|*.*",
                //InitialDirectory = _openDirectory
            };

            if (_openFileDialog.ShowDialog(this).Value)
            {
                this.Cursor = Cursors.Wait;

                //_openDirectory = Path.GetDirectoryName(_openFileDialog.FileName);

                List<string> fileNamesList = new List<string>(_openFileDialog.FileNames);
                fileNamesList.Sort();

                BMDatabase dbAll = null; 
                for (int i = 0; i < fileNamesList.Count; i++)
                {
                    BMDatabase db = BMDatabase.Open(fileNamesList[i]);
                    if (db != null)
                        dbAll = BMDatabaseMap.INSTANCE.Merge(db);
                    else
                        MessageBox.Show("Error open file: \n" + fileNamesList[i]);
                }

                UpdateDeviceList();
                UpdateChart();

                this.Cursor = Cursors.Arrow;
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            BMDatabase db = GetSelectedDB();
            if (db != null)
            {
                db.Records.Clear();
                UpdateDeviceList();
                UpdateChart();
            }
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            string SelectedBleDeviceId = "BluetoothLE#BluetoothLEc0:b6:f9:73:92:8a-d0:7e:ef:ef:61:4f";
            
            using (BTConnectionDirect client = new BTConnectionDirect())
            {
                client.Connect(SelectedBleDeviceId);
            }
        }

        private void Scenario1Button_Click(object sender, RoutedEventArgs e)
        {
            Scenario1_Discovery client = new Scenario1_Discovery();

            try
            {
                client.ShowActivated = true;
                client.Owner = this;
                client.ShowDialog();
            }
            catch (Exception err)
            {
                CommonTools.ErrorMessage(err.ToString());
            }
        }

        private static bool IsUserDragged(UIElement e)
        {
            return e.IsFocused || e.IsMouseDirectlyOver || e.IsMouseOver || e.IsKeyboardFocused || e.IsKeyboardFocusWithin;
        }

        private void OnUpdateChart(object sender, RoutedEventArgs e)
        {
            UpdateChart();
        }

        private void _cmbInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsUserDragged(_cmbInterval))
                _chkAutoZoom.IsChecked = false;
            UpdateChart();
        }

        private void MenuSettings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow.ShowOptions(this, Settings, "Barometer App Settings", 300);
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
