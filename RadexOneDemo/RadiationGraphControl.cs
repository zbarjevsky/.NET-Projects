using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadexOneDemo
{
    public partial class RadiationGraphControl : UserControl
    {
        public RadiationGraphControl()
        {
            InitializeComponent();
        }

        private void RadiationGraphControl_Load(object sender, EventArgs e)
        {

        }

        public void ClearChart()
        {
            ChartHelper.ClearChart(m_chart1);
        }

        public void AddPointXY(ChartPoint pt, double threshold)
        {
            ChartHelper.AddPointXY(m_chart1, "SeriesCPM", pt.CPM, pt.date);
            ChartHelper.AddPointXY(m_chart1, "SeriesDOSE", pt.RATE, pt.date);
            ChartHelper.AddPointXY(m_chart1, "SeriesThreshold", threshold, pt.date);
        }
    }
}
