using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RadexOneDemo
{
    public partial class RadiationGraphControl : UserControl
    {
        private ChartPoint _firstPoint = null;

        public RadiationGraphControl()
        {
            InitializeComponent();
        }

        private void RadiationGraphControl_Load(object sender, EventArgs e)
        {
            m_chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
        }

        public void ClearChart()
        {
            _firstPoint = null;
            ChartHelper.ClearChart(m_chart1);
        }

        public void AddPointXY(ChartPoint pt, double threshold, int maxCount)
        {
            if (_firstPoint == null)
                _firstPoint = pt;


            ChartHelper.AddPointXY(m_chart1, "SeriesCPM", pt.CPM, pt.date, maxCount);
            ChartHelper.AddPointXY(m_chart1, "SeriesDOSE", pt.RATE, pt.date, maxCount);
            ChartHelper.AddPointXY(m_chart1, "SeriesThreshold", threshold, pt.date, maxCount);

            double minutes = (pt.date - _firstPoint.date).TotalMinutes;
            if (minutes < 15)
            {
                m_chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            }
            else if (minutes >= 15 && minutes < 24 * 60)
            {
                m_chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
            }
            else
            {
                m_chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd, HH:mm";
            }
        }

        internal void UpdateThreshold(double threshold)
        {
            foreach (DataPoint pt in m_chart1.Series["SeriesThreshold"].Points)
            {
                pt.YValues[0] = threshold;
            }
            m_chart1.ResetAutoValues();
            m_chart1.Refresh();
        }
    }
}
