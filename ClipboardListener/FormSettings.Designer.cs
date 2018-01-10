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
			this.m_txtHotKey = new System.Windows.Forms.TextBox();
			this.m_btnOK = new System.Windows.Forms.Button();
			this.m_btnCancel = new System.Windows.Forms.Button();
			this.m_lblHistory1 = new System.Windows.Forms.Label();
			this.m_chkUseHotKey = new System.Windows.Forms.CheckBox();
			this.m_lblHistory2 = new System.Windows.Forms.Label();
			this.m_numHistoryLen = new System.Windows.Forms.NumericUpDown();
			this.m_chkStartWithWindows = new System.Windows.Forms.CheckBox();
			this.m_chkReconnect = new System.Windows.Forms.CheckBox();
			this.m_Logo = new System.Windows.Forms.PictureBox();
			this.m_chkLog = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.m_numHistoryLen)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_Logo)).BeginInit();
			this.SuspendLayout();
			// 
			// m_txtHotKey
			// 
			this.m_txtHotKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_txtHotKey.Location = new System.Drawing.Point(104, 12);
			this.m_txtHotKey.Name = "m_txtHotKey";
			this.m_txtHotKey.Size = new System.Drawing.Size(85, 20);
			this.m_txtHotKey.TabIndex = 1;
			this.m_txtHotKey.Text = "Ctrl+Q";
			this.m_txtHotKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.m_txtHotKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_txtHotKey_KeyUp);
			this.m_txtHotKey.TextChanged += new System.EventHandler(this.m_txtHotKey_TextChanged);
			// 
			// m_btnOK
			// 
			this.m_btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOK.Image")));
			this.m_btnOK.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.m_btnOK.Location = new System.Drawing.Point(248, 84);
			this.m_btnOK.Name = "m_btnOK";
			this.m_btnOK.Size = new System.Drawing.Size(75, 23);
			this.m_btnOK.TabIndex = 8;
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
			this.m_btnCancel.Location = new System.Drawing.Point(248, 115);
			this.m_btnCancel.Name = "m_btnCancel";
			this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
			this.m_btnCancel.TabIndex = 9;
			this.m_btnCancel.Text = "&Cancel";
			this.m_btnCancel.UseVisualStyleBackColor = true;
			this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
			// 
			// m_lblHistory1
			// 
			this.m_lblHistory1.AutoSize = true;
			this.m_lblHistory1.Location = new System.Drawing.Point(9, 49);
			this.m_lblHistory1.Name = "m_lblHistory1";
			this.m_lblHistory1.Size = new System.Drawing.Size(81, 13);
			this.m_lblHistory1.TabIndex = 2;
			this.m_lblHistory1.Text = "Remember Last";
			// 
			// m_chkUseHotKey
			// 
			this.m_chkUseHotKey.AutoSize = true;
			this.m_chkUseHotKey.Checked = true;
			this.m_chkUseHotKey.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_chkUseHotKey.Location = new System.Drawing.Point(12, 15);
			this.m_chkUseHotKey.Name = "m_chkUseHotKey";
			this.m_chkUseHotKey.Size = new System.Drawing.Size(86, 17);
			this.m_chkUseHotKey.TabIndex = 0;
			this.m_chkUseHotKey.Text = "Use Hot Key";
			this.m_chkUseHotKey.UseVisualStyleBackColor = true;
			this.m_chkUseHotKey.CheckedChanged += new System.EventHandler(this.m_chkUseHotKey_CheckedChanged);
			// 
			// m_lblHistory2
			// 
			this.m_lblHistory2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_lblHistory2.AutoSize = true;
			this.m_lblHistory2.Location = new System.Drawing.Point(195, 49);
			this.m_lblHistory2.Name = "m_lblHistory2";
			this.m_lblHistory2.Size = new System.Drawing.Size(32, 13);
			this.m_lblHistory2.TabIndex = 4;
			this.m_lblHistory2.Text = "Items";
			// 
			// m_numHistoryLen
			// 
			this.m_numHistoryLen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_numHistoryLen.Location = new System.Drawing.Point(104, 47);
			this.m_numHistoryLen.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
			this.m_numHistoryLen.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.m_numHistoryLen.Name = "m_numHistoryLen";
			this.m_numHistoryLen.Size = new System.Drawing.Size(85, 20);
			this.m_numHistoryLen.TabIndex = 3;
			this.m_numHistoryLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.m_numHistoryLen.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
			// 
			// m_chkStartWithWindows
			// 
			this.m_chkStartWithWindows.AutoSize = true;
			this.m_chkStartWithWindows.Location = new System.Drawing.Point(12, 99);
			this.m_chkStartWithWindows.Name = "m_chkStartWithWindows";
			this.m_chkStartWithWindows.Size = new System.Drawing.Size(119, 17);
			this.m_chkStartWithWindows.TabIndex = 6;
			this.m_chkStartWithWindows.Text = "Load with &Windows";
			this.m_chkStartWithWindows.UseVisualStyleBackColor = true;
			// 
			// m_chkReconnect
			// 
			this.m_chkReconnect.AutoSize = true;
			this.m_chkReconnect.Location = new System.Drawing.Point(12, 76);
			this.m_chkReconnect.Name = "m_chkReconnect";
			this.m_chkReconnect.Size = new System.Drawing.Size(124, 17);
			this.m_chkReconnect.TabIndex = 5;
			this.m_chkReconnect.Text = "Automatic reconnect";
			this.m_chkReconnect.UseVisualStyleBackColor = true;
			// 
			// m_Logo
			// 
			this.m_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.m_Logo.Image = ((System.Drawing.Image)(resources.GetObject("m_Logo.Image")));
			this.m_Logo.InitialImage = ((System.Drawing.Image)(resources.GetObject("m_Logo.InitialImage")));
			this.m_Logo.Location = new System.Drawing.Point(262, 2);
			this.m_Logo.Name = "m_Logo";
			this.m_Logo.Size = new System.Drawing.Size(47, 50);
			this.m_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.m_Logo.TabIndex = 9;
			this.m_Logo.TabStop = false;
			// 
			// m_chkLog
			// 
			this.m_chkLog.AutoSize = true;
			this.m_chkLog.Location = new System.Drawing.Point(12, 122);
			this.m_chkLog.Name = "m_chkLog";
			this.m_chkLog.Size = new System.Drawing.Size(145, 17);
			this.m_chkLog.TabIndex = 7;
			this.m_chkLog.Text = "Log ( for debug purpose )";
			this.m_chkLog.UseVisualStyleBackColor = true;
			// 
			// FormSettings
			// 
			this.AcceptButton = this.m_btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.m_btnCancel;
			this.ClientSize = new System.Drawing.Size(335, 150);
			this.Controls.Add(this.m_chkLog);
			this.Controls.Add(this.m_Logo);
			this.Controls.Add(this.m_chkReconnect);
			this.Controls.Add(this.m_chkStartWithWindows);
			this.Controls.Add(this.m_numHistoryLen);
			this.Controls.Add(this.m_lblHistory2);
			this.Controls.Add(this.m_chkUseHotKey);
			this.Controls.Add(this.m_lblHistory1);
			this.Controls.Add(this.m_btnCancel);
			this.Controls.Add(this.m_btnOK);
			this.Controls.Add(this.m_txtHotKey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(500, 250);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(341, 150);
			this.Name = "FormSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " Clipboard Manager Options";
			this.Load += new System.EventHandler(this.FormSettings_Load);
			((System.ComponentModel.ISupportInitialize)(this.m_numHistoryLen)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_Logo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox m_txtHotKey;
		private System.Windows.Forms.Button m_btnOK;
		private System.Windows.Forms.Button m_btnCancel;
		private System.Windows.Forms.Label m_lblHistory1;
		private System.Windows.Forms.CheckBox m_chkUseHotKey;
		private System.Windows.Forms.Label m_lblHistory2;
		private System.Windows.Forms.NumericUpDown m_numHistoryLen;
		private System.Windows.Forms.CheckBox m_chkStartWithWindows;
		private System.Windows.Forms.CheckBox m_chkReconnect;
		private System.Windows.Forms.PictureBox m_Logo;
        private System.Windows.Forms.CheckBox m_chkLog;
	}
}