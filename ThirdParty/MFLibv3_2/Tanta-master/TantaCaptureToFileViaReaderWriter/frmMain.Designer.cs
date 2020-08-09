namespace TantaCaptureToFileViaReaderWriter
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.radioButtonUseSpecified = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonVideoFormatAutoSelect = new System.Windows.Forms.RadioButton();
            this.ctlTantaVideoPicker1 = new TantaCommon.ctlTantaVideoPicker();
            this.buttonStartStopCapture = new System.Windows.Forms.Button();
            this.textBoxCaptureFileNameAndPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // radioButtonUseSpecified
            // 
            this.radioButtonUseSpecified.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonUseSpecified.AutoSize = true;
            this.radioButtonUseSpecified.Location = new System.Drawing.Point(25, 292);
            this.radioButtonUseSpecified.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonUseSpecified.Name = "radioButtonUseSpecified";
            this.radioButtonUseSpecified.Size = new System.Drawing.Size(175, 17);
            this.radioButtonUseSpecified.TabIndex = 25;
            this.radioButtonUseSpecified.Text = "&Use selected device and format";
            this.radioButtonUseSpecified.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 255);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Source Video Format && Size";
            // 
            // radioButtonVideoFormatAutoSelect
            // 
            this.radioButtonVideoFormatAutoSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButtonVideoFormatAutoSelect.AutoSize = true;
            this.radioButtonVideoFormatAutoSelect.Checked = true;
            this.radioButtonVideoFormatAutoSelect.Location = new System.Drawing.Point(25, 273);
            this.radioButtonVideoFormatAutoSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButtonVideoFormatAutoSelect.Name = "radioButtonVideoFormatAutoSelect";
            this.radioButtonVideoFormatAutoSelect.Size = new System.Drawing.Size(78, 17);
            this.radioButtonVideoFormatAutoSelect.TabIndex = 23;
            this.radioButtonVideoFormatAutoSelect.TabStop = true;
            this.radioButtonVideoFormatAutoSelect.Text = "&Auto select";
            this.radioButtonVideoFormatAutoSelect.UseVisualStyleBackColor = true;
            // 
            // ctlTantaVideoPicker1
            // 
            this.ctlTantaVideoPicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlTantaVideoPicker1.Location = new System.Drawing.Point(24, 72);
            this.ctlTantaVideoPicker1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ctlTantaVideoPicker1.Name = "ctlTantaVideoPicker1";
            this.ctlTantaVideoPicker1.Size = new System.Drawing.Size(397, 175);
            this.ctlTantaVideoPicker1.TabIndex = 22;
            // 
            // buttonStartStopCapture
            // 
            this.buttonStartStopCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStartStopCapture.Location = new System.Drawing.Point(157, 327);
            this.buttonStartStopCapture.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonStartStopCapture.Name = "buttonStartStopCapture";
            this.buttonStartStopCapture.Size = new System.Drawing.Size(128, 29);
            this.buttonStartStopCapture.TabIndex = 21;
            this.buttonStartStopCapture.Text = "Start Capture";
            this.buttonStartStopCapture.UseVisualStyleBackColor = true;
            this.buttonStartStopCapture.Click += new System.EventHandler(this.buttonStartStopCapture_Click);
            // 
            // textBoxCaptureFileNameAndPath
            // 
            this.textBoxCaptureFileNameAndPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCaptureFileNameAndPath.Location = new System.Drawing.Point(24, 25);
            this.textBoxCaptureFileNameAndPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxCaptureFileNameAndPath.Name = "textBoxCaptureFileNameAndPath";
            this.textBoxCaptureFileNameAndPath.Size = new System.Drawing.Size(398, 20);
            this.textBoxCaptureFileNameAndPath.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Capture File Name and Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Video Capture Devices on the System";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 366);
            this.Controls.Add(this.radioButtonUseSpecified);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.radioButtonVideoFormatAutoSelect);
            this.Controls.Add(this.ctlTantaVideoPicker1);
            this.Controls.Add(this.buttonStartStopCapture);
            this.Controls.Add(this.textBoxCaptureFileNameAndPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(379, 332);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tanta: Capture Video to File";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton radioButtonUseSpecified;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonVideoFormatAutoSelect;
        private TantaCommon.ctlTantaVideoPicker ctlTantaVideoPicker1;
        private System.Windows.Forms.Button buttonStartStopCapture;
        private System.Windows.Forms.TextBox textBoxCaptureFileNameAndPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

