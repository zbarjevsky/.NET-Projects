using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Bluetooth
{
    public class BluetoothDevice
    {
        public string Name { get; set; }
        public ulong Address { get; set; }
        public string State { get; set; }
        public string DisplayName { get; set; }

        //for serialization
        public BluetoothDevice()
        {

        }

        public BluetoothDevice(string name, ulong address, string state)
        {
            Name = name;
            Address = address;
            State = state;
            DisplayName = name;
        }

        public string GetAddressString()
        {
            return ConvertAddress(Address);
        }

        public static string ConvertAddress(ulong address)
        {
            return BitConverter.ToString(BitConverter.GetBytes(address).Reverse().ToArray());
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, ConvertAddress(Address));
        }
    }
}
