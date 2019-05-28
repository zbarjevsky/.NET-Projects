namespace ClipboardManager
{
	partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_Logo = new System.Windows.Forms.PictureBox();
            this.m_gridSettings = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_HotKeyEditor = new ClipboardManager.Utils.HotKeyEditorUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.m_Logo)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOK.Image")));
            this.m_btnOK.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.m_btnOK.Location = new System.Drawing.Point(430, 380);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 2;
            this.m_btnOK.Text = "&OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.m_btnCancel.Location = new System.Drawing.Point(430, 411);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 3;
            this.m_btnCancel.Text = "&Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_Logo
            // 
            this.m_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.m_Logo.Image = ((System.Drawing.Image)(resources.GetObject("m_Logo.Image")));
            this.m_Logo.InitialImage = ((System.Drawing.Image)(resources.GetObject("m_Logo.InitialImage")));
            this.m_Logo.Location = new System.Drawing.Point(444, 2);
            this.m_Logo.Name = "m_Logo";
            this.m_Logo.Size = new System.Drawing.Size(47, 50);
            this.m_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.m_Logo.TabIndex = 9;
            this.m_Logo.TabStop = false;
            // 
            // m_gridSettings
            // 
            this.m_gridSettings.BackColor = System.Drawing.Color.White;
            this.m_gridSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_gridSettings.Location = new System.Drawing.Point(0, 0);
            this.m_gridSettings.Name = "m_gridSettings";
            this.m_gridSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.m_gridSettings.Size = new System.Drawing.Size(391, 381);
            this.m_gridSettings.TabIndex = 0;
            this.m_gridSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.m_gridSettings_PropertyValueChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.m_gridSettings);
            this.panel1.Location = new System.Drawing.Point(12, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 385);
            this.panel1.TabIndex = 1;
            // 
            // m_HotKeyEditor
            // 
            this.m_HotKeyEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_HotKeyEditor.Location = new System.Drawing.Point(11, 13);
            this.m_HotKeyEditor.Name = "m_HotKeyEditor";
            this.m_HotKeyEditor.Size = new System.Drawing.Size(398, 26);
            this.m_HotKeyEditor.TabIndex = 0;
            // 
            // FormSettings
            // 
            this.AcceptButton = this.m_btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(517, 446);
            this.Controls.Add(this.m_HotKeyEditor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_Logo);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 850);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(380, 300);
            this.Name = "FormSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Clipboard Manager Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_Logo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button m_btnOK;
		private System.Windows.Forms.Button m_btnCancel;
		private System.Windows.Forms.PictureBox m_Logo;
        private System.Windows.Forms.PropertyGrid m_gridSettings;
        private System.Windows.Forms.Panel panel1;
        private Utils.HotKeyEditorUserControl m_HotKeyEditor;
    }
}