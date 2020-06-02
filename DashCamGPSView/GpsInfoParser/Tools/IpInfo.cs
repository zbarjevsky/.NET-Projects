using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TinyJson;

namespace GPSDataParser.Tools
{
    public class IpInfo
    {
        //[JsonProperty("ip")]
        public string IP { get; set; }

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

        //[JsonProperty("Timezone")]
        public string Timezone { get; set; }

        public double GetLatitude()
        {
            string lat = Loc.Split(',')[0];
            return double.Parse(lat);
        }

        public double GetLongitude()
        {
            string lon = Loc.Split(',')[1];
            return double.Parse(lon);
        }

        public static IpInfo Parse(string json)
        {
            return json.FromJson<IpInfo>();
        }

        public static IpInfo Get()
        {
            string info = DownloadString("http://ipinfo.io");
            return info.FromJson<IpInfo>();
        }

        public static string GetIpV4()
        {
            string externalipv4 = DownloadString("http://ipv4.icanhazip.com");
            return externalipv4;
        }

        public static string GetIpV6()
        {
            string externalipv6 = DownloadString("http://ipv6.icanhazip.com");
            return externalipv6;
        }

        public static string DownloadString(string address)
        {
            try
            {
                using (var client = new WebClient())
                {
                    return client.DownloadString(address);
                }
            }
            catch(Exception err)
            {
                System.Diagnostics.Debug.WriteLine("WebClient.DownloadString: "+err);
                return string.Empty;
            }
        }
    }
}
