using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


using BarometerBT.Bluetooth;

namespace BarometerBT.BlueMaestro
{
    public class BlueMaestroRecord
    {
        private static readonly Regex burst_pattern =
               new Regex("T(-?[0-9]+\\.[0-9])H(-?[0-9]+\\.[0-9])D(-?[0-9]+\\.[0-9])");

        public byte _mode { get; private set; } = 0;

        public string Name { get; private set; }

        public DateTime Date { get; private set; }

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
        public double currDewPoint { get; private set; }

        // Number of threshold breaches
        public int numBreach { get; private set; }

        // User input used to determine altitute or sea adjusted pressure
        public double userInput { get; private set; }

        public byte[] Data { get; private set; }

        public const String UNITSF = "º F";
        public const String UNITSC = "º C";

        public static bool IsManufacturerID(ushort manufacturerID)
        {
            return manufacturerID == 0x0133;
        }

        public BlueMaestroRecord(string deviceName, DateTime recordDate, byte [] data)
        {
            Name = deviceName;
            Date = recordDate;
            SetCurrentData(data);
        }

        private void SetCurrentData(byte[] mData)
        {
            if (mData == null || mData.Length != 14)
                return;

            Data = mData;

            const int offset = 3;

            this.battery = mData[4 - offset];

            this._loggingInterval = convertToInt16(mData[5 - offset], mData[6 - offset]);
            //BMDatabase database = BMDeviceMap.INSTANCE.getBMDatabase(getAddress());
            //database.setLoggingInterval(loggingInterval);

            this.currTemperature = convertToInt16(mData[9 - offset], mData[10 - offset]) / 10.0;
            this.currHumidity = convertToInt16(mData[11 - offset], mData[12 - offset]) / 10.0;
            this.currPressure = convertToInt16(mData[13 - offset], mData[14 - offset]) / 10.0;
            this.currDewPoint = (this.currTemperature - ((100 - this.currHumidity) / 5));

            this._mode = mData[15 - offset];

            this.numBreach = convertToInt8(mData[16 - offset]);

            Debug.WriteLine("Temperature: " + currTemperature);
            Debug.WriteLine("AirPressure: " + currPressure);
        }

        /**
         * Convert two bytes to signed int 16
         * @param first
         * @param second
         * @return
         */
        protected static int convertToInt16(byte first, byte second)
        {
            int value = (int)first & 0xFF;
            value *= 256;
            value += (int)second & 0xFF;
            value -= (value > 32768) ? 65536 : 0;
            return value;
        }

        /**
         * Ensure an unsigned int 8 is treated correct
         * @param b
         * @return
         */
        protected static int convertToInt8(byte b)
        {
            int c = (int)(b & 0xff);
            return c;
        }
    }
}
