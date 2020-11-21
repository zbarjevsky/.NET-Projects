using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BarometerBT.Utils;

namespace BarometerBT.BlueMaestro.UX
{
    public class BMDeviceRecordVM : NotifyPropertyChangedImpl
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _interval;
        public string Interval
        {
            get { return _interval; }
            set { SetProperty(ref _interval, value); }
        }

        private string _battery;
        public string Battery
        {
            get { return _battery; }
            set { SetProperty(ref _battery, value); }
        }

        private string _logsCount;
        public string LogsCount
        {
            get { return _logsCount; }
            set { SetProperty(ref _logsCount, value); }
        }

        private string _signal;
        public string Signal
        {
            get { return _signal; }
            set { SetProperty(ref _signal, value); }
        }
        public MeasurementVM Temperature { get; } = new MeasurementVM("Temperature");
        public MeasurementVM AirHumidity { get; } = new MeasurementVM("Humidity");
        public MeasurementVM AirPressure { get; } = new MeasurementVM("Pressure");
        public MeasurementVM AirDewPoint { get; } = new MeasurementVM("Dew Point");

        public BMDeviceRecordVM(BMDatabase db)
        {
            Update(db);
        }

        public void Update(BMDatabase db)
        {
            BMRecordCurrent r = new BMRecordCurrent();
            if(db.Records.Count>0)
                r = db.Records.Last();

            Name = db.Device.Name;
            Interval = "Int: " + r.LoggingInterval.ToString();
            Signal = "Sig: " + r.RSSI;
            LogsCount = "Logs: " + db.Records.Count;
            Battery = "Bat: " + r.BatteryLevel + "%";

            Temperature.Value = db.Units.ConvertTemperature(r.Temperature).ToString("0.0");
            Temperature.Units = db.Units.GetTemperatureUnitsDesc();

            AirHumidity.Value = (r.AirHumidity).ToString("0.0");
            AirHumidity.Units = UnitsDescriptor.UNITS_HUM;

            AirPressure.Value = db.Units.ConvertPressure(r.AirPressure).ToString("0.0");
            AirPressure.Units = db.Units.GetAirpressureUnitsDesc();

            AirDewPoint.Value = db.Units.ConvertTemperature(r.Temperature).ToString("0.0");
            AirDewPoint.Units = db.Units.GetTemperatureUnitsDesc();
        }
    }
}
