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

namespace DashCamGPSView.Controls
{
    /// <summary>
    /// Interaction logic for CompassUserControl.xaml
    /// </summary>
    public partial class CompassUserControl : UserControl
    {
        public CompassUserControl()
        {
            InitializeComponent();
        }

        public double Direction
        {
            get { return arrowDirection.Angle; }
            set { arrowDirection.Angle = value; }
        }

        public void SetDirection(double direction, bool bShowCar)
        {
            Direction = direction;
            car.Visibility = bShowCar ? Visibility.Visible : Visibility.Collapsed;
        }

        public void SetDirection(double direction, double speed)
        {
            SetDirection(direction, speed > 1);
        }
    }
}
