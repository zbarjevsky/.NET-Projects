namespace MeditationStopWatch
{
    partial class PlayListTabControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayListTabControl));
            this.m_btnEditTab = new System.Windows.Forms.Button();
            this.m_btnDelTab = new System.Windows.Forms.Button();
            this.m_btnAddTab = new System.Windows.Forms.Button();
            this.m_tabPlayLists = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_pnlTabs = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.m_mp3List = new MeditationStopWatch.FileListControl();
            this.m_tabPlayLists.SuspendLayout();
            this.m_pnlTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnEditTab
            // 
            this.m_btnEditTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnEditTab.Image = ((System.Drawing.Image)(resources.GetObject("m_btnEditTab.Image")));
            this.m_btnEditTab.Location = new System.Drawing.Point(300, 0);
            this.m_btnEditTab.Name = "m_btnEditTab";
            this.m_btnEditTab.Size = new System.Drawing.Size(28, 23);
            this.m_btnEditTab.TabIndex = 12;
            this.toolTip1.SetToolTip(this.m_btnEditTab, "Rename Selected");
            this.m_btnEditTab.UseVisualStyleBackColor = true;
            this.m_btnEditTab.Click += new System.EventHandler(this.m_btnEditTab_Click);
            // 
            // m_btnDelTab
            // 
            this.m_btnDelTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnDelTab.Image = ((System.Drawing.Image)(resources.GetObject("m_btnDelTab.Image")));
            this.m_btnDelTab.Location = new System.Drawing.Point(368, 0);
            this.m_btnDelTab.Name = "m_btnDelTab";
            this.m_btnDelTab.Size = new System.Drawing.Size(28, 23);
            this.m_btnDelTab.TabIndex = 11;
            this.toolTip1.SetToolTip(this.m_btnDelTab, "Delete Selected");
            this.m_btnDelTab.UseVisualStyleBackColor = true;
            this.m_btnDelTab.Click += new System.EventHandler(this.m_btnDelTab_Click);
            // 
            // m_btnAddTab
            // 
            this.m_btnAddTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnAddTab.Image = ((System.Drawing.Image)(resources.GetObject("m_btnAddTab.Image")));
            this.m_btnAddTab.Location = new System.Drawing.Point(334, 0);
            this.m_btnAddTab.Name = "m_btnAddTab";
            this.m_btnAddTab.Size = new System.Drawing.Size(28, 23);
            this.m_btnAddTab.TabIndex = 10;
            this.toolTip1.SetToolTip(this.m_btnAddTab, "Add New List");
            this.m_btnAddTab.UseVisualStyleBackColor = true;
            this.m_btnAddTab.Click += new System.EventHandler(this.m_btnAddTab_Click);
            // 
            // m_tabPlayLists
            // 
            this.m_tabPlayLists.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabPlayLists.Controls.Add(this.tabPage1);
            this.m_tabPlayLists.Location = new System.Drawing.Point(0, 0);
            this.m_tabPlayLists.Name = "m_tabPlayLists";
            this.m_tabPlayLists.SelectedIndex = 0;
            this.m_tabPlayLists.Size = new System.Drawing.Size(294, 37);
            this.m_tabPlayLists.TabIndex = 9;
            this.m_tabPlayLists.SelectedIndexChanged += new System.EventHandler(this.m_tabPlayLists_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(286, 11);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Music";
            // 
            // m_pnlTabs
            // 
            this.m_pnlTabs.Controls.Add(this.m_tabPlayLists);
            this.m_pnlTabs.Controls.Add(this.m_btnEditTab);
            this.m_pnlTabs.Controls.Add(this.m_btnAddTab);
            this.m_pnlTabs.Controls.Add(this.m_btnDelTab);
            this.m_pnlTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlTabs.Location = new System.Drawing.Point(0, 0);
            this.m_pnlTabs.Name = "m_pnlTabs";
            this.m_pnlTabs.Size = new System.Drawing.Size(399, 29);
            this.m_pnlTabs.TabIndex = 13;
            // 
            // m_mp3List
            // 
            this.m_mp3List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_mp3List.Location = new System.Drawing.Point(0, 29);
            this.m_mp3List.Name = "m_mp3List";
            this.m_mp3List.Size = new System.Drawing.Size(399, 159);
            this.m_mp3List.TabIndex = 14;
            // 
            // PlayListTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_mp3List);
            this.Controls.Add(this.m_pnlTabs);
            this.Name = "PlayListTabControl";
            this.Size = new System.Drawing.Size(399, 188);
            this.Load += new System.EventHandler(this.PlayListTabControl_Load);
            this.m_tabPlayLists.ResumeLayout(false);
            this.m_pnlTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_btnEditTab;
        private System.Windows.Forms.Button m_btnDelTab;
        private System.Windows.Forms.Button m_btnAddTab;
        private System.Windows.Forms.TabControl m_tabPlayLists;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel m_pnlTabs;
        private FileListControl m_mp3List;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
