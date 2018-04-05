namespace WinFFAvi
{
  partial class FormPreferences
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferences));
      this.m_PropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.m_statusStrip = new System.Windows.Forms.StatusStrip();
      this.m_toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.m_statusStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_PropertyGrid
      // 
      this.m_PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_PropertyGrid.HelpBackColor = System.Drawing.SystemColors.Info;
      this.m_PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
      this.m_PropertyGrid.Location = new System.Drawing.Point(0, 0);
      this.m_PropertyGrid.Name = "m_PropertyGrid";
      this.m_PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.m_PropertyGrid.Size = new System.Drawing.Size(458, 251);
      this.m_PropertyGrid.TabIndex = 2;
      this.m_PropertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.m_PropertyGrid_SelectedGridItemChanged);
      this.m_PropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.m_PropertyGrid_PropertyValueChanged);
      // 
      // m_statusStrip
      // 
      this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripStatusLabel1});
      this.m_statusStrip.Location = new System.Drawing.Point(0, 251);
      this.m_statusStrip.Name = "m_statusStrip";
      this.m_statusStrip.Size = new System.Drawing.Size(458, 22);
      this.m_statusStrip.TabIndex = 3;
      this.m_statusStrip.Text = "Ready";
      // 
      // m_toolStripStatusLabel1
      // 
      this.m_toolStripStatusLabel1.Name = "m_toolStripStatusLabel1";
      this.m_toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
      this.m_toolStripStatusLabel1.Text = "Ready";
      // 
      // FormPreferences
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(458, 273);
      this.Controls.Add(this.m_PropertyGrid);
      this.Controls.Add(this.m_statusStrip);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FormPreferences";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "FormPreferences";
      this.Load += new System.EventHandler(this.FormPreferences_Load);
      this.m_statusStrip.ResumeLayout(false);
      this.m_statusStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PropertyGrid m_PropertyGrid;
    private System.Windows.Forms.StatusStrip m_statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel1;
  }
}