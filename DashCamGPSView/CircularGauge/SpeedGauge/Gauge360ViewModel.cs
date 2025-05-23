﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedGauge
{
    public class Gauge360ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if(PropertyChanged!= null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public Gauge360ViewModel()
        {
            Angle = -85;
            Value = 0;
        }

        int _angle;
        public int Angle
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

        int _value;
        public int Value
        {
            get
            {
                return _value;
            }

            set
            {
                if (value >= 0 && value <= 360)
                {
                    _value = value;
                    Angle = value - 180;
                    NotifyPropertyChanged("Value");
                }
            }
        }
    }
}
