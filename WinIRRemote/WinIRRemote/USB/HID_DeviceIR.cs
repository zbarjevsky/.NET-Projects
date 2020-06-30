using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using Windows.Devices.Enumeration;
using Windows.Devices.HumanInterfaceDevice;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;

namespace WinIRRemote.USB
{
    public class HID_DeviceIR
    {
        public static async void Write()
        {
            string deviceID = @"HID\VID_10C4&PID_8468\6&1E482461&0&0000";
            HidDevice device = await HidDevice.FromIdAsync(deviceID, FileAccessMode.Read);
            if (device != null)
            {
                // Input reports contain data from the device.
                device.InputReportReceived += async (sender, args) =>
                {
                    HidInputReport inputReport = args.Report;
                    IBuffer buffer = inputReport.Data;

                    // Create a DispatchedHandler as we are interracting with the UI directly and the
                    // thread that this function is running on might not be the UI thread; 
                    // if a non-UI thread modifies the UI, an exception is thrown.

                    //await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    //    new DispatchedHandler(() =>
                    //    {
                    string info = "\nHID Input Report: " + inputReport.ToString() +
                    "\nTotal number of bytes received: " + buffer.Length.ToString();
                    Debug.WriteLine("*** " + info);

                    //    }));
                };
            }
        }
        // Find HID devices.
        public static async void EnumerateHidDevices(USB_Device dev)
        {
            string info = "HID device not found";

            // Create the selector.
            string selector = HidDevice.GetDeviceSelector(dev.usagePage, dev.usageId, dev.vendorId, dev.productId);

            // Enumerate devices using the selector.
            var devices = await DeviceInformation.FindAllAsync(selector);

            if (devices.Any())
            {
                // At this point the device is available to communicate with
                // So we can send/receive HID reports from it or 
                // query it for control descriptions.
                info = "HID devices found: " + devices.Count;
                Debug.WriteLine("*** " + info);

                // Open the target HID device.
                HidDevice device = await HidDevice.FromIdAsync(devices.ElementAt(0).Id, FileAccessMode.ReadWrite);

                if (device != null)
                {
                    // Input reports contain data from the device.
                    device.InputReportReceived += async (sender, args) =>
                    {
                        HidInputReport inputReport = args.Report;
                        IBuffer buffer = inputReport.Data;

                        // Create a DispatchedHandler as we are interracting with the UI directly and the
                        // thread that this function is running on might not be the UI thread; 
                        // if a non-UI thread modifies the UI, an exception is thrown.

                        //await Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                        //    new DispatchedHandler(() =>
                        //    {
                                info += "\nHID Input Report: " + inputReport.ToString() +
                                "\nTotal number of bytes received: " + buffer.Length.ToString();
                        Debug.WriteLine("*** " + info);

                        //    }));
                    };
                }

            }
            else
            {
                // There were no HID devices that met the selector criteria.
                info = "HID device not found";
                Debug.WriteLine("*** " + info);

            }
        }
    }
}