namespace DUMeterMZ
{
	partial class FormAbout
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
			if ( disposing && (components != null) )
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_richTextBoxAbout = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.m_lnkMailTo = new System.Windows.Forms.LinkLabel();
            this.m_contextMenuStrip_Mail = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_contextMenuStrip_Mail_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStrip_Mail_Send = new System.Windows.Forms.ToolStripMenuItem();
            this.m_lblAbout = new System.Windows.Forms.Label();
            this.m_lblCpyright = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.m_contextMenuStrip_Mail.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOK.Image")));
            this.m_btnOK.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.m_btnOK.Location = new System.Drawing.Point(348, 12);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 4;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_richTextBoxAbout
            // 
            this.m_richTextBoxAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_richTextBoxAbout.BackColor = System.Drawing.SystemColors.Info;
            this.m_richTextBoxAbout.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_richTextBoxAbout.Location = new System.Drawing.Point(12, 92);
            this.m_richTextBoxAbout.Name = "m_richTextBoxAbout";
            this.m_richTextBoxAbout.ReadOnly = true;
            this.m_richTextBoxAbout.Size = new System.Drawing.Size(411, 169);
            this.m_richTextBoxAbout.TabIndex = 3;
            this.m_richTextBoxAbout.Text = "";
            this.m_richTextBoxAbout.WordWrap = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // m_lnkMailTo
            // 
            this.m_lnkMailTo.AutoSize = true;
            this.m_lnkMailTo.ContextMenuStrip = this.m_contextMenuStrip_Mail;
            this.m_lnkMailTo.LinkArea = new System.Windows.Forms.LinkArea(9, 37);
            this.m_lnkMailTo.Location = new System.Drawing.Point(73, 63);
            this.m_lnkMailTo.Name = "m_lnkMailTo";
            this.m_lnkMailTo.Size = new System.Drawing.Size(251, 17);
            this.m_lnkMailTo.TabIndex = 2;
            this.m_lnkMailTo.TabStop = true;
            this.m_lnkMailTo.Text = "Mail to: Mark Zbarjevsky(zbarjevsky@gmail.com)";
            this.m_lnkMailTo.UseCompatibleTextRendering = true;
            this.m_lnkMailTo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkMailTo_LinkClicked);
            // 
            // m_contextMenuStrip_Mail
            // 
            this.m_contextMenuStrip_Mail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_contextMenuStrip_Mail_Copy,
            this.m_contextMenuStrip_Mail_Send});
            this.m_contextMenuStrip_Mail.Name = "m_contextMenuStrip_Mail";
            this.m_contextMenuStrip_Mail.Size = new System.Drawing.Size(145, 48);
            // 
            // m_contextMenuStrip_Mail_Copy
            // 
            this.m_contextMenuStrip_Mail_Copy.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStrip_Mail_Copy.Image")));
            this.m_contextMenuStrip_Mail_Copy.Name = "m_contextMenuStrip_Mail_Copy";
            this.m_contextMenuStrip_Mail_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.m_contextMenuStrip_Mail_Copy.Size = new System.Drawing.Size(144, 22);
            this.m_contextMenuStrip_Mail_Copy.Text = "&Copy";
            this.m_contextMenuStrip_Mail_Copy.Click += new System.EventHandler(this.m_contextMenuStrip_Mail_Copy_Click);
            // 
            // m_contextMenuStrip_Mail_Send
            // 
            this.m_contextMenuStrip_Mail_Send.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStrip_Mail_Send.Image")));
            this.m_contextMenuStrip_Mail_Send.Name = "m_contextMenuStrip_Mail_Send";
            this.m_contextMenuStrip_Mail_Send.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.m_contextMenuStrip_Mail_Send.Size = new System.Drawing.Size(144, 22);
            this.m_contextMenuStrip_Mail_Send.Text = "S&end";
            this.m_contextMenuStrip_Mail_Send.Click += new System.EventHandler(this.m_contextMenuStrip_Mail_Send_Click);
            // 
            // m_lblAbout
            // 
            this.m_lblAbout.AutoSize = true;
            this.m_lblAbout.Location = new System.Drawing.Point(73, 22);
            this.m_lblAbout.Name = "m_lblAbout";
            this.m_lblAbout.Size = new System.Drawing.Size(107, 13);
            this.m_lblAbout.TabIndex = 0;
            this.m_lblAbout.Text = "DU Meter v1.0.234.2";
            // 
            // m_lblCpyright
            // 
            this.m_lblCpyright.AutoSize = true;
            this.m_lblCpyright.Location = new System.Drawing.Point(73, 42);
            this.m_lblCpyright.Name = "m_lblCpyright";
            this.m_lblCpyright.Size = new System.Drawing.Size(120, 13);
            this.m_lblCpyright.TabIndex = 1;
            this.m_lblCpyright.Text = "DU Meter Mark Z. 2020";
            // 
            // FormAbout
            // 
            this.AcceptButton = this.m_btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnOK;
            this.ClientSize = new System.Drawing.Size(435, 273);
            this.Controls.Add(this.m_lblCpyright);
            this.Controls.Add(this.m_lblAbout);
            this.Controls.Add(this.m_lnkMailTo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.m_richTextBoxAbout);
            this.Controls.Add(this.m_btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "FormAbout";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.m_contextMenuStrip_Mail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button m_btnOK;
		private System.Windows.Forms.RichTextBox m_richTextBoxAbout;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.LinkLabel m_lnkMailTo;
		private System.Windows.Forms.Label m_lblAbout;
		private System.Windows.Forms.Label m_lblCpyright;
		private System.Windows.Forms.ContextMenuStrip m_contextMenuStrip_Mail;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStrip_Mail_Copy;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStrip_Mail_Send;
	}
}