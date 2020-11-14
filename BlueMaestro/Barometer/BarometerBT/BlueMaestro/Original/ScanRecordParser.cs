using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerBT.Bluetooth
{
    /**
     * Created by Garrett on 15/08/2016.
     *
     * BluetoothLeScanner and ScanRecord are not provided until Android SDK 21.
     * Instead, for compatibility, we create a small parser that allows us to
     * grab the manufacturer data (as that is all which is needed).
     *
     * The scan record holds e.g. service UUIDs, device name, etc. as well as
     * manufacturer data.
     * The data is concatenated togather, which each piece in the following format:
     * [data-id][data-values]
     *
     * For more information on IDs, see:
     * https://www.bluetooth.org/en-us/specification/assigned-numbers/generic-access-profile
     */
    public class ScanRecordParser
    {

        private readonly byte[] manufacturerData;
        private readonly byte[] scanResponseData;
        private readonly byte[] deviceNameFromScan;
        private String name;
        private bool nameFound = false;

        public ScanRecordParser(byte[] scanRecord) //throws UnsupportedEncodingException
        {
            // We look for 0xFF 0x33 0x01 to detect manufacturer/scan response data
            int startingPositionOfAdvertisement = 0;
            int startingPositionOfScanResponse = 0;
            int startingPositionOfName = 0;
            int versionNumber = 0;
            deviceNameFromScan = new byte[8];

            for (int i = 0; i <= scanRecord.Length; i++)
            {
                if (isManufacturerID(i, scanRecord) == true)
                {
                    startingPositionOfAdvertisement = i;
                    //Log.d("ScanRecordParser", "starting position of advertisement is :" + startingPositionOfAdvertisement);
                    break;
                }
            }

            for (int k = startingPositionOfAdvertisement + 1; k <= scanRecord.Length; k++)
            {
                if (isManufacturerID(k, scanRecord) == true)
                {
                    startingPositionOfScanResponse = k;
                    //Log.d("ScanRecordParser", "starting position of scan response is :" + startingPositionOfScanResponse);
                    break;
                }
            }

            if (startingPositionOfAdvertisement < scanRecord.Length - 3)
            {
                versionNumber = (scanRecord[startingPositionOfAdvertisement + 3] & 0xFF);
                //Log.d("ScanRecordParser", "Version " +versionNumber+ " is found");

            }
            /*
            //This method scrapes out the name assuming it is no more than 8 letters and starts 2 on from the name indicator 0x09
            //Begin by ensuring the starting position is around the right area, which is about at least 8 characters from the end of advertisement packet
            if ((startingPositionOfName < scanRecord.length - 8) && ((startingPositionOfAdvertisement + 3) < scanRecord.length)) {
                int sizeOfName = 0;
                Log.d("ScanRecordParser", "Parameters to attempt name scrape are good, starting position of name is " + startingPositionOfName + " and starting position of advertisement is : " + startingPositionOfAdvertisement);
                for (int k = startingPositionOfScanResponse; k > 0; k--) {
                    if ((scanRecord[k] & 0xFF) == 0x09) {
                        startingPositionOfName = k + 1;
                        nameFound = true;
                        Log.d("ScanRecordParser", "Name is found, starting position appears to be : " + startingPositionOfName);
                        break;
                    }
                }
            }

            //Scrape if name is found
            if (nameFound == true) {
                for (int n = 0; n < 8; n++){
                    this.deviceNameFromScan[n] = scanRecord[startingPositionOfName+n];
                }
                name = new String(deviceNameFromScan, "US-ASCII");
                Log.d("ScanRecordParser", "Finished scrapping : suggest name is " + name);
            } else {
                name = "Unknown";
            }
            */
            //Alternative method of getting scanned name which is not to scrape, but to start
            //at set inset on advertising packet
            if ((versionNumber == 23) || (versionNumber == 22) || (versionNumber == 13) || (versionNumber == 32) || (versionNumber == 42) || (versionNumber == 52) || (versionNumber == 62))
            {
                name = parseName(scanRecord);
            }

            /*
            if (versionNumber == 42) {
                startingPositionOfName = startingPositionOfAdvertisement + 13;
                Log.d("ScanRecordParser", "Looking for name.  Version is :" + versionNumber + " and starting position of name is :" + (scanRecord[startingPositionOfName] & 0xFF));
                if ((scanRecord[startingPositionOfName] & 0xFF) == 0x09) {
                    Log.d("ScanRecordParser", "Looking for name.  Version is :" + versionNumber + " and starting position of name is :" + (scanRecord[startingPositionOfName] & 0xFF));
                    for (int n = 0; n < 8; n++) {
                        Log.d("ScanRecordParser", "Looking for name.  Version is :" + versionNumber + " and starting position of name is :" + (scanRecord[startingPositionOfName] & 0xFF));
                        this.deviceNameFromScan[n] = scanRecord[startingPositionOfName + n + 2];
                    }
                    name = new String(deviceNameFromScan, "US-ASCII");
                    Log.d("ScanRecordParser", "Finished scrapping : suggest name is " + name);
                } else {
                    name = "Unknown";
                }
            }
            */



            // Manufacturer data
            if (startingPositionOfAdvertisement > startingPositionOfScanResponse) startingPositionOfAdvertisement = startingPositionOfScanResponse;

            this.manufacturerData = new byte[startingPositionOfScanResponse - startingPositionOfAdvertisement];
            for (int j = 0; j < manufacturerData.Length; j++)
            {
                manufacturerData[j] = scanRecord[startingPositionOfAdvertisement + j];
            }
            // Scan response data
            this.scanResponseData = new byte[scanRecord.Length - startingPositionOfScanResponse];
            for (int l = 0; l < scanResponseData.Length; l++)
            {
                scanResponseData[l] = scanRecord[startingPositionOfScanResponse + l];
            }
            //Log.d("ScanRecordParser", "Scan Record data: 0x" + Utility.bytesAsHexString(scanRecord));
            //Log.d("ScanRecordParser", "Manufacturer data: 0x" + Utility.bytesAsHexString(manufacturerData));
            //Log.d("ScanRecordParser", "Scan Response data: 0x" + Utility.bytesAsHexString(scanResponseData));
        }

        static String parseName(byte[] advData)
        {
            int ptr = 0;

            while (ptr < advData.Length - 2)
            {
                int length = advData[ptr++] & 0xff;
                if (length == 0)
                    break;

                int ad_type = (advData[ptr++] & 0xff);

                switch (ad_type)
                {
                    case 0xff:
                        break;
                    case 0x08:
                    case 0x09:
                        byte[] name = new byte[length - 1];
                        int i = 0;
                        length = length - 1;
                        while (length > 0)
                        {
                            length--;
                            name[i++] = advData[ptr++];
                        }
                        String nameString = Convert.ToString(name);

                        return nameString;
                }
                ptr += (length - 1);
            }
            return null;
        }

        public byte[] getManufacturerData()
        {
            return manufacturerData;
        }

        public byte[] getScanResponseData()
        {
            return scanResponseData;
        }

        public String getScannedName()
        {
            return name;
        }

        public static bool isManufacturerID(int i, byte[] scanRecord)
        {
            if (i + 2 > scanRecord.Length) { return false; }
            if ((((int)scanRecord[i] & 0xFF) == 0xFF) && (((int)scanRecord[i + 1] & 0xFF) == 0x33) && (((int)scanRecord[i + 2] & 0xFF) == 0x01))
            {
                return true;
            }
            return false;
        }
    }
}
