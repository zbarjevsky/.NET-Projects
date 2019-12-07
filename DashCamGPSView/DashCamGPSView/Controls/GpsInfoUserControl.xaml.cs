using GPSDataParser;
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
    /// Interaction logic for GpsInfoUserControl.xaml
    /// </summary>
    public partial class GpsInfoUserControl : UserControl
    {
        public GpsInfoUserControl()
        {
            InitializeComponent();
        }

        public void UpdateInfo(GpsPointData inf, int timeZone)
        {
            if (inf != null)
            {
                compass.Direction = inf.Course;

                txtSpeed.Text = "Speed: " + inf.SpeedMph.ToString("0.0 mph");
                txtLat.Text = "Lat: " + SexagesimalAngle.ToString(inf.Latitude);
                txtLon.Text = "Lon: " + SexagesimalAngle.ToString(inf.Longitude);
                txtTime.Text = "Time: " + inf.FixTime.AddHours(timeZone).ToString("yyyy/MM/dd HH:mm:ss");
            }
            else
            {
                compass.Direction = 0;

                txtSpeed.Text = "Speed: N/A";
                txtLat.Text = "N/A";
                txtLon.Text = "";
                txtTime.Text = "Time: N/A";
            }
        }
    }
}
