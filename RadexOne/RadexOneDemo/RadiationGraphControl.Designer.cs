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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.m_pnlTools = new System.Windows.Forms.Panel();
            this.m_pnlStatus = new System.Windows.Forms.Panel();
            this.m_hScrollBarZoom = new System.Windows.Forms.HScrollBar();
            this.m_chkAlert = new System.Windows.Forms.CheckBox();
            this.m_chkCPM = new System.Windows.Forms.CheckBox();
            this.m_chkRate = new System.Windows.Forms.CheckBox();
            this.m_chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.m_pnlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pnlTools
            // 
            this.m_pnlTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTools.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTools.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_pnlTools.Name = "m_pnlTools";
            this.m_pnlTools.Size = new System.Drawing.Size(641, 30);
            this.m_pnlTools.TabIndex = 0;
            // 
            // m_pnlStatus
            // 
            this.m_pnlStatus.Controls.Add(this.m_hScrollBarZoom);
            this.m_pnlStatus.Controls.Add(this.m_chkAlert);
            this.m_pnlStatus.Controls.Add(this.m_chkCPM);
            this.m_pnlStatus.Controls.Add(this.m_chkRate);
            this.m_pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlStatus.Location = new System.Drawing.Point(0, 251);
            this.m_pnlStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_pnlStatus.Name = "m_pnlStatus";
            this.m_pnlStatus.Size = new System.Drawing.Size(641, 36);
            this.m_pnlStatus.TabIndex = 1;
            // 
            // m_hScrollBarZoom
            // 
            this.m_hScrollBarZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_hScrollBarZoom.LargeChange = 20;
            this.m_hScrollBarZoom.Location = new System.Drawing.Point(37, 6);
            this.m_hScrollBarZoom.Maximum = 40;
            this.m_hScrollBarZoom.Name = "m_hScrollBarZoom";
            this.m_hScrollBarZoom.Size = new System.Drawing.Size(306, 17);
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
            this.m_chkAlert.Location = new System.Drawing.Point(554, 6);
            this.m_chkAlert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_chkAlert.Name = "m_chkAlert";
            this.m_chkAlert.Size = new System.Drawing.Size(59, 21);
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
            this.m_chkCPM.Location = new System.Drawing.Point(466, 6);
            this.m_chkCPM.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_chkCPM.Name = "m_chkCPM";
            this.m_chkCPM.Size = new System.Drawing.Size(59, 21);
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
            this.m_chkRate.Location = new System.Drawing.Point(369, 6);
            this.m_chkRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_chkRate.Name = "m_chkRate";
            this.m_chkRate.Size = new System.Drawing.Size(60, 21);
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
            chartArea2.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.DodgerBlue;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.DarkOrange;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.BackColor = System.Drawing.Color.White;
            chartArea2.BackSecondaryColor = System.Drawing.Color.White;
            chartArea2.Name = "ChartArea1";
            this.m_chart1.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.m_chart1.Legends.Add(legend2);
            this.m_chart1.Location = new System.Drawing.Point(8, 30);
            this.m_chart1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.m_chart1.Name = "m_chart1";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.DodgerBlue;
            series4.Legend = "Legend1";
            series4.LegendText = "Rate µSv/h";
            series4.Name = "SeriesDOSE";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Orange;
            series5.Legend = "Legend1";
            series5.LegendText = "CPM";
            series5.Name = "SeriesCPM";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series5.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series6.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            series6.BorderWidth = 2;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.Orange;
            series6.Legend = "Legend1";
            series6.LegendText = "Alert Threshold";
            series6.Name = "SeriesThreshold";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series6.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.m_chart1.Series.Add(series4);
            this.m_chart1.Series.Add(series5);
            this.m_chart1.Series.Add(series6);
            this.m_chart1.Size = new System.Drawing.Size(629, 225);
            this.m_chart1.TabIndex = 8;
            this.m_chart1.Text = "chart1";
            this.m_chart1.Click += new System.EventHandler(this.m_chart1_Click);
            this.m_chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_chart1_MouseMove);
            // 
            // RadiationGraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_chart1);
            this.Controls.Add(this.m_pnlStatus);
            this.Controls.Add(this.m_pnlTools);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(640, 280);
            this.Name = "RadiationGraphControl";
            this.Size = new System.Drawing.Size(641, 287);
            this.Load += new System.EventHandler(this.RadiationGraphControl_Load);
            this.m_pnlStatus.ResumeLayout(false);
            this.m_pnlStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel m_pnlTools;
        private System.Windows.Forms.Panel m_pnlStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart m_chart1;
        private System.Windows.Forms.CheckBox m_chkCPM;
        private System.Windows.Forms.CheckBox m_chkRate;
        private System.Windows.Forms.CheckBox m_chkAlert;
        private System.Windows.Forms.HScrollBar m_hScrollBarZoom;
    }
}
