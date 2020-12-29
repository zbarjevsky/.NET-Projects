using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;


using BarometerBT.Bluetooth;
using BarometerBT.Utils;
using MZ.Tools;

namespace BarometerBT.BlueMaestro
{
    [Serializable]
    public class BMRecordCurrent : BMRecordBase
    {
        private static readonly Regex burst_pattern =
               new Regex("T(-?[0-9]+\\.[0-9])H(-?[0-9]+\\.[0-9])D(-?[0-9]+\\.[0-9])");

        [XmlIgnore]
        public byte _mode { get; set; } = 0;

        // Battery level
        public int BatteryLevel { get; set; } = 0;

        // Reference Date
        public int ReferenceDateRawNumber { get; set; }

        // Logging interval
        public int LoggingInterval { get; set; }

        // Number of threshold breaches
        public int numBreach { get; set; }

        // User input used to determine altitute or sea adjusted pressure
        public double userInput { get; set; }

        //for serialization
        public BMRecordCurrent()
        {

        }

        public BMRecordCurrent(BluetoothDevice device, short rssi, DateTime recordDate,  byte[] mData)
            : base(device, rssi, recordDate, mData)
        {
            SetCurrentData(mData);
        }

        //empty record for average, min, max
        public BMRecordCurrent(BMRecordCurrent r = null) : base(r)
        {
            if (r == null)
                return;

            BatteryLevel = r.BatteryLevel;
            LoggingInterval = r.LoggingInterval;
            numBreach = r.numBreach;
            _mode = r._mode;
        }

        private void SetCurrentData(byte[] mData)
        {
            if (mData == null || mData.Length != 14)
                return;

            Data = mData;

            const int offset = 3;

            this.BatteryLevel = mData[4 - offset];

            this.LoggingInterval = CommonTools.convertToInt16(mData[5 - offset], mData[6 - offset]);
            //BMDatabase database = BMDeviceMap.INSTANCE.getBMDatabase(getAddress());
            //database.setLoggingInterval(loggingInterval);

            this.Temperature = CommonTools.convertToInt16(mData[9 - offset], mData[10 - offset]) / 10.0;
            this.AirHumidity = CommonTools.convertToInt16(mData[11 - offset], mData[12 - offset]) / 10.0;
            this.AirPressure = CommonTools.convertToInt16(mData[13 - offset], mData[14 - offset]) / 10.0;
            //this.currDewPoint = (this.currTemperature - ((100 - this.currHumidity) / 5));

            this._mode = mData[15 - offset];

            this.numBreach = CommonTools.convertToInt8(mData[16 - offset]);

            Log.d("Temperature: " + Temperature);
            Log.d("AirPressure: " + AirPressure);
        }

        //public void Max(BMRecordCurrent r)
        //{
        //    Temperature = Math.Max(Temperature, r.Temperature);
        //    AirHumidity = Math.Max(AirHumidity, r.AirHumidity);
        //    AirPressure = Math.Max(AirPressure, r.AirPressure);
        //}

        //public void Min(BMRecordCurrent r)
        //{
        //    Temperature = Math.Min(Temperature, r.Temperature);
        //    AirHumidity = Math.Min(AirHumidity, r.AirHumidity);
        //    AirPressure = Math.Min(AirPressure, r.AirPressure);
        //}

        public static bool operator ==(BMRecordCurrent r1, BMRecordCurrent r2)
        {
            if (object.ReferenceEquals(r1, r2))
                return true;

            if (object.ReferenceEquals(r1, null) || object.ReferenceEquals(r2, null))
                return false;

            const double EPSILON = 0.15;
            const double ONE_SECOND = 1.0;

            return Math.Abs((r1.Date - r2.Date).TotalSeconds) < ONE_SECOND &&
                   Math.Abs(r1.Temperature - r2.Temperature) < EPSILON &&
                   Math.Abs(r1.AirHumidity - r2.AirHumidity) < EPSILON &&
                   Math.Abs(r1.AirPressure - r2.AirPressure) < EPSILON &&
                   r1.BatteryLevel == r2.BatteryLevel;
        }

        public static bool operator !=(BMRecordCurrent r1, BMRecordCurrent r2)
        {
            return !(r1 == r2);
        }

        public static BMRecordCurrent operator +(BMRecordCurrent r1, BMRecordCurrent r2)
        {
            BMRecordCurrent rec = new BMRecordCurrent(r2); //later one for time, battery level etc.
            rec.Temperature += r1.Temperature;
            rec.AirHumidity += r1.AirHumidity;
            rec.AirPressure += r1.AirPressure;
            return rec;
        }

        public static BMRecordCurrent operator /(BMRecordCurrent r1, double factor)
        {
            BMRecordCurrent rec = new BMRecordCurrent(r1);
            rec.Temperature /= factor;
            rec.AirHumidity /= factor;
            rec.AirPressure /= factor;
            return rec;
        }

        public override string ToString()
        {
            string desc = "";
            desc += base.ToString();
            desc += ("Temperature: " + Temperature + " ºC \n");
            desc += ("Humidity: " + AirHumidity + " %RH \n");
            desc += ("AirPressure: " + AirPressure + " mBar \n");
            desc += ("Battery: " + BatteryLevel + " % \n");
            return desc;
        }
    }
}
