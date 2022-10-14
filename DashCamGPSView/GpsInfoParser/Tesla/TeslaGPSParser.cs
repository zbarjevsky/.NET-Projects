using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSDataParser.Tesla
{
    public class TeslaGPSParser
    {
        public static List<GpsPointData> FindGPSInfo(string fileName)
        {
            List<GpsPointData> points = new List<GpsPointData>();

            string dir = Path.GetDirectoryName(fileName);
            string [] eventFiles = Directory.GetFiles(dir, "event.json");

            if(eventFiles != null && eventFiles.Length > 0)
            {
                GpsPointData pt = ParseJsonData(eventFiles[0]);
                points.Add(pt);
            }

            return points;
        }

        private static GpsPointData ParseJsonData(string eventFile)
        {
            double latitude = 0;
            double longitude = 0;
            DateTime time = DateTime.MinValue;
            string city = "", reason = "", camera = "", sTime = "", sLat = "0", sLon = "0";

            string[] lines = File.ReadAllLines(eventFile);
            foreach (string line in lines)
            {
                if (ParseLine(line, "\"timestamp\"", ref sTime))
                {
                    time = DateTime.ParseExact(sTime, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                }
                else if (ParseLine(line, "\"city\"", ref city))
                {
                    
                }
                else if (ParseLine(line, "\"est_lat\"", ref sLat))
                {
                    latitude = double.Parse(sLat);
                }
                else if (ParseLine(line, "\"est_lon\"", ref sLon))
                {
                    longitude = double.Parse(sLon);
                }
                else if (ParseLine(line, "\"reason\"", ref reason))
                {

                }
                else if (ParseLine(line, "\"camera\"", ref camera))
                {

                }
            }

            return new GpsPointData(time, latitude, longitude);
        }

        private static bool ParseLine(string line, string prefix, ref string val)
        {
            if (!line.Contains(prefix))
                return false;

            val = line.Substring(line.IndexOf(prefix) + prefix.Length + 2).Trim('"', ',');

            return true;
        }
    }
}
