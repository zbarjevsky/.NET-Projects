namespace ClipboardManager
{
	partial class FormEncodings
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
            this.m_listEncodings = new System.Windows.Forms.ListView();
            this.m_columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_listEncodings
            // 
            this.m_listEncodings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_listEncodings.CheckBoxes = true;
            this.m_listEncodings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_columnHeader1,
            this.m_columnHeader2,
            this.m_columnHeader3,
            this.m_columnHeader4,
            this.m_columnHeader5});
            this.m_listEncodings.FullRowSelect = true;
            this.m_listEncodings.GridLines = true;
            this.m_listEncodings.Location = new System.Drawing.Point(12, 12);
            this.m_listEncodings.Name = "m_listEncodings";
            this.m_listEncodings.Size = new System.Drawing.Size(512, 402);
            this.m_listEncodings.TabIndex = 0;
            this.m_listEncodings.UseCompatibleStateImageBehavior = false;
            this.m_listEncodings.View = System.Windows.Forms.View.Details;
            // 
            // m_columnHeader1
            // 
            this.m_columnHeader1.Text = "Encoding";
            this.m_columnHeader1.Width = 150;
            // 
            // m_columnHeader2
            // 
            this.m_columnHeader2.Text = "Code Page";
            this.m_columnHeader2.Width = 80;
            // 
            // m_columnHeader3
            // 
            this.m_columnHeader3.Text = "Body Name";
            this.m_columnHeader3.Width = 100;
            // 
            // m_columnHeader4
            // 
            this.m_columnHeader4.Text = "Header Name";
            this.m_columnHeader4.Width = 80;
            // 
            // m_columnHeader5
            // 
            this.m_columnHeader5.Text = "Web Name";
            this.m_columnHeader5.Width = 80;
            // 
            // m_btnOK
            // 
            this.m_btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Location = new System.Drawing.Point(542, 362);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(75, 23);
            this.m_btnOK.TabIndex = 1;
            this.m_btnOK.Text = "&OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.m_btnOK_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(542, 391);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 2;
            this.m_btnCancel.Text = "&Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // FormEncodings
            // 
            this.AcceptButton = this.m_btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(629, 426);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_listEncodings);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "FormEncodings";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Encodings";
            this.Load += new System.EventHandler(this.FormEncodings_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView m_listEncodings;
		private System.Windows.Forms.Button m_btnOK;
		private System.Windows.Forms.Button m_btnCancel;
		private System.Windows.Forms.ColumnHeader m_columnHeader1;
		private System.Windows.Forms.ColumnHeader m_columnHeader2;
		private System.Windows.Forms.ColumnHeader m_columnHeader3;
		private System.Windows.Forms.ColumnHeader m_columnHeader4;
		private System.Windows.Forms.ColumnHeader m_columnHeader5;
	}
}