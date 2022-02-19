using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using MkZWeatherStation.Utils;
using MkZ.Windows;

namespace MkZ.WeatherStation.BlueMaestro.UX
{
    public class MeasurementVM : NotifyPropertyChangedImpl
    {
        private string _value = "";
        public string Value { get { return _value; } set { SetProperty(ref _value, value); } }

        private string _units = "";
        public string Units { get { return _units; } set { SetProperty(ref _units, value); } }

        private string _desc = "";
        public string Desc { get { return _desc; } set { SetProperty(ref _desc, value); } }

        public MeasurementVM(string desc)
        {
            Desc = desc;
        }

        public override string ToString()
        {
            return Desc + " " + Value + " " + Units;
        }
    }
}
