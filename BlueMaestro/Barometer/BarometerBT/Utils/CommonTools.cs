using MZ.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BarometerBT.Utils
{
    public class CommonTools
    {
        public static DispatcherOperation ExecuteOnUiThreadBeginInvoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            if (Application.Current != null && !Application.Current.Dispatcher.CheckAccess())
                return Application.Current.Dispatcher.BeginInvoke(action, priority);
            else
                action.Invoke();

            return null;
        }

        public static void ExecuteOnUiThreadInvoke(Action action, 
            DispatcherPriority priority = DispatcherPriority.Normal, 
            [CallerMemberName] string propertyName = null)
        {
            try
            {
                if (Application.Current != null && !Application.Current.Dispatcher.CheckAccess())
                    Application.Current.Dispatcher.Invoke(action, priority);
                else
                    action.Invoke();
            }
            catch (Exception ex)
            {
                Log.e("ExecuteOnUiThreadInvoke({0}), exeption: {1}\n", propertyName, ex);
            }        
        }

        public static string TimeSpanToString(TimeSpan ts)
        {
            if (ts.TotalMinutes < 1.0)
                return ts.ToString(@"ss\.f\s");
            if (ts.TotalHours < 1.0)
                return ts.ToString(@"mm\m\ ss\.f\s");
            if (ts.TotalDays < 1.0)
                return ts.ToString(@"hh\h\ mm\m\ ss\s");
            if (ts.TotalDays <= 30.0)
                return ts.ToString(@"d\d\ hh\h\ mm\m\ ss\s");

            return ts.ToString(@"D\d\ hh\h\ mm\m\ ss\s");
        }

        public static void WriteInfoLine([CallerMemberName] string caller = "", [CallerFilePath] string file = "")
        {
            file = Path.GetFileName(file);
            Log.i("=> Caller: " + caller + ", file: " + file);
        }

        public static bool AskForSucceded(string title)
        {
            return MessageBox.Show(
                "Algorithm Succeeded?", title,
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification)
                == MessageBoxResult.Yes;
        }

        public static MessageBoxResult ErrorMessage(string errorMessage, string title = "Error")
        {
            return MessageBox.Show(errorMessage, title,
                MessageBoxButton.OKCancel, MessageBoxImage.Error, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
        }


        public static MessageBoxResult InfoMessage(string infoMessage, string title = "Information")
        {
            return MessageBox.Show(infoMessage, title,
                MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes, MessageBoxOptions.ServiceNotification);
        }

        /**
         * Convert two bytes to signed int 16
         * @param first
         * @param second
         * @return
         */
        public static int convertToInt16(byte first, byte second)
        {
            int value = (int)first & 0xFF;
            value *= 256;
            value += (int)second & 0xFF;
            value -= (value > 32768) ? 65536 : 0;
            return value;
        }

        /**
         * Convert a byte to signed int 8
         * @param b
         * @return
         */
        public static int convertToUInt8(byte b)
        {
            return (b >= 0) ? b : b + 256;
        }

        /**
         * Ensure an unsigned int 8 is treated correct
         * @param b
         * @return
         */
        public static int convertToInt8(byte b)
        {
            int c = (int)(b & 0xff);
            return c;
        }
    }
}
