using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerBT.BlueMaestro
{
    public class BlueMaestroDevice
    {
        public byte[] Data { get; }

        public static bool IsManufacturerID(ushort manufacturerID)
        {
            return manufacturerID == 133;
        }

        public BlueMaestroDevice(byte [] data)
        {
            Data = data;
        }
    }
}
