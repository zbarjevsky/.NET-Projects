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
using System.Windows.Forms.DataVisualization.Charting;

namespace BarometerBT.Controls
{
    public partial class WeatherChartUserControl : UserControl
    {
        public class ChartPoint
        {
            public DateTime Date;
            public double Value;

            public ChartPoint(DateTime date, double val)
            {
                Date = date;
                Value = val;
            }

            public ChartPoint(ChartPoint pt)
            {
                Date = pt.Date;
                Value = pt.Value;
            }

            public static ChartPoint operator+(ChartPoint point1, ChartPoint point2)
            {
                //date in the middle
                TimeSpan ts = point1.Date - point2.Date;
                TimeSpan ts1 = TimeSpan.FromMilliseconds(ts.TotalMilliseconds / 2);

                return new ChartPoint(point1.Date - ts1, point1.Value + point2.Value);
            }

            public override string ToString()
            {
                return string.Format("{0:0.0} -- {1}", Value, Date.ToString("g"));
            }
        }

        private List<ChartPoint> _bufferFull = new List<ChartPoint>();
        private List<ChartPoint> _bufferFilter = new List<ChartPoint>();

        public class Theme
        {
            public Color color = Color.Black;
            public string title = "Loading...";
            public string units = " ºC";
        }

        private Theme _theme = new Theme();

        public WeatherChartUserControl()
        {
            InitializeComponent();
        }

        private void WeatherChartUserControl_Load(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Color = Color.Black;
            chart1.Series[0].Name = "Loading...";

            var chartArea = chart1.ChartAreas[0];

            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.ScrollBar.IsPositionedInside = true;

            // enable autoscroll
            chartArea.CursorX.AutoScroll = true;
            chartArea.CursorX.Interval = 0;
            chartArea.CursorX.IntervalOffset = 0;

            // let's zoom to [0,blockSize] (e.g. [0,100])
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.ScaleView.SizeType = DateTimeIntervalType.Number;
        }

        private void UpdateChartScroll()
        {
            var chartArea1 = chart1.ChartAreas[0];
            var points = chart1.Series[0].Points;

            // set view range to [0,max]
            chartArea1.AxisX.Minimum = points.First().XValue;
            chartArea1.AxisX.Maximum = points.Last().XValue;

            double size1 = chartArea1.AxisX.Maximum - chartArea1.AxisX.Minimum;
            double chunkSize = 30 * size1 / points.Count;
            chartArea1.AxisX.ScaleView.Zoom(chartArea1.AxisX.Maximum - chunkSize, chartArea1.AxisX.Maximum);
            var size = chartArea1.AxisX.ScaleView.Size;

            // set scrollbar small change to blockSize (e.g. 100)
            chartArea1.AxisX.ScaleView.SmallScrollSize = size1 / points.Count;
            chartArea1.AxisX.ScaleView.SmallScrollMinSize = 0;
            chartArea1.CursorX.SetCursorPosition(chartArea1.AxisX.Maximum);

            if (points.Count > 2 && (size1 > size))
            {
                chartArea1.AxisX.ScrollBar.Enabled = true;
            }
            else
            {
                chartArea1.AxisX.ScrollBar.Enabled = false;
            }
        }

        public void UpdateChartTemperature(BMDatabase weatherDB)
        {
            UpdateChart(weatherDB, "Temperature", Color.Red, " ºC",
                (record) => { return record.currTemperature; });
        }

        public void UpdateChartHumidity(BMDatabase weatherDB)
        {
            UpdateChart(weatherDB, "Humidity", Color.Green, " % RH",
                (record) => { return record.currHumidity; });
        }

        public void UpdateChartAirPressure(BMDatabase weatherDB)
        {
            UpdateChart(weatherDB, "Air Pressure", Color.Blue, " mBar",
                (record) => { return record.currPressure; });
        }

        public void UpdateChart(BMDatabase weatherDB, 
            string title, Color color, string suffix, 
            Func<BMRecordCurrent, double> GetValue)
        {
            _theme.color = color;
            _theme.title = title;
            _theme.units = suffix;

            _bufferFull.Clear();

            chart1.Series[0].Points.Clear();
            chart1.Series[0].Color = color;
            chart1.Series[0].Name = title;

            if (weatherDB == null || weatherDB.Records.Count == 0)
                return;

            for (int i = 0; i < weatherDB.Records.Count; i++)
            {
                BMRecordCurrent record = weatherDB.Records[i];
                double val = GetValue(record);
                _bufferFull.Add(new ChartPoint(record.Date, val));
            }

            UpdateChart();
        }

        private void m_trackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            if (_inUpdate)
                return;
            //UpdateChart();
        }

        private void m_hScrollBarTimeLine_Scroll(object sender, ScrollEventArgs e)
        {
            if (_inUpdate)
                return;
            //UpdateChart();
        }

        private bool _inUpdate = false;
        private void UpdateChart()
        {
            if (_inUpdate)
                return;
            _inUpdate = true;

            //int zoom = (int)Math.Pow(2, m_trackBarZoom.Value);
            //m_lblZoom.Text = string.Format("Zoom Out({0})", zoom);

            //List<ChartPoint> buffer = ChartHelper.CompactHistoryByPoints(_bufferFull, zoom);
            //UpdateScrollBar(buffer, m_chkAutoScroll.Checked);

            ////get buffer that corresponds to specific scroll
            //buffer = ChartHelper.GetSubBuffer(buffer, m_hScrollBarTimeLine.Value, m_hScrollBarTimeLine.Width);
            
            InternalSet(_bufferFull, _theme);

            _inUpdate = false;
        }

        private void InternalSet(List<ChartPoint> points, Theme theme)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Color = theme.color;
            chart1.Series[0].Name = theme.title;

            if (points == null || points.Count == 0)
                return;

            EnableRedraw(false);

            double max = points[0].Value, min = points[0].Value;

            for (int i = 0; i < points.Count; i++)
            {
                chart1.Series[0].Points.AddXY(points[i].Date, points[i].Value);
                m_txtValue.Text = points[i].Value.ToString("0.0") + theme.units;

                max = Math.Max(max, points[i].Value);
                min = Math.Min(min, points[i].Value);
            }

            double margin = 1 + (max - min) / 5.0;

            chart1.ChartAreas[0].AxisY.Minimum = min - margin;
            chart1.ChartAreas[0].AxisY.Maximum = max + margin;
            chart1.ChartAreas[0].RecalculateAxesScale();

            UpdateChartScroll();
            UpdateTimeLabelsFormat(points);

            EnableRedraw(true);
        }

        private void UpdateTimeLabelsFormat(List<ChartPoint> points)
        {
            if (points.Count == 0)
                return;

            DateTime first = points[0].Date;
            DateTime last = points.Last().Date;
            TimeSpan ts = (last - first);
            if (ts.TotalMinutes < 3)
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            }
            else if (first.DayOfYear == last.DayOfYear)//same day
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd\n HH:mm:ss";
            }
            else if (ts.TotalDays < 3)//different day
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd\n HH:mm:ss";
            }
            else
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd\n HH:mm:ss";
            }
        }

        //private bool _inUpdate = false;
        //private void Update(bool scrollToLastBuffer, bool resetAutoValues)
        //{
        //    if (_inUpdate)
        //        return;
        //    _inUpdate = true;

        //    UpdateScrollBar(scrollToLastBuffer);
        //    UpdateChart();

        //    if (resetAutoValues)
        //        chart1.ResetAutoValues();

        //    _inUpdate = false;
        //}

        //private void UpdateScrollBar(List<ChartPoint> buffer, bool scrollToLastBuffer)
        //{
        //    int width = m_hScrollBarTimeLine.Width;
        //    if (buffer.Count < width)
        //    {
        //        m_hScrollBarTimeLine.Enabled = false;
        //    }
        //    else
        //    {
        //        int oldMax = m_hScrollBarTimeLine.Maximum;
        //        int oldPos = m_hScrollBarTimeLine.Value;

        //        //scroll bar calculations
        //        m_hScrollBarTimeLine.Maximum = buffer.Count;
        //        m_hScrollBarTimeLine.LargeChange = width;
        //        m_hScrollBarTimeLine.Enabled = true;

        //        if (scrollToLastBuffer)
        //        {
        //            int start = buffer.Count > width ? buffer.Count - width : 0;
        //            m_hScrollBarTimeLine.Value = start;
        //        }
        //        else
        //        {
        //            int newPos = (int)(m_hScrollBarTimeLine.Maximum * (oldPos / (double)oldMax));
        //            m_hScrollBarTimeLine.Value = newPos; //restore pos
        //        }
        //    }
        //}

        private void EnableRedraw(bool bEnable)
        {
            if (bEnable)
                chart1.EndInit();
            else
                chart1.BeginInit();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            Point pos = chart1.PointToClient(MousePosition);
            ShowToolTipWithValue(pos);
        }

        Point? _prevPosition = null;
        ToolTip _tooltip = new ToolTip();

        private void ShowToolTipWithValue(Point pos)
        {
            if (_prevPosition.HasValue && pos == _prevPosition.Value)
                return;

            _tooltip.RemoveAll();
            _prevPosition = pos;
            HitTestResult[] results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            HitTestResult prop = ChartHelper.FindClosestResult(results, pos);
            if (prop != null)
            {
                DataPoint point = prop.Object as DataPoint;
                DateTime dt = DateTime.FromOADate(point.XValue);
                string txt = string.Format("({0}{1}) - {2} - {3}",
                    point.YValues[0],
                    _theme.units,
                    prop.Series.Name,
                    dt.ToString("MMM, dd HH:mm:ss")
                    );
                _tooltip.Show(txt, this.chart1, pos.X, pos.Y + 20, 5000);
            }
            else
            {
                _tooltip.Show("No Value Here :(", this.chart1, pos.X, pos.Y + 20, 2000);
            }
        }
    }

    public static class ChartHelper
    {
        //http://stackoverflow.com/questions/3458791/ms-chart-control-two-y-axis

        public static void AddPointXY(Chart c, int series, double valueY, DateTime time)
        {
            c.Series[series].Points.AddXY(time, valueY);
            RemovePreviousIdenticalPoints(c.Series[series].Points);
        }

        private static void RemovePreviousIdenticalPoints(DataPointCollection points)
        {
            int count = points.Count;
            if (count < 10)
                return;

            DataPoint pt1 = points[count - 1];
            DataPoint pt2 = points[count - 2];
            DataPoint pt3 = points[count - 3];

            if (pt1.YValues[0] == pt2.YValues[0] && pt2.YValues[0] == pt3.YValues[0])
                points.RemoveAt(count - 2);
        }

        public static void ClearChart(Chart c)
        {
            foreach (var s in c.Series)
            {
                s.Points.Clear();
            }
            c.ResetAutoValues();
        }

        public static HitTestResult FindClosestResult(HitTestResult[] results, Point pos, double maxDistance = 10.0)
        {
            double min = 10000;
            HitTestResult data = null;
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
                        if (distance < min)
                        {
                            min = distance;
                            data = result;
                        }
                    }
                }
            }

            if (min <= maxDistance)
                return data;
            return null;
        }

        private static double Length(double X, double Y)
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public static List<WeatherChartUserControl.ChartPoint> GetSubBuffer(List<WeatherChartUserControl.ChartPoint> history, int startIdx, int count)
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

            List<WeatherChartUserControl.ChartPoint> buffer = new List<WeatherChartUserControl.ChartPoint>(count);

            for (int i = startIdx; i < (startIdx + count); i++)
            {
                buffer.Add(history[i]);
            }

            return buffer;
        }

        //Dillute - create one point per minute
        public static List<WeatherChartUserControl.ChartPoint> CompactHistoryByPoints(List<WeatherChartUserControl.ChartPoint> input, int divider)
        {
            int maxPoints = (int)(input.Count / (double)divider);
            if (input.Count <= maxPoints)
                return input;

            List<WeatherChartUserControl.ChartPoint> history = new List<WeatherChartUserControl.ChartPoint>(maxPoints);

            List<WeatherChartUserControl.ChartPoint> bucket = new List<WeatherChartUserControl.ChartPoint>();

            for (int i = 0; i < input.Count; i++)
            {
                if (bucket.Count >= divider)
                {
                    if (bucket.Count > 0)
                    {
                        history.Add(Average_Point(bucket));
                        bucket.Clear();
                    }
                }

                bucket.Add(input[i]);
            }

            if (bucket.Count > 0) //last one
                history.Add(Average_Point(bucket));

            return history;
        }

        private static WeatherChartUserControl.ChartPoint Average_Point(List<WeatherChartUserControl.ChartPoint> bucket)
        {
            if (bucket == null || bucket.Count == 0)
                return null;

            WeatherChartUserControl.ChartPoint pt = new WeatherChartUserControl.ChartPoint(bucket.First());
            for (int i = 1; i < bucket.Count; i++)
            {
                pt += bucket[i];
            }
            pt.Value /= bucket.Count;

            return pt;
        }
    }
}
