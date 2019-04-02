using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace RadexOneDemo
{
    public class ChartPoint
    {
        public DateTime date = DateTime.Now;
        public double CPM, RATE, DOSE, Threshold;

        public override string ToString()
        {
            return string.Format("{0} - Rate: {1:0.00} µSv/h, CPM: {2} Dose: {3} Threshold: {4}", 
                date.ToString("s"), RATE, CPM, DOSE, Threshold);
        }
    }

    public class RadiationGraphControlChartHelper
    {
        private static DateTime _start = DateTime.Now;

        //http://stackoverflow.com/questions/3458791/ms-chart-control-two-y-axis
        //chrtMain.Series[0].YAxisType = AxisType.Primary;
        //chrtMain.Series[1].YAxisType = AxisType.Secondary;

        //chrtMain.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
        //chrtMain.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
        //chrtMain.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
        //chrtMain.ChartAreas[0].AxisY2.IsStartedFromZero = chrtMain.ChartAreas[0].AxisY.IsStartedFromZero;

        public static void AddPointXY(Chart c, int series, double valueY, DateTime time)
        {
            c.Series[series].Points.AddXY(time, valueY);

            RemovePreviousIdenticalPoints(c.Series[series].Points);
        }

        public static void AddPointXY(Chart c, int series, double valueY, DateTime time, TimeSpan interval, bool resetAutoValues)
        {
            c.Series[series].Points.AddXY(time, valueY);

            RemovePreviousIdenticalPoints(c.Series[series].Points);

            DateTime startTime = time - interval;
            while (DateTime.FromOADate(c.Series[series].Points.First().XValue) < startTime)
            {
                if (CanRemoveFirst(c.Series[series].Points, startTime, interval))
                {
                    c.Series[series].Points.RemoveAt(0);
                }
                else //move point time to start time
                {
                    c.Series[series].Points[0].XValue = startTime.ToOADate();
                    break;
                }
            }

            if(resetAutoValues)
                c.ResetAutoValues();
        }

        private static bool CanRemoveFirst(DataPointCollection points, DateTime startTime, TimeSpan interval)
        {
            DateTime time = DateTime.FromOADate(points[1].XValue);
            TimeSpan diff = time - startTime;

            //if distance to next point is less than 10% of interval
            return (diff.TotalMilliseconds < interval.TotalMilliseconds / 10);
        }

        //remove identical points in the middle - improve line performance
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
    }
}
