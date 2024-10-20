﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


using MkZ.Bluetooth;
using MkZ.Physics;
//using MkZWeatherStation.Utils;

namespace BlueMaestro.Old
{

    [Serializable]
    public class BMRecordBase
    {
        public const ushort MANUFACTURER_ID = 0x0133;

        [XmlIgnore]
        public byte[] Data { get; set; }

        //public string Name { get; set; }

        //public ulong Address { get; set; }

        public short RSSI { get; set; }

        public DateTime Date { get; set; }

        [XmlIgnore]
        public bool IsValid => Temperature != 0 && AirHumidity != 0 && AirPressure != 0;

        public static bool IsManufacturerID(ushort manufacturerID)
        {
            return manufacturerID == MANUFACTURER_ID;
        }

        // Current temperature, humidity, and dew point
        public double Temperature { get; set; }
        public double AirHumidity { get; set; }
        public double AirPressure { get; set; }

        public double GetTemperature(IUnitBase<eTemperatureUnits> units) { return units.Convert(Temperature); }
        public double GetAirHumidity() { return AirHumidity; }
        public double GetAirPressure(IUnitBase<eAirPressureUnits> units) { return units.Convert(AirPressure); }
        public double GetAirDewPoint(IUnitBase<eTemperatureUnits> units) { return (this.GetTemperature(units) - ((100 - this.AirHumidity) / 5)); }

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
