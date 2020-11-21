using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerBT.Utils
{
    public enum TemperatureUnits
    {
        Celcius,
        Farenheit
    }

    public enum AirPressureUnits
    {
        MilliBars,
        InchesOfMercury,
        KiloPascals
    }

    public class UnitsDescriptor
    {
        public TemperatureUnits TemperatureUnits { get; set; } = TemperatureUnits.Celcius;

        public AirPressureUnits AirPressureUnits { get; set; } = AirPressureUnits.MilliBars;

        public static IEnumerable<TemperatureUnits> GetEnumTemperatureUnits()
        {
            return Enum.GetValues(typeof(TemperatureUnits)).Cast<TemperatureUnits>();
        }

        public static IEnumerable<AirPressureUnits> GetEnumAirPressureUnits()
        {
            return Enum.GetValues(typeof(AirPressureUnits)).Cast<AirPressureUnits>();
        }

        public string GetTemperatureUnitsDesc()
        {
            switch (TemperatureUnits)
            {
                case TemperatureUnits.Farenheit:
                    return " ºF";
                case TemperatureUnits.Celcius:
                default:
                    return " ºC";
            }
        }

        public string GetAirpressureUnitsDesc()
        {
            switch (AirPressureUnits)
            {
                case AirPressureUnits.InchesOfMercury:
                    return " inHg";
                case AirPressureUnits.KiloPascals:
                    return " kPa";
                case AirPressureUnits.MilliBars:
                default:
                    return " mBar";
            }
        }

        public double ConvertTemperature(double temperatureInC)
        {
            switch (TemperatureUnits)
            {
                case TemperatureUnits.Farenheit:
                    return 32 + temperatureInC * 1.8;
                case TemperatureUnits.Celcius:
                default:
                    return temperatureInC;
            }
        }

        public double ConvertPressure(double pressure_mBar)
        {
            switch (AirPressureUnits)
            {
                case AirPressureUnits.InchesOfMercury:
                    return pressure_mBar / 33.8639;
                case AirPressureUnits.KiloPascals:
                    return pressure_mBar / 10.0;
                case AirPressureUnits.MilliBars:
                default:
                    return pressure_mBar;
            }
        }
    }
}
