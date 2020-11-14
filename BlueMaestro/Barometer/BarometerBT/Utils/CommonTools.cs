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
        public static void ExecuteOnUiThreadBeginInvoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            Application.Current.Dispatcher.BeginInvoke(action, priority);
        }

        public static void WriteInfoLine([CallerMemberName] string caller = "", [CallerFilePath] string file = "")
        {
            file = Path.GetFileName(file);
            System.Diagnostics.Debug.WriteLine("=> Caller: " + caller + ", file: " + file);
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
