using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.Windows
{
    public enum RegKeyType
    {
        LocalMachine,
        CurrentUser
    }

    public enum RegKeyOp
    {
        Update,
        Remove
    }

    public class WindowsRegistryHelper
    {
        public string ApplicationPath { get; }

        private string _appName = "MkZ";
        private RegKeyType _keyType = RegKeyType.CurrentUser;

        public bool IsLoadWithWindows
        {
            get { return GetIsLoadWithWindows(_keyType, _appName, ApplicationPath); }
            set { SetIsLoadWithWindows(_keyType, _appName, ApplicationPath, value ? RegKeyOp.Update : RegKeyOp.Remove); }
        }

        public WindowsRegistryHelper(string appName, RegKeyType keyType)
        {
            _appName = appName;
            _keyType = keyType;

            ApplicationPath = System.Reflection.Assembly.GetCallingAssembly().Location;
        }

        private static Microsoft.Win32.RegistryKey OpenRunSubKey(RegKeyType regType, bool bWritable)
        {
            const string REG_KEY_MACHINE = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run";
            const string REG_KEY_USER = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

            if(regType == RegKeyType.LocalMachine)
                return Microsoft.Win32.Registry.LocalMachine.OpenSubKey(REG_KEY_MACHINE, bWritable);
            else
                return Microsoft.Win32.Registry.CurrentUser.OpenSubKey(REG_KEY_USER, bWritable);
        }

        public static bool GetIsLoadWithWindows(RegKeyType regType, string appName, string appPath)
        {
            Microsoft.Win32.RegistryKey key = OpenRunSubKey(regType, false);
            string path = (string)key.GetValue(appName);
            return string.Compare(path, appPath, ignoreCase: true) == 0;
        }

        public static void SetIsLoadWithWindows(RegKeyType regType, string appName, string appPath, RegKeyOp operation)
        {
            Microsoft.Win32.RegistryKey key = OpenRunSubKey(regType, true);
            if (operation == RegKeyOp.Update)
            {
                key.SetValue(appName, appPath);
            }
            else //remove
            {
                key.DeleteValue(appName);
            }
        }
    }
}
