using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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


using BarometerBT.BlueMaestro;
using BarometerBT.BlueMaestro.UX;
using BarometerBT.Bluetooth;
using BarometerBT.Utils;
using Microsoft.Win32;

namespace BarometerBT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BluetoothConnection _btWatcher = new BluetoothConnection();

        private readonly ObservableCollection<BMDeviceRecordVM> _devices = new ObservableCollection<BMDeviceRecordVM>();

        public MainWindow()
        {
            InitializeComponent();

            _listDevices.ItemsSource = _devices;

            _cmbTemperatureUnits.ItemsSource = UnitsDescriptor.GetEnumTemperatureUnits();
            _cmbAirPressureUnits.ItemsSource = UnitsDescriptor.GetEnumAirPressureUnits();
            
            _cmbTemperatureUnits.SelectedIndex = 0;
            _cmbAirPressureUnits.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _btWatcher.OnBMDeviceMsgReceivedAction = (info) => { SetInfo(info); };
            _btWatcher.OnBMDeviceCheckAction = (elapsed) => { UpdateDeviceList(); };
            _btWatcher.StartBluetoothSearch();

            BMDatabaseMap.INSTANCE.OnRecordAddedAction = (r) => { MainWindow.UpdateAllAsync(r); };
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _btWatcher.StopBluetoothSearch();
            SaveButton_Click(sender, null);
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

        public static void UpdateAllAsync(BMRecordCurrent r)
        {
            CommonTools.ExecuteOnUiThreadBeginInvoke(() =>
            {
                MainWindow wnd = (Application.Current.MainWindow as MainWindow);
                if (wnd != null)
                {
                    wnd.UpdateDeviceList();
                    wnd.UpdateChart(r);
                }
            });
        }

        private BMDatabase GetSelectedDB()
        {
            UpdateListSelectionUI();

            BMDeviceRecordVM selected = _listDevices.SelectedItem as BMDeviceRecordVM;
            if (selected != null && selected.Database != null)
                return selected.Database;
            return null;
        }

        private void UpdateListSelectionUI()
        {
            BMDeviceRecordVM sel = _listDevices.SelectedItem as BMDeviceRecordVM;
            for (int i = 0; i < _listDevices.Items.Count; i++)
            {
                BMDeviceRecordVM vm = (BMDeviceRecordVM)_listDevices.Items[i];
                vm.IsSelected = (vm.DeviceAddress == sel.DeviceAddress);
                vm.Index = i;
            }
        }

        private void UpdateDeviceList()
        {
            CommonTools.ExecuteOnUiThreadInvoke(() => 
            { 
                foreach (BMDatabase db in BMDatabaseMap.INSTANCE.Map.Values)
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
                dev = new BMDeviceRecordVM(db);
                _devices.Add(dev);
            }
            dev.Index = _devices.IndexOf(dev);
            dev.Update(db);
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

        private void UpdateChart(BMRecordCurrent r = null)
        {
            BMDatabase selected = GetSelectedDB();
            if (selected != null)
            {
                if(r == null || selected.Device.Address == r.Address)
                    UpdateChartFromSelectedDB(selected);
            }
        }

        private bool _isInUpdate = false;
        private void UpdateChartFromSelectedDB(BMDatabase db)
        {
            if (_isInUpdate)
                return;
            _isInUpdate = true;

            this.Title = "Barometer - " + db.Device;

            this.Cursor = Cursors.AppStarting;

            db.Units.TemperatureUnits = (TemperatureUnits)_cmbTemperatureUnits.SelectedItem;
            db.Units.AirPressureUnits = (AirPressureUnits)_cmbAirPressureUnits.SelectedItem;

            double[] daysValues = new double[] { 0.25, 0.5, 1, 3, 7, 14, 30, 90, 180, 365, 3000 };
            TimeSpan daysBack = TimeSpan.FromDays(daysValues[(int)_sliderLastNDays.Value]);
            if(daysBack.TotalHours < 24)
                _txtLastNDays.Text = $"Show Last {daysBack.Hours} hours";
            else
                _txtLastNDays.Text = $"Show Last {daysBack.Days} days";

            List<BMRecordCurrent> recordsIn = db.GetLastRecords(daysBack);

            bool isIntervalZoom = false;
            List<BMRecordCurrent> recordsOut;
            if (isIntervalZoom)
            {
                if (_chkAutoZoom.IsChecked.Value)
                {
                    int bucketSize = db.Records.Count / 1000; //1000 records after zoom
                    _sliderDillute.Value = Math.Ceiling(Math.Pow(bucketSize, 0.5));
                }

                int zoom = (int)Math.Pow(2, _sliderDillute.Value);
                _txtDilluteValue.Text = string.Format("Dillute x{0:0.0}", zoom);

                recordsOut = db.DilluteByPointAndConvertUnits(recordsIn, zoom);
            }
            else //dillute by time
            {
                const double HOUR = 3600, MINUTE = 60;
                double[] intervals = new double[] { 1.0, 0.5*MINUTE, MINUTE, 10*MINUTE, 15*MINUTE, 0.5*HOUR, HOUR, 2*HOUR, 3*HOUR, 6*HOUR, 12*HOUR };

                double bucketIntervalInSec = intervals[(int)_sliderDillute.Value]; //min 10 seconds
                if (_chkAutoZoom.IsChecked.Value)
                {
                    bucketIntervalInSec = 15; 
                    _sliderDillute.Value = 0;
                    if(recordsIn.Count > 1000)
                    {
                        TimeSpan range = recordsIn.Last().Date - recordsIn.First().Date;
                        bucketIntervalInSec = range.TotalSeconds / 1000;
                        bucketIntervalInSec = intervals.First(x => x >= bucketIntervalInSec);
                        _sliderDillute.Value = intervals.ToList().IndexOf(bucketIntervalInSec);
                    }
                }

                TimeSpan ts = TimeSpan.FromSeconds(bucketIntervalInSec);
                _txtDilluteValue.Text = string.Format("Interval: {0}", ts.ToString(@"d\d\ hh\h\ mm\m\ ss\s"));

                recordsOut = db.DilluteByTimeAndConvertUnits(recordsIn, bucketIntervalInSec);
            }

            _txtDilluteResult.Text = string.Format("Count: {0} -> {1} -> {2}", db.Records.Count, recordsIn.Count, recordsOut.Count);

            _chart1.UpdateChartTemperature(recordsOut, db.Units.GetTemperatureUnitsDesc());
            _chart2.UpdateChartHumidity(recordsOut);
            _chart3.UpdateChartAirPressure(recordsOut, db.Units.GetAirpressureUnitsDesc());

            this.Cursor = Cursors.Arrow;

            _isInUpdate = false;
        }

        private void ClearChart()
        {
            if (_isInUpdate)
                return;
            _isInUpdate = true;

            _chart1.UpdateChartTemperature(null, "");
            _chart2.UpdateChartHumidity(null, "");
            _chart3.UpdateChartAirPressure(null, "");

            _isInUpdate = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            foreach (BMDatabase db in BMDatabaseMap.INSTANCE.Map.Values)
            {
                db.Save();
            }
            this.Cursor = Cursors.Arrow;
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
                //_openDirectory = Path.GetDirectoryName(_openFileDialog.FileName);

                List<string> fileNamesList = new List<string>(_openFileDialog.FileNames);
                fileNamesList.Sort();

                BMDatabase dbAll = null; 
                for (int i = 0; i < fileNamesList.Count; i++)
                {
                    BMDatabase db = BMDatabase.Open(fileNamesList[i]);
                    if(db != null)
                        dbAll = BMDatabaseMap.INSTANCE.Merge(db);
                }

                UpdateDeviceList();
                UpdateChart();
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

        private void Scenario2Button_Click(object sender, RoutedEventArgs e)
        {
            Scenario2_Client client = new Scenario2_Client();
            
            BMDatabase db = BMDatabaseMap.INSTANCE.Map.FirstOrDefault().Value;

            if (db != null)
            {
                client.rootPage.SelectedBleDeviceId = "BluetoothLE#BluetoothLEc0:b6:f9:73:92:8a-d0:7e:ef:ef:61:4f"; //db.Device.Address;
                client.rootPage.SelectedBleDeviceName = db.Device.Name;

                client.ShowActivated = true;
                client.Owner = this;
                client.ShowDialog();
            }
            else
            {
                CommonTools.ErrorMessage("Not Found");
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

        private void _sliderDillute_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(IsUserDragged(_sliderDillute))
                _chkAutoZoom.IsChecked = false;

            UpdateChart();
        }

        private static bool IsUserDragged(UIElement e)
        {
            return e.IsFocused || e.IsMouseDirectlyOver || e.IsMouseOver || e.IsKeyboardFocused || e.IsKeyboardFocusWithin;
        }

        private void _chkAutoZoom_Checked(object sender, RoutedEventArgs e)
        {
            UpdateChart();
        }

        private void _cmbTemperatureUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void _cmbAirPressureUnits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateChart();
        }

        private void _sliderDays_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateChart();
        }
    }
}
