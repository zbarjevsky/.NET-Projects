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
        private readonly BluetoothWatcher _btWatcher = new BluetoothWatcher();

        private readonly ObservableCollection<BMDeviceRecordVM> _devices = new ObservableCollection<BMDeviceRecordVM>();

        public MainWindow()
        {
            InitializeComponent();

            _listDevices.ItemsSource = _devices;

            _cmbTemperatureUnits.ItemsSource = UnitsDescriptor.GetEnumTemperatureUnits();
            _cmbAirPressureUnits.ItemsSource = UnitsDescriptor.GetEnumAirPressureUnits();
            
            _cmbTemperatureUnits.SelectedIndex = 0;
            _cmbAirPressureUnits.SelectedIndex = 0;

            ClearChart();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BMDatabaseMap.INSTANCE.Load();
            UpdateDeviceList();
            UpdateChart();

            _btWatcher.OnBMDeviceMsgReceivedAction = (info) => { SetInfo(info); };
            _btWatcher.OnBMDeviceCheckAction = (elapsed) => { UpdateDeviceList(); };
            _btWatcher.StartBluetoothSearch();

            BMDatabaseMap.INSTANCE.OnRecordAddedAction = (r) => { MainWindow.UpdateAllAsync(r); };
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _btWatcher.StopBluetoothSearch();
            BMDatabaseMap.INSTANCE.Save();
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
                    if(wnd._chkAutoUpdate.IsChecked.Value)
                    {
                        wnd.UpdateDeviceList();
                        wnd.UpdateChart(r);
                    }
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

            DateTime startUpdateTime = DateTime.Now;

            this.Title = "Barometer - " + db.Device;

            this.Cursor = Cursors.AppStarting;

            db.Units.TemperatureUnits = (TemperatureUnits)_cmbTemperatureUnits.SelectedItem;
            db.Units.AirPressureUnits = (AirPressureUnits)_cmbAirPressureUnits.SelectedItem;

            double days = double.Parse(((ComboBoxItem)_cmbDays.SelectedItem).Tag.ToString());
            TimeSpan daysBack = TimeSpan.FromDays(days);

            List<BMRecordCurrent> recordsIn = db.GetLastRecords(daysBack);

            List<BMRecordCurrent> recordsOut;
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
            {
                double bucketIntervalInSec = 60 * double.Parse(((ComboBoxItem)_cmbInterval.SelectedItem).Tag.ToString());
                if (_chkAutoZoom.IsChecked.Value)
                {
                    bucketIntervalInSec = 15;
                    //_cmbInterval.SelectedIndex = 0; //all
                    if(recordsIn.Count >= BMDatabase.MIN_RECORDS_TO_FILTER)
                    {
                        TimeSpan range = recordsIn.Last().Date - recordsIn.First().Date;
                        bucketIntervalInSec = range.TotalSeconds / 1000; //interval in seconds to get 1000 points
                        bucketIntervalInSec = GetClosestIntervalAndSetSelectInComboBox(bucketIntervalInSec);
                    }
                }

                ////////
                //
                recordsOut = BMDatabase.DilluteByTime(recordsIn, bucketIntervalInSec);
            }

            BMDeviceRecordVM dev = _listDevices.SelectedItem as BMDeviceRecordVM;
            _chart1.UpdateChartTemperature(recordsOut, db.Units, dev.IsActive);
            _chart2.UpdateChartHumidity(recordsOut, db.Units, dev.IsActive);
            _chart3.UpdateChartAirPressure(recordsOut, db.Units, dev.IsActive);

            TimeSpan tsElapsed = DateTime.Now - startUpdateTime;
            _txtDilluteResult.Text = string.Format("Total: {0:###,###} -> {1:###,###} -> {2} ({3:0.0} ms)", 
                db.Records.Count, recordsIn.Count, recordsOut.Count, tsElapsed.TotalMilliseconds);

            this.Cursor = Cursors.Arrow;

            _isInUpdate = false;
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

            UnitsDescriptor units = new UnitsDescriptor();
            _chart1.UpdateChartTemperature(null, units, false);
            _chart2.UpdateChartHumidity(null, units, false);
            _chart3.UpdateChartAirPressure(null, units, false);

            _isInUpdate = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            foreach (BMDatabase db in BMDatabaseMap.INSTANCE.Databases)
            {
                db.SaveAs(db.GenerateFileName(), 1);
            }
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
    }
}
