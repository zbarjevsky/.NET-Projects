namespace RadexOneDemo
{
    partial class RadiationGraphControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.m_pnlTools = new System.Windows.Forms.Panel();
            this.m_pnlStatus = new System.Windows.Forms.Panel();
            this.m_chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pnlTools
            // 
            this.m_pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTools.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTools.Name = "m_pnlTools";
            this.m_pnlTools.Size = new System.Drawing.Size(872, 24);
            this.m_pnlTools.TabIndex = 0;
            // 
            // m_pnlStatus
            // 
            this.m_pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlStatus.Location = new System.Drawing.Point(0, 488);
            this.m_pnlStatus.Name = "m_pnlStatus";
            this.m_pnlStatus.Size = new System.Drawing.Size(872, 29);
            this.m_pnlStatus.TabIndex = 1;
            // 
            // m_chart1
            // 
            this.m_chart1.BorderlineColor = System.Drawing.Color.Empty;
            this.m_chart1.BorderSkin.BorderColor = System.Drawing.Color.Empty;
            this.m_chart1.BorderSkin.PageColor = System.Drawing.SystemColors.Control;
            this.m_chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Sunken;
            chartArea1.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DodgerBlue;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.DarkOrange;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.m_chart1.ChartAreas.Add(chartArea1);
            this.m_chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.m_chart1.Legends.Add(legend1);
            this.m_chart1.Location = new System.Drawing.Point(0, 24);
            this.m_chart1.Name = "m_chart1";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.LegendText = "DOSE µSv/h";
            series1.Name = "SeriesDOSE";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.LegendText = "CPM";
            series2.Name = "SeriesCPM";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.LegendText = "Alert Threshold";
            series3.Name = "SeriesThreshold";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.m_chart1.Series.Add(series1);
            this.m_chart1.Series.Add(series2);
            this.m_chart1.Series.Add(series3);
            this.m_chart1.Size = new System.Drawing.Size(872, 464);
            this.m_chart1.TabIndex = 8;
            this.m_chart1.Text = "chart1";
            // 
            // RadiationGraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_chart1);
            this.Controls.Add(this.m_pnlStatus);
            this.Controls.Add(this.m_pnlTools);
            this.Name = "RadiationGraphControl";
            this.Size = new System.Drawing.Size(872, 517);
            this.Load += new System.EventHandler(this.RadiationGraphControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnlTools;
        private System.Windows.Forms.Panel m_pnlStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart m_chart1;
    }
}
