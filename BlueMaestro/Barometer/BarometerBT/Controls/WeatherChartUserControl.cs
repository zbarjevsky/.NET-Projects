using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BarometerBT.BlueMaestro;

namespace BarometerBT.Controls
{
    public partial class WeatherChartUserControl : UserControl
    {
        public WeatherChartUserControl()
        {
            InitializeComponent();
        }

        private void WeatherChartUserControl_Load(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Color = Color.Black;
            chart1.Series[0].Name = "Please Wait...";
        }

        public void UpdateChart1(BMDatabase weatherDB)
        {
            UpdateChart(weatherDB, "Temperature", Color.Red, " º",
                (record) => { return record.currTemperature; },
                weatherDB._max.currTemperature, weatherDB._min.currTemperature);
        }

        public void UpdateChart2(BMDatabase weatherDB)
        {
            UpdateChart(weatherDB, "Humidity", Color.Green, " %",
                (record) => { return record.currHumidity; },
                weatherDB._max.currHumidity, weatherDB._min.currHumidity);
        }

        public void UpdateChart3(BMDatabase weatherDB)
        {
            UpdateChart(weatherDB, "Air Pressure", Color.Blue, " mBar",
                (record) => { return record.currPressure; },
                weatherDB._max.currPressure, weatherDB._min.currPressure);
        }

        public void UpdateChart(BMDatabase weatherDB, 
            string title, Color color, string suffix, 
            Func<BMRecordCurrent, double> GetValue,
            double max, double min)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Color = color;
            chart1.Series[0].Name = title;

            if (weatherDB == null || weatherDB.Records.Count == 0)
                return;

            foreach (BMRecordCurrent record in weatherDB.Records)
            {
                double val = GetValue(record);

                chart1.Series[0].Points.AddXY(record.Date, val);
                m_txtValue.Text = val.ToString("0.0") + suffix;
            }

            double margin = 1 + (max - min) / 5.0;

            chart1.ChartAreas[0].AxisY.Minimum = min - margin;
            chart1.ChartAreas[0].AxisY.Maximum = max + margin;
            chart1.ChartAreas[0].RecalculateAxesScale();
        }
    }
}
