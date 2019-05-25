namespace MeditationStopWatch
{
    partial class ZoomablePictureBoxUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZoomablePictureBoxUserControl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_btnZoomIn = new System.Windows.Forms.Button();
            this.m_btnZoomOut = new System.Windows.Forms.Button();
            this.m_btnFitWindow = new System.Windows.Forms.Button();
            this.m_btnOrigSize = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 367);
            this.panel1.TabIndex = 0;
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(435, 367);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // m_btnZoomIn
            // 
            this.m_btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("m_btnZoomIn.Image")));
            this.m_btnZoomIn.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_btnZoomIn.Location = new System.Drawing.Point(0, 0);
            this.m_btnZoomIn.Name = "m_btnZoomIn";
            this.m_btnZoomIn.Size = new System.Drawing.Size(26, 25);
            this.m_btnZoomIn.TabIndex = 1;
            this.toolTip1.SetToolTip(this.m_btnZoomIn, "Zoom In");
            this.m_btnZoomIn.UseVisualStyleBackColor = true;
            this.m_btnZoomIn.Click += new System.EventHandler(this.m_btnZoomIn_Click);
            // 
            // m_btnZoomOut
            // 
            this.m_btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("m_btnZoomOut.Image")));
            this.m_btnZoomOut.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_btnZoomOut.Location = new System.Drawing.Point(25, 0);
            this.m_btnZoomOut.Name = "m_btnZoomOut";
            this.m_btnZoomOut.Size = new System.Drawing.Size(26, 25);
            this.m_btnZoomOut.TabIndex = 2;
            this.toolTip1.SetToolTip(this.m_btnZoomOut, "Zoom Out");
            this.m_btnZoomOut.UseVisualStyleBackColor = true;
            this.m_btnZoomOut.Click += new System.EventHandler(this.m_btnZoomOut_Click);
            // 
            // m_btnFitWindow
            // 
            this.m_btnFitWindow.Image = ((System.Drawing.Image)(resources.GetObject("m_btnFitWindow.Image")));
            this.m_btnFitWindow.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_btnFitWindow.Location = new System.Drawing.Point(75, 0);
            this.m_btnFitWindow.Name = "m_btnFitWindow";
            this.m_btnFitWindow.Size = new System.Drawing.Size(26, 25);
            this.m_btnFitWindow.TabIndex = 3;
            this.toolTip1.SetToolTip(this.m_btnFitWindow, "Fit Window");
            this.m_btnFitWindow.UseVisualStyleBackColor = true;
            this.m_btnFitWindow.Click += new System.EventHandler(this.m_btnFitWindow_Click);
            // 
            // m_btnOrigSize
            // 
            this.m_btnOrigSize.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOrigSize.Image")));
            this.m_btnOrigSize.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.m_btnOrigSize.Location = new System.Drawing.Point(50, 0);
            this.m_btnOrigSize.Name = "m_btnOrigSize";
            this.m_btnOrigSize.Size = new System.Drawing.Size(26, 25);
            this.m_btnOrigSize.TabIndex = 4;
            this.toolTip1.SetToolTip(this.m_btnOrigSize, "Original Size");
            this.m_btnOrigSize.UseVisualStyleBackColor = true;
            this.m_btnOrigSize.Click += new System.EventHandler(this.m_btnOrigSize_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ZoomablePictureBoxUserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.m_btnFitWindow);
            this.Controls.Add(this.m_btnZoomOut);
            this.Controls.Add(this.m_btnZoomIn);
            this.Controls.Add(this.m_btnOrigSize);
            this.Controls.Add(this.panel1);
            this.Name = "ZoomablePictureBoxUserControl";
            this.Size = new System.Drawing.Size(435, 367);
            this.Load += new System.EventHandler(this.ZoomablePictureBoxUserControl_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button m_btnZoomIn;
        private System.Windows.Forms.Button m_btnZoomOut;
        private System.Windows.Forms.Button m_btnFitWindow;
        private System.Windows.Forms.Button m_btnOrigSize;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
    }
}
