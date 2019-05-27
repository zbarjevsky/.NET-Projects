namespace ClipboardManager.Utils
{
    partial class HotKeyEditorUserControl
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
            this.m_chkUseHotKey = new System.Windows.Forms.CheckBox();
            this.m_txtHotKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_chkUseHotKey
            // 
            this.m_chkUseHotKey.AutoSize = true;
            this.m_chkUseHotKey.Checked = true;
            this.m_chkUseHotKey.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkUseHotKey.Location = new System.Drawing.Point(3, 6);
            this.m_chkUseHotKey.Name = "m_chkUseHotKey";
            this.m_chkUseHotKey.Size = new System.Drawing.Size(86, 17);
            this.m_chkUseHotKey.TabIndex = 2;
            this.m_chkUseHotKey.Text = "Use Hot Key";
            this.m_chkUseHotKey.UseVisualStyleBackColor = true;
            this.m_chkUseHotKey.CheckedChanged += new System.EventHandler(this.m_chkUseHotKey_CheckedChanged);
            // 
            // m_txtHotKey
            // 
            this.m_txtHotKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtHotKey.Location = new System.Drawing.Point(95, 3);
            this.m_txtHotKey.Name = "m_txtHotKey";
            this.m_txtHotKey.Size = new System.Drawing.Size(187, 20);
            this.m_txtHotKey.TabIndex = 3;
            this.m_txtHotKey.Text = "Ctrl+Q";
            this.m_txtHotKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_txtHotKey.TextChanged += new System.EventHandler(this.m_txtHotKey_TextChanged);
            this.m_txtHotKey.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_txtHotKey_KeyUp);
            // 
            // HotKeyEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_chkUseHotKey);
            this.Controls.Add(this.m_txtHotKey);
            this.Name = "HotKeyEditorUserControl";
            this.Size = new System.Drawing.Size(285, 26);
            this.Load += new System.EventHandler(this.HotKeyEditorUserControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox m_chkUseHotKey;
        private System.Windows.Forms.TextBox m_txtHotKey;
    }
}
