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
        BluetoothConnection _bt = new BluetoothConnection();

        public App()
        {
            _bt.StartBluetoothSearch();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _bt.StopBluetoothSearch();
        }
    }
}
