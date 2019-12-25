using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedGauge
{
    public class Gauge180ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if(PropertyChanged!= null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Gauge180ViewModel()
        {
            Angle = -85;
            Value = 0;
        }

        private double _angle;
        public double Angle
        {
            get
            {
                return _angle;
            }

            private set
            {
                _angle = value;
                NotifyPropertyChanged("Angle");
            }
        }

        public double MaxValue { get; set; } = 140;

        int _value;
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value >= 0 && value <= MaxValue)
                {
                    _value = value;
                    Angle = -90 + value * 180.0 / MaxValue;
                    NotifyPropertyChanged("Value");
                }
            }
        }
    }
}
