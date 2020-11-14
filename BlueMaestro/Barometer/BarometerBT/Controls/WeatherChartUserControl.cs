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

        }

        public void UpdateChart1(List<BlueMaestroRecord> weatherRecords)
        {
            UpdateChart(weatherRecords, "Temperature", Color.Red, " º",
                (record) => { return record.currTemperature; },
                -100, 100);
        }

        public void UpdateChart2(List<BlueMaestroRecord> weatherRecords)
        {
            UpdateChart(weatherRecords, "Humidity", Color.Green, " %",
                (record) => { return record.currHumidity; },
                0, 100);
        }

        public void UpdateChart3(List<BlueMaestroRecord> weatherRecords)
        {
            UpdateChart(weatherRecords, "Air Pressure", Color.Blue, " mBar",
                (record) => { return record.currPressure; },
                0, 1200);
        }

        public void UpdateChart(List<BlueMaestroRecord> weatherRecords, 
            string title, Color color, string suffix, 
            Func<BlueMaestroRecord, double> GetValue,
            double max, double min)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Color = color;
            chart1.Series[0].Name = title;

            foreach (BlueMaestroRecord record in weatherRecords)
            {
                double val = GetValue(record);
                max = Math.Max(max, val);
                min = Math.Min(min, val);

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
