namespace SPUD
{
  partial class CamDetailsControl
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
      this.m_cmbState = new System.Windows.Forms.ComboBox();
      this.m_lblLong = new System.Windows.Forms.Label();
      this.m_txtLon = new System.Windows.Forms.TextBox();
      this.m_lblLat = new System.Windows.Forms.Label();
      this.m_cmbType = new System.Windows.Forms.ComboBox();
      this.m_txtLat = new System.Windows.Forms.TextBox();
      this.m_cmbDirection = new System.Windows.Forms.ComboBox();
      this.m_lblDirection = new System.Windows.Forms.Label();
      this.m_lblState = new System.Windows.Forms.Label();
      this.m_lblType = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // m_cmbState
      // 
      this.m_cmbState.FormattingEnabled = true;
      this.m_cmbState.Location = new System.Drawing.Point(6, 16);
      this.m_cmbState.Name = "m_cmbState";
      this.m_cmbState.Size = new System.Drawing.Size(97, 21);
      this.m_cmbState.TabIndex = 13;
      // 
      // m_lblLong
      // 
      this.m_lblLong.AutoSize = true;
      this.m_lblLong.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.m_lblLong.Location = new System.Drawing.Point(3, 53);
      this.m_lblLong.Name = "m_lblLong";
      this.m_lblLong.Size = new System.Drawing.Size(57, 13);
      this.m_lblLong.TabIndex = 14;
      this.m_lblLong.Text = "Longtitude";
      // 
      // m_txtLon
      // 
      this.m_txtLon.Location = new System.Drawing.Point(6, 70);
      this.m_txtLon.Name = "m_txtLon";
      this.m_txtLon.Size = new System.Drawing.Size(100, 20);
      this.m_txtLon.TabIndex = 15;
      // 
      // m_lblLat
      // 
      this.m_lblLat.AutoSize = true;
      this.m_lblLat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.m_lblLat.Location = new System.Drawing.Point(119, 53);
      this.m_lblLat.Name = "m_lblLat";
      this.m_lblLat.Size = new System.Drawing.Size(45, 13);
      this.m_lblLat.TabIndex = 16;
      this.m_lblLat.Text = "Latitude";
      // 
      // m_cmbType
      // 
      this.m_cmbType.FormattingEnabled = true;
      this.m_cmbType.Location = new System.Drawing.Point(7, 166);
      this.m_cmbType.Name = "m_cmbType";
      this.m_cmbType.Size = new System.Drawing.Size(145, 21);
      this.m_cmbType.TabIndex = 21;
      // 
      // m_txtLat
      // 
      this.m_txtLat.Location = new System.Drawing.Point(122, 70);
      this.m_txtLat.Name = "m_txtLat";
      this.m_txtLat.Size = new System.Drawing.Size(100, 20);
      this.m_txtLat.TabIndex = 17;
      // 
      // m_cmbDirection
      // 
      this.m_cmbDirection.FormattingEnabled = true;
      this.m_cmbDirection.Location = new System.Drawing.Point(6, 114);
      this.m_cmbDirection.Name = "m_cmbDirection";
      this.m_cmbDirection.Size = new System.Drawing.Size(146, 21);
      this.m_cmbDirection.TabIndex = 19;
      // 
      // m_lblDirection
      // 
      this.m_lblDirection.AutoSize = true;
      this.m_lblDirection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.m_lblDirection.Location = new System.Drawing.Point(3, 97);
      this.m_lblDirection.Name = "m_lblDirection";
      this.m_lblDirection.Size = new System.Drawing.Size(49, 13);
      this.m_lblDirection.TabIndex = 18;
      this.m_lblDirection.Text = "Direction";
      // 
      // m_lblState
      // 
      this.m_lblState.AutoSize = true;
      this.m_lblState.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.m_lblState.Location = new System.Drawing.Point(3, 0);
      this.m_lblState.Name = "m_lblState";
      this.m_lblState.Size = new System.Drawing.Size(32, 13);
      this.m_lblState.TabIndex = 12;
      this.m_lblState.Text = "State";
      // 
      // m_lblType
      // 
      this.m_lblType.AutoSize = true;
      this.m_lblType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.m_lblType.Location = new System.Drawing.Point(4, 149);
      this.m_lblType.Name = "m_lblType";
      this.m_lblType.Size = new System.Drawing.Size(31, 13);
      this.m_lblType.TabIndex = 20;
      this.m_lblType.Text = "Type";
      // 
      // CamDetailsControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_cmbState);
      this.Controls.Add(this.m_lblLong);
      this.Controls.Add(this.m_txtLon);
      this.Controls.Add(this.m_lblLat);
      this.Controls.Add(this.m_cmbType);
      this.Controls.Add(this.m_txtLat);
      this.Controls.Add(this.m_cmbDirection);
      this.Controls.Add(this.m_lblDirection);
      this.Controls.Add(this.m_lblState);
      this.Controls.Add(this.m_lblType);
      this.Name = "CamDetailsControl";
      this.Size = new System.Drawing.Size(222, 187);
      this.Load += new System.EventHandler(this.CamDetailsControl_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox m_cmbState;
    private System.Windows.Forms.Label m_lblLong;
    private System.Windows.Forms.TextBox m_txtLon;
    private System.Windows.Forms.Label m_lblLat;
    private System.Windows.Forms.ComboBox m_cmbType;
    private System.Windows.Forms.TextBox m_txtLat;
    private System.Windows.Forms.ComboBox m_cmbDirection;
    private System.Windows.Forms.Label m_lblDirection;
    private System.Windows.Forms.Label m_lblState;
    private System.Windows.Forms.Label m_lblType;
  }
}
