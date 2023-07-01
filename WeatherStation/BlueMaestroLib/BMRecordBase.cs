using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


using MkZ.Bluetooth;
using MkZ.Physics;

namespace MkZ.BlueMaestroLib
{

    [Serializable]
    public class BMRecordBase : IDataPoint
    {
        public const ushort MANUFACTURER_ID = 0x0133;

        [XmlIgnore]
        public override bool IsValid { get => Temperature != 0 && AirHumidity != 0 && AirPressure != 0; }

        public override DateTime Date { get; set; } = DateTime.Now;

        [XmlIgnore]
        public override double[] Values { get; protected set; } = new double[5];

        [XmlIgnore]
        public byte[] Data { get; set; }

        //public string Name { get; set; }

        //public ulong Address { get; set; }

        public short RSSI { get; set; }

        public static bool IsManufacturerID(ushort manufacturerID)
        {
            return manufacturerID == MANUFACTURER_ID;
        }

        // Current temperature, humidity, and dew point
        public double Temperature { get { return Values[0]; } set { Values[0] = value; } }
        public double AirPressure { get { return Values[1]; } set { Values[1] = value; } }
        public double AirHumidity { get { return Values[2]; } set { Values[2] = value; } }

        public double BatteryLevel { get { return Values[3]; } set { Values[3] = value; } }
        public double LoggingInterval { get { return Values[4]; } set { Values[4] = value; } }

        public double GetTemperature(IUnitBase<eTemperatureUnits> measurementType) { return measurementType.Convert(Temperature); }
        public double GetAirHumidity() { return AirHumidity; }
        public double GetAirPressure(IUnitBase<eAirPressureUnits> measurementType) { return measurementType.Convert(AirPressure); }
        public double GetAirDewPoint(IUnitBase<eTemperatureUnits> measurementType) { return (this.GetTemperature(measurementType) - ((100 - this.AirHumidity) / 5)); }

        public override double GetValue<T>(IUnitBase<T> measurementType) //where T : struct, IConvertible
        {
            if (measurementType is TemperatureUnits t)
            {
                return t.Convert(Temperature);
            }
            else if (measurementType is AirPressureUnits a)
            {
                return a.Convert(AirPressure);
            }
            else if (measurementType is RelativeHumidityUnits)
            {
                return AirHumidity;
            }
            return 0;
        }

        //for serialization
        public BMRecordBase()
        {

        }

        public BMRecordBase(BluetoothDevice device, short rssi, DateTime recordDate, byte[] data)
        {
            //Name = device.Name;
            //Address = device.Address;
            RSSI = rssi;

            Date = recordDate;
            Data = data;
        }

        public BMRecordBase(BMRecordBase r = null)
        {
            if (r == null)
                return;

            //Name = r.Name;
            //Address = r.Address;
            RSSI = r.RSSI;

            Date = r.Date;
            Data = r.Data;

            Temperature = r.Temperature;
            AirHumidity = r.AirHumidity;
            AirPressure = r.AirPressure;

            BatteryLevel = r.BatteryLevel;
            LoggingInterval = r.LoggingInterval;
        }

        public override string ToString()
        {
            string desc = "";
            //desc += ("Name: " + Name + " \n");
            desc += ("Signal: " + RSSI + " dBm \n");
            return desc;
        }
    }
}
