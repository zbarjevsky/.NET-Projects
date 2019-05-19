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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_lblVolume = new MeditationStopWatch.Tools.LabelWithTimeout();
            this.m_analogClock = new MeditationStopWatch.AnalogClock();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_analogClock)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
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
            this.m_btnCancel.Location = new System.Drawing.Point(773, 2);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(25, 26);
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
            this.m_lblVolume.Location = new System.Drawing.Point(452, 494);
            this.m_lblVolume.Margin = new System.Windows.Forms.Padding(12);
            this.m_lblVolume.Name = "m_lblVolume";
            this.m_lblVolume.Size = new System.Drawing.Size(305, 42);
            this.m_lblVolume.TabIndex = 4;
            this.m_lblVolume.Text = "Volume 100.0 %";
            // 
            // m_analogClock
            // 
            this.m_analogClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_analogClock.BackColor = System.Drawing.Color.Transparent;
            this.m_analogClock.Location = new System.Drawing.Point(367, 33);
            this.m_analogClock.MinimumSize = new System.Drawing.Size(20, 20);
            this.m_analogClock.Name = "m_analogClock";
            this.m_analogClock.Size = new System.Drawing.Size(390, 377);
            this.m_analogClock.TabIndex = 3;
            this.m_analogClock.TabStop = false;
            // 
            // FormFullScreenImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.m_lblVolume);
            this.Controls.Add(this.m_analogClock);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormFullScreenImage";
            this.ShowInTaskbar = false;
            this.Text = "FullScreenImage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFullScreenImage_FormClosed);
            this.Load += new System.EventHandler(this.FormFullScreenImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_analogClock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button m_btnCancel;
        private AnalogClock m_analogClock;
        private Tools.LabelWithTimeout m_lblVolume;
    }
}