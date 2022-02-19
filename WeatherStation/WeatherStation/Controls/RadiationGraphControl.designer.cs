namespace MkZ.WeatherStation.Controls
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
            this.m_pnlStatus = new System.Windows.Forms.Panel();
            this.m_hScrollBarZoom = new System.Windows.Forms.HScrollBar();
            this.m_chkAlert = new System.Windows.Forms.CheckBox();
            this.m_chkCPM = new System.Windows.Forms.CheckBox();
            this.m_chkRate = new System.Windows.Forms.CheckBox();
            this.m_chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_pnlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).BeginInit();
            this.m_pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_pnlStatus
            // 
            this.m_pnlStatus.Controls.Add(this.m_hScrollBarZoom);
            this.m_pnlStatus.Controls.Add(this.m_chkAlert);
            this.m_pnlStatus.Controls.Add(this.m_chkCPM);
            this.m_pnlStatus.Controls.Add(this.m_chkRate);
            this.m_pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlStatus.Location = new System.Drawing.Point(0, 143);
            this.m_pnlStatus.Name = "m_pnlStatus";
            this.m_pnlStatus.Size = new System.Drawing.Size(976, 29);
            this.m_pnlStatus.TabIndex = 1;
            // 
            // m_hScrollBarZoom
            // 
            this.m_hScrollBarZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_hScrollBarZoom.LargeChange = 20;
            this.m_hScrollBarZoom.Location = new System.Drawing.Point(38, 5);
            this.m_hScrollBarZoom.Maximum = 40;
            this.m_hScrollBarZoom.Name = "m_hScrollBarZoom";
            this.m_hScrollBarZoom.Size = new System.Drawing.Size(715, 17);
            this.m_hScrollBarZoom.TabIndex = 26;
            this.m_hScrollBarZoom.Value = 10;
            this.m_hScrollBarZoom.Scroll += new System.Windows.Forms.ScrollEventHandler(this.m_hScrollBarZoom_Scroll);
            // 
            // m_chkAlert
            // 
            this.m_chkAlert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkAlert.AutoSize = true;
            this.m_chkAlert.Checked = true;
            this.m_chkAlert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkAlert.Location = new System.Drawing.Point(907, 5);
            this.m_chkAlert.Name = "m_chkAlert";
            this.m_chkAlert.Size = new System.Drawing.Size(47, 17);
            this.m_chkAlert.TabIndex = 2;
            this.m_chkAlert.Text = "Alert";
            this.m_chkAlert.UseVisualStyleBackColor = true;
            this.m_chkAlert.CheckedChanged += new System.EventHandler(this.m_chkAlert_CheckedChanged);
            // 
            // m_chkCPM
            // 
            this.m_chkCPM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkCPM.AutoSize = true;
            this.m_chkCPM.Checked = true;
            this.m_chkCPM.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkCPM.Location = new System.Drawing.Point(840, 5);
            this.m_chkCPM.Name = "m_chkCPM";
            this.m_chkCPM.Size = new System.Drawing.Size(49, 17);
            this.m_chkCPM.TabIndex = 1;
            this.m_chkCPM.Text = "CPM";
            this.m_chkCPM.UseVisualStyleBackColor = true;
            this.m_chkCPM.CheckedChanged += new System.EventHandler(this.m_chkCPM_CheckedChanged);
            // 
            // m_chkRate
            // 
            this.m_chkRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chkRate.AutoSize = true;
            this.m_chkRate.Checked = true;
            this.m_chkRate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkRate.Location = new System.Drawing.Point(768, 5);
            this.m_chkRate.Name = "m_chkRate";
            this.m_chkRate.Size = new System.Drawing.Size(49, 17);
            this.m_chkRate.TabIndex = 0;
            this.m_chkRate.Text = "Rate";
            this.m_chkRate.UseVisualStyleBackColor = true;
            this.m_chkRate.CheckedChanged += new System.EventHandler(this.m_chkRate_CheckedChanged);
            // 
            // m_chart1
            // 
            this.m_chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_chart1.BorderlineColor = System.Drawing.Color.Empty;
            this.m_chart1.BorderSkin.BorderColor = System.Drawing.Color.Empty;
            this.m_chart1.BorderSkin.PageColor = System.Drawing.SystemColors.Control;
            this.m_chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Sunken;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Angle = 5;
            chartArea1.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DodgerBlue;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.DarkOrange;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 77.71274F;
            chartArea1.InnerPlotPosition.Width = 90.33515F;
            chartArea1.InnerPlotPosition.X = 2.664848F;
            chartArea1.InnerPlotPosition.Y = 8.12792F;
            chartArea1.Name = "ChartArea1";
            this.m_chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.m_chart1.Legends.Add(legend1);
            this.m_chart1.Location = new System.Drawing.Point(3, 3);
            this.m_chart1.Name = "m_chart1";
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.DodgerBlue;
            series1.Legend = "Legend1";
            series1.LegendText = "Rate µSv/h";
            series1.Name = "SeriesDOSE";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Orange;
            series2.Legend = "Legend1";
            series2.LegendText = "CPM";
            series2.Name = "SeriesCPM";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Orange;
            series3.Legend = "Legend1";
            series3.LegendText = "Alert Threshold";
            series3.Name = "SeriesThreshold";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.m_chart1.Series.Add(series1);
            this.m_chart1.Series.Add(series2);
            this.m_chart1.Series.Add(series3);
            this.m_chart1.Size = new System.Drawing.Size(975, 142);
            this.m_chart1.TabIndex = 8;
            this.m_chart1.Text = "chart1";
            this.m_chart1.Click += new System.EventHandler(this.m_chart1_Click);
            this.m_chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_chart1_MouseMove);
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.BackColor = System.Drawing.SystemColors.Control;
            this.m_pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlMain.Controls.Add(this.m_chart1);
            this.m_pnlMain.Controls.Add(this.m_pnlStatus);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(980, 176);
            this.m_pnlMain.TabIndex = 9;
            // 
            // RadiationGraphControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this.m_pnlMain);
            this.MinimumSize = new System.Drawing.Size(480, 140);
            this.Name = "RadiationGraphControl";
            this.Size = new System.Drawing.Size(980, 176);
            this.Load += new System.EventHandler(this.RadiationGraphControl_Load);
            this.m_pnlStatus.ResumeLayout(false);
            this.m_pnlStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).EndInit();
            this.m_pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel m_pnlStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart m_chart1;
        private System.Windows.Forms.CheckBox m_chkCPM;
        private System.Windows.Forms.CheckBox m_chkRate;
        private System.Windows.Forms.CheckBox m_chkAlert;
        private System.Windows.Forms.HScrollBar m_hScrollBarZoom;
        private System.Windows.Forms.Panel m_pnlMain;
    }
}
