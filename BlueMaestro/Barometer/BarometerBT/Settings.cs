using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MkZ.Windows;

namespace BarometerBT
{
    public class Settings : NotifyPropertyChangedImpl
    {
#if DEBUG
        public const string BarometerMkZ = "BarometerMkZ_Debug";
#else
        public const string BarometerMkZ = "BarometerMkZ";
#endif

        public static readonly string ApplicationPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

        private bool _isStartWithWindows = GetLoadWithWindows();
        public bool LoadWithWindows
        {
            get { return GetLoadWithWindows(); }
            set { if(SetProperty(ref _isStartWithWindows,  value)) SetLoadWithWindows(value); }
        }

        private static Microsoft.Win32.RegistryKey OpenLoadSubKey(bool bWritable)
        {
            const string REG_KEY = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run";
            return Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_KEY, bWritable);
        }

        private static bool GetLoadWithWindows()
        {
            Microsoft.Win32.RegistryKey key = OpenLoadSubKey(false);
            string path = (string)key.GetValue(BarometerMkZ);
            return !string.IsNullOrWhiteSpace(path);
        }

        private static void SetLoadWithWindows(bool bLoad)
        {
            Microsoft.Win32.RegistryKey key = OpenLoadSubKey(true);
            if (bLoad)
            {
                key.SetValue(BarometerMkZ, ApplicationPath);
            }
            else
            {
                key.DeleteValue(BarometerMkZ);
            }
        }
    }
}
