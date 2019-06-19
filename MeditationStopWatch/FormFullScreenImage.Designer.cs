namespace MeditationStopWatch
{
    partial class FormFullScreenImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFullScreenImage));
            this.pictureBox1 = new MeditationStopWatch.ZoomablePictureBoxUserControl();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_lblVolume = new MeditationStopWatch.Tools.LabelWithTimeout();
            this.m_analogClock = new MeditationStopWatch.AnalogClock();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.m_analogClock)).BeginInit();
            this.m_pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1067, 738);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.FlatAppearance.BorderSize = 0;
            this.m_btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.m_btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.m_btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.Location = new System.Drawing.Point(1017, 15);
            this.m_btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(33, 32);
            this.m_btnCancel.TabIndex = 2;
            this.m_btnCancel.UseVisualStyleBackColor = false;
            // 
            // m_lblVolume
            // 
            this.m_lblVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lblVolume.AutoSize = true;
            this.m_lblVolume.BackColor = System.Drawing.Color.Transparent;
            this.m_lblVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_lblVolume.ForeColor = System.Drawing.Color.Lime;
            this.m_lblVolume.Location = new System.Drawing.Point(603, 608);
            this.m_lblVolume.Margin = new System.Windows.Forms.Padding(16, 15, 16, 15);
            this.m_lblVolume.Name = "m_lblVolume";
            this.m_lblVolume.Size = new System.Drawing.Size(378, 54);
            this.m_lblVolume.TabIndex = 4;
            this.m_lblVolume.Text = "Volume 100.0 %";
            // 
            // m_analogClock
            // 
            this.m_analogClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_analogClock.BackColor = System.Drawing.Color.Black;
            this.m_analogClock.Location = new System.Drawing.Point(486, 41);
            this.m_analogClock.Margin = new System.Windows.Forms.Padding(4);
            this.m_analogClock.MinimumSize = new System.Drawing.Size(27, 25);
            this.m_analogClock.Name = "m_analogClock";
            this.m_analogClock.Size = new System.Drawing.Size(520, 464);
            this.m_analogClock.TabIndex = 3;
            this.m_analogClock.TabStop = false;
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.Controls.Add(this.m_btnCancel);
            this.m_pnlMain.Controls.Add(this.m_lblVolume);
            this.m_pnlMain.Controls.Add(this.m_analogClock);
            this.m_pnlMain.Controls.Add(this.pictureBox1);
            this.m_pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlMain.Location = new System.Drawing.Point(0, 0);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(1067, 738);
            this.m_pnlMain.TabIndex = 5;
            // 
            // FormFullScreenImage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(1067, 738);
            this.Controls.Add(this.m_pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormFullScreenImage";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FullScreenImage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFullScreenImage_FormClosed);
            this.Load += new System.EventHandler(this.FormFullScreenImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_analogClock)).EndInit();
            this.m_pnlMain.ResumeLayout(false);
            this.m_pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZoomablePictureBoxUserControl pictureBox1;
        private System.Windows.Forms.Button m_btnCancel;
        private AnalogClock m_analogClock;
        private Tools.LabelWithTimeout m_lblVolume;
        private System.Windows.Forms.Panel m_pnlMain;
    }
}