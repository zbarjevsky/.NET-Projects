using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MkZ.Windows;

namespace MkZWeatherStation
{
    public class Settings : NotifyPropertyChangedImpl
    {
#if DEBUG
        public const string BarometerMkZ = "BarometerMkZ_Debug";
#else
        public const string BarometerMkZ = "BarometerMkZ";
#endif

        private WindowsRegistryHelper _windowsRegistryHelper = new WindowsRegistryHelper(BarometerMkZ, RegKeyType.LocalMachine);

        public bool LoadWithWindows
        {
            get { return _windowsRegistryHelper.IsLoadWithWindows; }
            set { _windowsRegistryHelper.IsLoadWithWindows = value; }
        }
    }
}
