using BarometerBT.Bluetooth;
using BarometerBT.Utils;
using MZ.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BarometerBT.BlueMaestro.Original
{
    public class BMPebble27 : BMDevice
    {

        private static readonly Regex burst_pattern =
                new Regex("T(-?[0-9]+\\.[0-9])H(-?[0-9]+\\.[0-9])D(-?[0-9]+\\.[0-9])");

        // Battery level
        private int battery;

        // Reference Date
        private int referenceDateRawNumber;

        // Logging interval
        private int _loggingInterval;

        // Current temperature, humidity, and dew point
        private double currTemperature;
        private double currHumidity;
        private double currPressure;
        private double currDewPoint;

        // Number of threshold breaches
        private int numBreach;

        // User input used to determine altitute or sea adjusted pressure
        private double userInput;

        // Highest temperature and humidity recorded
        private double highTemperature;
        private double highHumidity;

        // Lowest temperature and humidity recorded
        private double lowTemperature;
        private double lowHumidity;

        // Highest temperature, humidity, and dew point recorded in last 24 hours
        private double high24Temperature;
        private double high24Humidity;
        private double high24Pressure;
        private double high24DewPoint;

        // Lowest temperature, humidity, and dew point recorded in last 24 hours
        private double low24Temperature;
        private double low24Humidity;
        private double low24Pressure;
        private double low24DewPoint;

        // Average temperature, humidity, and dew point recorded in last 24 hours
        private double avg24Temperature;
        private double avg24Humidity;
        private double avg24Pressure;
        private double avg24DewPoint;

        public const String UNITSF = "º F";
        public const String UNITSC = "º C";


        //Class ID
        private int classIDNumber;

        public BMPebble27(BluetoothDevice device, byte id) : base(device, id)
        {

        }

        public void Set_mData(byte [] mData)
        {
            if (mData == null || mData.Length != 14)
                return;

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

            Log.d("Temperature: " + currTemperature);
            Log.d("AirPressure: " + currPressure);
        }

        public void Set_sData(byte [] sData)
        {
            if (sData == null || sData.Length != 25)
                return;

            const int offset = 3;

            this.high24Pressure = convertToInt16(sData[3 - offset], sData[4 - offset]) / 10.0;
            this.avg24Pressure = convertToInt16(sData[5 - offset], sData[6 - offset]) / 10.0;
            this.low24Pressure = convertToInt16(sData[7 - offset], sData[8 - offset]) / 10.0;

            this.userInput = convertToInt16(sData[9 - offset], sData[10 - offset]);

            this.high24Temperature = convertToInt16(sData[11 - offset], sData[12 - offset]) / 10.0;
            this.high24Humidity = convertToInt16(sData[13 - offset], sData[14 - offset]) / 10.0;
            this.high24DewPoint = (this.high24Temperature - ((100 - this.high24Humidity) / 5));

            this.low24Temperature = convertToInt16(sData[15 - offset], sData[16 - offset]) / 10.0;
            this.low24Humidity = convertToInt16(sData[17 - offset], sData[18 - offset]) / 10.0;
            this.low24DewPoint = (this.low24Temperature - ((100 - this.low24Humidity) / 5));

            this.avg24Temperature = convertToInt16(sData[19 - offset], sData[20 - offset]) / 10.0;
            this.avg24Humidity = convertToInt16(sData[21 - offset], sData[22 - offset]) / 10.0;
            this.avg24DewPoint = (this.avg24Temperature - ((100 - this.avg24Humidity) / 5));

            this.classIDNumber = convertToUInt8(sData[23 - offset]);

            this.referenceDateRawNumber = ((0xFF & sData[24 - offset]) << 24) | ((0xFF & sData[25 - offset]) << 16) | ((0xFF & sData[26 - offset]) << 8) | (0xFF & sData[27 - offset]);
            Log.d("BMTempHumi", "referenceDateNumber" + this.referenceDateRawNumber);

            Log.d("AVG24 Temperature: " + avg24Temperature);
            Log.d("AVG24 AirPressure: " + avg24Pressure);
        }

        public override void updateWithData(int rssi, String name, byte[] mData, byte[] sData)
        {
            base.updateWithData(rssi, name, mData, sData);
            this.battery = mData[4];

            this._loggingInterval = convertToInt16(mData[5], mData[6]);
            //BMDatabase database = BMDeviceMap.INSTANCE.getBMDatabase(getAddress());
            //database.setLoggingInterval(loggingInterval);

            this.currTemperature = convertToInt16(mData[9], mData[10]) / 10.0;
            this.currHumidity = convertToInt16(mData[11], mData[12]) / 10.0;
            this.currPressure = convertToInt16(mData[13], mData[14]) / 10.0;
            this.currDewPoint = (this.currTemperature - ((100 - this.currHumidity) / 5));

            this._mode = mData[15];

            this.numBreach = convertToInt8(mData[16]);

            this.high24Pressure = convertToInt16(sData[3], sData[4]) / 10.0;
            this.avg24Pressure = convertToInt16(sData[5], sData[6]) / 10.0;
            this.low24Pressure = convertToInt16(sData[7], sData[8]) / 10.0;

            this.userInput = convertToInt16(sData[9], sData[10]);

            this.high24Temperature = convertToInt16(sData[11], sData[12]) / 10.0;
            this.high24Humidity = convertToInt16(sData[13], sData[14]) / 10.0;
            this.high24DewPoint = (this.high24Temperature - ((100 - this.high24Humidity) / 5));

            this.low24Temperature = convertToInt16(sData[15], sData[16]) / 10.0;
            this.low24Humidity = convertToInt16(sData[17], sData[18]) / 10.0;
            this.low24DewPoint = (this.low24Temperature - ((100 - this.low24Humidity) / 5));

            this.avg24Temperature = convertToInt16(sData[19], sData[20]) / 10.0;
            this.avg24Humidity = convertToInt16(sData[21], sData[22]) / 10.0;
            this.avg24DewPoint = (this.avg24Temperature - ((100 - this.avg24Humidity) / 5));

            this.classIDNumber = convertToUInt8(sData[23]);

            this.referenceDateRawNumber = ((0xFF & sData[24]) << 24) | ((0xFF & sData[25]) << 16) | ((0xFF & sData[26]) << 8) | (0xFF & sData[27]);
            Log.d("BMTempHumi", "referenceDateNumber" + this.referenceDateRawNumber);
        }

        internal string Description()
        {
            string desc = "";
            desc += ("Name: " + name + "\n");
            desc += ("Temperature: " + currTemperature + "\n");
            desc += ("Humidity: " + currHumidity + "\n");
            desc += ("AirPressure: " + currPressure + "\n");
            desc += ("AVG24 Temperature: " + avg24Temperature + "\n");
            desc += ("AVG24 Humidity: " + avg24Humidity + "\n");
            desc += ("AVG24 AirPressure: " + avg24Pressure + "\n");
            return desc;
        }

        public int getBatteryLevel()
        {
            return battery;
        }

        public int getLoggingInterval()
        {
            return _loggingInterval;
        }

        public double getCurrentTemperature(String units)
        {
            if (units.Contains(UNITSF)) return ((currTemperature * 1.8) + 32);
            return currTemperature;
        }

        public double getCurrentHumidity()
        {
            return currHumidity;
        }
        public double getCurrentPressure() { return currPressure; }
        public double getCurrentDewPoint(String units)
        {
            if (units.Contains(UNITSF)) return ((currDewPoint * 1.8) + 32);
            return currDewPoint;
        }

        public bool isInAeroplaneMode()
        {
            return _mode % 100 == 5;
        }

        public bool isInFahrenheit()
        {
            return _mode >= 100;
        }

        public String getTempUnits()
        {
            return isInFahrenheit() ? UNITSF : UNITSC;
        }

        public int getNumBreach()
        {
            return numBreach;
        }

        public double getHighest24Temperature(String units)
        {
            if (units.Contains(UNITSF)) return ((high24Temperature * 1.8) + 32);
            return high24Temperature;
        }
        public double getHighest24Humidity()
        {
            return high24Humidity;
        }

        public double getHighest24Pressure() { return high24Pressure; }
        public double getHighest24DewPoint(String units)
        {
            if (units.Contains(UNITSF)) return ((high24DewPoint * 1.8) + 32);
            return high24DewPoint;
        }

        public double getLowest24Temperature(String units)
        {
            if (units.Contains(UNITSF)) return ((low24Temperature * 1.8) + 32);
            return low24Temperature;
        }
        public double getLowest24Humidity()
        {
            return low24Humidity;
        }
        public double getLowest24Pressure() { return low24Pressure; }
        public double getLowest24DewPoint(String units)
        {
            if (units.Contains(UNITSF)) return ((low24DewPoint * 1.8) + 32);
            return low24DewPoint;
        }

        public double getUserInput() { return userInput; }

        public double getAverage24Temperature(String units)
        {
            if (units.Contains(UNITSF)) return ((avg24Temperature * 1.8) + 32);
            return avg24Temperature;
        }
        public double getAverage24Humidity()
        {
            return avg24Humidity;
        }
        public double getAverage24Pressure() { return avg24Pressure; }
        public double getAverage24DewPoint(String units)
        {
            if (units.Contains(UNITSF)) return ((avg24DewPoint * 1.8) + 32);
            return avg24DewPoint;
        }
        public String getReferenceDate()
        {
            Utility dateUtility = new Utility();
            return dateUtility.convertNumberIntoDate("" + referenceDateRawNumber);
        }
        public String getLockedUnlocked()
        {
            if (((this._mode % 100) / 10) > 1) return "Device Locked";
            return "Device Unlocked";
        }
        public int getClassID()
        {
            return classIDNumber;
        }

        public override void updateViewGroup(ViewGroup vg)
        {
            base.updateViewGroup(vg);

            // Battery
            TextView tvbatt = (TextView)vg.findViewById("battery");

            // Logging interval
            TextView tvlogi = (TextView)vg.findViewById("loggingInterval");

            //Altitude
            TextView tvAtt = (TextView)vg.findViewById("altitude");
            TextView tvAttMode = (TextView)vg.findViewById("pressure_mode_v27");

            // Current Temperature
            TextView tvtemplabel = (TextView)vg.findViewById("temperature_label_v27");
            TextView tvtempvalue = (TextView)vg.findViewById("temperature_value_v27");

            //Current Humidity
            TextView tvhumilabel = (TextView)vg.findViewById("humidity_label_v27");
            TextView tvhumivalue = (TextView)vg.findViewById("humidity_value_v27");

            //Current Pressure
            TextView tvpresslabel = (TextView)vg.findViewById("pressure_label_v27");
            TextView tvpressvalue = (TextView)vg.findViewById("pressure_value_v27");

            //Current Dewpoint
            TextView tvdewplabel = (TextView)vg.findViewById("dewPoint_label_v27");
            TextView tvdewpvalue = (TextView)vg.findViewById("dewPoint_value_v27");

            // Mode
            TextView tvmode = (TextView)vg.findViewById("mode");

            //Units
            TextView tvunits = (TextView)vg.findViewById("units");

            // Number of breaches
            TextView tvnumb = (TextView)vg.findViewById("numBreach");

            // Set visible
            vg.findViewById("temperature_box_v27).setVisibility(Visibility.Visible");
            vg.findViewById("humidity_box_v27).setVisibility(Visibility.Visible");
            vg.findViewById("pressure_box_v27).setVisibility(Visibility.Visible");
            vg.findViewById("dewpoint_box_v27).setVisibility(Visibility.Visible");
            vg.findViewById("tables).setVisibility(Visibility.Visible");
            vg.findViewById("extras).setVisibility(Visibility.Visible");

            // Battery
            int battery = getBatteryLevel();
            if (battery >= 0 && battery <= 100)
            {
                tvbatt.setVisibility(Visibility.Visible);
                tvbatt.setText("Battery: " + battery + "%");
            }

            // Mode
            String modeText = "Mode: " +
                    (isInAeroplaneMode() ? "\nFlight" : "Normal");
            tvmode.setVisibility(Visibility.Visible);
            tvmode.setText(modeText);

            //Units
            String unitsText = "Units: " +
                    (isInFahrenheit() ? "Fahrenheit" : "Celsius") +
                    " (" + getTempUnits() + ")";
            tvunits.setVisibility(Visibility.Visible);
            tvunits.setText(unitsText);
            String units = UNITSC;

            // Number of breaches
            tvnumb.setVisibility(Visibility.Visible);
            tvnumb.setText("No. of threshold breaches: " + getNumBreach());

            //Altitude
            tvAtt.setVisibility(Visibility.Visible);
            if (getClassID() > 0)
            {
                tvAttMode.setText("Measuring Altitude");
                tvAtt.setText("Sea pressure: " + getUserInput() + " hPa");
            }
            else
            {
                if (getMode() > 100)
                {
                    tvAtt.setText("Altitude: " + (getUserInput() * 3.28) + " feet");
                }
                else
                {
                    tvAtt.setText("Altitude: " + (getUserInput() + " meters"));
                }
                tvAtt.setText("Sea level pressure: " + getUserInput() + " hPa");
                tvAttMode.setText("Measuring Pressure");
            }

            // Current Temperature
            tvtemplabel.setVisibility(Visibility.Visible);
            tvtemplabel.setText("TEMP");
            tvtempvalue.setVisibility(Visibility.Visible);
            tvtempvalue.setText("" + Utility.convertValueTo(""+(getCurrentTemperature(units)), getTempUnits()) + getTempUnits());

            // Current Humidity
            tvhumilabel.setVisibility(Visibility.Visible);
            tvhumilabel.setText("HUMIDITY");
            tvhumivalue.setVisibility(Visibility.Visible);
            tvhumivalue.setText("" + +getCurrentHumidity() + "%");

            // Current Pressure
            tvpresslabel.setVisibility(Visibility.Visible);
            if (getClassID() == 0)
            {
                tvpresslabel.setText("PRESSURE");
                tvpressvalue.setVisibility(Visibility.Visible);
                tvpressvalue.setText("" + getCurrentPressure() + " hPa");
            }
            else
            {
                tvpresslabel.setText("ALTITUDE");
                tvpressvalue.setVisibility(Visibility.Visible);
                if (getMode() > 100)
                {
                    tvpressvalue.setText("" + (getUserInput() * 3.28));
                }
                else
                {
                    tvpressvalue.setText("" + (getUserInput() + " mm"));
                }
            }

            // Current Dewpoint
            tvdewplabel.setVisibility(Visibility.Visible);
            tvdewplabel.setText("DEWPNT");
            tvdewpvalue.setVisibility(Visibility.Visible);
            tvdewpvalue.setText("" + Utility.convertValueTo(""+(getCurrentDewPoint(units)), getTempUnits()) + getTempUnits());

        }

        public override void updateViewGroupForDetails(ViewGroup vg)
        {

            String unitsForTemperature;
            String unitsForHumidity = "%";
            String unitsForPressure = " hPa";
            String unitsForAltitude;
            if ((int)getMode() > 100)
            {
                unitsForTemperature = UNITSF;
                unitsForAltitude = " Feet";
            }
            else
            {
                unitsForTemperature = UNITSC;
                unitsForAltitude = " Meters";
            }

            //Device Information
            TextView name = (TextView)vg.findViewById("deviceName");
            TextView address = (TextView)vg.findViewById("deviceAddress");
            TextView battery = (TextView)vg.findViewById("batteryLevel");
            TextView signal = (TextView)vg.findViewById("rssiStrength");
            TextView referenceDate = (TextView)vg.findViewById("referenceDate");
            TextView lastDownloadDate = (TextView)vg.findViewById("lastDownloadDate");
            TextView version = (TextView)vg.findViewById("versionNumber");
            TextView locked = (TextView)vg.findViewById("locked_unlock_label");
            TextView userInput = (TextView)vg.findViewById("altitude");
            TextView breachNumber = (TextView)vg.findViewById("breachCount");

            //Current Values
            TextView unitsForTemp = (TextView)vg.findViewById("unitsForTempDeviceInfo");
            TextView temperatureValue = (TextView)vg.findViewById("tempValue_for_main_v27");
            TextView humidityValue = (TextView)vg.findViewById("humValue_main_v27");
            TextView pressureValue = (TextView)vg.findViewById("pressureValue_for_main_27");
            TextView dewpointValue = (TextView)vg.findViewById("dewpValue_for_main_v27");

            //Last 24 Hour Values
            TextView high24Temp = (TextView)vg.findViewById("high24TempValue");
            TextView avg24Temp = (TextView)vg.findViewById("avg24TempValue");
            TextView low24Temp = (TextView)vg.findViewById("low24TempValue");
            TextView high24Humi = (TextView)vg.findViewById("high24HumValue");
            TextView avg24Humi = (TextView)vg.findViewById("avg24HumValue");
            TextView low24Humi = (TextView)vg.findViewById("low24HumValue");
            TextView high24Press = (TextView)vg.findViewById("high24PressValue");
            TextView avg24Press = (TextView)vg.findViewById("avg24PressValue");
            TextView low24Press = (TextView)vg.findViewById("low24PressValue");
            TextView high24Dewp = (TextView)vg.findViewById("high24DewpValue");
            TextView avg24Dewp = (TextView)vg.findViewById("avg24DewpValue");
            TextView low24Dewp = (TextView)vg.findViewById("low24DewpValue");

            //Set Values
            name.setText(getName());
            address.setText("Device ID: " + getAddress());
            battery.setText("Battery: " + getBatteryLevel() + "%");
            signal.setText("Signal: " + getRSSI() + "dB");
            version.setText("Version: " + getVersion());
            locked.setText(getLockedUnlocked());
            breachNumber.setText("Number of breaches: " + getNumBreach());
            userInput.setText("" + getUserInput() + " Meters");
            userInput.setVisibility(Visibility.Visible);

            if ((int)getMode() > 100)
            {
                unitsForTemp.setText("Fahrenheit");

            }
            else
            {
                unitsForTemp.setText("Celsius");
            }

            String temperatureForDisplay = String.Format("%.1f", getCurrentTemperature(unitsForTemperature));
            String dewpointForDisplay = String.Format("%.1f", getCurrentDewPoint(unitsForTemperature));
            temperatureValue.setText(temperatureForDisplay + unitsForTemperature);
            dewpointValue.setText(dewpointForDisplay + unitsForTemperature);
            humidityValue.setText("" + getCurrentHumidity() + unitsForHumidity);
            pressureValue.setText("" + getCurrentPressure() + " hPa");

            String highTempForDisplay = String.Format("%.1f", getHighest24Temperature(unitsForTemperature));
            String avgTempForDisplay = String.Format("%.1f", getAverage24Temperature(unitsForTemperature));
            String lowTempForDisplay = String.Format("%.1f", getLowest24Temperature(unitsForTemperature));

            high24Temp.setText(highTempForDisplay + unitsForTemperature);
            avg24Temp.setText(avgTempForDisplay + unitsForTemperature);
            low24Temp.setText(lowTempForDisplay + unitsForTemperature);

            high24Humi.setText("" + getHighest24Humidity() + unitsForHumidity);
            avg24Humi.setText("" + getAverage24Humidity() + unitsForHumidity);
            low24Humi.setText("" + getLowest24Humidity() + unitsForHumidity);

            high24Press.setText("" + getHighest24Pressure() + unitsForPressure);
            avg24Press.setText("" + getAverage24Pressure() + unitsForPressure);
            low24Press.setText("" + getLowest24Pressure() + unitsForPressure);

            String highDewpForDisplay = String.Format("%.1f", getHighest24DewPoint(unitsForTemperature));
            String avgDewpForDisplay = String.Format("%.1f", getAverage24DewPoint(unitsForTemperature));
            String lowDewpForDisplay = String.Format("%.1f", getLowest24DewPoint(unitsForTemperature));

            high24Dewp.setText(highDewpForDisplay + unitsForTemperature);
            avg24Dewp.setText(avgDewpForDisplay + unitsForTemperature);
            low24Dewp.setText(lowDewpForDisplay + unitsForTemperature);

            referenceDate.setText("Reference Date : " + getReferenceDate());
            lastDownloadDate.setVisibility(Visibility.Hidden);

        }


        public override void setupChart(BMChart chart, String command)
        {
            if (!(chart is BMLineChart)) return;
            BMLineChart lineChart = (BMLineChart)chart;

            if (command.Equals("*bur"))
            {
                isDownloadReady = !isDownloadReady;
                // If we sent the "*bur" command, setup the chart
                lineChart.init("", 5);
                lineChart.setLabels(
                        new String[]{
                            "Temperature (" + getTempUnits() + ")",
                            "Humidity",
                            "Dew Point (" + getTempUnits() + ")"
                        },
                        new Color[]{
                            Colors.Red,
                            Colors.Blue,
                            Colors.Green
                        }
                );
            }
            else if (new Regex("\\*units[c|f]").IsMatch(command))
            {
                // If we sent the "*units" command, change units
                bool change = true;
                String oldUnits = getTempUnits();
                switch (command[command.Length - 1])
                {
                    case 'c': if (isInFahrenheit()) _mode -= 100; break;
                    case 'f': if (!isInFahrenheit()) _mode += 100; break;
                    default: change = false; break;
                }
                if (!change) return;
                int length = lineChart.getEntryCount() / 3;
                float[] temperature = new float[length];
                float[] humidity = new float[length];
                float[] dewPoint = new float[length];
                Log.d("BMTempHumi", "Length: " + length);
                // Store the old temperatures
                for (int i = 0; i < length; i++)
                {
                    temperature[i] = lineChart.getEntry("Temperature (" + oldUnits + ")", i).getY();
                    humidity[i] = lineChart.getEntry("Humidity", i).getY();
                    dewPoint[i] = lineChart.getEntry("Dew Point (" + oldUnits + ")", i).getY();
                }
                // Manipulate the graph
                lineChart.setLabels(
                        new String[]{
                            "Temperature (" + getTempUnits() + ")",
                            "Humidity",
                            "Dew Point (" + getTempUnits() + ")"
                        },
                        new Color[]{
                            Colors.Red,
                            Colors.Blue,
                            Colors.Green
                        }
                );
                // Manipulate the data
                for (int i = 0; i < length; i++)
                {
                    temperature[i] = (isInFahrenheit())
                            ? temperature[i] * 1.8f + 32.0f
                            : (temperature[i] - 32.0f) / 1.8f;
                    dewPoint[i] = (isInFahrenheit())
                            ? dewPoint[i] * 1.8f + 32.0f
                            : (dewPoint[i] - 32.0f) / 1.8f;
                }
                // Add the temperatures back in
                for (int i = 0; i < length; i++)
                {
                    lineChart.addEntry("Temperature (" + getTempUnits() + ")", new Entry(i, temperature[i]));
                    lineChart.addEntry("Humidity", new Entry(i, humidity[i]));
                    lineChart.addEntry("Dew Point (" + getTempUnits() + ")", new Entry(i, dewPoint[i]));
                }
            }
            else if (new Regex("\\*lint([0-9]+)").IsMatch(command))
            {
                Match matcher = new Regex("\\*lint([0-9]+)").Match(command);
                //matcher.matches();
                int newLoggingInterval = int.Parse(matcher.Groups[1].Value.Trim());
                //BMDatabase database = BMDeviceMap.INSTANCE.getBMDatabase(getAddress());
                //if (loggingInterval != newLoggingInterval)
                //{
                //    database.clear();
                //}
                _loggingInterval = newLoggingInterval;
                //database.setLoggingInterval(loggingInterval);
            }
        }

        public override void updateChart(BMChart chart, String text)
        {
            if (!(chart is BMLineChart)) return;
            BMLineChart lineChart = (BMLineChart)chart;

            // Only continue if it's THD values
            //Matcher matcher = burst_pattern.matcher(text);
            //if (!matcher.matches()) return;
            Match matcher = burst_pattern.Match(text);
            if (!matcher.Success) return;

            // Parse values
            float[] value = new float[3];
            for (int i = 0; i < value.Length; i++)
            {
                value[i] = float.Parse(matcher.Groups[i + 1].Value.Trim());
            }

            // Insert values as new entries
            int index = lineChart.getEntryCount() / 3;
            lineChart.addEntry("Temperature (" + getTempUnits() + ")", new Entry(index, value[0]));
            lineChart.addEntry("Humidity", new Entry(index, value[1]));
            lineChart.addEntry("Dew Point (" + getTempUnits() + ")", new Entry(index, value[2]));
        }
    }
}
