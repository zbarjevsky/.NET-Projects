using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BarometerBT.Bluetooth;

namespace BarometerBT.BlueMaestro
{
    public class BMRecordBase
    {
        public byte[] Data { get; protected set; }

        public const String UNITSF = "º F";
        public const String UNITSC = "º C";

        public string Name { get; protected set; }

        public ulong Address { get; protected set; }

        public short RSSI { get; protected set; }

        public DateTime Date { get; protected set; }

        public static bool IsManufacturerID(ushort manufacturerID)
        {
            return manufacturerID == 0x0133;
        }

        public BMRecordBase(BluetoothDevice device, short rssi, DateTime recordDate, byte[] data)
        {
            Name = device.getName();
            Address = device.getAddress();
            RSSI = rssi;

            Date = recordDate;
            Data = data;
        }

        public BMRecordBase(BMRecordBase r)
        {
            Name = r.Name;
            Address = r.Address;
            RSSI = r.RSSI;

            Date = r.Date;
            Data = r.Data;
        }

        public override string ToString()
        {
            string desc = "";
            desc += ("Name: " + Name + "\n");
            desc += ("Signal: " + RSSI + " dB\n");
            return desc;
        }
    }
}
