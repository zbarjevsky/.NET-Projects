using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


using BarometerBT.Utils;

namespace BarometerBT.BlueMaestro.UX
{
    public class BMDeviceRecordVM : NotifyPropertyChangedImpl
    {
        private Brush _background = Brushes.White;
        public Brush Background
        {
            get { return _background; }
            set { SetProperty(ref _background, value); }
        }

        private Brush _borderBrush = Brushes.White;
        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set { SetProperty(ref _borderBrush, value); }
        }

        private int _index = -1;
        public int Index //alternate background
        {
            get { return _index; }
            set { SetProperty(ref _index, value); Background = (_index % 2 == 0) ? Brushes.AliceBlue : Brushes.AntiqueWhite; }
        }

        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

        private bool _isSelected = false;
        public bool IsSelected //border color
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); BorderBrush = _isSelected ? Brushes.Navy : Brushes.Gainsboro; }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _address = "";
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _interval = "";
        public string Interval
        {
            get { return _interval; }
            set { SetProperty(ref _interval, value); }
        }

        private string _battery = "0%";
        public string Battery
        {
            get { return _battery; }
            set { SetProperty(ref _battery, value); }
        }

        private string _logsCount = "";
        public string LogsCount
        {
            get { return _logsCount; }
            set { SetProperty(ref _logsCount, value); }
        }

        private string _signal = "";
        public string Signal
        {
            get { return _signal; }
            set { SetProperty(ref _signal, value); }
        }

        public MeasurementVM Temperature { get; } = new MeasurementVM("Temperature");
        public MeasurementVM AirHumidity { get; } = new MeasurementVM("Humidity");
        public MeasurementVM AirPressure { get; } = new MeasurementVM("Air Pressure");
        public MeasurementVM AirDewPoint { get; } = new MeasurementVM("Dew Point");

        public ulong DeviceAddress { get { return Database.Device.Address; } }

        public BMDatabase Database { get; private set; }

        public BMDeviceRecordVM(BMDatabase db)
        {
            Update(db);
        }

        public void Update(BMDatabase db)
        {
            Database = db;

            //alternate background
            //Background = (index%2==0)? Brushes.AliceBlue : Brushes.AntiqueWhite;
            //IsSelected = isSelected;

            BMRecordCurrent r = new BMRecordCurrent();
            if(db.Records.Count>0)
                r = db.Records.Last();

            TimeSpan age = DateTime.Now - r.Date;
            IsActive = age.TotalSeconds < 60;

            Name = db.Device.Name;
            Address = "(" + db.Device.GetAddressString() + ")";
            Interval = "Int: " + r.LoggingInterval.ToString();
            Signal = "Sig: " + r.RSSI;
            LogsCount = "Logs: " + db.Records.Count;
            Battery = "Bat: " + r.BatteryLevel + "%";

            Temperature.Value = db.Units.ConvertTemperature(r.Temperature).ToString("0.0");
            Temperature.Units = db.Units.GetTemperatureUnitsDesc();

            AirHumidity.Value = (r.AirHumidity).ToString("0.0");
            AirHumidity.Units = UnitsDescriptor.UNITS_RH;

            AirPressure.Value = db.Units.ConvertPressure(r.AirPressure).ToString("0.0");
            AirPressure.Units = db.Units.GetAirpressureUnitsDesc();

            AirDewPoint.Value = db.Units.ConvertTemperature(r.Temperature).ToString("0.0");
            AirDewPoint.Units = db.Units.GetTemperatureUnitsDesc();
        }
    }
}
