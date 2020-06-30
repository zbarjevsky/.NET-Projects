namespace TantaCommon
{
    partial class ctlTantaVideoPicker
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
            this.comboBoxCaptureDevices = new System.Windows.Forms.ComboBox();
            this.listViewSupportedFormats = new System.Windows.Forms.ListView();
            this.comboBoxFormat = new System.Windows.Forms.ComboBox();
            this.comboBoxResolution = new System.Windows.Forms.ComboBox();
            this.comboBoxFps = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxCaptureDevices
            // 
            this.comboBoxCaptureDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCaptureDevices.FormattingEnabled = true;
            this.comboBoxCaptureDevices.Location = new System.Drawing.Point(0, 0);
            this.comboBoxCaptureDevices.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxCaptureDevices.Name = "comboBoxCaptureDevices";
            this.comboBoxCaptureDevices.Size = new System.Drawing.Size(391, 21);
            this.comboBoxCaptureDevices.TabIndex = 10;
            this.comboBoxCaptureDevices.SelectedIndexChanged += new System.EventHandler(this.comboBoxCaptureDevices_SelectedIndexChanged);
            // 
            // listViewSupportedFormats
            // 
            this.listViewSupportedFormats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSupportedFormats.FullRowSelect = true;
            this.listViewSupportedFormats.GridLines = true;
            this.listViewSupportedFormats.HideSelection = false;
            this.listViewSupportedFormats.Location = new System.Drawing.Point(0, 31);
            this.listViewSupportedFormats.MultiSelect = false;
            this.listViewSupportedFormats.Name = "listViewSupportedFormats";
            this.listViewSupportedFormats.Size = new System.Drawing.Size(391, 171);
            this.listViewSupportedFormats.TabIndex = 11;
            this.listViewSupportedFormats.UseCompatibleStateImageBehavior = false;
            this.listViewSupportedFormats.View = System.Windows.Forms.View.Details;
            this.listViewSupportedFormats.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSupportedFormats_ColumnClick);
            this.listViewSupportedFormats.SelectedIndexChanged += new System.EventHandler(this.listViewSupportedFormats_SelectedIndexChanged);
            this.listViewSupportedFormats.DoubleClick += new System.EventHandler(this.listViewSupportedFormats_DoubleClick);
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new System.Drawing.Point(0, 208);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFormat.TabIndex = 12;
            this.comboBoxFormat.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFormat_SelectionChangeCommitted);
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Location = new System.Drawing.Point(127, 208);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(121, 21);
            this.comboBoxResolution.TabIndex = 13;
            this.comboBoxResolution.SelectionChangeCommitted += new System.EventHandler(this.comboBoxResolution_SelectionChangeCommitted);
            // 
            // comboBoxFps
            // 
            this.comboBoxFps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxFps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFps.FormattingEnabled = true;
            this.comboBoxFps.Location = new System.Drawing.Point(254, 208);
            this.comboBoxFps.Name = "comboBoxFps";
            this.comboBoxFps.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFps.TabIndex = 14;
            this.comboBoxFps.SelectionChangeCommitted += new System.EventHandler(this.comboBoxFps_SelectionChangeCommitted);
            // 
            // ctlTantaVideoPicker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.comboBoxFps);
            this.Controls.Add(this.comboBoxResolution);
            this.Controls.Add(this.comboBoxFormat);
            this.Controls.Add(this.listViewSupportedFormats);
            this.Controls.Add(this.comboBoxCaptureDevices);
            this.Name = "ctlTantaVideoPicker";
            this.Size = new System.Drawing.Size(392, 238);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCaptureDevices;
        private System.Windows.Forms.ListView listViewSupportedFormats;
        private System.Windows.Forms.ComboBox comboBoxFormat;
        private System.Windows.Forms.ComboBox comboBoxResolution;
        private System.Windows.Forms.ComboBox comboBoxFps;
    }
}
