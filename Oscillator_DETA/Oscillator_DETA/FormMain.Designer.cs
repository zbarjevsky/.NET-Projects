namespace Oscillator_DETA
{
    partial class FormMain
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.m_btnPlay = new System.Windows.Forms.Button();
            this.m_trackFreq = new System.Windows.Forms.TrackBar();
            this.m_numFreq = new System.Windows.Forms.NumericUpDown();
            this.m_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_rbSquare = new System.Windows.Forms.RadioButton();
            this.m_rbSine = new System.Windows.Forms.RadioButton();
            this.m_pnlCmd = new System.Windows.Forms.Panel();
            this.m_numFreq2 = new System.Windows.Forms.NumericUpDown();
            this.m_splitMain = new System.Windows.Forms.SplitContainer();
            this.m_splitOut = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_numOffset = new System.Windows.Forms.NumericUpDown();
            this.m_numBufferSize = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.m_trackFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.m_pnlCmd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numFreq2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).BeginInit();
            this.m_splitMain.Panel2.SuspendLayout();
            this.m_splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitOut)).BeginInit();
            this.m_splitOut.Panel1.SuspendLayout();
            this.m_splitOut.Panel2.SuspendLayout();
            this.m_splitOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numBufferSize)).BeginInit();
            this.SuspendLayout();
            // 
            // m_btnPlay
            // 
            this.m_btnPlay.Location = new System.Drawing.Point(151, 82);
            this.m_btnPlay.Name = "m_btnPlay";
            this.m_btnPlay.Size = new System.Drawing.Size(75, 23);
            this.m_btnPlay.TabIndex = 0;
            this.m_btnPlay.Text = "Play";
            this.m_btnPlay.UseVisualStyleBackColor = true;
            this.m_btnPlay.Click += new System.EventHandler(this.m_btnPlay_Click);
            // 
            // m_trackFreq
            // 
            this.m_trackFreq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_trackFreq.LargeChange = 5000;
            this.m_trackFreq.Location = new System.Drawing.Point(12, 12);
            this.m_trackFreq.Maximum = 20000;
            this.m_trackFreq.Minimum = 10;
            this.m_trackFreq.Name = "m_trackFreq";
            this.m_trackFreq.Size = new System.Drawing.Size(1154, 45);
            this.m_trackFreq.SmallChange = 500;
            this.m_trackFreq.TabIndex = 1;
            this.m_trackFreq.TickFrequency = 500;
            this.m_trackFreq.Value = 1000;
            this.m_trackFreq.ValueChanged += new System.EventHandler(this.m_trackFreq_ValueChanged);
            // 
            // m_numFreq
            // 
            this.m_numFreq.DecimalPlaces = 2;
            this.m_numFreq.Location = new System.Drawing.Point(12, 65);
            this.m_numFreq.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.m_numFreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_numFreq.Name = "m_numFreq";
            this.m_numFreq.Size = new System.Drawing.Size(120, 20);
            this.m_numFreq.TabIndex = 2;
            this.m_numFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numFreq.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_numFreq.ValueChanged += new System.EventHandler(this.m_numFreq_ValueChanged);
            // 
            // m_chart
            // 
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.DarkOrange;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.BackColor = System.Drawing.Color.Black;
            chartArea2.Name = "ChartArea1";
            this.m_chart.ChartAreas.Add(chartArea2);
            this.m_chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.m_chart.Legends.Add(legend2);
            this.m_chart.Location = new System.Drawing.Point(0, 0);
            this.m_chart.Name = "m_chart";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.m_chart.Series.Add(series2);
            this.m_chart.Size = new System.Drawing.Size(965, 327);
            this.m_chart.TabIndex = 3;
            this.m_chart.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_rbSquare);
            this.groupBox1.Controls.Add(this.m_rbSine);
            this.groupBox1.Location = new System.Drawing.Point(246, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 49);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wave Form";
            // 
            // m_rbSquare
            // 
            this.m_rbSquare.AutoSize = true;
            this.m_rbSquare.Location = new System.Drawing.Point(92, 20);
            this.m_rbSquare.Name = "m_rbSquare";
            this.m_rbSquare.Size = new System.Drawing.Size(59, 17);
            this.m_rbSquare.TabIndex = 1;
            this.m_rbSquare.Text = "Square";
            this.m_rbSquare.UseVisualStyleBackColor = true;
            // 
            // m_rbSine
            // 
            this.m_rbSine.AutoSize = true;
            this.m_rbSine.Checked = true;
            this.m_rbSine.Location = new System.Drawing.Point(24, 20);
            this.m_rbSine.Name = "m_rbSine";
            this.m_rbSine.Size = new System.Drawing.Size(46, 17);
            this.m_rbSine.TabIndex = 0;
            this.m_rbSine.TabStop = true;
            this.m_rbSine.Text = "Sine";
            this.m_rbSine.UseVisualStyleBackColor = true;
            // 
            // m_pnlCmd
            // 
            this.m_pnlCmd.Controls.Add(this.m_numFreq2);
            this.m_pnlCmd.Controls.Add(this.m_trackFreq);
            this.m_pnlCmd.Controls.Add(this.groupBox1);
            this.m_pnlCmd.Controls.Add(this.m_btnPlay);
            this.m_pnlCmd.Controls.Add(this.m_numFreq);
            this.m_pnlCmd.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlCmd.Location = new System.Drawing.Point(0, 0);
            this.m_pnlCmd.Name = "m_pnlCmd";
            this.m_pnlCmd.Size = new System.Drawing.Size(1178, 124);
            this.m_pnlCmd.TabIndex = 5;
            // 
            // m_numFreq2
            // 
            this.m_numFreq2.DecimalPlaces = 2;
            this.m_numFreq2.Location = new System.Drawing.Point(12, 94);
            this.m_numFreq2.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.m_numFreq2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.m_numFreq2.Name = "m_numFreq2";
            this.m_numFreq2.Size = new System.Drawing.Size(120, 20);
            this.m_numFreq2.TabIndex = 5;
            this.m_numFreq2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numFreq2.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // m_splitMain
            // 
            this.m_splitMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.m_splitMain.Location = new System.Drawing.Point(0, 124);
            this.m_splitMain.Name = "m_splitMain";
            // 
            // m_splitMain.Panel2
            // 
            this.m_splitMain.Panel2.Controls.Add(this.m_splitOut);
            this.m_splitMain.Size = new System.Drawing.Size(1178, 450);
            this.m_splitMain.SplitterDistance = 205;
            this.m_splitMain.TabIndex = 6;
            // 
            // m_splitOut
            // 
            this.m_splitOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_splitOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitOut.Location = new System.Drawing.Point(0, 0);
            this.m_splitOut.Name = "m_splitOut";
            this.m_splitOut.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitOut.Panel1
            // 
            this.m_splitOut.Panel1.Controls.Add(this.label2);
            this.m_splitOut.Panel1.Controls.Add(this.label1);
            this.m_splitOut.Panel1.Controls.Add(this.m_numOffset);
            this.m_splitOut.Panel1.Controls.Add(this.m_numBufferSize);
            // 
            // m_splitOut.Panel2
            // 
            this.m_splitOut.Panel2.Controls.Add(this.m_chart);
            this.m_splitOut.Size = new System.Drawing.Size(969, 450);
            this.m_splitOut.SplitterDistance = 115;
            this.m_splitOut.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Length";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Offset";
            // 
            // m_numOffset
            // 
            this.m_numOffset.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_numOffset.Location = new System.Drawing.Point(12, 66);
            this.m_numOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numOffset.Name = "m_numOffset";
            this.m_numOffset.Size = new System.Drawing.Size(120, 20);
            this.m_numOffset.TabIndex = 6;
            this.m_numOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numOffset.ValueChanged += new System.EventHandler(this.m_numBufferSize_ValueChanged);
            // 
            // m_numBufferSize
            // 
            this.m_numBufferSize.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.m_numBufferSize.Location = new System.Drawing.Point(160, 66);
            this.m_numBufferSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numBufferSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_numBufferSize.Name = "m_numBufferSize";
            this.m_numBufferSize.Size = new System.Drawing.Size(120, 20);
            this.m_numBufferSize.TabIndex = 5;
            this.m_numBufferSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numBufferSize.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numBufferSize.ValueChanged += new System.EventHandler(this.m_numBufferSize_ValueChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 574);
            this.Controls.Add(this.m_splitMain);
            this.Controls.Add(this.m_pnlCmd);
            this.Name = "FormMain";
            this.Text = "Oscillator";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_trackFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.m_pnlCmd.ResumeLayout(false);
            this.m_pnlCmd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numFreq2)).EndInit();
            this.m_splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).EndInit();
            this.m_splitMain.ResumeLayout(false);
            this.m_splitOut.Panel1.ResumeLayout(false);
            this.m_splitOut.Panel1.PerformLayout();
            this.m_splitOut.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitOut)).EndInit();
            this.m_splitOut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_numOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numBufferSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_btnPlay;
        private System.Windows.Forms.TrackBar m_trackFreq;
        private System.Windows.Forms.NumericUpDown m_numFreq;
        private System.Windows.Forms.DataVisualization.Charting.Chart m_chart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton m_rbSquare;
        private System.Windows.Forms.RadioButton m_rbSine;
        private System.Windows.Forms.Panel m_pnlCmd;
        private System.Windows.Forms.SplitContainer m_splitMain;
        private System.Windows.Forms.SplitContainer m_splitOut;
        private System.Windows.Forms.NumericUpDown m_numBufferSize;
        private System.Windows.Forms.NumericUpDown m_numFreq2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown m_numOffset;
    }
}

