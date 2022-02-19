using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;


using BarometerBT.Utils;
using MkZ.Bluetooth;

namespace BlueMaestro.Old.Original
{
    public abstract class BMDevice
    {

        /**
         * MAC address of the device
         */
        protected readonly ulong address;

    /**
     * Name of the device
     */
    protected String name;

        /**
         * ID/Version of the device
         */
        protected readonly byte version;

        /**
         * Received Signal Strength Indicator
         */
        protected byte rssi;

        /**
         * Bond state
         */
        protected int bondState;

        /**
         * Device mode
         */
        protected byte _mode;

        /**
         * Whether logs can be downloaded from the device at this stage
         */
        protected bool isDownloadReady;

        /**
         * Whether logs can be downloaded from the device at this stage
         */
        protected int logCount;

        /**
         * Constructor
         * @param device Bluetooth device
         * @param version Version of this Blue Maestro device
         */
        public BMDevice(MkZ.Bluetooth.BluetoothDevice device, byte version)
        {
            this.address = device.Address;
            this.name = device.Name;
            this.version = version;
            this.bondState = 11; // device.State;
            this._mode = 0;
        }

        public ulong getAddress() { return address; }
        public String getName() { return name; }
        public byte getVersion() { return version; }
        public byte getRSSI() { return rssi; }
        public int getBondState() { return bondState; }
        public byte getMode()
        {
            return _mode;
        }

        public bool IsDownloadReady()
        {
            return isDownloadReady;
        }
        public int getLogCount() { return logCount; }

        public void setName(String name)
        {
            this.name = name;
        }
        public void setDownloadReady(bool isDownloadReady)
        {
            this.isDownloadReady = isDownloadReady;
        }
        public void setMode(byte mode) { this._mode = mode; }

        /**
         * Updates this Blue Maestro device with new info
         * @param device
         */
        public void updateWithInfo(BluetoothDevice device)
        {
            this.name = device.Name;
            this.bondState = 11; // device.getBondState();
        }

        /**
         * Updates this Blue Maestro device with new data
         * @param rssi The new RSSI of the device
         * @param mData The new manufacturer data of the device
         * @param sData The new scan response data of the device
         */
        public virtual void updateWithData(int rssi, String name, byte[] mData, byte[] sData)
        {
            this.rssi = (byte)rssi;
            this.name = name;
            this.logCount = convertToUInt16(mData[7], mData[8]);
        }

        public void processCommand(String command)
        {
            Regex matcher = new Regex("\\*nam(.*)");
            //Matcher matcher = Pattern.compile("\\*nam(.*)").matcher(command);
            //if (matcher.matches())
            if(matcher.IsMatch(command))
            {
                this.name = matcher.GetGroupNames()[1];
            }
        }

        /**
         * Sets up the chart for data
         * @param chart The chart to setup
         * @param command The command sent to the device
         */
        public abstract void setupChart(BMChart chart, String command);

        /**
         * Updates the chart with data
         * @param chart The chart to update
         * @param text The data to update the chart with
         */
        public abstract void updateChart(BMChart chart, String text);

        /**
         * Updates the device's view group to update its display in the device element list
         * @param vg The view group to update
         */
        public virtual void updateViewGroup(ViewGroup vg)
        {
            //TextView tvaddr = (TextView)vg.findViewById(R.id.address);
            //TextView tvname = (TextView)vg.findViewById(R.id.name);
            //TextView tvrssi = (TextView)vg.findViewById(R.id.rssi);

            //// Name
            //tvname.setText(getName());
            //tvname.setVisibility(Visibility.Visible);

            //// MAC address
            //tvaddr.setText(getAddress());
            //tvaddr.setVisibility(Visibility.Visible);

            //// RSSI
            //byte rssival = getRSSI();
            //if (rssival != 0)
            //{
            //    tvrssi.setVisibility(Visibility.Visible);
            //    tvrssi.setText("RSSI: " + rssival + " dBm");
            //}
            //else
            //{
            //    tvrssi.setVisibility(Visibility.Collapsed);
            //}
        }

        public abstract void updateViewGroupForDetails(ViewGroup vg);

        public bool equals(Object o)
        {
            if (o == null) return false;
            if (!(o is BMDevice)) return false;
            return address == ((BMDevice)o).address;
        }

        public int hashCode()
        {
            return address.GetHashCode();
        }

        /**
         * Checks if the scan data is from a Blue Maestro BLE device
         * @param data
         * @return
         */
        public static bool isBMDevice(byte[] data)
        {
            return data.Length >= 3
                    && ((int)data[1] & 0xFF) == 0x33
                    && ((int)data[2] & 0xFF) == 0x01;
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

        protected static int convertToUInt16(byte first, byte second)
        {
            int value = ((first & 0xff) << 8) | (second & 0xff);
            return value;
        }

        /**
         * Convert a byte to signed int 8
         * @param b
         * @return
         */
        protected static int convertToUInt8(byte b)
        {
            return (b >= 0) ? b : b + 256;
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
