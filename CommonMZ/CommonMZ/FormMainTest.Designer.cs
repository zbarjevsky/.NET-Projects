namespace MZ
{
    partial class FormMainTest
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
            this.m_btnTestEdit = new System.Windows.Forms.Button();
            this.foldersTreeUserControl1 = new MZ.ControlsWinForms.FoldersTreeUserControl();
            this.colorBarsProgressBar1 = new MZ.Controls.ColorBarsProgressBar();
            this.fileExplorerUserControl1 = new MZ.ControlsWinForms.FileExplorerUserControl();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_cmbListViewType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.colorBarsProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnTestEdit
            // 
            this.m_btnTestEdit.Location = new System.Drawing.Point(99, 13);
            this.m_btnTestEdit.Name = "m_btnTestEdit";
            this.m_btnTestEdit.Size = new System.Drawing.Size(144, 116);
            this.m_btnTestEdit.TabIndex = 1;
            this.m_btnTestEdit.Text = "Test In-Place-Edit Box";
            this.m_btnTestEdit.UseVisualStyleBackColor = true;
            this.m_btnTestEdit.Click += new System.EventHandler(this.m_btnTestEdit_Click);
            // 
            // foldersTreeUserControl1
            // 
            this.foldersTreeUserControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.foldersTreeUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.foldersTreeUserControl1.Location = new System.Drawing.Point(0, 0);
            this.foldersTreeUserControl1.Name = "foldersTreeUserControl1";
            this.foldersTreeUserControl1.Size = new System.Drawing.Size(218, 714);
            this.foldersTreeUserControl1.TabIndex = 2;
            // 
            // colorBarsProgressBar1
            // 
            this.colorBarsProgressBar1.ActiveColor = System.Drawing.Color.LimeGreen;
            this.colorBarsProgressBar1.ActiveColorTheme = MZ.Controls.ColorBarsProgressBar.ActiveColorsTheme.Multicolor;
            this.colorBarsProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.colorBarsProgressBar1.InactiveColorTheme = MZ.Controls.ColorBarsProgressBar.InactiveColorsTheme.Pale;
            this.colorBarsProgressBar1.Location = new System.Drawing.Point(26, 34);
            this.colorBarsProgressBar1.Maximum = 100;
            this.colorBarsProgressBar1.Minimum = 0;
            this.colorBarsProgressBar1.Name = "colorBarsProgressBar1";
            this.colorBarsProgressBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorBarsProgressBar1.Size = new System.Drawing.Size(21, 635);
            this.colorBarsProgressBar1.TabIndex = 0;
            this.colorBarsProgressBar1.TabStop = false;
            this.colorBarsProgressBar1.Value = 65;
            // 
            // fileExplorerUserControl1
            // 
            this.fileExplorerUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileExplorerUserControl1.Location = new System.Drawing.Point(0, 0);
            this.fileExplorerUserControl1.Name = "fileExplorerUserControl1";
            this.fileExplorerUserControl1.Size = new System.Drawing.Size(432, 714);
            this.fileExplorerUserControl1.TabIndex = 3;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(64, 181);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(326, 545);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.SmallIcon;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(396, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.foldersTreeUserControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.fileExplorerUserControl1);
            this.splitContainer1.Size = new System.Drawing.Size(654, 714);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 6;
            // 
            // m_cmbListViewType
            // 
            this.m_cmbListViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbListViewType.FormattingEnabled = true;
            this.m_cmbListViewType.Location = new System.Drawing.Point(64, 154);
            this.m_cmbListViewType.Name = "m_cmbListViewType";
            this.m_cmbListViewType.Size = new System.Drawing.Size(121, 21);
            this.m_cmbListViewType.TabIndex = 7;
            this.m_cmbListViewType.SelectedIndexChanged += new System.EventHandler(this.m_cmbListViewType_SelectedIndexChanged);
            // 
            // FormMainTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 732);
            this.Controls.Add(this.m_cmbListViewType);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.m_btnTestEdit);
            this.Controls.Add(this.colorBarsProgressBar1);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FormMainTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form Main Test";
            this.Load += new System.EventHandler(this.FormMainTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.colorBarsProgressBar1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ColorBarsProgressBar colorBarsProgressBar1;
        private System.Windows.Forms.Button m_btnTestEdit;
        private ControlsWinForms.FoldersTreeUserControl foldersTreeUserControl1;
        private ControlsWinForms.FileExplorerUserControl fileExplorerUserControl1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox m_cmbListViewType;
    }
}