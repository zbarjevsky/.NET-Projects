﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MkZ.WinForms
{
    public partial class ChartProgressUserControl : UserControl
    {
        private List<double> _history = new List<double>();
        RectangleAnnotation _annotationText = new RectangleAnnotation();
        HorizontalLineAnnotation _annotationLine = new HorizontalLineAnnotation();

        [Description("Graph Back Color"), Category("Graph")]
        public Color GraphBackColor { get { return chart1.Series[0].Color; } set { chart1.Series[0].Color = Semitransparent(value); } }
        [Description("Graph Main Color"), Category("Graph")]
        public Color GraphMainColor { get { return chart1.Series[1].Color; } set { chart1.Series[1].Color = Semitransparent(value); } }
        [Description("Graph Title"), Category("Graph")]
        public string GraphTitle 
        { 
            get { return chart1.Titles[0].Text; } 
            set { chart1.Titles[0].Text = value; } 
        }

        public ChartProgressUserControl()
        {
            const int MAX = 1000;

            InitializeComponent();

            chart1.Titles.Clear();
            chart1.Titles.Add("Progress Bar");
            chart1.Titles[0].ForeColor = Color.Gray;

            //remove all margins
            chart1.ChartAreas[0].Position.X = 0;
            chart1.ChartAreas[0].Position.Width = 100;
            chart1.ChartAreas[0].Position.Height = 100;
            chart1.ChartAreas[0].Position.Y = 0;

            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = MAX / 10;
            //remove labels
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisX.Maximum = MAX;

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

            chart1.Annotations.Clear();

            //foreground line
            _annotationLine.AxisX = chart1.ChartAreas[0].AxisX;
            _annotationLine.AxisY = chart1.ChartAreas[0].AxisY;
            _annotationLine.IsSizeAlwaysRelative = false;
            _annotationLine.AnchorY = 2;
            _annotationLine.IsInfinitive = true;
            _annotationLine.ClipToChartArea = chart1.ChartAreas[0].Name;
            _annotationLine.LineColor = Color.Navy;
            _annotationLine.LineWidth = 1;
            chart1.Annotations.Add(_annotationLine);

            //annotation text

            //annotation.AnchorDataPoint = chart1.Series[1].Points[3];
            _annotationText.Y = 0; // percent from top
            _annotationText.X = 0; // percent from left
            //_annotationText.Width = 0.3 * chart1.ChartAreas[0].AxisX.Maximum;
            //_annotationText.Height = Auto;
            _annotationText.ShadowColor = Color.Pink;
            _annotationText.Text = "Speed: ? Kb/s";
            _annotationText.ForeColor = Color.Black;
            _annotationText.BackColor = Color.Transparent;
            _annotationText.Font = new Font("Arial", 10, FontStyle.Regular);
            _annotationText.LineWidth = 0; //rectangle border
            chart1.Annotations.Add(_annotationText);
            UpdateTextLabelWidthAndPositionXY(0);
        }

        private void ChartProgressUserControl_Load(object sender, EventArgs e)
        {

            foreach (var ser in chart1.Series)
            {
                ser.IsVisibleInLegend = false;
                ser.Color = Semitransparent(ser.Color); //semitransparent
            }
        }

        private Color Semitransparent(Color c, byte alpha = 168)
        {
            return Color.FromArgb(alpha, c); //semitransparent
        }

        public void SetHistory(List<double> values, int maxX, string title)
        {
            foreach (var ser in chart1.Series)
            {
                ser.Points.Clear();
            }

            _annotationLine.AnchorY = 0;
            _annotationText.Y = 0; //Top
            _annotationText.Text = "";
            chart1.Titles[0].Text = title;
            if (values.Count == 0)
                return;

            chart1.ChartAreas[0].AxisX.Minimum = -0.1;
            chart1.ChartAreas[0].AxisX.Maximum = maxX;
            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = maxX / 10;

            double maxY = 1.2 * values.Max();
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = maxY;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = maxY / 5;

            double current = values.Last();

            _annotationText.Text = string.Format("Speed: {0:###,##0.0} Kb/s", current);

            double textY = 100 * ((maxY - current)/maxY); //percent from top
            double annotationHeightInPixels = 21;
            textY -= annotationHeightInPixels * 100.0 / chart1.Height; //
            _annotationText.Y = textY;
            
            _annotationLine.AnchorY = current;

            chart1.Series[0].Points.AddXY(0, maxY);
            chart1.Series[1].Points.AddXY(0, values[0]);

            for (int i = 0; i < maxX; i++)
            {
                if (i < values.Count)
                {
                    chart1.Series[0].Points.AddXY(i+1, maxY);
                    chart1.Series[1].Points.AddXY(i+1, values[i]);
                }
            }
        }

        private void chart1_SizeChanged(object sender, EventArgs e)
        {
            UpdateTextLabelWidthAndPositionXY(_annotationText.Y);
        }

        private void UpdateTextLabelWidthAndPositionXY(double yPercentsFromTop)
        {
            double textWidthInPixels = 150;
            double pixelsPerPercent = chart1.Width / 100.0;
            double widthInPercents = textWidthInPixels / pixelsPerPercent;
            _annotationText.Width = widthInPercents; // percent from chart Width;
            _annotationText.X = 100.0 - widthInPercents; // percent from left
            _annotationText.Y = yPercentsFromTop; //update
        }
    }
}
