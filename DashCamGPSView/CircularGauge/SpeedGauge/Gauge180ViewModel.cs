using CircularGauge;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SpeedGauge
{
    public class Gauge180ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Gauge180ViewModel()
        {
            Angle = -85;
            SpeedValue = "0";
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
                OnPropertyChanged();
            }
        }

        public double MaxValue { get; set; } = 140;

        private string _value;
        public string SpeedValue
        {
            get
            {
                return _value;
            }

            set
            {
                 _value = value;

                double speed;
                if(double.TryParse(value, out speed))
                {
                    if (speed >= 0 && speed <= MaxValue)
                    {
                        Angle = -90 + speed * 180.0 / MaxValue;
                    }
                }
                else
                {
                    Angle = -85;
                }
                OnPropertyChanged();
            }
        }

        private string _speedUnits = "mph";
        public string SpeedUnits
        {
            get { return _speedUnits; }
            set { _speedUnits = value; OnPropertyChanged(); }
        }

        public SolidColorBrush SpeedBrush
        {
            get { return _speedBrush.Brush; }
            set { _speedBrush = new HighlightBrush(value); OnPropertyChanged(); }
        }

        private HighlightBrush _speedBrush = new HighlightBrush();
        public HighlightBrush.HighlightColor SpeedHighlightBrush
        {
            get { return _speedBrush.eColor; }
            set { _speedBrush = new HighlightBrush(value); OnPropertyChanged(); OnPropertyChanged(nameof(SpeedBrush)); }
        }
    }
}
