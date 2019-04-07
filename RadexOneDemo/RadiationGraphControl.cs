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
        private List<ChartPoint> _history = new List<ChartPoint>(); //initial buffer

        public List<ChartPoint> History { get { return _history; } }

        public string Series3LegendText
        {
            get { return m_chart1.Series[2].LegendText; }
            set { m_chart1.Series[2].LegendText = value; }
        }

        public Color Series3Color
        {
            get { return m_chart1.Series[2].Color; }
            set { m_chart1.Series[2].Color = value; }
        }

        public int ScrollPosition
        {
            get { return m_hScrollBarZoom.Value; }
            set { m_hScrollBarZoom.Value = value; }
        }

        public int GraphWidth {  get { return m_hScrollBarZoom.Width; } }

        public RadiationGraphControl()
        {
            InitializeComponent();
        }

        private void RadiationGraphControl_Load(object sender, EventArgs e)
        {
            m_chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
        }

        private void EnableRedraw(bool bEnable)
        {
            if (bEnable)
                m_chart1.EndInit();
            else
                m_chart1.BeginInit();
        }

        public void ClearChart()
        {
            _history.Clear();
            RadiationGraphControlChartHelper.ClearChart(m_chart1);
        }

        public void Set(List<ChartPoint> points, bool scrollToLastBuffer, bool resetAutoValues)
        {
            _history = new List<ChartPoint>(points); //copy
            Update(scrollToLastBuffer, resetAutoValues);
        }

        public void AddPointXY(ChartPoint pt, bool resetAutoValues)
        {
            _history.Add(pt);
            Update(true, resetAutoValues);
        }

        private void UpdateScrollBar(bool scrollToLastBuffer)
        {
            int width = m_hScrollBarZoom.Width;
            if(_history.Count < width)
            {
                m_hScrollBarZoom.Enabled = false;
            }
            else
            {
                int oldMax = m_hScrollBarZoom.Maximum;
                int oldPos = m_hScrollBarZoom.Value;

                //scroll bar calculations
                m_hScrollBarZoom.Maximum = _history.Count;
                m_hScrollBarZoom.LargeChange = width;
                m_hScrollBarZoom.Enabled = true;

                if (scrollToLastBuffer)
                {
                    int start = _history.Count > width ? _history.Count - width : 0;
                    m_hScrollBarZoom.Value = start; 
                }
                else
                {
                    int newPos = (int)(m_hScrollBarZoom.Maximum * oldPos / (double)oldMax);
                    m_hScrollBarZoom.Value = newPos; //restore pos
                }
            }
        }

        private void UpdateTimeLabelsFormat()
        {
            DateTime first = _history[0].date;
            DateTime last = _history.Last().date;
            double minutes = (last - first).TotalMinutes;
            if (minutes < 3)
            {
                m_chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            }
            else if (first.DayOfYear == last.DayOfYear)//same day
            {
                m_chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";
            }
            else //different day
            {
                m_chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd, HH:mm";
            }
        }

        private bool _inUpdate = false;
        private void Update(bool scrollToLastBuffer, bool resetAutoValues)
        {
            if (_inUpdate)
                return;
            _inUpdate = true;

            UpdateTimeLabelsFormat();
            UpdateScrollBar(scrollToLastBuffer);
            UpdateChart();

            if (resetAutoValues)
                m_chart1.ResetAutoValues();

            _inUpdate = false;
        }

        private void UpdateChart()
        {
            List<ChartPoint> buffer = GetSubBuffer(_history, m_hScrollBarZoom.Value, m_hScrollBarZoom.Width);
            InternalSet(buffer);
        }

        private void InternalSet(List<ChartPoint> points)
        {
            EnableRedraw(false);

            RadiationGraphControlChartHelper.ClearChart(m_chart1);
            foreach (ChartPoint pt in points)
            {
                RadiationGraphControlChartHelper.AddPointXY(m_chart1, 0, pt.RATE, pt.date);
                RadiationGraphControlChartHelper.AddPointXY(m_chart1, 1, pt.CPM, pt.date);
                RadiationGraphControlChartHelper.AddPointXY(m_chart1, 2, pt.Threshold, pt.date);
            }

            EnableRedraw(true);
        }

        private List<ChartPoint> GetSubBuffer(List<ChartPoint> history, int startIdx, int count)
        {
            if (count > history.Count)
            {
                count = history.Count;
                startIdx = 0;
            }
            else if (startIdx + count > history.Count)
            {
                startIdx = history.Count - count;
            }

            List<ChartPoint> buffer = new List<ChartPoint>(count);

            for (int i = startIdx; i < (startIdx + count); i++)
            {
                buffer.Add(history[i]);
            }

            return buffer;
        }

        internal void UpdateThreshold(double threshold)
        {
            foreach (DataPoint pt in m_chart1.Series[2].Points)
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

        private void m_hScrollBarZoom_Scroll(object sender, ScrollEventArgs e)
        {
            if (_inUpdate)
                return;
            UpdateChart();
        }

        private void m_chart1_Click(object sender, EventArgs e)
        {
            Point pos = m_chart1.PointToClient(MousePosition);
            ShowToolTipWithValue(pos);
        }

        private void m_chart1_MouseMove(object sender, MouseEventArgs e)
        {
            //ShowToolTipWithValue(e.Location);
        }

        Point? _prevPosition = null;
        ToolTip _tooltip = new ToolTip();

        private void ShowToolTipWithValue(Point pos)
        {
            if (_prevPosition.HasValue && pos == _prevPosition.Value)
                return;

            _tooltip.RemoveAll();
            _prevPosition = pos;
            HitTestResult [] results = m_chart1.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            DataPoint prop = FindClosestPoint(results, pos);
            if(prop != null)
            { 
                DateTime dt = DateTime.FromOADate(prop.XValue);
                string txt = string.Format("{0} - {1}: {2}", 
                    dt.ToString("MMM, dd HH:mm:ss"), 
                    prop.LegendText,
                    prop.YValues[0]);
                _tooltip.Show(txt, this.m_chart1, pos.X, pos.Y + 20, 5000);
            }
        }

        private DataPoint FindClosestPoint(HitTestResult[] results, Point pos, double maxDistance = 10.0)
        {
            double min = 10000;
            DataPoint data = null;
            foreach (HitTestResult result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    DataPoint prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);
                        if (result.Series.YAxisType == AxisType.Secondary)
                            pointYPixel = result.ChartArea.AxisY2.ValueToPixelPosition(prop.YValues[0]);

                        double distance = Length(pos.X - pointXPixel, pos.Y - pointYPixel);
                        if(distance < min)
                        {
                            min = distance;
                            data = prop;
                        }
                    }
                }
            }

            if(min<=maxDistance)
                return data;
            return null;
        }

        private double Length(double X, double Y)
        {
            return Math.Sqrt(X * X + Y * Y);
        }
    }
}
