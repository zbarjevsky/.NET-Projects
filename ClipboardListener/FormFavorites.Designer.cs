namespace ClipboardManager
{
	partial class FormFavorites
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFavorites));
			this.m_listFavorites = new System.Windows.Forms.ListView();
			this.m_columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.m_btnRemove = new System.Windows.Forms.Button();
			this.m_btnCancel = new System.Windows.Forms.Button();
			this.m_btnUp = new System.Windows.Forms.Button();
			this.m_btnDown = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_listFavorites
			// 
			this.m_listFavorites.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_listFavorites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.m_columnHeader1});
			this.m_listFavorites.FullRowSelect = true;
			this.m_listFavorites.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.m_listFavorites.HideSelection = false;
			this.m_listFavorites.Location = new System.Drawing.Point(12, 12);
			this.m_listFavorites.Name = "m_listFavorites";
			this.m_listFavorites.Size = new System.Drawing.Size(355, 249);
			this.m_listFavorites.TabIndex = 0;
			this.m_listFavorites.UseCompatibleStateImageBehavior = false;
			this.m_listFavorites.View = System.Windows.Forms.View.Details;
			this.m_listFavorites.DoubleClick += new System.EventHandler(this.m_listFavorites_DoubleClick);
			this.m_listFavorites.SelectedIndexChanged += new System.EventHandler(this.m_listFavorites_SelectedIndexChanged);
			this.m_listFavorites.SizeChanged += new System.EventHandler(this.m_listFavorites_SizeChanged);
			// 
			// m_columnHeader1
			// 
			this.m_columnHeader1.Text = "Clipboard Entry";
			this.m_columnHeader1.Width = 351;
			// 
			// m_btnRemove
			// 
			this.m_btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("m_btnRemove.Image")));
			this.m_btnRemove.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.m_btnRemove.Location = new System.Drawing.Point(381, 12);
			this.m_btnRemove.Name = "m_btnRemove";
			this.m_btnRemove.Size = new System.Drawing.Size(75, 23);
			this.m_btnRemove.TabIndex = 1;
			this.m_btnRemove.Text = "&Delete";
			this.m_btnRemove.UseVisualStyleBackColor = true;
			this.m_btnRemove.Click += new System.EventHandler(this.m_btnRemove_Click);
			// 
			// m_btnCancel
			// 
			this.m_btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("m_btnCancel.Image")));
			this.m_btnCancel.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.m_btnCancel.Location = new System.Drawing.Point(381, 238);
			this.m_btnCancel.Name = "m_btnCancel";
			this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
			this.m_btnCancel.TabIndex = 4;
			this.m_btnCancel.Text = "&Close";
			this.m_btnCancel.UseVisualStyleBackColor = true;
			this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
			// 
			// m_btnUp
			// 
			this.m_btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnUp.Image = ((System.Drawing.Image)(resources.GetObject("m_btnUp.Image")));
			this.m_btnUp.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.m_btnUp.Location = new System.Drawing.Point(381, 109);
			this.m_btnUp.Name = "m_btnUp";
			this.m_btnUp.Size = new System.Drawing.Size(75, 23);
			this.m_btnUp.TabIndex = 2;
			this.m_btnUp.Text = "&Up";
			this.m_btnUp.UseVisualStyleBackColor = true;
			this.m_btnUp.Click += new System.EventHandler(this.m_btnUp_Click);
			// 
			// m_btnDown
			// 
			this.m_btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_btnDown.Image = ((System.Drawing.Image)(resources.GetObject("m_btnDown.Image")));
			this.m_btnDown.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.m_btnDown.Location = new System.Drawing.Point(381, 138);
			this.m_btnDown.Name = "m_btnDown";
			this.m_btnDown.Size = new System.Drawing.Size(75, 23);
			this.m_btnDown.TabIndex = 3;
			this.m_btnDown.Text = "Do&wn";
			this.m_btnDown.UseVisualStyleBackColor = true;
			this.m_btnDown.Click += new System.EventHandler(this.m_btnDown_Click);
			// 
			// FormFavorites
			// 
			this.AcceptButton = this.m_btnCancel;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.m_btnCancel;
			this.ClientSize = new System.Drawing.Size(468, 273);
			this.Controls.Add(this.m_btnDown);
			this.Controls.Add(this.m_btnUp);
			this.Controls.Add(this.m_btnCancel);
			this.Controls.Add(this.m_btnRemove);
			this.Controls.Add(this.m_listFavorites);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(200, 300);
			this.Name = "FormFavorites";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Organize Favorites";
			this.Load += new System.EventHandler(this.FormFavorites_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView m_listFavorites;
		private System.Windows.Forms.Button m_btnRemove;
		private System.Windows.Forms.Button m_btnCancel;
		private System.Windows.Forms.ColumnHeader m_columnHeader1;
		private System.Windows.Forms.Button m_btnUp;
		private System.Windows.Forms.Button m_btnDown;
	}
}