namespace SPUD
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
      this.m_btnOK = new System.Windows.Forms.Button();
      this.m_lnkAbout = new System.Windows.Forms.LinkLabel();
      this.m_lblVersion = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // m_btnOK
      // 
      resources.ApplyResources(this.m_btnOK, "m_btnOK");
      this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.m_btnOK.Name = "m_btnOK";
      this.m_btnOK.UseVisualStyleBackColor = true;
      // 
      // m_lnkAbout
      // 
      resources.ApplyResources(this.m_lnkAbout, "m_lnkAbout");
      this.m_lnkAbout.Name = "m_lnkAbout";
      this.m_lnkAbout.TabStop = true;
      this.m_lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lnkAbout_LinkClicked);
      // 
      // m_lblVersion
      // 
      resources.ApplyResources(this.m_lblVersion, "m_lblVersion");
      this.m_lblVersion.Name = "m_lblVersion";
      // 
      // FormAbout
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_lblVersion);
      this.Controls.Add(this.m_lnkAbout);
      this.Controls.Add(this.m_btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormAbout";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button m_btnOK;
    private System.Windows.Forms.LinkLabel m_lnkAbout;
    private System.Windows.Forms.Label m_lblVersion;
  }
}