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
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(403, 198);
			this.Controls.Add(this.m_btnColor);
			this.Controls.Add(this.m_txtColor);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button1);
			this.Name = "FormMain";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox m_txtColor;
		private System.Windows.Forms.Button m_btnColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
	}
}

