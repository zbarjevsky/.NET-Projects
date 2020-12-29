using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BarometerBT.Utils;
using MZ.Tools;

namespace BarometerBT.Bluetooth
{
    /**
     * Created by Garrett on 24/08/2016.
     */
    public class Utility
    {

        String referenceDateString;

        public Utility()
        {

        }

        public static String bytesAsHexString(byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length);
            foreach (byte byteChar in bytes)
                stringBuilder.Append(String.Format("{0:02X}", byteChar)).Append(" ");
            return stringBuilder.ToString();
        }

        public static String convertValueTo(String value, String units)
        {
            if (value == null) return null;
            float val = Convert.ToSingle(value);
            switch (units[units.Length - 1])
            {
                case 'F':
                    val = val * 1.8f + 32;
                    break;
                default:
                    break;
            }
            return String.Format("%.1f", val);
        }

        public static String formatKey(String key)
        {
            key = key.Trim();
            key = key.Replace("_", " ");
            key = key.ToLower();
            char[] array = key.ToCharArray();
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == ' ') array[i + 1] = Char.ToUpper(array[i + 1]);
            }
            array[0] = Char.ToUpper(array[0]);
            return new String(array);
        }

        public static void sendCommand(UartService service, String command) //throws UnsupportedEncodingException
        {
            service.writeRXCharacteristic(Encoding.UTF8.GetBytes(command));// command.getBytes("UTF-8"));
        }

        public String convertNumberIntoDate(String referenceDateNumber)
        {
            referenceDateString = "null";
            int numberPassedIn = Convert.ToInt32(referenceDateNumber);
            //const string shortFormat = ("yy/MM/dd HH:mm");
            const string longFormat = ("yyyy-MMM-dd HH:mm");

            if (referenceDateNumber.Length > 10 || numberPassedIn == 0)
            {
                return referenceDateString;
            }

            int minutes = numberPassedIn % 100;
            int hours = (numberPassedIn / 100) % 100;
            int days = (numberPassedIn / 10000) % 100;
            int months = (numberPassedIn / 1000000) % 100;
            int years = (numberPassedIn / 100000000) % 100;

            try
            {
                DateTime referenceDate = new DateTime(years, months, days, hours, minutes, 0);
                referenceDateString = referenceDate.ToString(longFormat);
                //Log.d("Utility", "Date number passed in is "+referenceDateNumber+" date parsed is " + referenceDateString);
                /*  //Code that increments time by 3600 seconds (or 1 hour)
                Calendar cal = Calendar.getInstance();
                cal.setTime(referenceDateDate);
                cal.add(Calendar.SECOND, 3600);
                Date newDate = cal.getTime();
                */

            }
            catch (Exception e)
            {
                //Log.d("Utility", "Date parsing failed");
                // TODO Auto-generated catch block
                Log.e(e.ToString());
            }
            return referenceDateString;
        }
    }
}
