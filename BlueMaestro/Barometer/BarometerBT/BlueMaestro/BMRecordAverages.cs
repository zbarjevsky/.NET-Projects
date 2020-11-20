using BarometerBT.Bluetooth;
using BarometerBT.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerBT.BlueMaestro
{
    public class BMRecordAverages : BMRecordBase
    {
        // User input used to determine altitute or sea adjusted pressure
        private double userInput;

        // Reference Date
        private int referenceDateRawNumber;

        // Highest temperature, humidity, and dew point recorded in last 24 hours
        private double high24Temperature;
        private double high24Humidity;
        private double high24Pressure;
        private double high24DewPoint { get { return (this.high24Temperature - ((100 - this.high24Humidity) / 5)); } }

        // Lowest temperature, humidity, and dew point recorded in last 24 hours
        private double low24Temperature;
        private double low24Humidity;
        private double low24Pressure;
        private double low24DewPoint { get { return (this.low24Temperature - ((100 - this.low24Humidity) / 5)); } }

        // Average temperature, humidity, and dew point recorded in last 24 hours
        private double avg24Temperature;
        private double avg24Humidity;
        private double avg24Pressure;
        private double avg24DewPoint { get { return (this.avg24Temperature - ((100 - this.avg24Humidity) / 5)); } }

        //Class ID
        private int classIDNumber;

        public BMRecordAverages(BluetoothDevice device, short rssi, DateTime recordDate, byte[] sData)
            : base(device, rssi, recordDate, sData)
        {
            Set_sData(sData);
        }

        public void Set_sData(byte[] sData)
        {
            if (sData == null || sData.Length != 25)
                return;

            Data = sData;

            const int offset = 3;

            this.high24Pressure = CommonTools.convertToInt16(sData[3 - offset], sData[4 - offset]) / 10.0;
            this.avg24Pressure = CommonTools.convertToInt16(sData[5 - offset], sData[6 - offset]) / 10.0;
            this.low24Pressure = CommonTools.convertToInt16(sData[7 - offset], sData[8 - offset]) / 10.0;

            this.userInput = CommonTools.convertToInt16(sData[9 - offset], sData[10 - offset]);

            this.high24Temperature = CommonTools.convertToInt16(sData[11 - offset], sData[12 - offset]) / 10.0;
            this.high24Humidity = CommonTools.convertToInt16(sData[13 - offset], sData[14 - offset]) / 10.0;
            //this.high24DewPoint = (this.high24Temperature - ((100 - this.high24Humidity) / 5));

            this.low24Temperature = CommonTools.convertToInt16(sData[15 - offset], sData[16 - offset]) / 10.0;
            this.low24Humidity = CommonTools.convertToInt16(sData[17 - offset], sData[18 - offset]) / 10.0;
            //this.low24DewPoint = (this.low24Temperature - ((100 - this.low24Humidity) / 5));

            this.avg24Temperature = CommonTools.convertToInt16(sData[19 - offset], sData[20 - offset]) / 10.0;
            this.avg24Humidity = CommonTools.convertToInt16(sData[21 - offset], sData[22 - offset]) / 10.0;
            //this.avg24DewPoint = (this.avg24Temperature - ((100 - this.avg24Humidity) / 5));

            this.classIDNumber = CommonTools.convertToUInt8(sData[23 - offset]);

            this.referenceDateRawNumber = ((0xFF & sData[24 - offset]) << 24) | ((0xFF & sData[25 - offset]) << 16) | ((0xFF & sData[26 - offset]) << 8) | (0xFF & sData[27 - offset]);
            Log.d("BMTempHumi", "referenceDateNumber" + this.referenceDateRawNumber);

            Debug.WriteLine("AVG24 Temperature: " + avg24Temperature);
            Debug.WriteLine("AVG24 AirPressure: " + avg24Pressure);
        }

        public override string ToString()
        {
            string desc = "";
            desc += base.ToString();
            desc += ("AVG24 Temperature: " + avg24Temperature + " ºC \n");
            desc += ("AVG24 Humidity: " + avg24Humidity + " %RH \n");
            desc += ("AVG24 Dew Point: " + avg24DewPoint + " ºC \n");
            desc += ("AVG24 AirPressure: " + avg24Pressure + " mBar \n");
            return desc;
        }
    }
}
