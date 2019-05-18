using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipboardManager.Utils
{
    public partial class ClipboardHistoryListControl : UserControl
    {
        private ListView m_listHistory;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClipboardHistoryListControl));
            this.m_listHistory = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_imageListClipboardTypes = new System.Windows.Forms.ImageList(this.components);
            this.m_pnlOperations = new System.Windows.Forms.Panel();
            this.m_btnFindNext = new System.Windows.Forms.Button();
            this.m_lblSearch = new System.Windows.Forms.Label();
            this.m_txtSearch = new System.Windows.Forms.TextBox();
            this.m_errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_pnlOperations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // m_listHistory
            // 
            this.m_listHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.m_listHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listHistory.FullRowSelect = true;
            this.m_listHistory.GridLines = true;
            this.m_listHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_listHistory.HideSelection = false;
            this.m_listHistory.Location = new System.Drawing.Point(0, 43);
            this.m_listHistory.MultiSelect = false;
            this.m_listHistory.Name = "m_listHistory";
            this.m_listHistory.ShowItemToolTips = true;
            this.m_listHistory.Size = new System.Drawing.Size(301, 481);
            this.m_listHistory.SmallImageList = this.m_imageListClipboardTypes;
            this.m_listHistory.TabIndex = 1;
            this.m_listHistory.UseCompatibleStateImageBehavior = false;
            this.m_listHistory.View = System.Windows.Forms.View.Details;
            this.m_listHistory.VirtualMode = true;
            this.m_listHistory.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ClipboardHistoryListView_RetrieveVirtualItem);
            this.m_listHistory.DoubleClick += new System.EventHandler(this.ClipboardHistoryListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "...";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Clipboard Histort";
            this.columnHeader3.Width = 25;
            // 
            // m_imageListClipboardTypes
            // 
            this.m_imageListClipboardTypes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListClipboardTypes.ImageStream")));
            this.m_imageListClipboardTypes.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageListClipboardTypes.Images.SetKeyName(0, "Text.ico");
            this.m_imageListClipboardTypes.Images.SetKeyName(1, "Unicode.ico");
            this.m_imageListClipboardTypes.Images.SetKeyName(2, "");
            this.m_imageListClipboardTypes.Images.SetKeyName(3, "");
            this.m_imageListClipboardTypes.Images.SetKeyName(4, "");
            this.m_imageListClipboardTypes.Images.SetKeyName(5, "Cat.ico");
            this.m_imageListClipboardTypes.Images.SetKeyName(6, "FileCopy.ico");
            this.m_imageListClipboardTypes.Images.SetKeyName(7, "FileCut.ico");
            this.m_imageListClipboardTypes.Images.SetKeyName(8, "FilesCopy.ico");
            this.m_imageListClipboardTypes.Images.SetKeyName(9, "FilesCut.ico");
            // 
            // m_pnlOperations
            // 
            this.m_pnlOperations.Controls.Add(this.m_btnFindNext);
            this.m_pnlOperations.Controls.Add(this.m_lblSearch);
            this.m_pnlOperations.Controls.Add(this.m_txtSearch);
            this.m_pnlOperations.Dock = System.Windows.Forms.DockStyle.Top;
            this.m_pnlOperations.Location = new System.Drawing.Point(0, 0);
            this.m_pnlOperations.Name = "m_pnlOperations";
            this.m_pnlOperations.Size = new System.Drawing.Size(301, 43);
            this.m_pnlOperations.TabIndex = 0;
            // 
            // m_btnFindNext
            // 
            this.m_btnFindNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnFindNext.Location = new System.Drawing.Point(227, 6);
            this.m_btnFindNext.Name = "m_btnFindNext";
            this.m_btnFindNext.Size = new System.Drawing.Size(64, 23);
            this.m_btnFindNext.TabIndex = 2;
            this.m_btnFindNext.Text = "Find Next";
            this.m_btnFindNext.UseVisualStyleBackColor = true;
            this.m_btnFindNext.Click += new System.EventHandler(this.m_btnFindNext_Click);
            // 
            // m_lblSearch
            // 
            this.m_lblSearch.AutoSize = true;
            this.m_lblSearch.Location = new System.Drawing.Point(3, 9);
            this.m_lblSearch.Name = "m_lblSearch";
            this.m_lblSearch.Size = new System.Drawing.Size(47, 13);
            this.m_lblSearch.TabIndex = 0;
            this.m_lblSearch.Text = "Search: ";
            // 
            // m_txtSearch
            // 
            this.m_txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtSearch.Location = new System.Drawing.Point(56, 6);
            this.m_txtSearch.Name = "m_txtSearch";
            this.m_txtSearch.Size = new System.Drawing.Size(154, 20);
            this.m_txtSearch.TabIndex = 1;
            this.m_txtSearch.TextChanged += new System.EventHandler(this.m_txtSearch_TextChanged);
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // ClipboardHistoryListControl
            // 
            this.Controls.Add(this.m_listHistory);
            this.Controls.Add(this.m_pnlOperations);
            this.Name = "ClipboardHistoryListControl";
            this.Size = new System.Drawing.Size(301, 524);
            this.Load += new System.EventHandler(this.ClipboardHistoryListControl_Load);
            this.m_pnlOperations.ResumeLayout(false);
            this.m_pnlOperations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        private Panel m_pnlOperations;
        private TextBox m_txtSearch;
        private Label m_lblSearch;
        private ImageList m_imageListClipboardTypes;
        private System.ComponentModel.IContainer components;
        private ErrorProvider m_errorProvider;
        private Button m_btnFindNext;
    }
}
