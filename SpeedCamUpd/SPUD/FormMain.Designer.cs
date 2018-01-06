namespace SPUD
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.m_splitContainerDetails = new System.Windows.Forms.SplitContainer();
            this.m_pnlDetails = new System.Windows.Forms.Panel();
            this.m_cmbState = new System.Windows.Forms.ComboBox();
            this.m_btnAdd = new System.Windows.Forms.Button();
            this.m_lblLong = new System.Windows.Forms.Label();
            this.m_btnSave = new System.Windows.Forms.Button();
            this.m_txtLong = new System.Windows.Forms.TextBox();
            this.m_lblLat = new System.Windows.Forms.Label();
            this.m_cmbType = new System.Windows.Forms.ComboBox();
            this.m_txtLat = new System.Windows.Forms.TextBox();
            this.m_cmbDirection = new System.Windows.Forms.ComboBox();
            this.m_lblDirection = new System.Windows.Forms.Label();
            this.m_lblState = new System.Windows.Forms.Label();
            this.m_lblType = new System.Windows.Forms.Label();
            this.m_pnlMap = new System.Windows.Forms.Panel();
            this.m_lblPos = new System.Windows.Forms.LinkLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.m_tbbtnShowAllOnMap = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnRefreshMap = new System.Windows.Forms.ToolStripButton();
            this.m_txtFileName = new System.Windows.Forms.TextBox();
            this.m_openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.m_dataGridViewSpeed = new System.Windows.Forms.DataGridView();
            this.m_menuStripMain = new System.Windows.Forms.MenuStrip();
            this.m_mnuFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mnuExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnu_AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnuHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mnu_AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_pnlTools = new System.Windows.Forms.Panel();
            this.m_chkDeleted = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.m_tbbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnAppendFile = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tbbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.m_tbbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.m_splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_btnTest = new System.Windows.Forms.ToolStripButton();
            this.m_splitContainerDetails.Panel1.SuspendLayout();
            this.m_splitContainerDetails.Panel2.SuspendLayout();
            this.m_splitContainerDetails.SuspendLayout();
            this.m_pnlDetails.SuspendLayout();
            this.m_pnlMap.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridViewSpeed)).BeginInit();
            this.m_menuStripMain.SuspendLayout();
            this.m_pnlTools.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.m_statusStrip.SuspendLayout();
            this.m_splitContainerMain.Panel1.SuspendLayout();
            this.m_splitContainerMain.Panel2.SuspendLayout();
            this.m_splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_splitContainerDetails
            // 
            this.m_splitContainerDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.m_splitContainerDetails, "m_splitContainerDetails");
            this.m_splitContainerDetails.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.m_splitContainerDetails.Name = "m_splitContainerDetails";
            // 
            // m_splitContainerDetails.Panel1
            // 
            this.m_splitContainerDetails.Panel1.Controls.Add(this.m_pnlDetails);
            // 
            // m_splitContainerDetails.Panel2
            // 
            resources.ApplyResources(this.m_splitContainerDetails.Panel2, "m_splitContainerDetails.Panel2");
            this.m_splitContainerDetails.Panel2.BackgroundImage = global::SPUD.Properties.Resources.israel_map;
            this.m_splitContainerDetails.Panel2.Controls.Add(this.m_pnlMap);
            this.m_splitContainerDetails.Panel2.Controls.Add(this.toolStrip2);
            // 
            // m_pnlDetails
            // 
            resources.ApplyResources(this.m_pnlDetails, "m_pnlDetails");
            this.m_pnlDetails.Controls.Add(this.m_cmbState);
            this.m_pnlDetails.Controls.Add(this.m_btnAdd);
            this.m_pnlDetails.Controls.Add(this.m_lblLong);
            this.m_pnlDetails.Controls.Add(this.m_btnSave);
            this.m_pnlDetails.Controls.Add(this.m_txtLong);
            this.m_pnlDetails.Controls.Add(this.m_lblLat);
            this.m_pnlDetails.Controls.Add(this.m_cmbType);
            this.m_pnlDetails.Controls.Add(this.m_txtLat);
            this.m_pnlDetails.Controls.Add(this.m_cmbDirection);
            this.m_pnlDetails.Controls.Add(this.m_lblDirection);
            this.m_pnlDetails.Controls.Add(this.m_lblState);
            this.m_pnlDetails.Controls.Add(this.m_lblType);
            this.m_pnlDetails.Name = "m_pnlDetails";
            // 
            // m_cmbState
            // 
            this.m_cmbState.FormattingEnabled = true;
            resources.ApplyResources(this.m_cmbState, "m_cmbState");
            this.m_cmbState.Name = "m_cmbState";
            // 
            // m_btnAdd
            // 
            this.m_btnAdd.Image = global::SPUD.Properties.Resources.New;
            resources.ApplyResources(this.m_btnAdd, "m_btnAdd");
            this.m_btnAdd.Name = "m_btnAdd";
            this.m_btnAdd.UseVisualStyleBackColor = true;
            this.m_btnAdd.Click += new System.EventHandler(this.m_btnAddRow_Click);
            // 
            // m_lblLong
            // 
            resources.ApplyResources(this.m_lblLong, "m_lblLong");
            this.m_lblLong.Name = "m_lblLong";
            // 
            // m_btnSave
            // 
            this.m_btnSave.Image = global::SPUD.Properties.Resources.Save1;
            resources.ApplyResources(this.m_btnSave, "m_btnSave");
            this.m_btnSave.Name = "m_btnSave";
            this.m_btnSave.UseVisualStyleBackColor = true;
            this.m_btnSave.Click += new System.EventHandler(this.m_btnSaveRow_Click);
            // 
            // m_txtLong
            // 
            resources.ApplyResources(this.m_txtLong, "m_txtLong");
            this.m_txtLong.Name = "m_txtLong";
            this.m_txtLong.TextChanged += new System.EventHandler(this.m_txtLong_TextChanged);
            // 
            // m_lblLat
            // 
            resources.ApplyResources(this.m_lblLat, "m_lblLat");
            this.m_lblLat.Name = "m_lblLat";
            // 
            // m_cmbType
            // 
            this.m_cmbType.FormattingEnabled = true;
            resources.ApplyResources(this.m_cmbType, "m_cmbType");
            this.m_cmbType.Name = "m_cmbType";
            // 
            // m_txtLat
            // 
            resources.ApplyResources(this.m_txtLat, "m_txtLat");
            this.m_txtLat.Name = "m_txtLat";
            this.m_txtLat.TextChanged += new System.EventHandler(this.m_txtLat_TextChanged);
            // 
            // m_cmbDirection
            // 
            this.m_cmbDirection.FormattingEnabled = true;
            resources.ApplyResources(this.m_cmbDirection, "m_cmbDirection");
            this.m_cmbDirection.Name = "m_cmbDirection";
            // 
            // m_lblDirection
            // 
            resources.ApplyResources(this.m_lblDirection, "m_lblDirection");
            this.m_lblDirection.Name = "m_lblDirection";
            // 
            // m_lblState
            // 
            resources.ApplyResources(this.m_lblState, "m_lblState");
            this.m_lblState.Name = "m_lblState";
            // 
            // m_lblType
            // 
            resources.ApplyResources(this.m_lblType, "m_lblType");
            this.m_lblType.Name = "m_lblType";
            // 
            // m_pnlMap
            // 
            this.m_pnlMap.BackgroundImage = global::SPUD.Properties.Resources.israel_map1;
            resources.ApplyResources(this.m_pnlMap, "m_pnlMap");
            this.m_pnlMap.Controls.Add(this.m_lblPos);
            this.m_pnlMap.Name = "m_pnlMap";
            // 
            // m_lblPos
            // 
            resources.ApplyResources(this.m_lblPos, "m_lblPos");
            this.m_lblPos.BackColor = System.Drawing.Color.Transparent;
            this.m_lblPos.Name = "m_lblPos";
            this.m_lblPos.TabStop = true;
            this.m_ToolTip.SetToolTip(this.m_lblPos, resources.GetString("m_lblPos.ToolTip"));
            this.m_lblPos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.m_lblPos_LinkClicked);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tbbtnShowAllOnMap,
            this.m_tbbtnRefreshMap});
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.Name = "toolStrip2";
            // 
            // m_tbbtnShowAllOnMap
            // 
            resources.ApplyResources(this.m_tbbtnShowAllOnMap, "m_tbbtnShowAllOnMap");
            this.m_tbbtnShowAllOnMap.Name = "m_tbbtnShowAllOnMap";
            this.m_tbbtnShowAllOnMap.Click += new System.EventHandler(this.m_tbbtnShowAllOnMap_Click);
            // 
            // m_tbbtnRefreshMap
            // 
            resources.ApplyResources(this.m_tbbtnRefreshMap, "m_tbbtnRefreshMap");
            this.m_tbbtnRefreshMap.Name = "m_tbbtnRefreshMap";
            this.m_tbbtnRefreshMap.Click += new System.EventHandler(this.m_tbbtnRefreshMap_Click);
            // 
            // m_txtFileName
            // 
            resources.ApplyResources(this.m_txtFileName, "m_txtFileName");
            this.m_txtFileName.Name = "m_txtFileName";
            // 
            // m_openFileDialog
            // 
            this.m_openFileDialog.FileName = "*.*";
            resources.ApplyResources(this.m_openFileDialog, "m_openFileDialog");
            // 
            // m_dataGridViewSpeed
            // 
            this.m_dataGridViewSpeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_dataGridViewSpeed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.m_dataGridViewSpeed, "m_dataGridViewSpeed");
            this.m_dataGridViewSpeed.Name = "m_dataGridViewSpeed";
            this.m_dataGridViewSpeed.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.m_dataGridViewSpeed_ColumnHeaderMouseClick);
            this.m_dataGridViewSpeed.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.m_dataGridViewSpeed_DataError);
            this.m_dataGridViewSpeed.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dataGridViewSpeed_RowEnter);
            // 
            // m_menuStripMain
            // 
            this.m_menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuFileToolStripMenuItem,
            this.m_mnuEditToolStripMenuItem,
            this.m_mnuHelpToolStripMenuItem});
            resources.ApplyResources(this.m_menuStripMain, "m_menuStripMain");
            this.m_menuStripMain.Name = "m_menuStripMain";
            // 
            // m_mnuFileToolStripMenuItem
            // 
            this.m_mnuFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnuOpenToolStripMenuItem,
            this.m_mnuSaveToolStripMenuItem,
            this.m_mnuCloseToolStripMenuItem,
            this.toolStripSeparator1,
            this.m_mnuExitToolStripMenuItem});
            this.m_mnuFileToolStripMenuItem.Name = "m_mnuFileToolStripMenuItem";
            resources.ApplyResources(this.m_mnuFileToolStripMenuItem, "m_mnuFileToolStripMenuItem");
            // 
            // m_mnuOpenToolStripMenuItem
            // 
            resources.ApplyResources(this.m_mnuOpenToolStripMenuItem, "m_mnuOpenToolStripMenuItem");
            this.m_mnuOpenToolStripMenuItem.Name = "m_mnuOpenToolStripMenuItem";
            this.m_mnuOpenToolStripMenuItem.Click += new System.EventHandler(this.m_mnuOpenToolStripMenuItem_Click);
            // 
            // m_mnuSaveToolStripMenuItem
            // 
            this.m_mnuSaveToolStripMenuItem.Image = global::SPUD.Properties.Resources.Save1;
            this.m_mnuSaveToolStripMenuItem.Name = "m_mnuSaveToolStripMenuItem";
            resources.ApplyResources(this.m_mnuSaveToolStripMenuItem, "m_mnuSaveToolStripMenuItem");
            this.m_mnuSaveToolStripMenuItem.Click += new System.EventHandler(this.m_mnuSaveToolStripMenuItem_Click);
            // 
            // m_mnuCloseToolStripMenuItem
            // 
            this.m_mnuCloseToolStripMenuItem.Name = "m_mnuCloseToolStripMenuItem";
            resources.ApplyResources(this.m_mnuCloseToolStripMenuItem, "m_mnuCloseToolStripMenuItem");
            this.m_mnuCloseToolStripMenuItem.Click += new System.EventHandler(this.m_mnuCloseToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // m_mnuExitToolStripMenuItem
            // 
            this.m_mnuExitToolStripMenuItem.Name = "m_mnuExitToolStripMenuItem";
            resources.ApplyResources(this.m_mnuExitToolStripMenuItem, "m_mnuExitToolStripMenuItem");
            this.m_mnuExitToolStripMenuItem.Click += new System.EventHandler(this.m_mnuExitToolStripMenuItem_Click);
            // 
            // m_mnuEditToolStripMenuItem
            // 
            this.m_mnuEditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnu_AddToolStripMenuItem,
            this.m_mnuDeleteToolStripMenuItem});
            this.m_mnuEditToolStripMenuItem.Name = "m_mnuEditToolStripMenuItem";
            resources.ApplyResources(this.m_mnuEditToolStripMenuItem, "m_mnuEditToolStripMenuItem");
            // 
            // m_mnu_AddToolStripMenuItem
            // 
            this.m_mnu_AddToolStripMenuItem.Image = global::SPUD.Properties.Resources.New;
            this.m_mnu_AddToolStripMenuItem.Name = "m_mnu_AddToolStripMenuItem";
            resources.ApplyResources(this.m_mnu_AddToolStripMenuItem, "m_mnu_AddToolStripMenuItem");
            this.m_mnu_AddToolStripMenuItem.Click += new System.EventHandler(this.m_mnu_AddToolStripMenuItem_Click);
            // 
            // m_mnuDeleteToolStripMenuItem
            // 
            this.m_mnuDeleteToolStripMenuItem.Image = global::SPUD.Properties.Resources.CorrectionDelete;
            this.m_mnuDeleteToolStripMenuItem.Name = "m_mnuDeleteToolStripMenuItem";
            resources.ApplyResources(this.m_mnuDeleteToolStripMenuItem, "m_mnuDeleteToolStripMenuItem");
            this.m_mnuDeleteToolStripMenuItem.Click += new System.EventHandler(this.m_mnuDeleteToolStripMenuItem_Click);
            // 
            // m_mnuHelpToolStripMenuItem
            // 
            this.m_mnuHelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mnu_AboutToolStripMenuItem});
            this.m_mnuHelpToolStripMenuItem.Name = "m_mnuHelpToolStripMenuItem";
            resources.ApplyResources(this.m_mnuHelpToolStripMenuItem, "m_mnuHelpToolStripMenuItem");
            // 
            // m_mnu_AboutToolStripMenuItem
            // 
            this.m_mnu_AboutToolStripMenuItem.Name = "m_mnu_AboutToolStripMenuItem";
            resources.ApplyResources(this.m_mnu_AboutToolStripMenuItem, "m_mnu_AboutToolStripMenuItem");
            this.m_mnu_AboutToolStripMenuItem.Click += new System.EventHandler(this.m_mnu_AboutToolStripMenuItem_Click);
            // 
            // m_pnlTools
            // 
            this.m_pnlTools.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlTools.Controls.Add(this.m_chkDeleted);
            this.m_pnlTools.Controls.Add(this.m_txtFileName);
            resources.ApplyResources(this.m_pnlTools, "m_pnlTools");
            this.m_pnlTools.Name = "m_pnlTools";
            // 
            // m_chkDeleted
            // 
            resources.ApplyResources(this.m_chkDeleted, "m_chkDeleted");
            this.m_chkDeleted.Name = "m_chkDeleted";
            this.m_chkDeleted.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tbbtnOpen,
            this.m_tbbtnAppendFile,
            this.m_tbbtnSave,
            this.toolStripSeparator2,
            this.m_tbbtnAdd,
            this.m_tbbtnDelete,
            this.m_btnTest});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // m_tbbtnOpen
            // 
            resources.ApplyResources(this.m_tbbtnOpen, "m_tbbtnOpen");
            this.m_tbbtnOpen.Name = "m_tbbtnOpen";
            this.m_tbbtnOpen.Click += new System.EventHandler(this.m_mnuOpenToolStripMenuItem_Click);
            // 
            // m_tbbtnAppendFile
            // 
            resources.ApplyResources(this.m_tbbtnAppendFile, "m_tbbtnAppendFile");
            this.m_tbbtnAppendFile.Name = "m_tbbtnAppendFile";
            this.m_tbbtnAppendFile.Click += new System.EventHandler(this.m_tbbtnAppendFile_Click);
            // 
            // m_tbbtnSave
            // 
            this.m_tbbtnSave.Image = global::SPUD.Properties.Resources.Save1;
            resources.ApplyResources(this.m_tbbtnSave, "m_tbbtnSave");
            this.m_tbbtnSave.Name = "m_tbbtnSave";
            this.m_tbbtnSave.Click += new System.EventHandler(this.m_mnuSaveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // m_tbbtnAdd
            // 
            this.m_tbbtnAdd.Image = global::SPUD.Properties.Resources.New;
            resources.ApplyResources(this.m_tbbtnAdd, "m_tbbtnAdd");
            this.m_tbbtnAdd.Name = "m_tbbtnAdd";
            this.m_tbbtnAdd.Click += new System.EventHandler(this.m_mnu_AddToolStripMenuItem_Click);
            // 
            // m_tbbtnDelete
            // 
            resources.ApplyResources(this.m_tbbtnDelete, "m_tbbtnDelete");
            this.m_tbbtnDelete.Name = "m_tbbtnDelete";
            this.m_tbbtnDelete.Click += new System.EventHandler(this.m_mnuDeleteToolStripMenuItem_Click);
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripStatusLabel1,
            this.m_toolStripProgressBar1});
            this.m_statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            resources.ApplyResources(this.m_statusStrip, "m_statusStrip");
            this.m_statusStrip.Name = "m_statusStrip";
            // 
            // m_toolStripStatusLabel1
            // 
            this.m_toolStripStatusLabel1.Name = "m_toolStripStatusLabel1";
            resources.ApplyResources(this.m_toolStripStatusLabel1, "m_toolStripStatusLabel1");
            // 
            // m_toolStripProgressBar1
            // 
            this.m_toolStripProgressBar1.Name = "m_toolStripProgressBar1";
            resources.ApplyResources(this.m_toolStripProgressBar1, "m_toolStripProgressBar1");
            this.m_toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // m_splitContainerMain
            // 
            this.m_splitContainerMain.BackColor = System.Drawing.SystemColors.Control;
            this.m_splitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.m_splitContainerMain, "m_splitContainerMain");
            this.m_splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.m_splitContainerMain.Name = "m_splitContainerMain";
            // 
            // m_splitContainerMain.Panel1
            // 
            this.m_splitContainerMain.Panel1.Controls.Add(this.m_dataGridViewSpeed);
            // 
            // m_splitContainerMain.Panel2
            // 
            this.m_splitContainerMain.Panel2.Controls.Add(this.m_splitContainerDetails);
            // 
            // m_saveFileDialog
            // 
            resources.ApplyResources(this.m_saveFileDialog, "m_saveFileDialog");
            // 
            // m_btnTest
            // 
            this.m_btnTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.m_btnTest, "m_btnTest");
            this.m_btnTest.Name = "m_btnTest";
            this.m_btnTest.Click += new System.EventHandler(this.m_btnTest_Click);
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_splitContainerMain);
            this.Controls.Add(this.m_statusStrip);
            this.Controls.Add(this.m_pnlTools);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.m_menuStripMain);
            this.MainMenuStrip = this.m_menuStripMain;
            this.Name = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.m_splitContainerDetails.Panel1.ResumeLayout(false);
            this.m_splitContainerDetails.Panel2.ResumeLayout(false);
            this.m_splitContainerDetails.Panel2.PerformLayout();
            this.m_splitContainerDetails.ResumeLayout(false);
            this.m_pnlDetails.ResumeLayout(false);
            this.m_pnlDetails.PerformLayout();
            this.m_pnlMap.ResumeLayout(false);
            this.m_pnlMap.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridViewSpeed)).EndInit();
            this.m_menuStripMain.ResumeLayout(false);
            this.m_menuStripMain.PerformLayout();
            this.m_pnlTools.ResumeLayout(false);
            this.m_pnlTools.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            this.m_splitContainerMain.Panel1.ResumeLayout(false);
            this.m_splitContainerMain.Panel2.ResumeLayout(false);
            this.m_splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox m_txtFileName;
    private System.Windows.Forms.OpenFileDialog m_openFileDialog;
    private System.Windows.Forms.DataGridView m_dataGridViewSpeed;
    private System.Windows.Forms.MenuStrip m_menuStripMain;
    private System.Windows.Forms.ToolStripMenuItem m_mnuFileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_mnuOpenToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_mnuSaveToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem m_mnuExitToolStripMenuItem;
    private System.Windows.Forms.Panel m_pnlTools;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton m_tbbtnOpen;
    private System.Windows.Forms.ToolStripButton m_tbbtnSave;
    private System.Windows.Forms.StatusStrip m_statusStrip;
    private System.Windows.Forms.SplitContainer m_splitContainerMain;
    private System.Windows.Forms.ToolStripMenuItem m_mnuHelpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_mnu_AboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton m_tbbtnAdd;
    private System.Windows.Forms.ToolStripMenuItem m_mnuEditToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_mnu_AddToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
    private System.Windows.Forms.CheckBox m_chkDeleted;
    private System.Windows.Forms.ToolStripButton m_tbbtnDelete;
    private System.Windows.Forms.ToolStripMenuItem m_mnuDeleteToolStripMenuItem;
    private System.Windows.Forms.SplitContainer m_splitContainerDetails;
    private System.Windows.Forms.TextBox m_txtLat;
    private System.Windows.Forms.Label m_lblLat;
    private System.Windows.Forms.TextBox m_txtLong;
    private System.Windows.Forms.Label m_lblLong;
    private System.Windows.Forms.Label m_lblDirection;
    private System.Windows.Forms.Label m_lblType;
    private System.Windows.Forms.ComboBox m_cmbState;
    private System.Windows.Forms.ComboBox m_cmbType;
    private System.Windows.Forms.ComboBox m_cmbDirection;
    private System.Windows.Forms.Label m_lblState;
    private System.Windows.Forms.Button m_btnSave;
    private System.Windows.Forms.Button m_btnAdd;
    private System.Windows.Forms.Panel m_pnlDetails;
    private System.Windows.Forms.LinkLabel m_lblPos;
    private System.Windows.Forms.ToolTip m_ToolTip;
    private System.Windows.Forms.Panel m_pnlMap;
	private System.Windows.Forms.ToolStrip toolStrip2;
	private System.Windows.Forms.ToolStripButton m_tbbtnShowAllOnMap;
	private System.Windows.Forms.ToolStripButton m_tbbtnRefreshMap;
	private System.Windows.Forms.ToolStripButton m_tbbtnAppendFile;
	private System.Windows.Forms.ToolStripMenuItem m_mnuCloseToolStripMenuItem;
	private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel1;
	private System.Windows.Forms.ToolStripProgressBar m_toolStripProgressBar1;
        private System.Windows.Forms.ToolStripButton m_btnTest;
    }
}

