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
        private List<double> _history = new List<double>();
        RectangleAnnotation _annotationText = new RectangleAnnotation();
        HorizontalLineAnnotation _annotationLine = new HorizontalLineAnnotation();

        public ChartProgressUserControl()
        {
            InitializeComponent();
        }

        private void ChartProgressUserControl_Load(object sender, EventArgs e)
        {
            chart1.Titles.Clear();
            chart1.Titles.Add("Progress Bar");

            const int Max = 1000;

            //remove all margins
            chart1.ChartAreas[0].Position.X = 0;
            chart1.ChartAreas[0].Position.Width = 100;
            chart1.ChartAreas[0].Position.Height = 100;
            chart1.ChartAreas[0].Position.Y = 0;

            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = Max/10;
            //remove labels
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisX.Maximum = Max;

            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 20;
            chart1.ChartAreas[0].AxisY.Maximum = 100;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.LineColor = chart1.BackColor; //hide Y axis line

            ////background line
            //StripLine stripline = new StripLine();
            //stripline.Interval = 0;
            //stripline.IntervalOffset = 35; // average value of the y axis; eg: 35
            //stripline.StripWidth = 1;
            //stripline.BackColor = Color.Navy;
            //chart1.ChartAreas[0].AxisY.StripLines.Add(stripline);

            //foreground line
            _annotationLine.AxisX = chart1.ChartAreas[0].AxisX;
            _annotationLine.AxisY = chart1.ChartAreas[0].AxisY;
            _annotationLine.IsSizeAlwaysRelative = false;
            _annotationLine.AnchorY = 44;
            _annotationLine.IsInfinitive = true;
            _annotationLine.ClipToChartArea = chart1.ChartAreas[0].Name; 
            _annotationLine.LineColor = Color.Navy; 
            _annotationLine.LineWidth = 1;
            chart1.Annotations.Add(_annotationLine);

            //annotation text
            
            //annotation.AnchorDataPoint = chart1.Series[1].Points[3];
            _annotationText.Y = 40; // percent from top
            _annotationText.X = 60; // percent from left
            _annotationText.ShadowColor = Color.Pink;
            _annotationText.Text = "Speed: 946 Kb/s";
            _annotationText.ForeColor = Color.Black;
            _annotationText.BackColor = Color.Transparent;
            _annotationText.Font = new Font("Arial", 10, FontStyle.Regular);
            _annotationText.LineWidth = 0;
            chart1.Annotations.Add(_annotationText);

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
                    //chart1.Series[0].Points.AddXY(i, 0);
                    //chart1.Series[1].Points.AddXY(i, 0);
                }
            }
        }

        public void SetHistory(List<double> values, int maxX)
        {
            foreach (var ser in chart1.Series)
            {
                ser.Points.Clear();
            }

            if (values.Count == 0)
                return;

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = maxX;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = maxX / 10;

            double maxY = 1.2 * values.Max();
            chart1.ChartAreas[0].AxisY.Maximum = maxY;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = maxY / 5;

            double current = values.Last();

            _annotationText.Text = string.Format("Speed: {0:###,##0.0} Kb/s", current);
            _annotationText.Y = -20 + 100 * (maxY - current)/maxY;
            _annotationLine.AnchorY = current;

            for (int i = 0; i < maxX; i++)
            {
                if (i < values.Count)
                {
                    chart1.Series[0].Points.AddXY(i, 100);
                    chart1.Series[1].Points.AddXY(i, values[i]);
                }
                else
                {
                    //chart1.Series[0].Points.AddXY(i, 0);
                    //chart1.Series[1].Points.AddXY(i, 0);
                }
            }
        }
    }
}
