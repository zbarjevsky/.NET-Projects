using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerBT.Bluetooth
{
    public class BluetoothDevice
    {
        string _name;
        ulong _address;
        string _state;

        public BluetoothDevice(string name, ulong address, string state)
        {
            _name = name;
            _address = address;
            _state = state;
        }

        internal ulong getAddress()
        {
            return _address;
        }

        internal string getName()
        {
            return _name;
        }

        internal int getBondState()
        {
            return 0;
        }
    }
}
