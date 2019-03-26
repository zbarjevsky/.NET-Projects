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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHistory));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_chart1 = new RadexOneDemo.RadiationGraphControl();
            this.m_btnOpen = new System.Windows.Forms.Button();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.m_btnStop1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_numMaxCPM = new System.Windows.Forms.NumericUpDown();
            this.m_lblMaxCPM = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numMaxCPM)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.m_numMaxCPM);
            this.splitContainer1.Panel2.Controls.Add(this.m_lblMaxCPM);
            this.splitContainer1.Panel2.Controls.Add(this.m_btnOpen);
            this.splitContainer1.Panel2.Controls.Add(this.m_btnSave);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(1090, 474);
            this.splitContainer1.SplitterDistance = 958;
            this.splitContainer1.TabIndex = 0;
            // 
            // m_chart1
            // 
            this.m_chart1.BackColor = System.Drawing.SystemColors.Control;
            this.m_chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_chart1.Location = new System.Drawing.Point(0, 0);
            this.m_chart1.Name = "m_chart1";
            this.m_chart1.Size = new System.Drawing.Size(954, 470);
            this.m_chart1.TabIndex = 24;
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 474);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1090, 22);
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
            // m_numMaxCPM
            // 
            this.m_numMaxCPM.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.m_numMaxCPM.Location = new System.Drawing.Point(12, 97);
            this.m_numMaxCPM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.m_numMaxCPM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.m_numMaxCPM.Name = "m_numMaxCPM";
            this.m_numMaxCPM.Size = new System.Drawing.Size(73, 20);
            this.m_numMaxCPM.TabIndex = 9;
            this.m_numMaxCPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numMaxCPM.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.m_numMaxCPM.ValueChanged += new System.EventHandler(this.m_numMaxCPM_ValueChanged);
            // 
            // m_lblMaxCPM
            // 
            this.m_lblMaxCPM.AutoSize = true;
            this.m_lblMaxCPM.Location = new System.Drawing.Point(2, 81);
            this.m_lblMaxCPM.Name = "m_lblMaxCPM";
            this.m_lblMaxCPM.Size = new System.Drawing.Size(104, 13);
            this.m_lblMaxCPM.TabIndex = 8;
            this.m_lblMaxCPM.Text = "Alert Threshold CPM";
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 496);
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
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_numMaxCPM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private RadiationGraphControl m_chart1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel m_status1;
        private System.Windows.Forms.ToolStripDropDownButton m_btnStop1;
        private System.Windows.Forms.Button m_btnSave;
        private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
        private System.Windows.Forms.Button m_btnOpen;
        private System.Windows.Forms.OpenFileDialog m_openFileDialog;
        private System.Windows.Forms.NumericUpDown m_numMaxCPM;
        private System.Windows.Forms.Label m_lblMaxCPM;
    }
}