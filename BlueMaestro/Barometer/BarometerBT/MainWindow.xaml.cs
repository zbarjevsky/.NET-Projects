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
                    wnd.UpdateChart(db);
            });
        }

        private void UpdateChart(BMDatabase db)
        {
            _chart1.UpdateChartTemperature(db);
            _chart2.UpdateChartHumidity(db);
            _chart3.UpdateChartAirPressure(db);
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
                Multiselect = false,
                RestoreDirectory = true,
                Filter = "All supported files|*.xml|All files (*.*)|*.*",
                //InitialDirectory = _openDirectory
            };

            if (_openFileDialog.ShowDialog(this).Value)
            {
                //_openDirectory = Path.GetDirectoryName(_openFileDialog.FileName);

                List<string> fileNamesList = new List<string>(_openFileDialog.FileNames);
                fileNamesList.Sort();

                BMDatabase db = BMDatabase.Open(_openFileDialog.FileName);

                //var db = BMDatabaseMap.INSTANCE.Map.FirstOrDefault();
                //if (db.Value != null)
                //    db.Value.Save();
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
    }
}
