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
        public const String UNITS_F = "ºF";
        public const String UNITS_C = "ºC";

        public const String UNITS_HG = "InHg";
        public const String UNITS_MB = "mBar";
        public const String UNITS_PA = "kPa";
        public const String UNITS_RH = "%RH";

        public TemperatureUnits TemperatureUnits { get; set; }

        public AirPressureUnits AirPressureUnits { get; set; }

        public static UnitsDescriptor DefaultUnits { get; } = new UnitsDescriptor();

        public UnitsDescriptor()
        {
            Reset();
        }

        public UnitsDescriptor(UnitsDescriptor units)
        {
            this.TemperatureUnits = units.TemperatureUnits;
            this.AirPressureUnits = units.AirPressureUnits;
        }

        public void Reset()
        {
            this.TemperatureUnits = TemperatureUnits.Celcius;
            this.AirPressureUnits = AirPressureUnits.MilliBars;
        }

        public static IEnumerable<TemperatureUnits> GetEnumTemperatureUnits()
        {
            return Enum.GetValues(typeof(TemperatureUnits)).Cast<TemperatureUnits>();
        }

        public static IEnumerable<AirPressureUnits> GetEnumAirPressureUnits()
        {
            return Enum.GetValues(typeof(AirPressureUnits)).Cast<AirPressureUnits>();
        }

        public string GetHumidityUinitsDesc()
        {
            return " " + UNITS_RH;
        }

        public string GetTemperatureUnitsDesc()
        {
            switch (TemperatureUnits)
            {
                case TemperatureUnits.Farenheit:
                    return " " + UNITS_F;
                case TemperatureUnits.Celcius:
                default:
                    return " " + UNITS_C;
            }
        }

        public string GetAirPressureUnitsDesc()
        {
            switch (AirPressureUnits)
            {
                case AirPressureUnits.InchesOfMercury:
                    return " " + UNITS_HG;
                case AirPressureUnits.KiloPascals:
                    return " " + UNITS_PA;
                case AirPressureUnits.MilliBars:
                default:
                    return " " + UNITS_MB;
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
