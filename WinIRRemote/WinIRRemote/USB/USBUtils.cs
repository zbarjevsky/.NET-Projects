using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WinIRRemote.USB
{

    public class USBDeviceInfo
    {
        public USBDeviceInfo(string deviceID, string classGuid, string pnpDeviceID, string description, string caption)
        {
            this.DeviceID = deviceID;
            this.ClassGuid = classGuid;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;
            if(caption != null)
                this.Caption = caption;
        }

        public USBDeviceInfo(ManagementBaseObject device)
            : this(
                GetPropertyValue(device, "DeviceID"),
                GetPropertyValue(device, "ClassGuid"),
                GetPropertyValue(device, "PNPDeviceID"),
                GetPropertyValue(device, "Description"),
                GetPropertyValue(device, "Caption"))
        {
            EnumDeviceProperties(device);
        }

        public string ClassGuid { get; private set; } = "";
        public string DeviceID { get; private set; } = "";
        public string PnpDeviceID { get; private set; } = "";
        public string Description { get; private set; } = "";
        public string Caption { get; private set; } = "";

        public static void EnumDeviceProperties(ManagementBaseObject device)
        {
            foreach (var property in device.Properties)
            {
                Debug.WriteLine(string.Format("{0}|{1}: {2}", property.Origin, property.Name, property.Value));
            }
        }

        private static string GetPropertyValue(ManagementBaseObject device, string propertyName)
        {
            try
            {
                object o = device[propertyName];
                return (string)device.GetPropertyValue(propertyName);
            }
            catch (Exception err)
            {
                Debug.WriteLine("Cannot get property: " + propertyName + ", Error: " + err.Message);
                return "";
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Caption))
                return "Cap: "+Caption;
            if (!string.IsNullOrWhiteSpace(Description))
                return "Desc: "+Description;
            return DeviceID;
        }
    }

    public enum WMI_Objects
    {
        Win32_USBHub,
        Win32_USBControllerDevice,
        Win32_PnPEntity
    }

    public class USBUtils
    {
        public static List<USBDeviceInfo> GetUSBDevices(string id_contains = "", WMI_Objects from = WMI_Objects.Win32_PnPEntity)
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From " + from))// Win32_USBHub"))
                collection = searcher.Get();

            foreach (ManagementObject device in collection)
            {
                USBDeviceInfo dev = new USBDeviceInfo(device);
                devices.Add(dev);
                if (dev.DeviceID.Contains(id_contains))
                {
                    Debug.WriteLine("================="+dev.Caption+"==========================");
                    foreach (var property in device.Properties)
                    {
                        Debug.WriteLine(string.Format("{0}|{1}: {2}", property.Origin, property.Name, property.Value));
                    }
                }
            }

            collection.Dispose();

            devices.Sort((d1, d2) => d1.Caption.CompareTo(d2.Caption));
            List < USBDeviceInfo > outList = devices.Where(n => n.DeviceID.Contains(id_contains)).ToList();
            return outList;
        }
    }
}
