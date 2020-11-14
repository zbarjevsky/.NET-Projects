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

namespace BarometerBT.BlueMaestro
{
    public class BMRecordCurrent : BMRecordBase
    {
        private static readonly Regex burst_pattern =
               new Regex("T(-?[0-9]+\\.[0-9])H(-?[0-9]+\\.[0-9])D(-?[0-9]+\\.[0-9])");

        public byte _mode { get; private set; } = 0;

        // Battery level
        public int battery { get; private set; } = 0;

        // Reference Date
        public int referenceDateRawNumber { get; private set; }

        // Logging interval
        public int _loggingInterval { get; private set; }

        // Current temperature, humidity, and dew point
        public double currTemperature;
        public double currHumidity;
        public double currPressure { get; private set; }

        [XmlIgnore]
        public double currDewPoint { get { return (this.currTemperature - ((100 - this.currHumidity) / 5)); } }

        // Number of threshold breaches
        public int numBreach { get; private set; }

        // User input used to determine altitute or sea adjusted pressure
        public double userInput { get; private set; }


        public BMRecordCurrent(BluetoothDevice device, short rssi, DateTime recordDate,  byte[] mData)
            : base(device, rssi, recordDate, mData)
        {
            SetCurrentData(mData);
        }

        //empty record for average, min, max
        public BMRecordCurrent(BMRecordCurrent r) : base(r)
        {
            currTemperature = r.currTemperature;
            currHumidity = r.currHumidity;
            currPressure = r.currPressure;

            battery = r.battery;
            _loggingInterval = r._loggingInterval;
            numBreach = r.numBreach;
            _mode = r._mode;
        }

        private void SetCurrentData(byte[] mData)
        {
            if (mData == null || mData.Length != 14)
                return;

            Data = mData;

            const int offset = 3;

            this.battery = mData[4 - offset];

            this._loggingInterval = CommonTools.convertToInt16(mData[5 - offset], mData[6 - offset]);
            //BMDatabase database = BMDeviceMap.INSTANCE.getBMDatabase(getAddress());
            //database.setLoggingInterval(loggingInterval);

            this.currTemperature = CommonTools.convertToInt16(mData[9 - offset], mData[10 - offset]) / 10.0;
            this.currHumidity = CommonTools.convertToInt16(mData[11 - offset], mData[12 - offset]) / 10.0;
            this.currPressure = CommonTools.convertToInt16(mData[13 - offset], mData[14 - offset]) / 10.0;
            //this.currDewPoint = (this.currTemperature - ((100 - this.currHumidity) / 5));

            this._mode = mData[15 - offset];

            this.numBreach = CommonTools.convertToInt8(mData[16 - offset]);

            Debug.WriteLine("Temperature: " + currTemperature);
            Debug.WriteLine("AirPressure: " + currPressure);
        }

        public void Max(BMRecordCurrent r)
        {
            currTemperature = Math.Max(currTemperature, r.currTemperature);
            currHumidity = Math.Max(currHumidity, r.currHumidity);
            currPressure = Math.Max(currPressure, r.currPressure);
        }

        public void Min(BMRecordCurrent r)
        {
            currTemperature = Math.Min(currTemperature, r.currTemperature);
            currHumidity = Math.Min(currHumidity, r.currHumidity);
            currPressure = Math.Min(currPressure, r.currPressure);
        }

        public static bool operator ==(BMRecordCurrent r1, BMRecordCurrent r2)
        {
            if (object.ReferenceEquals(r1, r2))
                return true;

            if (object.ReferenceEquals(r1, null) || object.ReferenceEquals(r2, null))
                return false;

            return r1.currTemperature == r2.currTemperature &&
                   r1.currHumidity == r2.currHumidity &&
                   r1.currPressure == r2.currPressure;
        }

        public static bool operator !=(BMRecordCurrent r1, BMRecordCurrent r2)
        {
            return !(r1 == r2);
        }

        public override string ToString()
        {
            string desc = "";
            desc += base.ToString();
            desc += ("Temperature: " + currTemperature + "\n");
            desc += ("Humidity: " + currHumidity + "\n");
            desc += ("AirPressure: " + currPressure + "\n");
            desc += ("Battery: " + battery + " %\n");
            return desc;
        }
    }
}
