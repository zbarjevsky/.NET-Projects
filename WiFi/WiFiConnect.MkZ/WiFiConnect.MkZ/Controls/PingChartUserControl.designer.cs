namespace WiFiConnect.MkZ.Controls
{
    partial class PingChartUserControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.m_txtValue = new System.Windows.Forms.TextBox();
            this.m_chkAutoScale = new System.Windows.Forms.CheckBox();
            this.m_grpControls = new System.Windows.Forms.GroupBox();
            this.m_btnReset = new System.Windows.Forms.Button();
            this._tooltip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.m_grpControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Format = "HH:mm";
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.LabelStyle.Format = "{0}";
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Color = System.Drawing.Color.OrangeRed;
            series1.Legend = "Legend1";
            series1.MarkerSize = 3;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(491, 371);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this.chart1_AxisViewChanged);
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // m_txtValue
            // 
            this.m_txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_txtValue.Location = new System.Drawing.Point(5, 17);
            this.m_txtValue.Name = "m_txtValue";
            this.m_txtValue.Size = new System.Drawing.Size(75, 20);
            this.m_txtValue.TabIndex = 1;
            this.m_txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_chkAutoScale
            // 
            this.m_chkAutoScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_chkAutoScale.AutoSize = true;
            this.m_chkAutoScale.BackColor = System.Drawing.Color.White;
            this.m_chkAutoScale.Checked = true;
            this.m_chkAutoScale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkAutoScale.Location = new System.Drawing.Point(4, 82);
            this.m_chkAutoScale.Name = "m_chkAutoScale";
            this.m_chkAutoScale.Size = new System.Drawing.Size(78, 17);
            this.m_chkAutoScale.TabIndex = 30;
            this.m_chkAutoScale.Text = "Auto Scale";
            this.m_chkAutoScale.UseVisualStyleBackColor = false;
            this.m_chkAutoScale.CheckedChanged += new System.EventHandler(this.m_chkAutoScale_CheckedChanged);
            // 
            // m_grpControls
            // 
            this.m_grpControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_grpControls.BackColor = System.Drawing.Color.White;
            this.m_grpControls.Controls.Add(this.m_btnReset);
            this.m_grpControls.Controls.Add(this.m_chkAutoScale);
            this.m_grpControls.Controls.Add(this.m_txtValue);
            this.m_grpControls.Location = new System.Drawing.Point(363, 43);
            this.m_grpControls.Name = "m_grpControls";
            this.m_grpControls.Size = new System.Drawing.Size(91, 105);
            this.m_grpControls.TabIndex = 31;
            this.m_grpControls.TabStop = false;
            // 
            // m_btnReset
            // 
            this.m_btnReset.BackColor = System.Drawing.Color.Gainsboro;
            this.m_btnReset.Location = new System.Drawing.Point(4, 47);
            this.m_btnReset.Name = "m_btnReset";
            this.m_btnReset.Size = new System.Drawing.Size(78, 23);
            this.m_btnReset.TabIndex = 31;
            this.m_btnReset.Text = "Reset Zoom";
            this.m_btnReset.UseVisualStyleBackColor = false;
            this.m_btnReset.Click += new System.EventHandler(this.m_btnReset_Click);
            // 
            // _tooltip
            // 
            this._tooltip.IsBalloon = true;
            // 
            // WeatherChartUserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_grpControls);
            this.Controls.Add(this.chart1);
            this.Name = "WeatherChartUserControl";
            this.Size = new System.Drawing.Size(491, 371);
            this.Load += new System.EventHandler(this.WeatherChartUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.m_grpControls.ResumeLayout(false);
            this.m_grpControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox m_txtValue;
        private System.Windows.Forms.CheckBox m_chkAutoScale;
        private System.Windows.Forms.GroupBox m_grpControls;
        private System.Windows.Forms.ToolTip _tooltip;
        private System.Windows.Forms.Button m_btnReset;
    }
}
