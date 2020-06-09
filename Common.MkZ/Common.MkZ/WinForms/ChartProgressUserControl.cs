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

namespace WindowsFormsApp1
{
    public partial class ChartProgressUserControl : UserControl
    {
        public ChartProgressUserControl()
        {
            InitializeComponent();
        }

        private void ChartProgressUserControl_Load(object sender, EventArgs e)
        {
            chart1.Titles.Add("Progress Bar");

            const int Max = 1000;

            //remove all margins
            chart1.ChartAreas[0].Position.X = 0;
            chart1.ChartAreas[0].Position.Width = 100;
            chart1.ChartAreas[0].Position.Height = 100;
            chart1.ChartAreas[0].Position.Y = 0;

            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Max / 10;
            //remove labels
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisX.Maximum = Max;

            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 20;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = 0;

            ////background line
            //StripLine stripline = new StripLine();
            //stripline.Interval = 0;
            //stripline.IntervalOffset = 35; // average value of the y axis; eg: 35
            //stripline.StripWidth = 1;
            //stripline.BackColor = Color.Navy;
            //chart1.ChartAreas[0].AxisY.StripLines.Add(stripline);

            //foreground line
            HorizontalLineAnnotation ann = new HorizontalLineAnnotation();
            ann.AxisX = chart1.ChartAreas[0].AxisX;
            ann.AxisY = chart1.ChartAreas[0].AxisY;
            ann.IsSizeAlwaysRelative = false;
            ann.AnchorY = 44;
            ann.IsInfinitive = true;
            ann.ClipToChartArea = chart1.ChartAreas[0].Name; 
            ann.LineColor = Color.Navy; 
            ann.LineWidth = 2;
            chart1.Annotations.Add(ann);

            //annotation text
            RectangleAnnotation annotation = new RectangleAnnotation();
            //annotation.AnchorDataPoint = chart1.Series[1].Points[3];
            annotation.Y = 40; // percent from top
            annotation.X = 70; // percent from left
            annotation.ShadowColor = Color.Pink;
            annotation.Text = "Speed: 946 Kb/s";
            annotation.ForeColor = Color.Black;
            annotation.BackColor = Color.Transparent;
            annotation.Font = new Font("Arial", 10, FontStyle.Bold);
            annotation.LineWidth = 0;
            chart1.Annotations.Add(annotation);

            foreach (var ser in chart1.Series)
            {
                ser.IsVisibleInLegend = false;
                ser.Color = Color.FromArgb(192, ser.Color); //semitransparent
            }

            Random r = new Random();

            for (int i = 0; i < Max; i++)
            {
                if (i < 630)
                {
                    chart1.Series[0].Points.AddXY(i, 100);
                    chart1.Series[1].Points.AddXY(i, 43);
                }
                else
                {
                    chart1.Series[0].Points.AddXY(i, 0);
                    chart1.Series[1].Points.AddXY(i, 0);
                }
            }
        }
    }
}
