namespace MkZ.WinForms
{
    partial class FoldersTreeUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FoldersTreeUserControl));
            this.tvFolders = new System.Windows.Forms.TreeView();
            this.m_imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tvFolders
            // 
            this.tvFolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFolders.HideSelection = false;
            this.tvFolders.ImageIndex = 0;
            this.tvFolders.ImageList = this.m_imageListTreeView;
            this.tvFolders.ItemHeight = 20;
            this.tvFolders.LabelEdit = true;
            this.tvFolders.Location = new System.Drawing.Point(0, 0);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageIndex = 0;
            this.tvFolders.Size = new System.Drawing.Size(229, 480);
            this.tvFolders.StateImageList = this.m_imageListTreeView;
            this.tvFolders.TabIndex = 3;
            this.tvFolders.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvFolders_AfterLabelEdit);
            this.tvFolders.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFolders_BeforeExpand);
            this.tvFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFolders_AfterSelect);
            this.tvFolders.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvFolders_MouseDoubleClick);
            this.tvFolders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvFolders_MouseDown);
            // 
            // m_imageListTreeView
            // 
            this.m_imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imageListTreeView.ImageStream")));
            this.m_imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imageListTreeView.Images.SetKeyName(0, "Icon_272_16.png");
            this.m_imageListTreeView.Images.SetKeyName(1, "");
            this.m_imageListTreeView.Images.SetKeyName(2, "Icon_004_16.png");
            this.m_imageListTreeView.Images.SetKeyName(3, "Icon_126_16.png");
            this.m_imageListTreeView.Images.SetKeyName(4, "Icon_000_16.png");
            this.m_imageListTreeView.Images.SetKeyName(5, "");
            this.m_imageListTreeView.Images.SetKeyName(6, "");
            this.m_imageListTreeView.Images.SetKeyName(7, "Icon_188_16.png");
            this.m_imageListTreeView.Images.SetKeyName(8, "");
            this.m_imageListTreeView.Images.SetKeyName(9, "Copy.ico");
            // 
            // FoldersTreeUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvFolders);
            this.Name = "FoldersTreeUserControl";
            this.Size = new System.Drawing.Size(229, 480);
            this.Load += new System.EventHandler(this.FoldersTreeUserControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvFolders;
        private System.Windows.Forms.ImageList m_imageListTreeView;
    }
}
