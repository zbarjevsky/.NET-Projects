using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopManagerUX
{
    /// <summary>
    /// Interaction logic for AppChooserUserControl.xaml
    /// </summary>
    public partial class AppChooserUserControl : UserControl
    {
        private ViewModel _VM = null;

        public AppChooserUserControl()
        {
            InitializeComponent();
        }

        public void SetVM(ViewModel vm)
        {
            _VM = vm;
            this.DataContext = vm;
            cmbApps.ItemsSource = vm.Apps;
        }

        public AppInfo SelectedApp
        {
            get { return _VM.Apps[cmbApps.SelectedIndex]; }
        }
    }
}
