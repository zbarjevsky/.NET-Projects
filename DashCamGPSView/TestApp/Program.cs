using GPSDataParser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFilename = @"c:\Temp\DashcamDuDuBell\Parking\F\PARK191127-023323F.NMEA";// PARK191121-184210F.NMEA";
            string outFilename = @"c:\Temp\DashcamDuDuBell\PARK191127-023323F.NMEA.kml";// PARK191121-184210F.NMEA.kml";

            try
            {
                List<NmeaParser.Nmea.NmeaMessage> list = NMEAParser.NMEAParser.ReadFile(inFilename);
                List<GpsPointData> route = GpsPointData.Convert(list);

                GPSDataParser.FileFormats.KML.KmlEx kml = new GPSDataParser.FileFormats.KML.KmlEx();
                kml.SaveToFile(route, outFilename);
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.ToString());
                while (err.InnerException != null)
                {
                    Debug.WriteLine(err.InnerException.ToString());
                    err = err.InnerException;
                }
            }
        }
    }
}
