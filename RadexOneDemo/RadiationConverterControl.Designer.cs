namespace RadexOneDemo
{
    partial class RadiationConverterControl
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
            this.m_numFrom = new SpecialNumericUpDown();
            this.m_numTo = new SpecialNumericUpDown();
            this.m_cmbFrom = new System.Windows.Forms.ComboBox();
            this.m_cmbTo = new System.Windows.Forms.ComboBox();
            this.m_lblFrom = new System.Windows.Forms.Label();
            this.m_lblTo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_numFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numTo)).BeginInit();
            this.SuspendLayout();
            // 
            // m_numFrom
            // 
            this.m_numFrom.DecimalPlaces = 6;
            this.m_numFrom.Location = new System.Drawing.Point(55, 0);
            this.m_numFrom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_numFrom.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.m_numFrom.Name = "m_numFrom";
            this.m_numFrom.Size = new System.Drawing.Size(181, 26);
            this.m_numFrom.TabIndex = 0;
            this.m_numFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numFrom.ThousandsSeparator = true;
            this.m_numFrom.ValueChanged += new System.EventHandler(this.OnChange);
            this.m_numFrom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_numFrom_KeyUp);
            // 
            // m_numTo
            // 
            this.m_numTo.DecimalPlaces = 6;
            this.m_numTo.Location = new System.Drawing.Point(55, 33);
            this.m_numTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_numTo.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.m_numTo.Name = "m_numTo";
            this.m_numTo.Size = new System.Drawing.Size(181, 26);
            this.m_numTo.TabIndex = 1;
            this.m_numTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_numTo.ThousandsSeparator = true;
            this.m_numTo.ValueChanged += new System.EventHandler(this.OnChange);
            this.m_numTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_numFrom_KeyUp);
            // 
            // m_cmbFrom
            // 
            this.m_cmbFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbFrom.FormattingEnabled = true;
            this.m_cmbFrom.Items.AddRange(new object[] {
            "Sievert (Sv/h) ",
            "milliSievert (mSv/h) ",
            "microSieverts (µSv/h)",
            "Röntgen/h (rem/h)",
            "milliRöntgen (mrem/h) "});
            this.m_cmbFrom.Location = new System.Drawing.Point(244, 0);
            this.m_cmbFrom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_cmbFrom.Name = "m_cmbFrom";
            this.m_cmbFrom.Size = new System.Drawing.Size(199, 28);
            this.m_cmbFrom.TabIndex = 2;
            this.m_cmbFrom.SelectedIndexChanged += new System.EventHandler(this.OnChange);
            // 
            // m_cmbTo
            // 
            this.m_cmbTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbTo.FormattingEnabled = true;
            this.m_cmbTo.Items.AddRange(new object[] {
            "Sievert (Sv/h) ",
            "milliSievert (mSv/h) ",
            "microSieverts (µSv/h)",
            "Röntgen/h (rem/h)",
            "milliRöntgen (mrem/h) "});
            this.m_cmbTo.Location = new System.Drawing.Point(244, 31);
            this.m_cmbTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.m_cmbTo.Name = "m_cmbTo";
            this.m_cmbTo.Size = new System.Drawing.Size(199, 28);
            this.m_cmbTo.TabIndex = 3;
            this.m_cmbTo.SelectedIndexChanged += new System.EventHandler(this.OnChange);
            // 
            // m_lblFrom
            // 
            this.m_lblFrom.AutoSize = true;
            this.m_lblFrom.Location = new System.Drawing.Point(1, 3);
            this.m_lblFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblFrom.Name = "m_lblFrom";
            this.m_lblFrom.Size = new System.Drawing.Size(48, 20);
            this.m_lblFrom.TabIndex = 4;
            this.m_lblFrom.Text = "From";
            // 
            // m_lblTo
            // 
            this.m_lblTo.AutoSize = true;
            this.m_lblTo.Location = new System.Drawing.Point(1, 37);
            this.m_lblTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.m_lblTo.Name = "m_lblTo";
            this.m_lblTo.Size = new System.Drawing.Size(33, 20);
            this.m_lblTo.TabIndex = 5;
            this.m_lblTo.Text = "To:";
            // 
            // RadiationConverterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_lblTo);
            this.Controls.Add(this.m_lblFrom);
            this.Controls.Add(this.m_cmbTo);
            this.Controls.Add(this.m_cmbFrom);
            this.Controls.Add(this.m_numTo);
            this.Controls.Add(this.m_numFrom);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RadiationConverterControl";
            this.Size = new System.Drawing.Size(450, 59);
            this.Load += new System.EventHandler(this.RadiationConverterControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_numFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_numTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SpecialNumericUpDown m_numFrom;
        private SpecialNumericUpDown m_numTo;
        private System.Windows.Forms.ComboBox m_cmbFrom;
        private System.Windows.Forms.ComboBox m_cmbTo;
        private System.Windows.Forms.Label m_lblFrom;
        private System.Windows.Forms.Label m_lblTo;
    }
}
