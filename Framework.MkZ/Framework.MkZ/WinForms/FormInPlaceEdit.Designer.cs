namespace MZ.WinForms
{
    partial class FormInPlaceEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInPlaceEdit));
            this.m_txtInput = new System.Windows.Forms.TextBox();
            this.m_pnlText = new System.Windows.Forms.Panel();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_pnlText.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_txtInput
            // 
            this.m_txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_txtInput.Location = new System.Drawing.Point(5, 4);
            this.m_txtInput.Margin = new System.Windows.Forms.Padding(6);
            this.m_txtInput.Name = "m_txtInput";
            this.m_txtInput.Size = new System.Drawing.Size(183, 19);
            this.m_txtInput.TabIndex = 0;
            this.m_txtInput.Text = "Hello World";
            this.m_txtInput.TextChanged += new System.EventHandler(this.m_txtInput_TextChanged);
            // 
            // m_pnlText
            // 
            this.m_pnlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlText.BackColor = System.Drawing.SystemColors.Window;
            this.m_pnlText.Controls.Add(this.m_txtInput);
            this.m_pnlText.Location = new System.Drawing.Point(5, 4);
            this.m_pnlText.Name = "m_pnlText";
            this.m_pnlText.Size = new System.Drawing.Size(192, 26);
            this.m_pnlText.TabIndex = 2;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(201, 3);
            this.m_btnOk.Margin = new System.Windows.Forms.Padding(6);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(29, 28);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.Location = new System.Drawing.Point(230, 3);
            this.m_btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(29, 28);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // FormInPlaceEdit
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(263, 35);
            this.ControlBox = false;
            this.Controls.Add(this.m_pnlText);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FormInPlaceEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Deactivate += new System.EventHandler(this.FormInPlaceEdit_Deactivate);
            this.Load += new System.EventHandler(this.FormInPlaceEdit_Load);
            this.m_pnlText.ResumeLayout(false);
            this.m_pnlText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox m_txtInput;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Panel m_pnlText;
    }
}