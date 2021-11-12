using DashCamGPSView.Tools;
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

        public void UpdateInfo(DashCamFileInfo gps, int pointIdx)
        {
            if (pointIdx >= 0 && pointIdx < gps.GpsInfo.Count)
            {
                GpsPointData inf = gps[pointIdx];

                compass.SetDirection(inf.Course, inf.SpeedMph);

                txtSpeed.Text = "Speed: " + inf.SpeedMph.ToString("0.0 mph");
                txtLat.Text = "Lattitude:  " + SexagesimalAngle.ToString(inf.Latitude);
                txtLon.Text = "Longtitude: " + SexagesimalAngle.ToString(inf.Longitude);
                if(gps.TimeZone > -24 && gps.TimeZone < 24)
                    txtTime.Text = inf.FixTime.AddHours(gps.TimeZone).ToString("yyyy/MM/dd HH:mm:ss");
            }
            else
            {
                compass.SetDirection(0, false);

                txtSpeed.Text = "Speed: N/A";
                txtLat.Text = "...";
                txtLon.Text = "...";
                txtTime.Text = "...";
            }
        }
    }
}
