namespace AnimEffectApp
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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.m_txtColor = new System.Windows.Forms.TextBox();
            this.m_btnColor = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.m_status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_status2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(66, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(265, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(66, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(265, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // m_txtColor
            // 
            this.m_txtColor.Location = new System.Drawing.Point(66, 85);
            this.m_txtColor.Name = "m_txtColor";
            this.m_txtColor.Size = new System.Drawing.Size(186, 20);
            this.m_txtColor.TabIndex = 2;
            // 
            // m_btnColor
            // 
            this.m_btnColor.Location = new System.Drawing.Point(256, 83);
            this.m_btnColor.Name = "m_btnColor";
            this.m_btnColor.Size = new System.Drawing.Size(75, 23);
            this.m_btnColor.TabIndex = 3;
            this.m_btnColor.Text = "Color";
            this.m_btnColor.UseVisualStyleBackColor = true;
            this.m_btnColor.Click += new System.EventHandler(this.m_btnColor_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_status1,
            this.m_status2,
            this.m_statusProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 197);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(403, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // m_status1
            // 
            this.m_status1.Name = "m_status1";
            this.m_status1.Size = new System.Drawing.Size(16, 17);
            this.m_status1.Text = "...";
            // 
            // m_status2
            // 
            this.m_status2.Name = "m_status2";
            this.m_status2.Size = new System.Drawing.Size(139, 17);
            this.m_status2.Spring = true;
            this.m_status2.Text = "---";
            // 
            // m_statusProgress
            // 
            this.m_statusProgress.Name = "m_statusProgress";
            this.m_statusProgress.Size = new System.Drawing.Size(200, 16);
            this.m_statusProgress.Step = 1;
            this.m_statusProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 219);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.m_btnColor);
            this.Controls.Add(this.m_txtColor);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox m_txtColor;
		private System.Windows.Forms.Button m_btnColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel m_status1;
        private System.Windows.Forms.ToolStripStatusLabel m_status2;
        private System.Windows.Forms.ToolStripProgressBar m_statusProgress;
    }
}

