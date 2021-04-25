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

namespace WiFiConnect.MkZ.Controls
{
    public partial class PingChartUserControl : UserControl
    {
        private List<PingPoint> _bufferFull = new List<PingPoint>();
        private List<PingPoint> _bufferFilter = new List<PingPoint>();

        public struct MinMax
        {
            public double Min;
            public double Max;

            public MinMax(double min, double max)
            {
                Min = min;
                Max = max;
            }

            public void Update(double val)
            {
                Max = Math.Max(Max, val);
                Min = Math.Min(Min, val);
            }
        }

        private MinMax _scaleAbsolute, _scaleFromPoints;
        HorizontalLineAnnotation _annotationLine = new HorizontalLineAnnotation();

        public class Theme
        {
            public Color color = Color.Black;
            public string title = "Loading...";
            public string units = "ms";
        }

        private Theme _theme = new Theme();

        public PingChartUserControl()
        {
            InitializeComponent();

            _scaleAbsolute = new MinMax(0, 500);
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

            double oneMinute = (DateTime.Now.AddMinutes(100).ToOADate() - DateTime.Now.ToOADate()) / 100.0;
            // set scrollbar small change to blockSize (e.g. 100)
            chartArea.AxisX.ScaleView.SmallScrollSize = oneMinute;
            chartArea.AxisX.ScaleView.SmallScrollMinSize = 0;

            chart1.Annotations.Clear();

            //foreground line
            _annotationLine.AxisX = chart1.ChartAreas[0].AxisX;
            _annotationLine.AxisY = chart1.ChartAreas[0].AxisY;
            _annotationLine.IsSizeAlwaysRelative = false;
            _annotationLine.AnchorY = 2;
            _annotationLine.IsInfinitive = true;
            _annotationLine.ClipToChartArea = chart1.ChartAreas[0].Name;
            _annotationLine.LineColor = Color.Navy;
            _annotationLine.LineWidth = 2;
            chart1.Annotations.Add(_annotationLine);

            UpdateZoomResetButton();
            CheckZoomChanged();
        }

        public void ResetZoom()
        {
            var chartArea1 = chart1.ChartAreas[0];
            chartArea1.AxisX.ScaleView.ZoomReset();
            chartArea1.CursorX.SetCursorPosition(double.NaN);
            UpdateZoomResetButton();
        }

        private void SetZoom()
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

        public void UpdateChart(List<PingPoint> records, 
            string title, Color color, string units)
        {
            _theme.color = Color.FromArgb(128, color);
            _theme.title = title;
            _theme.units = units;

            _bufferFull.Clear();

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            chart1.Series[0].Color = _theme.color;
            chart1.Series[1].Color = Color.Red;

            chart1.Series[0].Name = _theme.title;
   
            _annotationLine.AnchorY = 0;
            _annotationLine.LineColor = _theme.color;

            UpdateZoomResetButton();

            if (records == null || records.Count == 0)
                return;

            _bufferFull.AddRange(records);

            UpdateChart();
        }

        private bool _inUpdate = false;
        private void UpdateChart()
        {
            if (_inUpdate)
                return;
            _inUpdate = true;
            
            InternalSet(_bufferFull, _theme);

            _inUpdate = false;
        }

        private void InternalSet(List<PingPoint> points, Theme theme)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Color = theme.color;
            chart1.Series[0].Name = theme.title;

            if (points == null || points.Count == 0)
                return;

            EnableRedraw(false);

            _scaleFromPoints = new MinMax(points[0].Value, points[0].Value);

            for (int i = 0; i < points.Count; i++)
            {
                chart1.Series[0].Points.AddXY(points[i].Date, points[i].Value);
                chart1.Series[1].Points.AddXY(points[i].Date, points[i].Error);

                _scaleFromPoints.Update(points[i].Value);
            }

            m_txtValue.Text = points.Last().Value.ToString("0.0") + theme.units;

            _annotationLine.AnchorY = points.Last().Value;

            UpdateGraphScale();
            UpdateTimeLabelsFormat();

            EnableRedraw(true);
        
            UpdateZoomResetButton();
            CheckZoomChanged();
        }

        private void UpdateGraphScale()
        {
            MinMax scale = _scaleAbsolute;
            if (m_chkAutoScale.Checked)
            {
                scale = _scaleFromPoints;
                double margin = 0.1 + (scale.Max - scale.Min) / 5.0;
                scale.Min -= margin;
                scale.Max += margin;
            }

            chart1.ChartAreas[0].AxisY.Minimum = scale.Min;
            chart1.ChartAreas[0].AxisY.Maximum = scale.Max;
            chart1.ChartAreas[0].RecalculateAxesScale();
        }

        private void UpdateTimeLabelsFormat()
        {
            if (chart1.Series[0].Points.Count == 0)
                return;

            TimeSpan ts = GetVisibleDateRange(out bool bSameDay);
            if (ts.TotalHours < 3)
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            }
            else if (bSameDay)//same day
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd\n HH:mm:ss";
            }
            else if (ts.TotalDays < 3)//different day
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd\n HH:mm";
            }
            else
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMM dd";
            }
        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            UpdateTimeLabelsFormat();
            UpdateZoomResetButton();
            CheckZoomChanged();
        }

        private TimeSpan GetVisibleDateRange(out bool bSameDay)
        {
            ChartArea chartArea = chart1.ChartAreas[0];
            Series series = chart1.Series[0];

            // these are the X-Values of the zoomed portion:
            double min = chartArea.AxisX.ScaleView.ViewMinimum;
            double max = chartArea.AxisX.ScaleView.ViewMaximum;

            // these are the respective DataPoints:
            DataPoint pt0 = series.Points.Select(x => x)
                             .Where(x => x.XValue >= min)
                             .DefaultIfEmpty(series.Points.First()).First();
            DataPoint pt1 = series.Points.Select(x => x)
                             .Where(x => x.XValue <= max)
                             .DefaultIfEmpty(series.Points.Last()).Last();

            DateTime dt0 = DateTime.FromOADate(pt0.XValue);
            DateTime dt1 = DateTime.FromOADate(pt1.XValue);

            bSameDay = dt0.DayOfYear == dt1.DayOfYear;
            
            return dt1 - dt0;
        }

        private void EnableRedraw(bool bEnable)
        {
            if (bEnable)
                chart1.EndInit();
            else
                chart1.BeginInit();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            if(e is MouseEventArgs m && m.Button == MouseButtons.Left)
            {
                //Point pos = chart1.PointToClient(MousePosition);
                ShowToolTipWithValue(m.Location);
            }
        }

        Point? _prevPosition = null;

        private void ShowToolTipWithValue(Point pos)
        {
            if (_prevPosition.HasValue && pos == _prevPosition.Value)
                return;

            _tooltip.RemoveAll();
            _tooltip.Hide(chart1);
            _prevPosition = pos;

            if (!ChartHelper.PointInsideChart(chart1, pos))
                return;

            string desc = "";
            string txt = "Unknown...";

            try
            {
                PingPoint pt = ChartHelper.FindInterpolateValueY(chart1, out desc, out ToolTipIcon icon);
                _tooltip.ToolTipIcon = icon;

                if (!double.IsNaN(pt.Value) || !double.IsNaN(pt.Error))
                {
                    double d = (pt.Value != 0.0) ? pt.Value : pt.Error;
                    txt = string.Format("[{0:0.0}{1}]\n{2}", d, _theme.units, pt.Date.ToString("MMM dd, HH:mm:ss"));
                }
                else
                {
                    return; //no tooltip
                }
            }
            catch (Exception err)
            {
                txt = err.Message;
            }

            _tooltip.ToolTipTitle = string.Format("{0}{1}", _theme.title, desc);
            _tooltip.Show("", this.chart1, pos.X, pos.Y + 20, 5000); //bug fix
            _tooltip.Show(txt, this.chart1, pos.X + 10, pos.Y + 20, 5000);
        }

        private void m_btnReset_Click(object sender, EventArgs e)
        {
            ResetZoom();
        }

        private bool UpdateZoomResetButton()
        {
            var chartArea1 = chart1.ChartAreas[0];

            bool isZoomed = chartArea1.AxisX.ScaleView.IsZoomed;
            m_btnReset.Enabled = isZoomed;
            m_btnReset.BackColor = isZoomed ? _theme.color : Color.LightGray;

            return isZoomed;
        }

        double oldSelStart = -1;
        double oldSelEnd = -1;
        private bool CheckZoomChanged()
        {
            var chartArea1 = chart1.ChartAreas[0];

            double newSelStart = chartArea1.CursorX.SelectionStart;
            double newSelEnd = chartArea1.CursorX.SelectionEnd;

            const double TOLERANCE = 0.01;

            if (Math.Abs(oldSelEnd - newSelEnd) > TOLERANCE || Math.Abs(newSelStart - oldSelStart) > TOLERANCE)
            {
                oldSelStart = newSelStart;
                oldSelEnd = newSelEnd;

                //Zoom has actually changed 
                return true;
            }

            return false;
        }

        private void m_chkAutoScale_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraphScale();
        }
    }

    public class PingPoint
    {
        public DateTime Date;
        public double Value;
        public double Error;

        public PingPoint(DateTime date, double val = 0, double err = 0)
        {
            Date = date;
            Value = val;
            Error = err;
        }

        public PingPoint(double date, double val = 0, double err = 0)
        {
            Date = DateTime.FromOADate(date);
            Value = val;
            Error = err;
        }

        public PingPoint(DataPoint ptVal, DataPoint ptErr) 
            : this(ptVal.XValue, ptVal.YValues[0], ptErr.YValues[0])
        {
        }

        public PingPoint(PingPoint pt)
        {
            Date = pt.Date;
            Value = pt.Value;
            Error = pt.Error;
        }

        public static PingPoint operator +(PingPoint point1, PingPoint point2)
        {
            //date in the middle
            TimeSpan ts = point1.Date - point2.Date;
            TimeSpan ts1 = TimeSpan.FromMilliseconds(ts.TotalMilliseconds / 2.0);

            return new PingPoint(point1.Date - ts1, (point1.Value + point2.Value)/2.0, Math.Max(point1.Error, point2.Error));
        }

        public override string ToString()
        {
            return string.Format("V:{0:0}, E:{1:0} -- {2}", Value, Error, Date.ToString("g"));
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

        public static bool PointInsideChart(Chart chart, Point point)
        {
            double valX = chart.ChartAreas[0].AxisX.PixelPositionToValue(point.X);
            double minX = chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
            double maxX = chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;

            double valY = chart.ChartAreas[0].AxisY.PixelPositionToValue(point.Y);
            double minY = chart.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
            double maxY = chart.ChartAreas[0].AxisY.ScaleView.ViewMaximum;
           
            if (valX < minX || valX > maxX || valY < minY || valY > maxY)
                return false;
            
            return true;
        }

        //HitTestResult[] results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
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

        public static PingPoint FindInterpolateValueY(Chart chart, out string desc, out ToolTipIcon icon)
        {
            double xval = chart.ChartAreas[0].CursorX.Position;
            DataPointCollection pointsVal = chart.Series[0].Points;
            DataPointCollection pointsErr = chart.Series[1].Points;

            desc = "";
            icon = ToolTipIcon.Info;
            if (pointsVal.Count == 0)
                return new PingPoint(double.NaN, double.NaN, double.NaN);

            if (pointsVal.Count == 1)
                return new PingPoint(pointsVal[0], pointsErr[0]);

            DataPoint ptVal0 = pointsVal[0];
            DataPoint ptVal1 = pointsVal.Last();

            DataPoint ptErr0 = pointsErr[0];
            DataPoint ptErr1 = pointsErr.Last();

            desc = "(out of range)";
            icon = ToolTipIcon.Warning;
            if (xval < ptVal0.XValue || xval > ptVal1.XValue)
                return new PingPoint(double.NaN, double.NaN, double.NaN); //out of range

            double timePerPoint = (ptVal1.XValue - ptVal0.XValue)/chart.Width;
            if(chart.ChartAreas[0].AxisX.ScaleView.IsZoomed)
            {
                double min = chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                double max = chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                timePerPoint = (max - min) / chart.Width;
            }
            double proximityInterval = 10 * timePerPoint;

            ptVal0 = pointsVal.Last(x => x.XValue <= xval);
            ptVal1 = pointsVal.First(x => x.XValue >= xval);

            double deltaX = ptVal1.XValue - ptVal0.XValue;
            double deltaX0 = xval - ptVal0.XValue;
            double deltaX1 = ptVal1.XValue - xval;

            desc = "";
            icon = ToolTipIcon.Info;
            if (deltaX1 < proximityInterval)
                return new PingPoint(ptVal1, ptErr1);

            if (deltaX0 < proximityInterval)
                return new PingPoint(ptVal0, ptErr0);

            //interpolate
            double coefficient = (xval - ptVal0.XValue) / (ptVal1.XValue - ptVal0.XValue);

            desc = "(approximate)";
            icon = ToolTipIcon.Warning;
            double approximateValue = ptVal0.YValues[0] + coefficient * (ptVal1.YValues[0] - ptVal0.YValues[0]);
            return new PingPoint(xval, approximateValue);
        }

        private static double Length(double X, double Y)
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public static List<PingPoint> GetSubBuffer(List<PingPoint> history, int startIdx, int count)
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

            List<PingPoint> buffer = new List<PingPoint>(count);

            for (int i = startIdx; i < (startIdx + count); i++)
            {
                buffer.Add(history[i]);
            }

            return buffer;
        }

        //Dillute - create one point per minute
        public static List<PingPoint> CompactHistoryByPoints(List<PingPoint> input, int divider)
        {
            int maxPoints = (int)(input.Count / (double)divider);
            if (input.Count <= maxPoints)
                return input;

            List<PingPoint> history = new List<PingPoint>(maxPoints);

            List<PingPoint> bucket = new List<PingPoint>();

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

        private static PingPoint Average_Point(List<PingPoint> bucket)
        {
            if (bucket == null || bucket.Count == 0)
                return null;

            PingPoint pt = new PingPoint(bucket.First());
            for (int i = 1; i < bucket.Count; i++)
            {
                pt += bucket[i];
            }
            pt.Value /= bucket.Count;

            return pt;
        }
    }
}
