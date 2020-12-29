using BarometerBT.Bluetooth;
using BarometerBT.Utils;
using MZ.Tools;
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

        private BMRecordBase High24, Low24, Avg24;

        //Class ID
        private int classIDNumber;

        public BMRecordAverages(BluetoothDevice device, short rssi, DateTime recordDate, byte[] sData)
            : base(device, rssi, recordDate, sData)
        {
            High24 = new BMRecordBase(device, rssi, recordDate, sData);
            Low24 = new BMRecordBase(device, rssi, recordDate, sData);
            Avg24 = new BMRecordBase(device, rssi, recordDate, sData);

            Set_sData(sData);
        }

        public void Set_sData(byte[] sData)
        {
            if (sData == null || sData.Length != 25)
                return;

            Date = DateTime.Now;
            Data = sData;

            const int offset = 3;

            this.High24.AirPressure = CommonTools.convertToInt16(sData[3 - offset], sData[4 - offset]) / 10.0;
            this.Avg24.AirPressure = CommonTools.convertToInt16(sData[5 - offset], sData[6 - offset]) / 10.0;
            this.Low24.AirPressure = CommonTools.convertToInt16(sData[7 - offset], sData[8 - offset]) / 10.0;

            this.userInput = CommonTools.convertToInt16(sData[9 - offset], sData[10 - offset]);

            this.High24.Temperature = CommonTools.convertToInt16(sData[11 - offset], sData[12 - offset]) / 10.0;
            this.High24.AirHumidity = CommonTools.convertToInt16(sData[13 - offset], sData[14 - offset]) / 10.0;
            //this.high24DewPoint = (this.high24Temperature - ((100 - this.high24Humidity) / 5));

            this.Low24.Temperature = CommonTools.convertToInt16(sData[15 - offset], sData[16 - offset]) / 10.0;
            this.Low24.AirHumidity = CommonTools.convertToInt16(sData[17 - offset], sData[18 - offset]) / 10.0;
            //this.low24DewPoint = (this.low24Temperature - ((100 - this.low24Humidity) / 5));

            this.Avg24.Temperature = CommonTools.convertToInt16(sData[19 - offset], sData[20 - offset]) / 10.0;
            this.Avg24.AirHumidity = CommonTools.convertToInt16(sData[21 - offset], sData[22 - offset]) / 10.0;
            //this.avg24DewPoint = (this.avg24Temperature - ((100 - this.avg24Humidity) / 5));

            this.classIDNumber = CommonTools.convertToUInt8(sData[23 - offset]);

            this.referenceDateRawNumber = ((0xFF & sData[24 - offset]) << 24) | ((0xFF & sData[25 - offset]) << 16) | ((0xFF & sData[26 - offset]) << 8) | (0xFF & sData[27 - offset]);
            Log.d("BMTempHumi", "referenceDateNumber" + this.referenceDateRawNumber);

            Log.d("AVG24 Temperature: " + Avg24.Temperature);
            Log.d("AVG24 AirPressure: " + Avg24.AirPressure);
        }

        public override string ToString()
        {
            string desc = "";
            desc += base.ToString();
            desc += ("AVG24 Temperature: " + Avg24.Temperature + " ºC \n");
            desc += ("AVG24 Humidity: " + Avg24.AirHumidity + " %RH \n");
            desc += ("AVG24 Dew Point: " + Avg24.GetAirDewPoint(UnitsDescriptor.DefaultUnits) + " ºC \n");
            desc += ("AVG24 AirPressure: " + Avg24.AirPressure + " mBar \n");
            return desc;
        }
    }
}
