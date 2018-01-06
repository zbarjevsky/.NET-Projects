using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace RadexOneDemo
{
    public class ChartHelper
    {
        private static DateTime _start = DateTime.Now;

        //http://stackoverflow.com/questions/3458791/ms-chart-control-two-y-axis
        //chrtMain.Series[0].YAxisType = AxisType.Primary;
        //chrtMain.Series[1].YAxisType = AxisType.Secondary;

        //chrtMain.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
        //chrtMain.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
        //chrtMain.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
        //chrtMain.ChartAreas[0].AxisY2.IsStartedFromZero = chrtMain.ChartAreas[0].AxisY.IsStartedFromZero;

        public static void AddPoint(Chart c, string series, double valueY)
        {
            //double timeSeconds = (DateTime.Now - _start).TotalSeconds;

            c.Series[series].Points.AddXY(DateTime.Now, valueY);
            //if (timeSeconds > 100.0)
            {
                //double min = timeSeconds - 100;
                while(/*c.Series[series].Points[0].XValue < min || */c.Series[series].Points.Count > 1000)
                    c.Series[series].Points.RemoveAt(0);
                c.ResetAutoValues();
            }

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
