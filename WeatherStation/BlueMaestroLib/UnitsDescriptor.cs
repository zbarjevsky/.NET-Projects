using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkZ.BlueMaestroLib
{
    public enum eTemperatureUnits
    {
        Celcius,
        Farenheit
    }

    public enum eAirPressureUnits
    {
        MilliBars,
        InchesOfMercury,
        KiloPascals,
        MllimetersOfMercury
    }

    public enum eRelativeHumidity
    {
        Percent
    }

    public struct Scale
    {
        public double Min;
        public double Max;

        public Scale(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public void Update(double val)
        {
            Max = Math.Max(Max, val);
            Min = Math.Min(Min, val);
        }
    }

    public interface IUnitBase<T> where T : struct, IConvertible
    {
        T Units { get; set; }
        string Desc { get; }
        void Reset();
        double Convert(double rawUnit);
        Scale Scale { get; }
        IEnumerable<T> GetEnum();
    }

    public class TemperatureUnits : IUnitBase<eTemperatureUnits>
    {
        public const String UNITS_F = "ºF";
        public const String UNITS_C = "ºC";

        public eTemperatureUnits Units { get; set; }
        public string Desc
        {
            get
            {
                switch (Units)
                {
                    case eTemperatureUnits.Farenheit:
                        return " " + UNITS_F;
                    case eTemperatureUnits.Celcius:
                    default:
                        return " " + UNITS_C;
                }
            }
        }

        public Scale Scale
        {
            get { return new Scale(Convert(10), Convert(40)); }
        }

        public void Reset()
        {
            Units = eTemperatureUnits.Celcius;
        }

        public double Convert(double temperatureInC)
        {
            switch (Units)
            {
                case eTemperatureUnits.Farenheit:
                    return 32 + temperatureInC * 1.8;
                case eTemperatureUnits.Celcius:
                default:
                    return temperatureInC;
            }
        }

        public IEnumerable<eTemperatureUnits> GetEnum()
        {
            return Enum.GetValues(typeof(eTemperatureUnits)).Cast<eTemperatureUnits>();
        }
    }

    public class AirPressureUnits : IUnitBase<eAirPressureUnits>
    {
        public const String UNITS_HG = "InHg";
        public const String UNITS_MB = "mBar";
        public const String UNITS_PA = "kPa";
        public const String UNITS_MM = "mmHg";

        public eAirPressureUnits Units { get; set; }
        public string Desc
        {
            get
            {
                switch (Units)
                {
                    case eAirPressureUnits.InchesOfMercury:
                        return " " + UNITS_HG;
                    case eAirPressureUnits.KiloPascals:
                        return " " + UNITS_PA;
                    case eAirPressureUnits.MllimetersOfMercury:
                        return " " + UNITS_MM;
                    case eAirPressureUnits.MilliBars:
                    default:
                        return " " + UNITS_MB;
                }
            }
        }

        public Scale Scale
        {
            get { return new Scale(Convert(940), Convert(1100)); }
        }

        public void Reset()
        {
            Units = eAirPressureUnits.MilliBars;
        }

        public double Convert(double pressure_mBar)
        {
            switch (Units)
            {
                case eAirPressureUnits.InchesOfMercury:
                    return pressure_mBar / 33.8639;
                case eAirPressureUnits.KiloPascals:
                    return pressure_mBar / 10.0;
                case eAirPressureUnits.MllimetersOfMercury:
                    return pressure_mBar / 1.3332239;
                case eAirPressureUnits.MilliBars:
                default:
                    return pressure_mBar;
            }
        }

        public IEnumerable<eAirPressureUnits> GetEnum()
        {
            return Enum.GetValues(typeof(eAirPressureUnits)).Cast<eAirPressureUnits>();
        }
    }

    public class RelativeHumidityUnits : IUnitBase<eRelativeHumidity>
    {
        public const String UNITS_RH = "%RH";

        public eRelativeHumidity Units { get; set; } = eRelativeHumidity.Percent;
        public string Desc
        {
            get { return UNITS_RH; }
        }

        public Scale Scale
        {
            get { return new Scale(Convert(5), Convert(95)); }
        }

        public void Reset()
        {
            Units = eRelativeHumidity.Percent;
        }

        public double Convert(double hum)
        {
            switch (Units)
            {
                case eRelativeHumidity.Percent:
                default:
                    return hum;
            }
        }

        public IEnumerable<eRelativeHumidity> GetEnum()
        {
            return Enum.GetValues(typeof(eRelativeHumidity)).Cast<eRelativeHumidity>();
        }
    }

    public class UnitsDescriptor
    {
        public TemperatureUnits TemperatureUnits { get; set; } = new TemperatureUnits();

        public AirPressureUnits AirPressureUnits { get; set; } = new AirPressureUnits();

        public RelativeHumidityUnits RelativeHumidityUnits { get; set; } = new RelativeHumidityUnits();

        public static UnitsDescriptor DefaultUnits { get; } = new UnitsDescriptor();

        public UnitsDescriptor()
        {
            Reset();
        }

        public UnitsDescriptor(UnitsDescriptor units)
        {
            this.TemperatureUnits.Units = units.TemperatureUnits.Units;
            this.AirPressureUnits.Units = units.AirPressureUnits.Units;
            this.RelativeHumidityUnits.Units = units.RelativeHumidityUnits.Units;
        }

        public void Reset()
        {
            this.TemperatureUnits.Reset();
            this.AirPressureUnits.Reset();
            this.RelativeHumidityUnits.Reset();
        }
    }
}
