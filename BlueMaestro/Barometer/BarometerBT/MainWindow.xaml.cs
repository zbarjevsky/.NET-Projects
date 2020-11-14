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
using BarometerBT.Utils;

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

        internal static void SetChartData(List<BlueMaestroRecord> weatherRecords)
        {
            CommonTools.ExecuteOnUiThreadBeginInvoke(() =>
            {
                MainWindow wnd = (Application.Current.MainWindow as MainWindow);
                if (wnd != null)
                    wnd.UpdateChart(weatherRecords);
            });
        }

        private void UpdateChart(List<BlueMaestroRecord> weatherRecords)
        {
            _chart1.UpdateChart1(weatherRecords);
            _chart2.UpdateChart2(weatherRecords);
            _chart3.UpdateChart3(weatherRecords);
        }
    }
}
