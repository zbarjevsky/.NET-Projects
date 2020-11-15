using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BarometerBT.Bluetooth;

namespace BarometerBT.BlueMaestro
{
    [Serializable]
    public class BMRecordBase
    {
        [XmlIgnore]
        public byte[] Data { get; set; }

        public const String UNITSF = "º F";
        public const String UNITSC = "º C";

        public string Name { get; set; }

        public ulong Address { get; set; }

        public short RSSI { get; set; }

        public DateTime Date { get; set; }

        public static bool IsManufacturerID(ushort manufacturerID)
        {
            return manufacturerID == 0x0133;
        }

        //for serialization
        public BMRecordBase()
        {

        }

        public BMRecordBase(BluetoothDevice device, short rssi, DateTime recordDate, byte[] data)
        {
            Name = device.Name;
            Address = device.Address;
            RSSI = rssi;

            Date = recordDate;
            Data = data;
        }

        public BMRecordBase(BMRecordBase r = null)
        {
            if (r == null)
                return;

            Name = r.Name;
            Address = r.Address;
            RSSI = r.RSSI;

            Date = r.Date;
            Data = r.Data;
        }

        public override string ToString()
        {
            string desc = "";
            desc += ("Name: " + Name + " \n");
            desc += ("Signal: " + RSSI + " dBm \n");
            return desc;
        }
    }
}
