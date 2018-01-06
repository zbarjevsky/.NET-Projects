using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskCryptorHelper
{
    public class DriveTools
    {
        public static List<string> GetDriveList()
        {
            List<string> list = new List<string>();

            DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                //if (drive.DriveType == DriveType.Removable) // || m_chkSowAllDrives.Checked)
                {
                    try
                    {
                        string strDrive = drive.Name + " [" + drive.VolumeLabel + "] - " + drive.TotalSize.ToString("###,###,##0");
                        list.Add(strDrive);

                    }
                    catch (Exception err)
                    {
                        list.Add(drive.Name + "<" + err.Message + ">");
                        Debug.WriteLine("GetDriveList error: (" + drive.Name + ") - " + err.Message);
                    }
                }
            }
            return list;
        }
        public static List<UsbEject.Library.Device> GetUsbDriveList()
        {
            UsbEject.Library.VolumeDeviceClass volumes = new UsbEject.Library.VolumeDeviceClass();
            List<UsbEject.Library.Device> drives = volumes.GetDevices();
            return drives;
        }

        public static List<string> GetAllDriveLettersForDevice(List<UsbEject.Library.Device> drives, string deviceFriendlyName)
        {
            List<string> driveLetters = new List<string>();
            foreach (UsbEject.Library.Device device in drives)
            {
                UsbEject.Library.Volume volume = device as UsbEject.Library.Volume;
                if (volume == null || volume.Disks.Count == 0)
                    continue;

                if (volume.Disks[0].FriendlyName != deviceFriendlyName)
                    continue;

                driveLetters.Add(volume.LogicalDrive);
            }
            return driveLetters;
        }
    }
}
