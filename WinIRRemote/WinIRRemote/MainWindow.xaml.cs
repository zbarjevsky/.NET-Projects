using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
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
using WinIRRemote.USB;

namespace WinIRRemote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private USBControl _USBControl;

        public MainWindow()
        {
            InitializeComponent();

            //start listening for USB
            _USBControl = new USBControl();
            _USBControl.DeviceArrivedAction = (e) => 
            {
                Debug.WriteLine("================= Arrived==========================");
                USBDeviceInfo.EnumDeviceProperties(e);
                WPFUtils.ExecuteOnUIThreadWPF(() => 
                {
                    txtOutput.AppendText("watcher_EventArrived: " + e + "\n"); 
                    return 0; 
                }); 
            };

            _USBControl.DeviceRemovedAction = (e) =>
            {
                Debug.WriteLine("================= Removed ==========================");
                USBDeviceInfo.EnumDeviceProperties(e);
                WPFUtils.ExecuteOnUIThreadWPF(() =>
                {
                    txtOutput.AppendText("watcher_EventRemoved: " + e + "\n");
                    return 0;
                });
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Test();
        }

        private void Test()
        {
            var names = SerialPort.GetPortNames();

            var usb1 = USBUtils.GetUSBDevices("VID_10C4");

            //HID_DeviceIR.Write();
            HID_DeviceIR.EnumerateHidDevices(new BaseusIR());


            //var usb2 = USBUtils.GetUSBDevices();

            //List<USBDeviceInfo> diff = new List<USBDeviceInfo>();
            //foreach (USBDeviceInfo dev in usb1)
            //{
            //    if (!Found(dev, usb2))
            //        diff.Add(dev);
            //}
        }

        private bool Found(USBDeviceInfo dev, List<USBDeviceInfo> usb)
        {
            foreach (USBDeviceInfo deviceInfo in usb)
            {
                if (deviceInfo.DeviceID == dev.DeviceID)
                    return true;
            }
            return false;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
