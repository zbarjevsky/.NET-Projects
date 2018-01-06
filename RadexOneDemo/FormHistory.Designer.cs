namespace RadexOneDemo
{
    partial class FormHistory
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

        #region Windows Form Designer generated code

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHistory));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.m_btnOpen = new System.Windows.Forms.Button();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.m_btnStop1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_chart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_btnOpen);
            this.splitContainer1.Panel2.Controls.Add(this.m_btnSave);
            this.splitContainer1.Size = new System.Drawing.Size(1031, 360);
            this.splitContainer1.SplitterDistance = 928;
            this.splitContainer1.TabIndex = 0;
            // 
            // m_chart1
            // 
            this.m_chart1.BackColor = System.Drawing.SystemColors.Control;
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
            chartArea1.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.BackSecondaryColor = System.Drawing.SystemColors.Control;
            chartArea1.Name = "ChartArea1";
            this.m_chart1.ChartAreas.Add(chartArea1);
            this.m_chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.m_chart1.Legends.Add(legend1);
            this.m_chart1.Location = new System.Drawing.Point(0, 0);
            this.m_chart1.Name = "m_chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series1.Legend = "Legend1";
            series1.LegendText = "DOSE µSv/h";
            series1.Name = "SeriesDOSE";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series2.Legend = "Legend1";
            series2.LegendText = "CPM";
            series2.Name = "SeriesCPM";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.m_chart1.Series.Add(series1);
            this.m_chart1.Series.Add(series2);
            this.m_chart1.Size = new System.Drawing.Size(924, 356);
            this.m_chart1.TabIndex = 24;
            this.m_chart1.Text = "chart1";
            // 
            // m_btnOpen
            // 
            this.m_btnOpen.Location = new System.Drawing.Point(10, 10);
            this.m_btnOpen.Name = "m_btnOpen";
            this.m_btnOpen.Size = new System.Drawing.Size(75, 23);
            this.m_btnOpen.TabIndex = 1;
            this.m_btnOpen.Text = "&Open...";
            this.m_btnOpen.UseVisualStyleBackColor = true;
            this.m_btnOpen.Click += new System.EventHandler(this.m_btnOpen_Click);
            // 
            // m_btnSave
            // 
            this.m_btnSave.Location = new System.Drawing.Point(10, 40);
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.Size = new System.Drawing.Size(75, 23);
            this.m_btnSave.TabIndex = 0;
            this.m_btnSave.Text = "&Save As...";
            this.m_btnSave.UseVisualStyleBackColor = true;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSave_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status1,
            this.toolStripProgressBar1,
            this.m_btnStop1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 360);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1031, 22);
            this.statusStrip1.TabIndex = 25;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // m_status1
            // 
            this.m_status1.Name = "m_status1";
            this.m_status1.Size = new System.Drawing.Size(59, 17);
            this.m_status1.Text = "Loading...";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // m_btnStop1
            // 
            this.m_btnStop1.Image = ((System.Drawing.Image)(resources.GetObject("m_btnStop1.Image")));
            this.m_btnStop1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_btnStop1.Name = "m_btnStop1";
            this.m_btnStop1.ShowDropDownArrow = false;
            this.m_btnStop1.Size = new System.Drawing.Size(51, 20);
            this.m_btnStop1.Text = "Stop";
            this.m_btnStop1.Click += new System.EventHandler(this.m_btnStop1_Click);
            // 
            // m_saveFileDialog
            // 
            this.m_saveFileDialog.DefaultExt = "hist";
            this.m_saveFileDialog.FileName = "History1";
            this.m_saveFileDialog.Filter = "History file(*.hist)|*.hist";
            this.m_saveFileDialog.InitialDirectory = "C:\\Temp\\Radex\\";
            this.m_saveFileDialog.Title = "Save history to:";
            // 
            // m_openFileDialog
            // 
            this.m_openFileDialog.DefaultExt = "hist";
            this.m_openFileDialog.FileName = "*.hist";
            this.m_openFileDialog.Filter = "History file(*.hist)|*.hist";
            this.m_openFileDialog.InitialDirectory = "C:\\Temp\\Radex\\";
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 382);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormHistory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormHistory";
            this.Load += new System.EventHandler(this.FormHistory_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_chart1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart m_chart1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel m_status1;
        private System.Windows.Forms.ToolStripDropDownButton m_btnStop1;
        private System.Windows.Forms.Button m_btnSave;
        private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
        private System.Windows.Forms.Button m_btnOpen;
        private System.Windows.Forms.OpenFileDialog m_openFileDialog;
    }
}