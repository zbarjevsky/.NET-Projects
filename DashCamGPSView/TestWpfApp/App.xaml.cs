using CircularGauge;
using SpeedGauge;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Device.Location;
using XLabs.Platform.Services.Geolocation;
using System.Net;
using System.IO;
using System.Net.Sockets;
using TinyJson;
using GPSDataParser.Tools;

namespace TestWpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //test
            //string fileName = @"D:\Archive\BackUp\DashCam\DCIM\Movie\2019_0624_075501_298.MP4";
            //var list = NovatekViofoGPSParser.ViofoParser.ReadMP4FileGpsInfo(fileName);
            //NovatekViofoGPSParser.NovatekParser.ReadMP4FileGpsInfo(fileName);

            //HighlightBrush brush = new HighlightBrush(HighlightBrush.HighlightColor.Red);

            //HighlightBrush.HighlightColor c = HighlightBrush.ConvertBack(brush.Brush);

            //GetLocationProperty();
            ////http://bot.whatismyipaddress.com
            string externalip = new WebClient().DownloadString("http://ipv4.icanhazip.com");
            string externalipv6 = new WebClient().DownloadString("http://ipv6.icanhazip.com");

            GetCountryByIP();

            Environment.Exit(-1);
        }

        public static void GetCountryByIP()
        {
            IpInfo ipInfo = IpInfo.Get();
        }

        public static string GetLocalIPAddress()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            return "";
        }

    }
}
