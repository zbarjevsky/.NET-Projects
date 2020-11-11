using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;


using BarometerBT.Bluetooth;

namespace BarometerBT
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        BluetoothUtils _bt = new BluetoothUtils();

        public App()
        {
            _bt.StartBluetoothSearch();
        }
    }
}
