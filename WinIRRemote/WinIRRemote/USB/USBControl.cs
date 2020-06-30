using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WinIRRemote.USB
{
    public class USBControl : IDisposable
    {
        // used for monitoring plugging and unplugging of USB devices.
        private ManagementEventWatcher watcherAttach;
        private ManagementEventWatcher watcherRemove;

        public Action<ManagementBaseObject> DeviceArrivedAction = (e) => { };
        public Action<ManagementBaseObject> DeviceRemovedAction = (e) => { };

        public USBControl()
        {
            // Add USB plugged event watching
            watcherAttach = new ManagementEventWatcher();
            //var queryAttach = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            watcherAttach.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
            watcherAttach.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            watcherAttach.Start();

            // Add USB unplugged event watching
            watcherRemove = new ManagementEventWatcher();
            //var queryRemove = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            watcherRemove.EventArrived += new EventArrivedEventHandler(watcher_EventRemoved);
            watcherRemove.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            watcherRemove.Start();
        }

        /// <summary>
        /// Used to dispose of the USB device watchers when the USBControl class is disposed of.
        /// </summary>
        public void Dispose()
        {
            if (watcherAttach != null)
            {
                watcherAttach.Stop();
                //Thread.Sleep(1000);
                watcherAttach.Dispose();
                watcherAttach = null;
            }
            if (watcherRemove != null)
            {
                watcherRemove.Stop();
                //Thread.Sleep(1000);
                watcherRemove.Dispose();
                watcherRemove = null;
            }
        }

        private void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            ulong time = (ulong)e.NewEvent["TIME_CREATED"];
            DateTime dt = DateTime.FromFileTimeUtc((long)time);

            var sec = e.NewEvent["SECURITY_DESCRIPTOR"];

            foreach (var item in e.NewEvent.Qualifiers)
            {
                Debug.WriteLine("Qualifier: " + item.Name + " : " + item.Value);
            }

            Debug.WriteLine("watcher_EventArrived");
            DeviceArrivedAction(e.NewEvent);
        }

        private void watcher_EventRemoved(object sender, EventArrivedEventArgs e)
        {
            ulong time = (ulong)e.NewEvent["TIME_CREATED"];
            DateTime dt = DateTime.FromFileTimeUtc((long)time);

            var sec = e.NewEvent["SECURITY_DESCRIPTOR"];

            foreach (var item in e.NewEvent.Qualifiers)
            {
                Debug.WriteLine("Qualifier: " + item.Name + " : " + item.Value);
            }

            Debug.WriteLine("watcher_EventRemoved");
            DeviceRemovedAction(e.NewEvent);
        }

        ~USBControl()
        {
            this.Dispose();
        }
    }
}
