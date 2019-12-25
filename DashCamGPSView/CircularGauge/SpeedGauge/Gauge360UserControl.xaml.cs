using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SpeedGauge
{
    /// <summary>
    /// Interaction logic for UserControlGauge.xaml
    /// </summary>
    public partial class Gauge360UserControl : UserControl
    {
        public Gauge360UserControl()
        {
            this.DataContext = new Gauge360ViewModel();
            InitializeComponent();
        }
    }
}
