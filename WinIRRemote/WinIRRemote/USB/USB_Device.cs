using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinIRRemote.USB
{
    //ANOTHER HID LIBRARY: https://github.com/mikeobrien/HidLibrary

    public class USB_Device
    {
        public  ushort vendorId { get; protected set; }
        public ushort productId { get; protected set; }
        public ushort usagePage { get; protected set; }
        public ushort usageId { get; protected set; }
    }


    /// <summary>
    /// "HID-compliant vendor-defined device"
    /// "HID\\VID_10C4&PID_8468\\6&1E482461&0&0000"
    /// "USB Input Device"
    /// "USB\\VID_10C4&PID_8468\\5&3B9D3DC&0&4"
    /// </summary>
    public class BaseusIR : USB_Device
    {
        public BaseusIR()
        {
            vendorId = 0x10C4;
            productId = 0x8468;
            usagePage = 0xFF00;
            usageId = 0x0001;
        }
    }
}
