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

            //    string ip = GetLocalIPAddress();
            //    if (!string.IsNullOrWhiteSpace(ip))
            //        CityStateCountByIp(externalip.Trim());

            GetCountryByIP();

            Environment.Exit(-1);
        }

        static void GetLocationProperty()
        {

            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
            if (watcher.Status == GeoPositionStatus.NoData)
            {

            }

            // Do not suppress prompt, and wait 1000 milliseconds to start.
            if (watcher.TryStart(false, TimeSpan.FromMilliseconds(10000)))
            {
                GeoCoordinate coord = watcher.Position.Location;
                if (coord.IsUnknown != true)
                {
                    Console.WriteLine("Lat: {0}, Long: {1}",
                        coord.Latitude,
                        coord.Longitude);
                }
                else
                {
                    Console.WriteLine("Unknown latitude and longitude.");
                }
            }

        }
            public static string CityStateCountByIp(string IP)
            {
                //var url = "http://freegeoip.net/json/" + IP;
                var url = "http://freegeoip.net/json/" + IP;
                //string url = "http://api.ipstack.com/" + IP + "?access_key=[KEY]";
                var request = System.Net.WebRequest.Create(url);

                using (WebResponse wrs = request.GetResponse())
                using (Stream stream = wrs.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    //var obj = JObject.Parse(json);
                    //string City = (string)obj["city"];
                    //string Country = (string)obj["region_name"];
                    //string CountryCode = (string)obj["country_code"];

                    //return (CountryCode + " - " + Country + "," + City);
                }

                return "";

            }
        public static void GetCountryByIP()
        {
            IpInfo ipInfo = new IpInfo();

            string info = new WebClient().DownloadString("http://ipinfo.io");

            //JavaScriptSerializer jsonObject = new JavaScriptSerializer();
            //ipInfo = jsonObject.Deserialize<IpInfo>(info);

            //RegionInfo region = new RegionInfo(ipInfo.Country);

            //Console.WriteLine(region.EnglishName);
            //Console.ReadLine();

            //{
            //    "ip": "73.88.4.120",
            //        "hostname": "c-73-88-4-120.hsd1.mn.comcast.net",
            //        "city": "Minneapolis",
            //        "region": "Minnesota",
            //        "country": "US",
            //        "loc": "44.9800,-93.2638",
            //        "org": "AS7922 Comcast Cable Communications, LLC",
            //        "postal": "55440",
            //        "timezone": "America/Chicago",
            //        "readme": "https://ipinfo.io/missingauth"
            //}


        }

        public class IpInfo
        {
            //country
            public string Country { get; set; }
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

    public class IpInfo
    {
        //[JsonProperty("ip")]
        public string Ip { get; set; }

        //[JsonProperty("hostname")]
        public string Hostname { get; set; }

        //[JsonProperty("city")]
        public string City { get; set; }

        //[JsonProperty("region")]
        public string Region { get; set; }

        //[JsonProperty("country")]
        public string Country { get; set; }

        //[JsonProperty("loc")]
        public string Loc { get; set; }

        //[JsonProperty("org")]
        public string Org { get; set; }

        //[JsonProperty("postal")]
        public string Postal { get; set; }
    }
}
