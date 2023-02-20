using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MkZ.RadexOneLib
{
    public class RadexOneDeviceInfo : INotifyPropertyChanged
    {
        [Category("1. Version"), ReadOnly(true)]
        public RadexOneSerialNumber SerialNumber { get; set; }

        [Category("2. Data"), ReadOnly(true)]
        [Description("Accumulated DOSE in mRem")]
        public double Dose { get; set; }

        private bool _sound = false;
        [Category("3. Configuration")]
        [Description("On/Off alarm sound")]
        public bool Sound
        {
            get { return _sound; }
            set { if (_sound == value) return; _sound = value; OnPropertyChanged(); }
        }

        private bool _vibrate = false;
        [Category("3. Configuration")]
        [Description("On/Off alarm vibration")]
        public bool Vibrate
        {
            get { return _vibrate; }
            set { if (_vibrate == value) return; _vibrate = value; OnPropertyChanged(); }
        }

        private double _threshold = 0.6;
        [Category("3. Configuration")]
        [Description("Alarm threshold")]
        public double Threshold
        {
            get { return _threshold; }
            set { if (_threshold == value) return; _threshold = value; OnPropertyChanged(); }
        }

        public void SetSettings(CommandGetSettings cmd)
        {
            Sound = cmd.Sound;
            Vibrate = cmd.Vibrate;
            Threshold = cmd.Threshold;
        }

        public void SetVersion(CommandGetVersion cmd)
        {
            SerialNumber = cmd.SerialNumber.Clone();
        }

        public string DoseToString()
        {
            return string.Format("Dose: {0:0.00} µSv/h", Dose);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
