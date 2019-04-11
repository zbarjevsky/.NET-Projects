namespace MeditationStopWatch
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_pnlCommands = new System.Windows.Forms.Panel();
            this.m_pnlText = new System.Windows.Forms.Panel();
            this.m_pnlMain = new System.Windows.Forms.Panel();
            this.m_pnlCommands.SuspendLayout();
            this.m_pnlText.SuspendLayout();
            this.m_pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(120, 23);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOk.Image = ((System.Drawing.Image)(resources.GetObject("m_btnOk.Image")));
            this.m_btnOk.Location = new System.Drawing.Point(6, 2);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(20, 20);
            this.m_btnOk.TabIndex = 0;
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
            this.m_btnCancel.Location = new System.Drawing.Point(30, 2);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(20, 20);
            this.m_btnCancel.TabIndex = 1;
            this.m_btnCancel.UseVisualStyleBackColor = true;
            this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
            // 
            // m_pnlCommands
            // 
            this.m_pnlCommands.Controls.Add(this.m_btnOk);
            this.m_pnlCommands.Controls.Add(this.m_btnCancel);
            this.m_pnlCommands.Dock = System.Windows.Forms.DockStyle.Right;
            this.m_pnlCommands.Location = new System.Drawing.Point(120, 0);
            this.m_pnlCommands.Name = "m_pnlCommands";
            this.m_pnlCommands.Size = new System.Drawing.Size(54, 23);
            this.m_pnlCommands.TabIndex = 0;
            // 
            // m_pnlText
            // 
            this.m_pnlText.Controls.Add(this.richTextBox1);
            this.m_pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlText.Location = new System.Drawing.Point(0, 0);
            this.m_pnlText.Name = "m_pnlText";
            this.m_pnlText.Size = new System.Drawing.Size(120, 23);
            this.m_pnlText.TabIndex = 4;
            // 
            // m_pnlMain
            // 
            this.m_pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_pnlMain.BackColor = System.Drawing.SystemColors.Control;
            this.m_pnlMain.Controls.Add(this.m_pnlText);
            this.m_pnlMain.Controls.Add(this.m_pnlCommands);
            this.m_pnlMain.Location = new System.Drawing.Point(3, 3);
            this.m_pnlMain.Name = "m_pnlMain";
            this.m_pnlMain.Size = new System.Drawing.Size(174, 23);
            this.m_pnlMain.TabIndex = 1;
            // 
            // FormInPlaceEdit
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(180, 30);
            this.ControlBox = false;
            this.Controls.Add(this.m_pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormInPlaceEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormInPlaceEdit";
            this.Deactivate += new System.EventHandler(this.FormInPlaceEdit_Deactivate);
            this.Load += new System.EventHandler(this.FormInPlaceEdit_Load);
            this.m_pnlCommands.ResumeLayout(false);
            this.m_pnlText.ResumeLayout(false);
            this.m_pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Panel m_pnlCommands;
        private System.Windows.Forms.Panel m_pnlText;
        private System.Windows.Forms.Panel m_pnlMain;
    }
}