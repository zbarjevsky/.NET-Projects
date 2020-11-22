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
        private readonly ObservableCollection<BMDeviceRecordVM> _devices;

        public MainWindow()
        {
            InitializeComponent();

            _devices = new ObservableCollection<BMDeviceRecordVM>();
            _listDevices.ItemsSource = _devices;

            _cmbTemperatureUnits.ItemsSource = UnitsDescriptor.GetEnumTemperatureUnits();
            _cmbAirPressureUnits.ItemsSource = UnitsDescriptor.GetEnumAirPressureUnits();
            
            _cmbTemperatureUnits.SelectedIndex = 0;
            _cmbAirPressureUnits.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveButton_Click(sender, null);
        }

        private void _listDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BMDatabase db = GetSelectedDB();
            if (db != null)
                UpdateChartFromSelectedDB(db);
            else
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
            foreach (BMDatabase db in BMDatabaseMap.INSTANCE.Map.Values)
            {
                UpdateOrAddBMDeviceRecordVM(db);
            }

            if(_devices.Count > 0)
            {
                if (_listDevices.SelectedItem == null)
                    _listDevices.SelectedIndex = 0; //select first by default
            }
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

            bool isIntervalZoom = false;
            List<BMRecordCurrent> records;
            if (isIntervalZoom)
            {
                if (_chkAutoZoom.IsChecked.Value)
                {
                    int bucketSize = db.Records.Count / 1000; //1000 records after zoom
                    _sliderDillute.Value = Math.Ceiling(Math.Pow(bucketSize, 0.5));
                }

                int zoom = (int)Math.Pow(2, _sliderDillute.Value);
                _txtDilluteValue.Text = string.Format("Dillute x{0:0.0}", zoom);

                records = db.DilluteByPointAndConvertUnits(zoom);
            }
            else //dillute by time
            {
                const double HOUR = 3600, MINUTE = 60;
                double[] intervals = new double[] { 15, 0.5*MINUTE, MINUTE, 10*MINUTE, 15*MINUTE, 0.5*HOUR, HOUR, 2*HOUR, 3*HOUR, 6*HOUR, 12*HOUR };

                double combineIntervalInSec = intervals[(int)_sliderDillute.Value]; //min 10 seconds
                if (_chkAutoZoom.IsChecked.Value)
                {
                    combineIntervalInSec = 15; 
                    _sliderDillute.Value = 0;
                    if(db.Records.Count > 1000)
                    {
                        TimeSpan range = db.Records.Last().Date - db.Records.First().Date;
                        combineIntervalInSec = range.TotalSeconds / 1000;
                        combineIntervalInSec = intervals.First(x => x >= combineIntervalInSec);
                        _sliderDillute.Value = intervals.ToList().IndexOf(combineIntervalInSec);
                    }
                }

                TimeSpan ts = TimeSpan.FromSeconds(combineIntervalInSec);
                _txtDilluteValue.Text = string.Format("Interval: {0}", ts.ToString(@"d\d\ hh\h\ mm\m\ ss\s"));

                records = db.DilluteByTimeAndConvertUnits(combineIntervalInSec);
            }

            _txtDilluteResult.Text = string.Format("Count: {0} -> {1}", db.Records.Count, records.Count);

            _chart1.UpdateChartTemperature(records, db.Units.GetTemperatureUnitsDesc());
            _chart2.UpdateChartHumidity(records);
            _chart3.UpdateChartAirPressure(records, db.Units.GetAirpressureUnitsDesc());

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
    }
}
