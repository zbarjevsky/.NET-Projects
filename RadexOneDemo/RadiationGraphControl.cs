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

        public void EnableRedraw(bool bEnable)
        {
            if (bEnable)
                m_chart1.EndInit();
            else
                m_chart1.BeginInit();
        }

        public void ClearChart()
        {
            _firstPoint = null;
            ChartHelper.ClearChart(m_chart1);
        }

        public void AddPointXY(ChartPoint pt, double threshold, TimeSpan interval, bool resetAutoValues)
        {
            if (_firstPoint == null)
                _firstPoint = pt;


            ChartHelper.AddPointXY(m_chart1, "SeriesCPM", pt.CPM, pt.date, interval, resetAutoValues);
            ChartHelper.AddPointXY(m_chart1, "SeriesDOSE", pt.RATE, pt.date, interval, resetAutoValues);
            ChartHelper.AddPointXY(m_chart1, "SeriesThreshold", threshold, pt.date, interval, resetAutoValues);

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

        private void m_chkRate_CheckedChanged(object sender, EventArgs e)
        {
            m_chart1.Series[0].Enabled = m_chkRate.Checked;
            m_chart1.ResetAutoValues();
            m_chart1.Refresh();
        }

        private void m_chkCPM_CheckedChanged(object sender, EventArgs e)
        {
            m_chart1.Series[1].Enabled = m_chkCPM.Checked;
            m_chart1.ResetAutoValues();
            m_chart1.Refresh();
        }

        private void m_chkAlert_CheckedChanged(object sender, EventArgs e)
        {
            m_chart1.Series[2].Enabled = m_chkAlert.Checked;
            m_chart1.ResetAutoValues();
            m_chart1.Refresh();
        }
    }
}
