using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Bluetooth
{
    public enum BleDataType : byte
    {
        Flags = 0x01,
        ShortenedLocalName = 0x08,
        CompleteLocalName = 0x09,
        ManufacturerSpecificData = 0xFF
    }
}
