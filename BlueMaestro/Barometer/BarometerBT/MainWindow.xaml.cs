using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        public static void UpdateList(Dictionary<ushort, DeviceRecordVM> devices)
        {
            CommonTools.ExecuteOnUiThreadBeginInvoke(() =>
            {
                MainWindow wnd = (Application.Current.MainWindow as MainWindow);
                if(wnd != null)
                    wnd._listDevices.ItemsSource = devices.Values.ToList();
            });
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

        internal static void SetChartData(BMDatabase db)
        {
            CommonTools.ExecuteOnUiThreadBeginInvoke(() =>
            {
                MainWindow wnd = (Application.Current.MainWindow as MainWindow);
                if (wnd != null)
                {
                    if (!wnd._chkAutoUpdate.IsChecked.Value)
                        return;

                    wnd.UpdateChart(db);
                }
            });
        }

        private void UpdateChart(BMDatabase db)
        {
            this.Cursor = Cursors.AppStarting;

            if (_chkAutoZoom.IsChecked.Value)
            {
                int bucketSize = db.Records.Count / 1000; //1000 records after zoom
                _sliderDillute.Value = Math.Ceiling(Math.Pow(bucketSize, 0.5));
            }

            int zoom = (int)Math.Pow(2, _sliderDillute.Value);
            _txtDilluteValue.Text = string.Format("Dillute x{0:0.0}", zoom);

            List<BMRecordCurrent> records = db.Dillute(zoom);

            _txtDilluteResult.Text = string.Format("Count: {0} -> {1}", db.Records.Count, records.Count);

            _chart1.UpdateChartTemperature(records);
            _chart2.UpdateChartHumidity(records);
            _chart3.UpdateChartAirPressure(records);

            this.Cursor = Cursors.Arrow;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var db = BMDatabaseMap.INSTANCE.Map.FirstOrDefault();
            if(db.Value != null)
                db.Value.Save();
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

                UpdateChart(dbAll);
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

            var db = BMDatabaseMap.INSTANCE.Map.FirstOrDefault();
            if (db.Value != null)
                UpdateChart(db.Value);
        }

        private static bool IsUserDragged(UIElement e)
        {
            return e.IsFocused || e.IsMouseDirectlyOver || e.IsMouseOver || e.IsKeyboardFocused || e.IsKeyboardFocusWithin;
        }

        private void _chkAutoZoom_Checked(object sender, RoutedEventArgs e)
        {
            var db = BMDatabaseMap.INSTANCE.Map.FirstOrDefault();
            if (db.Value != null)
                UpdateChart(db.Value);
        }
    }
}
