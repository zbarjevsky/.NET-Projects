namespace ClipboardManager
{
	partial class FormClipboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClipboard));
            this.m_notifyIconCoodClip = new System.Windows.Forms.NotifyIcon(this.components);
            this.m_contextMenuStripTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_contextMenuStripTrayIcon_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_History = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_Spy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_Reconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_UAC = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_contextMenuStripTrayIcon_Desktop = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_DesktopSave = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_DesktopRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_Sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_contextMenuStripTrayIcon_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_About = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripTrayIcon_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_contextMenuStripTrayIcon_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_menuStripMain = new System.Windows.Forms.MenuStrip();
            this.m_ToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_File_ShowHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_File_ClearHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_File_ClearLast = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_File_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripMenuItem_File_Hide = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Edit_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Edit_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Edit_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Edit_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripMenuItem_Edit_Test = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_View = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_View_SnapShot = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_View_Debug = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Favorites = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Favorites_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Favorites_Organize = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Favorites_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripMenuItem_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Tools_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Tools_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripMenuItem_Tools_ReverseChars = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Tools_Convert = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Tools_UnescapeURI = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Tools_Encoding = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Tools_Encoding_Config = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolStripMenuItem_Tools_Encoding_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStrip_RichTextBox_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStrip_RichTextBox_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStrip_RichTextBox_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.m_splitContainerClipboard = new System.Windows.Forms.SplitContainer();
            this.m_pnlClipboard = new System.Windows.Forms.Panel();
            this.m_richTextBoxClipboard = new ClipboardManager.CustomRichTextBox();
            this.m_contextMenuStrip_RichTextBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_pnlClipboardLeft = new System.Windows.Forms.Panel();
            this.m_icoClipboardApp = new System.Windows.Forms.PictureBox();
            this.m_lblClipboardType = new System.Windows.Forms.Label();
            this.m_imageListClipboardTypes = new System.Windows.Forms.ImageList(this.components);
            this.m_pnlSnapShot = new System.Windows.Forms.Panel();
            this.m_richTextBoxSnapShot = new System.Windows.Forms.RichTextBox();
            this.m_pnlSnapShotLeft = new System.Windows.Forms.Panel();
            this.m_icoSnapShotApp = new System.Windows.Forms.PictureBox();
            this.m_lblSnapShotType = new System.Windows.Forms.Label();
            this.m_HSplitterDebug = new System.Windows.Forms.Splitter();
            this.m_pnlDebug = new System.Windows.Forms.Panel();
            this.m_richTextBoxDebug = new System.Windows.Forms.RichTextBox();
            this.m_pnlDebugLeft = new System.Windows.Forms.Panel();
            this.m_btnDebugScroll = new System.Windows.Forms.Button();
            this.m_ImageList_Scroll = new System.Windows.Forms.ImageList(this.components);
            this.m_btnDebugCopy = new System.Windows.Forms.Button();
            this.m_btnDebugClear = new System.Windows.Forms.Button();
            this.m_toolStrip = new System.Windows.Forms.ToolStrip();
            this.m_toolStripButton_History = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolStripButton_OnTop = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolStripButton_Target = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolStripButton_Cut = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripButton_Copy = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripButton_Paste = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolStripButton_Print = new System.Windows.Forms.ToolStripButton();
            this.m_toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripComboBox_CFormats = new System.Windows.Forms.ToolStripComboBox();
            this.m_toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.m_ToolStripSplitButton_FontSize = new System.Windows.Forms.ToolStripSplitButton();
            this.m_toolStripButton_FontSize_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripButton_FontSize_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripButton_FontSize_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripButton_FontSize_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripButton_FontSize_0 = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolStripButton_Wordwrap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_tbbtnDataFolder = new System.Windows.Forms.ToolStripButton();
            this.m_contextMenuStripClipboard = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_contextMenuStripClipboard_Current = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripClipboard_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_contextMenuStripClipboard_Favorites = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStripClipboard_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_emptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ImageListSysMenu = new System.Windows.Forms.ImageList(this.components);
            this.m_contextMenuStrip_ClipboardEntry = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_contextMenuStrip_ClipboardEntry_AddToFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStrip_ClipboardEntry_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_contextMenuStrip_ClipboardEntry_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.m_SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.m_splitMain = new System.Windows.Forms.SplitContainer();
            this.m_listHistory = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.m_contextMenuStripTrayIcon.SuspendLayout();
            this.m_menuStripMain.SuspendLayout();
            this.m_statusStrip.SuspendLayout();
            this.m_toolStripContainer.ContentPanel.SuspendLayout();
            this.m_toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.m_toolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerClipboard)).BeginInit();
            this.m_splitContainerClipboard.Panel1.SuspendLayout();
            this.m_splitContainerClipboard.Panel2.SuspendLayout();
            this.m_splitContainerClipboard.SuspendLayout();
            this.m_pnlClipboard.SuspendLayout();
            this.m_contextMenuStrip_RichTextBox.SuspendLayout();
            this.m_pnlClipboardLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_icoClipboardApp)).BeginInit();
            this.m_pnlSnapShot.SuspendLayout();
            this.m_pnlSnapShotLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_icoSnapShotApp)).BeginInit();
            this.m_pnlDebug.SuspendLayout();
            this.m_pnlDebugLeft.SuspendLayout();
            this.m_toolStrip.SuspendLayout();
            this.m_contextMenuStripClipboard.SuspendLayout();
            this.m_contextMenuStrip_ClipboardEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).BeginInit();
            this.m_splitMain.Panel1.SuspendLayout();
            this.m_splitMain.Panel2.SuspendLayout();
            this.m_splitMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_notifyIconCoodClip
            // 
            this.m_notifyIconCoodClip.ContextMenuStrip = this.m_contextMenuStripTrayIcon;
            this.m_notifyIconCoodClip.Icon = ((System.Drawing.Icon)(resources.GetObject("m_notifyIconCoodClip.Icon")));
            this.m_notifyIconCoodClip.Text = "Clipboard Manager";
            this.m_notifyIconCoodClip.Visible = true;
            this.m_notifyIconCoodClip.MouseClick += new System.Windows.Forms.MouseEventHandler(this.m_notifyIconCoodClip_MouseClick);
            this.m_notifyIconCoodClip.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.m_notifyIconCoodClip_MouseDoubleClick);
            // 
            // m_contextMenuStripTrayIcon
            // 
            this.m_contextMenuStripTrayIcon.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_contextMenuStripTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_contextMenuStripTrayIcon_Show,
            this.m_contextMenuStripTrayIcon_History,
            this.m_contextMenuStripTrayIcon_Spy,
            this.m_contextMenuStripTrayIcon_Reconnect,
            this.m_contextMenuStripTrayIcon_UAC,
            this.m_contextMenuStripTrayIcon_Sep2,
            this.m_contextMenuStripTrayIcon_Desktop,
            this.m_contextMenuStripTrayIcon_Sep3,
            this.m_contextMenuStripTrayIcon_Settings,
            this.m_contextMenuStripTrayIcon_About,
            this.m_contextMenuStripTrayIcon_Sep1,
            this.m_contextMenuStripTrayIcon_Exit});
            this.m_contextMenuStripTrayIcon.Name = "m_contextMenuStripTrayIcon";
            this.m_contextMenuStripTrayIcon.Size = new System.Drawing.Size(217, 256);
            this.m_contextMenuStripTrayIcon.Text = "Clipboard History Tray Menu";
            // 
            // m_contextMenuStripTrayIcon_Show
            // 
            this.m_contextMenuStripTrayIcon_Show.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_contextMenuStripTrayIcon_Show.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStripTrayIcon_Show.Image")));
            this.m_contextMenuStripTrayIcon_Show.Name = "m_contextMenuStripTrayIcon_Show";
            this.m_contextMenuStripTrayIcon_Show.Size = new System.Drawing.Size(216, 26);
            this.m_contextMenuStripTrayIcon_Show.Text = "Sho&w";
            this.m_contextMenuStripTrayIcon_Show.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_Show_Click);
            // 
            // m_contextMenuStripTrayIcon_History
            // 
            this.m_contextMenuStripTrayIcon_History.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStripTrayIcon_History.Image")));
            this.m_contextMenuStripTrayIcon_History.Name = "m_contextMenuStripTrayIcon_History";
            this.m_contextMenuStripTrayIcon_History.Size = new System.Drawing.Size(216, 26);
            this.m_contextMenuStripTrayIcon_History.Text = "&History";
            this.m_contextMenuStripTrayIcon_History.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_History_Click);
            // 
            // m_contextMenuStripTrayIcon_Spy
            // 
            this.m_contextMenuStripTrayIcon_Spy.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStripTrayIcon_Spy.Image")));
            this.m_contextMenuStripTrayIcon_Spy.Name = "m_contextMenuStripTrayIcon_Spy";
            this.m_contextMenuStripTrayIcon_Spy.Size = new System.Drawing.Size(216, 26);
            this.m_contextMenuStripTrayIcon_Spy.Text = "&Finder Tool";
            this.m_contextMenuStripTrayIcon_Spy.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_Spy_Click);
            // 
            // m_contextMenuStripTrayIcon_Reconnect
            // 
            this.m_contextMenuStripTrayIcon_Reconnect.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStripTrayIcon_Reconnect.Image")));
            this.m_contextMenuStripTrayIcon_Reconnect.Name = "m_contextMenuStripTrayIcon_Reconnect";
            this.m_contextMenuStripTrayIcon_Reconnect.Size = new System.Drawing.Size(216, 26);
            this.m_contextMenuStripTrayIcon_Reconnect.Text = "Reconnect";
            this.m_contextMenuStripTrayIcon_Reconnect.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_Reconnect_Click);
            // 
            // m_contextMenuStripTrayIcon_UAC
            // 
            this.m_contextMenuStripTrayIcon_UAC.Name = "m_contextMenuStripTrayIcon_UAC";
            this.m_contextMenuStripTrayIcon_UAC.Size = new System.Drawing.Size(216, 26);
            this.m_contextMenuStripTrayIcon_UAC.Text = "UAC";
            this.m_contextMenuStripTrayIcon_UAC.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_UAC_Click);
            // 
            // m_contextMenuStripTrayIcon_Sep2
            // 
            this.m_contextMenuStripTrayIcon_Sep2.Name = "m_contextMenuStripTrayIcon_Sep2";
            this.m_contextMenuStripTrayIcon_Sep2.Size = new System.Drawing.Size(213, 6);
            // 
            // m_contextMenuStripTrayIcon_Desktop
            // 
            this.m_contextMenuStripTrayIcon_Desktop.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_contextMenuStripTrayIcon_DesktopSave,
            this.m_contextMenuStripTrayIcon_DesktopRestore});
            this.m_contextMenuStripTrayIcon_Desktop.Name = "m_contextMenuStripTrayIcon_Desktop";
            this.m_contextMenuStripTrayIcon_Desktop.Size = new System.Drawing.Size(216, 26);
            this.m_contextMenuStripTrayIcon_Desktop.Text = "Desktop Icons";
            // 
            // m_contextMenuStripTrayIcon_DesktopSave
            // 
            this.m_contextMenuStripTrayIcon_DesktopSave.Name = "m_contextMenuStripTrayIcon_DesktopSave";
            this.m_contextMenuStripTrayIcon_DesktopSave.Size = new System.Drawing.Size(164, 22);
            this.m_contextMenuStripTrayIcon_DesktopSave.Text = "Save Snapshot";
            this.m_contextMenuStripTrayIcon_DesktopSave.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_DesktopSave_Click);
            // 
            // m_contextMenuStripTrayIcon_DesktopRestore
            // 
            this.m_contextMenuStripTrayIcon_DesktopRestore.Name = "m_contextMenuStripTrayIcon_DesktopRestore";
            this.m_contextMenuStripTrayIcon_DesktopRestore.Size = new System.Drawing.Size(164, 22);
            this.m_contextMenuStripTrayIcon_DesktopRestore.Text = "Restore Positions";
            this.m_contextMenuStripTrayIcon_DesktopRestore.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_DesktopRestore_Click);
            // 
            // m_contextMenuStripTrayIcon_Sep3
            // 
            this.m_contextMenuStripTrayIcon_Sep3.Name = "m_contextMenuStripTrayIcon_Sep3";
            this.m_contextMenuStripTrayIcon_Sep3.Size = new System.Drawing.Size(213, 6);
            // 
            // m_contextMenuStripTrayIcon_Settings
            // 
            this.m_contextMenuStripTrayIcon_Settings.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStripTrayIcon_Settings.Image")));
            this.m_contextMenuStripTrayIcon_Settings.Name = "m_contextMenuStripTrayIcon_Settings";
            this.m_contextMenuStripTrayIcon_Settings.Size = new System.Drawing.Size(216, 26);
            this.m_contextMenuStripTrayIcon_Settings.Text = "&Settings";
            this.m_contextMenuStripTrayIcon_Settings.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_Settings_Click);
            // 
            // m_contextMenuStripTrayIcon_About
            // 
            this.m_contextMenuStripTrayIcon_About.Name = "m_contextMenuStripTrayIcon_About";
            this.m_contextMenuStripTrayIcon_About.Size = new System.Drawing.Size(216, 26);
            this.m_contextMenuStripTrayIcon_About.Text = "&About Clipboard Manager";
            this.m_contextMenuStripTrayIcon_About.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_About_Click);
            // 
            // m_contextMenuStripTrayIcon_Sep1
            // 
            this.m_contextMenuStripTrayIcon_Sep1.Name = "m_contextMenuStripTrayIcon_Sep1";
            this.m_contextMenuStripTrayIcon_Sep1.Size = new System.Drawing.Size(213, 6);
            // 
            // m_contextMenuStripTrayIcon_Exit
            // 
            this.m_contextMenuStripTrayIcon_Exit.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStripTrayIcon_Exit.Image")));
            this.m_contextMenuStripTrayIcon_Exit.Name = "m_contextMenuStripTrayIcon_Exit";
            this.m_contextMenuStripTrayIcon_Exit.Size = new System.Drawing.Size(216, 26);
            this.m_contextMenuStripTrayIcon_Exit.Text = "E&xit";
            this.m_contextMenuStripTrayIcon_Exit.Click += new System.EventHandler(this.m_contextMenuStripTrayIcon_Exit_Click);
            // 
            // m_menuStripMain
            // 
            this.m_menuStripMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.m_menuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItem_File,
            this.m_ToolStripMenuItem_Edit,
            this.m_ToolStripMenuItem_View,
            this.m_ToolStripMenuItem_Favorites,
            this.m_ToolStripMenuItem_Tools,
            this.ToolStripMenuItem_Help});
            this.m_menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.m_menuStripMain.Name = "m_menuStripMain";
            this.m_menuStripMain.Size = new System.Drawing.Size(984, 24);
            this.m_menuStripMain.TabIndex = 0;
            this.m_menuStripMain.Text = "Clipboard History Main Menu";
            // 
            // m_ToolStripMenuItem_File
            // 
            this.m_ToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItem_File_ShowHistory,
            this.m_ToolStripMenuItem_File_Save,
            this.m_ToolStripMenuItem_File_ClearHistory,
            this.m_ToolStripMenuItem_File_ClearLast,
            this.m_ToolStripMenuItem_File_Sep1,
            this.m_ToolStripMenuItem_File_Hide,
            this.m_ToolStripMenuItem_File_Exit});
            this.m_ToolStripMenuItem_File.Name = "m_ToolStripMenuItem_File";
            this.m_ToolStripMenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.m_ToolStripMenuItem_File.Text = "&File";
            // 
            // m_ToolStripMenuItem_File_ShowHistory
            // 
            this.m_ToolStripMenuItem_File_ShowHistory.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_File_ShowHistory.Image")));
            this.m_ToolStripMenuItem_File_ShowHistory.Name = "m_ToolStripMenuItem_File_ShowHistory";
            this.m_ToolStripMenuItem_File_ShowHistory.Size = new System.Drawing.Size(163, 22);
            this.m_ToolStripMenuItem_File_ShowHistory.Text = "S&how History";
            this.m_ToolStripMenuItem_File_ShowHistory.Click += new System.EventHandler(this.m_ToolStripMenuItem_File_ShowHistory_Click);
            // 
            // m_ToolStripMenuItem_File_Save
            // 
            this.m_ToolStripMenuItem_File_Save.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_File_Save.Image")));
            this.m_ToolStripMenuItem_File_Save.Name = "m_ToolStripMenuItem_File_Save";
            this.m_ToolStripMenuItem_File_Save.Size = new System.Drawing.Size(163, 22);
            this.m_ToolStripMenuItem_File_Save.Text = "&Save As...";
            this.m_ToolStripMenuItem_File_Save.Click += new System.EventHandler(this.m_ToolStripMenuItem_File_Save_Click);
            // 
            // m_ToolStripMenuItem_File_ClearHistory
            // 
            this.m_ToolStripMenuItem_File_ClearHistory.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_File_ClearHistory.Image")));
            this.m_ToolStripMenuItem_File_ClearHistory.Name = "m_ToolStripMenuItem_File_ClearHistory";
            this.m_ToolStripMenuItem_File_ClearHistory.Size = new System.Drawing.Size(163, 22);
            this.m_ToolStripMenuItem_File_ClearHistory.Text = "&Clear History";
            this.m_ToolStripMenuItem_File_ClearHistory.Click += new System.EventHandler(this.m_ToolStripMenuItem_File_ClearHistory_Click);
            // 
            // m_ToolStripMenuItem_File_ClearLast
            // 
            this.m_ToolStripMenuItem_File_ClearLast.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_File_ClearLast.Image")));
            this.m_ToolStripMenuItem_File_ClearLast.Name = "m_ToolStripMenuItem_File_ClearLast";
            this.m_ToolStripMenuItem_File_ClearLast.Size = new System.Drawing.Size(163, 22);
            this.m_ToolStripMenuItem_File_ClearLast.Text = "Clear Clipboard";
            this.m_ToolStripMenuItem_File_ClearLast.Click += new System.EventHandler(this.m_ToolStripMenuItem_File_ClearLast_Click);
            // 
            // m_ToolStripMenuItem_File_Sep1
            // 
            this.m_ToolStripMenuItem_File_Sep1.Name = "m_ToolStripMenuItem_File_Sep1";
            this.m_ToolStripMenuItem_File_Sep1.Size = new System.Drawing.Size(160, 6);
            // 
            // m_ToolStripMenuItem_File_Hide
            // 
            this.m_ToolStripMenuItem_File_Hide.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_File_Hide.Image")));
            this.m_ToolStripMenuItem_File_Hide.Name = "m_ToolStripMenuItem_File_Hide";
            this.m_ToolStripMenuItem_File_Hide.Size = new System.Drawing.Size(163, 22);
            this.m_ToolStripMenuItem_File_Hide.Text = "&Minimize to Tray";
            this.m_ToolStripMenuItem_File_Hide.Click += new System.EventHandler(this.m_ToolStripMenuItem_File_Hide_Click);
            // 
            // m_ToolStripMenuItem_File_Exit
            // 
            this.m_ToolStripMenuItem_File_Exit.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_File_Exit.Image")));
            this.m_ToolStripMenuItem_File_Exit.Name = "m_ToolStripMenuItem_File_Exit";
            this.m_ToolStripMenuItem_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.m_ToolStripMenuItem_File_Exit.Size = new System.Drawing.Size(163, 22);
            this.m_ToolStripMenuItem_File_Exit.Text = "E&xit";
            this.m_ToolStripMenuItem_File_Exit.Click += new System.EventHandler(this.m_ToolStripMenuItem_File_Exit_Click);
            // 
            // m_ToolStripMenuItem_Edit
            // 
            this.m_ToolStripMenuItem_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItem_Edit_Cut,
            this.m_ToolStripMenuItem_Edit_Copy,
            this.m_ToolStripMenuItem_Edit_Paste,
            this.m_ToolStripMenuItem_Edit_Sep1,
            this.m_ToolStripMenuItem_Edit_Test});
            this.m_ToolStripMenuItem_Edit.Name = "m_ToolStripMenuItem_Edit";
            this.m_ToolStripMenuItem_Edit.Size = new System.Drawing.Size(39, 20);
            this.m_ToolStripMenuItem_Edit.Text = "&Edit";
            // 
            // m_ToolStripMenuItem_Edit_Cut
            // 
            this.m_ToolStripMenuItem_Edit_Cut.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_Edit_Cut.Image")));
            this.m_ToolStripMenuItem_Edit_Cut.Name = "m_ToolStripMenuItem_Edit_Cut";
            this.m_ToolStripMenuItem_Edit_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.m_ToolStripMenuItem_Edit_Cut.Size = new System.Drawing.Size(144, 22);
            this.m_ToolStripMenuItem_Edit_Cut.Text = "C&rop";
            this.m_ToolStripMenuItem_Edit_Cut.Click += new System.EventHandler(this.m_ToolStripMenuItem_Edit_Cut_Click);
            // 
            // m_ToolStripMenuItem_Edit_Copy
            // 
            this.m_ToolStripMenuItem_Edit_Copy.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_Edit_Copy.Image")));
            this.m_ToolStripMenuItem_Edit_Copy.Name = "m_ToolStripMenuItem_Edit_Copy";
            this.m_ToolStripMenuItem_Edit_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.m_ToolStripMenuItem_Edit_Copy.Size = new System.Drawing.Size(144, 22);
            this.m_ToolStripMenuItem_Edit_Copy.Text = "&Copy";
            this.m_ToolStripMenuItem_Edit_Copy.Click += new System.EventHandler(this.m_ToolStripMenuItem_Edit_Copy_Click);
            // 
            // m_ToolStripMenuItem_Edit_Paste
            // 
            this.m_ToolStripMenuItem_Edit_Paste.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_Edit_Paste.Image")));
            this.m_ToolStripMenuItem_Edit_Paste.Name = "m_ToolStripMenuItem_Edit_Paste";
            this.m_ToolStripMenuItem_Edit_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.m_ToolStripMenuItem_Edit_Paste.Size = new System.Drawing.Size(144, 22);
            this.m_ToolStripMenuItem_Edit_Paste.Text = "P&aste";
            this.m_ToolStripMenuItem_Edit_Paste.Click += new System.EventHandler(this.m_ToolStripMenuItem_Edit_Paste_Click);
            // 
            // m_ToolStripMenuItem_Edit_Sep1
            // 
            this.m_ToolStripMenuItem_Edit_Sep1.Name = "m_ToolStripMenuItem_Edit_Sep1";
            this.m_ToolStripMenuItem_Edit_Sep1.Size = new System.Drawing.Size(141, 6);
            // 
            // m_ToolStripMenuItem_Edit_Test
            // 
            this.m_ToolStripMenuItem_Edit_Test.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_Edit_Test.Image")));
            this.m_ToolStripMenuItem_Edit_Test.Name = "m_ToolStripMenuItem_Edit_Test";
            this.m_ToolStripMenuItem_Edit_Test.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.m_ToolStripMenuItem_Edit_Test.Size = new System.Drawing.Size(144, 22);
            this.m_ToolStripMenuItem_Edit_Test.Text = "&Test";
            this.m_ToolStripMenuItem_Edit_Test.Click += new System.EventHandler(this.m_ToolStripMenuItem_Edit_Test_Click);
            // 
            // m_ToolStripMenuItem_View
            // 
            this.m_ToolStripMenuItem_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItem_View_SnapShot,
            this.m_ToolStripMenuItem_View_Debug});
            this.m_ToolStripMenuItem_View.Name = "m_ToolStripMenuItem_View";
            this.m_ToolStripMenuItem_View.Size = new System.Drawing.Size(44, 20);
            this.m_ToolStripMenuItem_View.Text = "&View";
            // 
            // m_ToolStripMenuItem_View_SnapShot
            // 
            this.m_ToolStripMenuItem_View_SnapShot.Checked = true;
            this.m_ToolStripMenuItem_View_SnapShot.CheckOnClick = true;
            this.m_ToolStripMenuItem_View_SnapShot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_ToolStripMenuItem_View_SnapShot.Name = "m_ToolStripMenuItem_View_SnapShot";
            this.m_ToolStripMenuItem_View_SnapShot.Size = new System.Drawing.Size(203, 22);
            this.m_ToolStripMenuItem_View_SnapShot.Text = "Show &SnapShot Window";
            this.m_ToolStripMenuItem_View_SnapShot.Click += new System.EventHandler(this.m_ToolStripMenuItem_View_SnapShot_Click);
            // 
            // m_ToolStripMenuItem_View_Debug
            // 
            this.m_ToolStripMenuItem_View_Debug.CheckOnClick = true;
            this.m_ToolStripMenuItem_View_Debug.Name = "m_ToolStripMenuItem_View_Debug";
            this.m_ToolStripMenuItem_View_Debug.Size = new System.Drawing.Size(203, 22);
            this.m_ToolStripMenuItem_View_Debug.Text = "Show &Debug Window";
            this.m_ToolStripMenuItem_View_Debug.Click += new System.EventHandler(this.m_ToolStripMenuItem_View_Debug_Click);
            // 
            // m_ToolStripMenuItem_Favorites
            // 
            this.m_ToolStripMenuItem_Favorites.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItem_Favorites_Add,
            this.m_ToolStripMenuItem_Favorites_Organize,
            this.m_ToolStripMenuItem_Favorites_Sep1});
            this.m_ToolStripMenuItem_Favorites.Name = "m_ToolStripMenuItem_Favorites";
            this.m_ToolStripMenuItem_Favorites.Size = new System.Drawing.Size(66, 20);
            this.m_ToolStripMenuItem_Favorites.Text = "F&avorites";
            this.m_ToolStripMenuItem_Favorites.DropDownOpening += new System.EventHandler(this.m_ToolStripMenuItem_Favorites_DropDownOpening);
            // 
            // m_ToolStripMenuItem_Favorites_Add
            // 
            this.m_ToolStripMenuItem_Favorites_Add.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_Favorites_Add.Image")));
            this.m_ToolStripMenuItem_Favorites_Add.Name = "m_ToolStripMenuItem_Favorites_Add";
            this.m_ToolStripMenuItem_Favorites_Add.Size = new System.Drawing.Size(171, 22);
            this.m_ToolStripMenuItem_Favorites_Add.Text = "&Add to Favorites";
            this.m_ToolStripMenuItem_Favorites_Add.Click += new System.EventHandler(this.m_ToolStripMenuItem_Favorites_Add_Click);
            // 
            // m_ToolStripMenuItem_Favorites_Organize
            // 
            this.m_ToolStripMenuItem_Favorites_Organize.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_Favorites_Organize.Image")));
            this.m_ToolStripMenuItem_Favorites_Organize.Name = "m_ToolStripMenuItem_Favorites_Organize";
            this.m_ToolStripMenuItem_Favorites_Organize.Size = new System.Drawing.Size(171, 22);
            this.m_ToolStripMenuItem_Favorites_Organize.Text = "&Organize Favorites";
            this.m_ToolStripMenuItem_Favorites_Organize.Click += new System.EventHandler(this.m_ToolStripMenuItem_Favorites_Organize_Click);
            // 
            // m_ToolStripMenuItem_Favorites_Sep1
            // 
            this.m_ToolStripMenuItem_Favorites_Sep1.Name = "m_ToolStripMenuItem_Favorites_Sep1";
            this.m_ToolStripMenuItem_Favorites_Sep1.Size = new System.Drawing.Size(168, 6);
            // 
            // m_ToolStripMenuItem_Tools
            // 
            this.m_ToolStripMenuItem_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItem_Tools_Settings,
            this.m_ToolStripMenuItem_Tools_Sep1,
            this.m_ToolStripMenuItem_Tools_ReverseChars,
            this.m_ToolStripMenuItem_Tools_Convert,
            this.m_ToolStripMenuItem_Tools_Encoding});
            this.m_ToolStripMenuItem_Tools.Name = "m_ToolStripMenuItem_Tools";
            this.m_ToolStripMenuItem_Tools.Size = new System.Drawing.Size(48, 20);
            this.m_ToolStripMenuItem_Tools.Text = "&Tools";
            // 
            // m_ToolStripMenuItem_Tools_Settings
            // 
            this.m_ToolStripMenuItem_Tools_Settings.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_Tools_Settings.Image")));
            this.m_ToolStripMenuItem_Tools_Settings.Name = "m_ToolStripMenuItem_Tools_Settings";
            this.m_ToolStripMenuItem_Tools_Settings.Size = new System.Drawing.Size(177, 22);
            this.m_ToolStripMenuItem_Tools_Settings.Text = "&Properties";
            this.m_ToolStripMenuItem_Tools_Settings.Click += new System.EventHandler(this.m_ToolStripMenuItem_Tools_Settings_Click);
            // 
            // m_ToolStripMenuItem_Tools_Sep1
            // 
            this.m_ToolStripMenuItem_Tools_Sep1.Name = "m_ToolStripMenuItem_Tools_Sep1";
            this.m_ToolStripMenuItem_Tools_Sep1.Size = new System.Drawing.Size(174, 6);
            // 
            // m_ToolStripMenuItem_Tools_ReverseChars
            // 
            this.m_ToolStripMenuItem_Tools_ReverseChars.Name = "m_ToolStripMenuItem_Tools_ReverseChars";
            this.m_ToolStripMenuItem_Tools_ReverseChars.Size = new System.Drawing.Size(177, 22);
            this.m_ToolStripMenuItem_Tools_ReverseChars.Text = "&Reverese char order";
            this.m_ToolStripMenuItem_Tools_ReverseChars.Click += new System.EventHandler(this.m_ToolStripMenuItem_Tools_ReverseChars_Click);
            // 
            // m_ToolStripMenuItem_Tools_Convert
            // 
            this.m_ToolStripMenuItem_Tools_Convert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItem_Tools_UnescapeURI});
            this.m_ToolStripMenuItem_Tools_Convert.Name = "m_ToolStripMenuItem_Tools_Convert";
            this.m_ToolStripMenuItem_Tools_Convert.Size = new System.Drawing.Size(177, 22);
            this.m_ToolStripMenuItem_Tools_Convert.Text = "Text Convert";
            // 
            // m_ToolStripMenuItem_Tools_UnescapeURI
            // 
            this.m_ToolStripMenuItem_Tools_UnescapeURI.Name = "m_ToolStripMenuItem_Tools_UnescapeURI";
            this.m_ToolStripMenuItem_Tools_UnescapeURI.Size = new System.Drawing.Size(146, 22);
            this.m_ToolStripMenuItem_Tools_UnescapeURI.Text = "Unescape URI";
            this.m_ToolStripMenuItem_Tools_UnescapeURI.Click += new System.EventHandler(this.m_ToolStripMenuItem_Tools_UnescapeURI_Click);
            // 
            // m_ToolStripMenuItem_Tools_Encoding
            // 
            this.m_ToolStripMenuItem_Tools_Encoding.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_ToolStripMenuItem_Tools_Encoding_Config,
            this.m_ToolStripMenuItem_Tools_Encoding_Sep1});
            this.m_ToolStripMenuItem_Tools_Encoding.Name = "m_ToolStripMenuItem_Tools_Encoding";
            this.m_ToolStripMenuItem_Tools_Encoding.Size = new System.Drawing.Size(177, 22);
            this.m_ToolStripMenuItem_Tools_Encoding.Text = "&Encoding";
            // 
            // m_ToolStripMenuItem_Tools_Encoding_Config
            // 
            this.m_ToolStripMenuItem_Tools_Encoding_Config.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripMenuItem_Tools_Encoding_Config.Image")));
            this.m_ToolStripMenuItem_Tools_Encoding_Config.Name = "m_ToolStripMenuItem_Tools_Encoding_Config";
            this.m_ToolStripMenuItem_Tools_Encoding_Config.Size = new System.Drawing.Size(185, 22);
            this.m_ToolStripMenuItem_Tools_Encoding_Config.Text = "Con&figure Encodings";
            this.m_ToolStripMenuItem_Tools_Encoding_Config.Click += new System.EventHandler(this.m_ToolStripMenuItem_Tools_Encoding_Config_Click);
            // 
            // m_ToolStripMenuItem_Tools_Encoding_Sep1
            // 
            this.m_ToolStripMenuItem_Tools_Encoding_Sep1.Name = "m_ToolStripMenuItem_Tools_Encoding_Sep1";
            this.m_ToolStripMenuItem_Tools_Encoding_Sep1.Size = new System.Drawing.Size(182, 6);
            // 
            // ToolStripMenuItem_Help
            // 
            this.ToolStripMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Help_About});
            this.ToolStripMenuItem_Help.Name = "ToolStripMenuItem_Help";
            this.ToolStripMenuItem_Help.Size = new System.Drawing.Size(44, 20);
            this.ToolStripMenuItem_Help.Text = "&Help";
            // 
            // ToolStripMenuItem_Help_About
            // 
            this.ToolStripMenuItem_Help_About.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_Help_About.Image")));
            this.ToolStripMenuItem_Help_About.Name = "ToolStripMenuItem_Help_About";
            this.ToolStripMenuItem_Help_About.Size = new System.Drawing.Size(107, 22);
            this.ToolStripMenuItem_Help_About.Text = "&About";
            this.ToolStripMenuItem_Help_About.Click += new System.EventHandler(this.m_ToolStripMenuItem_Help_About_Click);
            // 
            // m_contextMenuStrip_RichTextBox_Cut
            // 
            this.m_contextMenuStrip_RichTextBox_Cut.Name = "m_contextMenuStrip_RichTextBox_Cut";
            this.m_contextMenuStrip_RichTextBox_Cut.Size = new System.Drawing.Size(202, 22);
            this.m_contextMenuStrip_RichTextBox_Cut.Text = "From Edit -> Cut";
            // 
            // m_contextMenuStrip_RichTextBox_Copy
            // 
            this.m_contextMenuStrip_RichTextBox_Copy.Name = "m_contextMenuStrip_RichTextBox_Copy";
            this.m_contextMenuStrip_RichTextBox_Copy.Size = new System.Drawing.Size(202, 22);
            this.m_contextMenuStrip_RichTextBox_Copy.Text = "From Main Edit -> Copy";
            // 
            // m_contextMenuStrip_RichTextBox_Paste
            // 
            this.m_contextMenuStrip_RichTextBox_Paste.Name = "m_contextMenuStrip_RichTextBox_Paste";
            this.m_contextMenuStrip_RichTextBox_Paste.Size = new System.Drawing.Size(202, 22);
            this.m_contextMenuStrip_RichTextBox_Paste.Text = "From Main Edit -> Paste";
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripStatusLabel1,
            this.m_toolStripStatusLabel2,
            this.m_toolStripStatusLabel3});
            this.m_statusStrip.Location = new System.Drawing.Point(0, 536);
            this.m_statusStrip.Name = "m_statusStrip";
            this.m_statusStrip.Size = new System.Drawing.Size(984, 26);
            this.m_statusStrip.TabIndex = 2;
            this.m_statusStrip.Text = "m_statusStrip";
            // 
            // m_toolStripStatusLabel1
            // 
            this.m_toolStripStatusLabel1.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.m_toolStripStatusLabel1.Name = "m_toolStripStatusLabel1";
            this.m_toolStripStatusLabel1.Size = new System.Drawing.Size(923, 21);
            this.m_toolStripStatusLabel1.Spring = true;
            this.m_toolStripStatusLabel1.Text = "Ready";
            this.m_toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_toolStripStatusLabel2
            // 
            this.m_toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.m_toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(0, 5, 0, 2);
            this.m_toolStripStatusLabel2.Name = "m_toolStripStatusLabel2";
            this.m_toolStripStatusLabel2.Size = new System.Drawing.Size(23, 19);
            this.m_toolStripStatusLabel2.Text = "    ";
            // 
            // m_toolStripStatusLabel3
            // 
            this.m_toolStripStatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.m_toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.m_toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(0, 5, 0, 2);
            this.m_toolStripStatusLabel3.Name = "m_toolStripStatusLabel3";
            this.m_toolStripStatusLabel3.Size = new System.Drawing.Size(23, 19);
            this.m_toolStripStatusLabel3.Text = "    ";
            // 
            // m_toolStripContainer
            // 
            // 
            // m_toolStripContainer.ContentPanel
            // 
            this.m_toolStripContainer.ContentPanel.Controls.Add(this.m_splitContainerClipboard);
            this.m_toolStripContainer.ContentPanel.Controls.Add(this.m_HSplitterDebug);
            this.m_toolStripContainer.ContentPanel.Controls.Add(this.m_pnlDebug);
            this.m_toolStripContainer.ContentPanel.Size = new System.Drawing.Size(618, 481);
            this.m_toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.m_toolStripContainer.Name = "m_toolStripContainer";
            this.m_toolStripContainer.Size = new System.Drawing.Size(618, 510);
            this.m_toolStripContainer.TabIndex = 1;
            this.m_toolStripContainer.Text = "Tools";
            // 
            // m_toolStripContainer.TopToolStripPanel
            // 
            this.m_toolStripContainer.TopToolStripPanel.Controls.Add(this.m_toolStrip);
            // 
            // m_splitContainerClipboard
            // 
            this.m_splitContainerClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitContainerClipboard.Location = new System.Drawing.Point(0, 0);
            this.m_splitContainerClipboard.Name = "m_splitContainerClipboard";
            this.m_splitContainerClipboard.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainerClipboard.Panel1
            // 
            this.m_splitContainerClipboard.Panel1.Controls.Add(this.m_pnlClipboard);
            // 
            // m_splitContainerClipboard.Panel2
            // 
            this.m_splitContainerClipboard.Panel2.Controls.Add(this.m_pnlSnapShot);
            this.m_splitContainerClipboard.Size = new System.Drawing.Size(618, 374);
            this.m_splitContainerClipboard.SplitterDistance = 179;
            this.m_splitContainerClipboard.TabIndex = 0;
            // 
            // m_pnlClipboard
            // 
            this.m_pnlClipboard.Controls.Add(this.m_richTextBoxClipboard);
            this.m_pnlClipboard.Controls.Add(this.m_pnlClipboardLeft);
            this.m_pnlClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlClipboard.Location = new System.Drawing.Point(0, 0);
            this.m_pnlClipboard.Name = "m_pnlClipboard";
            this.m_pnlClipboard.Size = new System.Drawing.Size(618, 179);
            this.m_pnlClipboard.TabIndex = 3;
            // 
            // m_richTextBoxClipboard
            // 
            this.m_richTextBoxClipboard.AcceptsTab = true;
            this.m_richTextBoxClipboard.ContextMenuStrip = this.m_contextMenuStrip_RichTextBox;
            this.m_richTextBoxClipboard.DetectUrls = false;
            this.m_richTextBoxClipboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_richTextBoxClipboard.EnableAutoDragDrop = true;
            this.m_richTextBoxClipboard.Location = new System.Drawing.Point(26, 0);
            this.m_richTextBoxClipboard.Name = "m_richTextBoxClipboard";
            this.m_richTextBoxClipboard.Size = new System.Drawing.Size(592, 179);
            this.m_richTextBoxClipboard.TabIndex = 0;
            this.m_richTextBoxClipboard.Text = "";
            this.m_richTextBoxClipboard.WordWrap = false;
            this.m_richTextBoxClipboard.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.m_richTextBoxClipboard_LinkClicked);
            this.m_richTextBoxClipboard.SelectionChanged += new System.EventHandler(this.m_richTextBoxClipboard_SelectionChanged);
            this.m_richTextBoxClipboard.TextChanged += new System.EventHandler(this.m_richTextBoxClipboard_TextChanged);
            this.m_richTextBoxClipboard.Enter += new System.EventHandler(this.m_richTextBoxClipboard_Enter);
            // 
            // m_contextMenuStrip_RichTextBox
            // 
            this.m_contextMenuStrip_RichTextBox.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_contextMenuStrip_RichTextBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_contextMenuStrip_RichTextBox_Cut,
            this.m_contextMenuStrip_RichTextBox_Copy,
            this.m_contextMenuStrip_RichTextBox_Paste});
            this.m_contextMenuStrip_RichTextBox.Name = "m_contextMenuStrip_RichTextBox";
            this.m_contextMenuStrip_RichTextBox.Size = new System.Drawing.Size(203, 70);
            this.m_contextMenuStrip_RichTextBox.Opening += new System.ComponentModel.CancelEventHandler(this.m_contextMenuStrip_RichTextBox_Opening);
            // 
            // m_pnlClipboardLeft
            // 
            this.m_pnlClipboardLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlClipboardLeft.Controls.Add(this.m_icoClipboardApp);
            this.m_pnlClipboardLeft.Controls.Add(this.m_lblClipboardType);
            this.m_pnlClipboardLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_pnlClipboardLeft.Location = new System.Drawing.Point(0, 0);
            this.m_pnlClipboardLeft.Name = "m_pnlClipboardLeft";
            this.m_pnlClipboardLeft.Size = new System.Drawing.Size(26, 179);
            this.m_pnlClipboardLeft.TabIndex = 0;
            // 
            // m_icoClipboardApp
            // 
            this.m_icoClipboardApp.Image = ((System.Drawing.Image)(resources.GetObject("m_icoClipboardApp.Image")));
            this.m_icoClipboardApp.InitialImage = ((System.Drawing.Image)(resources.GetObject("m_icoClipboardApp.InitialImage")));
            this.m_icoClipboardApp.Location = new System.Drawing.Point(3, 3);
            this.m_icoClipboardApp.Name = "m_icoClipboardApp";
            this.m_icoClipboardApp.Size = new System.Drawing.Size(16, 16);
            this.m_icoClipboardApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_icoClipboardApp.TabIndex = 3;
            this.m_icoClipboardApp.TabStop = false;
            // 
            // m_lblClipboardType
            // 
            this.m_lblClipboardType.AutoSize = true;
            this.m_lblClipboardType.ImageIndex = 5;
            this.m_lblClipboardType.ImageList = this.m_imageListClipboardTypes;
            this.m_lblClipboardType.Location = new System.Drawing.Point(3, 28);
            this.m_lblClipboardType.MinimumSize = new System.Drawing.Size(16, 16);
            this.m_lblClipboardType.Name = "m_lblClipboardType";
            this.m_lblClipboardType.Size = new System.Drawing.Size(16, 16);
            this.m_lblClipboardType.TabIndex = 0;
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
            // m_pnlSnapShot
            // 
            this.m_pnlSnapShot.Controls.Add(this.m_richTextBoxSnapShot);
            this.m_pnlSnapShot.Controls.Add(this.m_pnlSnapShotLeft);
            this.m_pnlSnapShot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlSnapShot.Location = new System.Drawing.Point(0, 0);
            this.m_pnlSnapShot.Name = "m_pnlSnapShot";
            this.m_pnlSnapShot.Size = new System.Drawing.Size(618, 191);
            this.m_pnlSnapShot.TabIndex = 4;
            // 
            // m_richTextBoxSnapShot
            // 
            this.m_richTextBoxSnapShot.BackColor = System.Drawing.SystemColors.Info;
            this.m_richTextBoxSnapShot.ContextMenuStrip = this.m_contextMenuStrip_RichTextBox;
            this.m_richTextBoxSnapShot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_richTextBoxSnapShot.EnableAutoDragDrop = true;
            this.m_richTextBoxSnapShot.HideSelection = false;
            this.m_richTextBoxSnapShot.Location = new System.Drawing.Point(26, 0);
            this.m_richTextBoxSnapShot.Name = "m_richTextBoxSnapShot";
            this.m_richTextBoxSnapShot.ReadOnly = true;
            this.m_richTextBoxSnapShot.Size = new System.Drawing.Size(592, 191);
            this.m_richTextBoxSnapShot.TabIndex = 1;
            this.m_richTextBoxSnapShot.Text = "";
            this.m_richTextBoxSnapShot.WordWrap = false;
            this.m_richTextBoxSnapShot.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.m_richTextBoxSnapShot_LinkClicked);
            this.m_richTextBoxSnapShot.SelectionChanged += new System.EventHandler(this.m_richTextBoxSnapShot_SelectionChanged);
            this.m_richTextBoxSnapShot.Enter += new System.EventHandler(this.m_richTextBoxSnapShot_Enter);
            // 
            // m_pnlSnapShotLeft
            // 
            this.m_pnlSnapShotLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlSnapShotLeft.Controls.Add(this.m_icoSnapShotApp);
            this.m_pnlSnapShotLeft.Controls.Add(this.m_lblSnapShotType);
            this.m_pnlSnapShotLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_pnlSnapShotLeft.Location = new System.Drawing.Point(0, 0);
            this.m_pnlSnapShotLeft.Name = "m_pnlSnapShotLeft";
            this.m_pnlSnapShotLeft.Size = new System.Drawing.Size(26, 191);
            this.m_pnlSnapShotLeft.TabIndex = 0;
            // 
            // m_icoSnapShotApp
            // 
            this.m_icoSnapShotApp.Image = ((System.Drawing.Image)(resources.GetObject("m_icoSnapShotApp.Image")));
            this.m_icoSnapShotApp.InitialImage = ((System.Drawing.Image)(resources.GetObject("m_icoSnapShotApp.InitialImage")));
            this.m_icoSnapShotApp.Location = new System.Drawing.Point(3, 3);
            this.m_icoSnapShotApp.Name = "m_icoSnapShotApp";
            this.m_icoSnapShotApp.Size = new System.Drawing.Size(16, 16);
            this.m_icoSnapShotApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.m_icoSnapShotApp.TabIndex = 2;
            this.m_icoSnapShotApp.TabStop = false;
            // 
            // m_lblSnapShotType
            // 
            this.m_lblSnapShotType.AutoSize = true;
            this.m_lblSnapShotType.ImageIndex = 5;
            this.m_lblSnapShotType.ImageList = this.m_imageListClipboardTypes;
            this.m_lblSnapShotType.Location = new System.Drawing.Point(3, 28);
            this.m_lblSnapShotType.MinimumSize = new System.Drawing.Size(16, 16);
            this.m_lblSnapShotType.Name = "m_lblSnapShotType";
            this.m_lblSnapShotType.Size = new System.Drawing.Size(16, 16);
            this.m_lblSnapShotType.TabIndex = 0;
            // 
            // m_HSplitterDebug
            // 
            this.m_HSplitterDebug.BackColor = System.Drawing.SystemColors.Control;
            this.m_HSplitterDebug.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_HSplitterDebug.Location = new System.Drawing.Point(0, 374);
            this.m_HSplitterDebug.MinExtra = 50;
            this.m_HSplitterDebug.MinSize = 50;
            this.m_HSplitterDebug.Name = "m_HSplitterDebug";
            this.m_HSplitterDebug.Size = new System.Drawing.Size(618, 4);
            this.m_HSplitterDebug.TabIndex = 1;
            this.m_HSplitterDebug.TabStop = false;
            this.m_HSplitterDebug.Visible = false;
            // 
            // m_pnlDebug
            // 
            this.m_pnlDebug.Controls.Add(this.m_richTextBoxDebug);
            this.m_pnlDebug.Controls.Add(this.m_pnlDebugLeft);
            this.m_pnlDebug.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_pnlDebug.Location = new System.Drawing.Point(0, 378);
            this.m_pnlDebug.Name = "m_pnlDebug";
            this.m_pnlDebug.Size = new System.Drawing.Size(618, 103);
            this.m_pnlDebug.TabIndex = 5;
            this.m_pnlDebug.Visible = false;
            // 
            // m_richTextBoxDebug
            // 
            this.m_richTextBoxDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_richTextBoxDebug.Location = new System.Drawing.Point(26, 0);
            this.m_richTextBoxDebug.Name = "m_richTextBoxDebug";
            this.m_richTextBoxDebug.Size = new System.Drawing.Size(592, 103);
            this.m_richTextBoxDebug.TabIndex = 1;
            this.m_richTextBoxDebug.Text = "";
            // 
            // m_pnlDebugLeft
            // 
            this.m_pnlDebugLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_pnlDebugLeft.Controls.Add(this.m_btnDebugScroll);
            this.m_pnlDebugLeft.Controls.Add(this.m_btnDebugCopy);
            this.m_pnlDebugLeft.Controls.Add(this.m_btnDebugClear);
            this.m_pnlDebugLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.m_pnlDebugLeft.ForeColor = System.Drawing.SystemColors.WindowText;
            this.m_pnlDebugLeft.Location = new System.Drawing.Point(0, 0);
            this.m_pnlDebugLeft.Name = "m_pnlDebugLeft";
            this.m_pnlDebugLeft.Size = new System.Drawing.Size(26, 103);
            this.m_pnlDebugLeft.TabIndex = 0;
            // 
            // m_btnDebugScroll
            // 
            this.m_btnDebugScroll.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnDebugScroll.ImageIndex = 0;
            this.m_btnDebugScroll.ImageList = this.m_ImageList_Scroll;
            this.m_btnDebugScroll.Location = new System.Drawing.Point(2, 52);
            this.m_btnDebugScroll.Name = "m_btnDebugScroll";
            this.m_btnDebugScroll.Size = new System.Drawing.Size(19, 18);
            this.m_btnDebugScroll.TabIndex = 2;
            this.m_btnDebugScroll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.m_ToolTip.SetToolTip(this.m_btnDebugScroll, "Scroll Unlocked");
            this.m_btnDebugScroll.UseVisualStyleBackColor = false;
            this.m_btnDebugScroll.Click += new System.EventHandler(this.m_btnDebugScroll_Click);
            // 
            // m_ImageList_Scroll
            // 
            this.m_ImageList_Scroll.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ImageList_Scroll.ImageStream")));
            this.m_ImageList_Scroll.TransparentColor = System.Drawing.Color.Transparent;
            this.m_ImageList_Scroll.Images.SetKeyName(0, "ScrollUnLock.ico");
            this.m_ImageList_Scroll.Images.SetKeyName(1, "ScrollLock.ico");
            // 
            // m_btnDebugCopy
            // 
            this.m_btnDebugCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnDebugCopy.Image = ((System.Drawing.Image)(resources.GetObject("m_btnDebugCopy.Image")));
            this.m_btnDebugCopy.Location = new System.Drawing.Point(2, 28);
            this.m_btnDebugCopy.Name = "m_btnDebugCopy";
            this.m_btnDebugCopy.Size = new System.Drawing.Size(19, 18);
            this.m_btnDebugCopy.TabIndex = 1;
            this.m_ToolTip.SetToolTip(this.m_btnDebugCopy, "Copy Selected text To Clipboard");
            this.m_btnDebugCopy.UseVisualStyleBackColor = true;
            this.m_btnDebugCopy.Click += new System.EventHandler(this.m_btnDebugCopy_Click);
            // 
            // m_btnDebugClear
            // 
            this.m_btnDebugClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.m_btnDebugClear.Image = ((System.Drawing.Image)(resources.GetObject("m_btnDebugClear.Image")));
            this.m_btnDebugClear.Location = new System.Drawing.Point(2, 4);
            this.m_btnDebugClear.Name = "m_btnDebugClear";
            this.m_btnDebugClear.Size = new System.Drawing.Size(19, 18);
            this.m_btnDebugClear.TabIndex = 0;
            this.m_ToolTip.SetToolTip(this.m_btnDebugClear, "Clear Debug Window");
            this.m_btnDebugClear.UseVisualStyleBackColor = true;
            this.m_btnDebugClear.Click += new System.EventHandler(this.m_btnDebugClear_Click);
            // 
            // m_toolStrip
            // 
            this.m_toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.m_toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripButton_History,
            this.m_toolStripSeparator1,
            this.m_toolStripButton_OnTop,
            this.m_toolStripSeparator2,
            this.m_toolStripButton_Target,
            this.m_toolStripSeparator3,
            this.m_toolStripButton_Cut,
            this.m_toolStripButton_Copy,
            this.m_toolStripButton_Paste,
            this.m_toolStripSeparator4,
            this.m_toolStripButton_Print,
            this.m_toolStripSeparator5,
            this.m_ToolStripComboBox_CFormats,
            this.m_toolStripSeparator6,
            this.m_ToolStripSplitButton_FontSize,
            this.m_toolStripSeparator7,
            this.m_toolStripButton_Wordwrap,
            this.toolStripSeparator1,
            this.m_tbbtnDataFolder});
            this.m_toolStrip.Location = new System.Drawing.Point(3, 0);
            this.m_toolStrip.Name = "m_toolStrip";
            this.m_toolStrip.Size = new System.Drawing.Size(615, 29);
            this.m_toolStrip.TabIndex = 0;
            // 
            // m_toolStripButton_History
            // 
            this.m_toolStripButton_History.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_History.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_History.Image")));
            this.m_toolStripButton_History.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_History.Name = "m_toolStripButton_History";
            this.m_toolStripButton_History.Size = new System.Drawing.Size(24, 26);
            this.m_toolStripButton_History.Text = "Save";
            this.m_toolStripButton_History.ToolTipText = "Clipboard History";
            this.m_toolStripButton_History.Click += new System.EventHandler(this.m_toolStripButton_History_Click);
            // 
            // m_toolStripSeparator1
            // 
            this.m_toolStripSeparator1.Name = "m_toolStripSeparator1";
            this.m_toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // m_toolStripButton_OnTop
            // 
            this.m_toolStripButton_OnTop.CheckOnClick = true;
            this.m_toolStripButton_OnTop.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_OnTop.Image")));
            this.m_toolStripButton_OnTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_OnTop.Name = "m_toolStripButton_OnTop";
            this.m_toolStripButton_OnTop.Size = new System.Drawing.Size(71, 26);
            this.m_toolStripButton_OnTop.Text = "On &Top";
            this.m_toolStripButton_OnTop.ToolTipText = "Always on Top";
            this.m_toolStripButton_OnTop.Click += new System.EventHandler(this.m_toolStripButton_OnTop_Click);
            // 
            // m_toolStripSeparator2
            // 
            this.m_toolStripSeparator2.Name = "m_toolStripSeparator2";
            this.m_toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // m_toolStripButton_Target
            // 
            this.m_toolStripButton_Target.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Target.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Target.Image")));
            this.m_toolStripButton_Target.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Target.Name = "m_toolStripButton_Target";
            this.m_toolStripButton_Target.Size = new System.Drawing.Size(24, 26);
            this.m_toolStripButton_Target.Text = "toolStripButton1";
            this.m_toolStripButton_Target.ToolTipText = "Copy from other window";
            this.m_toolStripButton_Target.Click += new System.EventHandler(this.m_toolStripButton_Target_Click);
            // 
            // m_toolStripSeparator3
            // 
            this.m_toolStripSeparator3.Name = "m_toolStripSeparator3";
            this.m_toolStripSeparator3.Size = new System.Drawing.Size(6, 29);
            // 
            // m_toolStripButton_Cut
            // 
            this.m_toolStripButton_Cut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Cut.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Cut.Image")));
            this.m_toolStripButton_Cut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Cut.Name = "m_toolStripButton_Cut";
            this.m_toolStripButton_Cut.Size = new System.Drawing.Size(24, 26);
            this.m_toolStripButton_Cut.Text = "Crop";
            this.m_toolStripButton_Cut.ToolTipText = "Crop/Copy";
            this.m_toolStripButton_Cut.Click += new System.EventHandler(this.m_toolStripButton_Cut_Click);
            // 
            // m_toolStripButton_Copy
            // 
            this.m_toolStripButton_Copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Copy.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Copy.Image")));
            this.m_toolStripButton_Copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Copy.Name = "m_toolStripButton_Copy";
            this.m_toolStripButton_Copy.Size = new System.Drawing.Size(24, 26);
            this.m_toolStripButton_Copy.Text = "Copy";
            this.m_toolStripButton_Copy.ToolTipText = "Copy";
            this.m_toolStripButton_Copy.Click += new System.EventHandler(this.m_toolStripButton_Copy_Click);
            // 
            // m_toolStripButton_Paste
            // 
            this.m_toolStripButton_Paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Paste.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Paste.Image")));
            this.m_toolStripButton_Paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Paste.Name = "m_toolStripButton_Paste";
            this.m_toolStripButton_Paste.Size = new System.Drawing.Size(24, 26);
            this.m_toolStripButton_Paste.Text = "Paste";
            this.m_toolStripButton_Paste.ToolTipText = "Paste";
            this.m_toolStripButton_Paste.Click += new System.EventHandler(this.m_toolStripButton_Paste_Click);
            // 
            // m_toolStripSeparator4
            // 
            this.m_toolStripSeparator4.Name = "m_toolStripSeparator4";
            this.m_toolStripSeparator4.Size = new System.Drawing.Size(6, 29);
            // 
            // m_toolStripButton_Print
            // 
            this.m_toolStripButton_Print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Print.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Print.Image")));
            this.m_toolStripButton_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Print.Name = "m_toolStripButton_Print";
            this.m_toolStripButton_Print.Size = new System.Drawing.Size(24, 26);
            this.m_toolStripButton_Print.Text = "Print";
            this.m_toolStripButton_Print.ToolTipText = "Print";
            this.m_toolStripButton_Print.Click += new System.EventHandler(this.m_toolStripButton_Print_Click);
            // 
            // m_toolStripSeparator5
            // 
            this.m_toolStripSeparator5.Name = "m_toolStripSeparator5";
            this.m_toolStripSeparator5.Size = new System.Drawing.Size(6, 29);
            // 
            // m_ToolStripComboBox_CFormats
            // 
            this.m_ToolStripComboBox_CFormats.BackColor = System.Drawing.SystemColors.Info;
            this.m_ToolStripComboBox_CFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_ToolStripComboBox_CFormats.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.m_ToolStripComboBox_CFormats.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.m_ToolStripComboBox_CFormats.Name = "m_ToolStripComboBox_CFormats";
            this.m_ToolStripComboBox_CFormats.Size = new System.Drawing.Size(180, 23);
            this.m_ToolStripComboBox_CFormats.SelectedIndexChanged += new System.EventHandler(this.m_ToolStripComboBox_CFormats_SelectedIndexChanged);
            // 
            // m_toolStripSeparator6
            // 
            this.m_toolStripSeparator6.Name = "m_toolStripSeparator6";
            this.m_toolStripSeparator6.Size = new System.Drawing.Size(6, 29);
            // 
            // m_ToolStripSplitButton_FontSize
            // 
            this.m_ToolStripSplitButton_FontSize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_ToolStripSplitButton_FontSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toolStripButton_FontSize_4,
            this.m_toolStripButton_FontSize_3,
            this.m_toolStripButton_FontSize_2,
            this.m_toolStripButton_FontSize_1,
            this.m_toolStripButton_FontSize_0});
            this.m_ToolStripSplitButton_FontSize.Image = ((System.Drawing.Image)(resources.GetObject("m_ToolStripSplitButton_FontSize.Image")));
            this.m_ToolStripSplitButton_FontSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_ToolStripSplitButton_FontSize.Name = "m_ToolStripSplitButton_FontSize";
            this.m_ToolStripSplitButton_FontSize.Size = new System.Drawing.Size(36, 26);
            this.m_ToolStripSplitButton_FontSize.Text = "Text Size";
            this.m_ToolStripSplitButton_FontSize.ButtonClick += new System.EventHandler(this.m_ToolStripSplitButton_FontSize_ButtonClick);
            // 
            // m_toolStripButton_FontSize_4
            // 
            this.m_toolStripButton_FontSize_4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_toolStripButton_FontSize_4.Name = "m_toolStripButton_FontSize_4";
            this.m_toolStripButton_FontSize_4.Size = new System.Drawing.Size(119, 22);
            this.m_toolStripButton_FontSize_4.Text = "Lar&gest";
            this.m_toolStripButton_FontSize_4.Click += new System.EventHandler(this.m_toolStripButton_FontSize_4_Click);
            // 
            // m_toolStripButton_FontSize_3
            // 
            this.m_toolStripButton_FontSize_3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_toolStripButton_FontSize_3.Name = "m_toolStripButton_FontSize_3";
            this.m_toolStripButton_FontSize_3.Size = new System.Drawing.Size(119, 22);
            this.m_toolStripButton_FontSize_3.Text = "&Larger";
            this.m_toolStripButton_FontSize_3.Click += new System.EventHandler(this.m_toolStripButton_FontSize_3_Click);
            // 
            // m_toolStripButton_FontSize_2
            // 
            this.m_toolStripButton_FontSize_2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_toolStripButton_FontSize_2.Name = "m_toolStripButton_FontSize_2";
            this.m_toolStripButton_FontSize_2.Size = new System.Drawing.Size(119, 22);
            this.m_toolStripButton_FontSize_2.Text = "&Medium";
            this.m_toolStripButton_FontSize_2.Click += new System.EventHandler(this.m_toolStripButton_FontSize_2_Click);
            // 
            // m_toolStripButton_FontSize_1
            // 
            this.m_toolStripButton_FontSize_1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_toolStripButton_FontSize_1.Name = "m_toolStripButton_FontSize_1";
            this.m_toolStripButton_FontSize_1.Size = new System.Drawing.Size(119, 22);
            this.m_toolStripButton_FontSize_1.Text = "&Smaller";
            this.m_toolStripButton_FontSize_1.Click += new System.EventHandler(this.m_toolStripButton_FontSize_1_Click);
            // 
            // m_toolStripButton_FontSize_0
            // 
            this.m_toolStripButton_FontSize_0.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.m_toolStripButton_FontSize_0.Name = "m_toolStripButton_FontSize_0";
            this.m_toolStripButton_FontSize_0.Size = new System.Drawing.Size(119, 22);
            this.m_toolStripButton_FontSize_0.Text = "Sm&allest";
            this.m_toolStripButton_FontSize_0.Click += new System.EventHandler(this.m_toolStripButton_FontSize_0_Click);
            // 
            // m_toolStripSeparator7
            // 
            this.m_toolStripSeparator7.Name = "m_toolStripSeparator7";
            this.m_toolStripSeparator7.Size = new System.Drawing.Size(6, 29);
            // 
            // m_toolStripButton_Wordwrap
            // 
            this.m_toolStripButton_Wordwrap.CheckOnClick = true;
            this.m_toolStripButton_Wordwrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolStripButton_Wordwrap.Image = ((System.Drawing.Image)(resources.GetObject("m_toolStripButton_Wordwrap.Image")));
            this.m_toolStripButton_Wordwrap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolStripButton_Wordwrap.Name = "m_toolStripButton_Wordwrap";
            this.m_toolStripButton_Wordwrap.Size = new System.Drawing.Size(24, 26);
            this.m_toolStripButton_Wordwrap.Text = "Word Wrap";
            this.m_toolStripButton_Wordwrap.ToolTipText = "Word Wrap";
            this.m_toolStripButton_Wordwrap.Click += new System.EventHandler(this.m_toolStripButton_Wordwrap_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // m_tbbtnDataFolder
            // 
            this.m_tbbtnDataFolder.Image = ((System.Drawing.Image)(resources.GetObject("m_tbbtnDataFolder.Image")));
            this.m_tbbtnDataFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_tbbtnDataFolder.Name = "m_tbbtnDataFolder";
            this.m_tbbtnDataFolder.Size = new System.Drawing.Size(132, 26);
            this.m_tbbtnDataFolder.Text = "Open Data Folder...";
            this.m_tbbtnDataFolder.Click += new System.EventHandler(this.m_tbbtnDataFolder_Click);
            // 
            // m_contextMenuStripClipboard
            // 
            this.m_contextMenuStripClipboard.ImageScalingSize = new System.Drawing.Size(33, 16);
            this.m_contextMenuStripClipboard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_contextMenuStripClipboard_Current,
            this.m_contextMenuStripClipboard_Sep1,
            this.m_contextMenuStripClipboard_Favorites,
            this.m_contextMenuStripClipboard_Sep2,
            this.m_emptyToolStripMenuItem});
            this.m_contextMenuStripClipboard.Name = "m_contextMenuStripClipboard";
            this.m_contextMenuStripClipboard.Size = new System.Drawing.Size(195, 82);
            this.m_contextMenuStripClipboard.TabStop = true;
            this.m_contextMenuStripClipboard.Text = "Clipboard History Context Menu";
            this.m_contextMenuStripClipboard.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.m_contextMenuStripClipboard_Closed);
            this.m_contextMenuStripClipboard.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.m_contextMenuStripClipboard_Closing);
            // 
            // m_contextMenuStripClipboard_Current
            // 
            this.m_contextMenuStripClipboard_Current.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.m_contextMenuStripClipboard_Current.Name = "m_contextMenuStripClipboard_Current";
            this.m_contextMenuStripClipboard_Current.Size = new System.Drawing.Size(194, 22);
            this.m_contextMenuStripClipboard_Current.Text = "--LastMenuItem--";
            this.m_contextMenuStripClipboard_Current.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_contextMenuStrip_ClipboardEntry_MouseDown);
            this.m_contextMenuStripClipboard_Current.MouseUp += new System.Windows.Forms.MouseEventHandler(this.m_contextMenuStrip_ClipboardEntry_MouseUp);
            // 
            // m_contextMenuStripClipboard_Sep1
            // 
            this.m_contextMenuStripClipboard_Sep1.Name = "m_contextMenuStripClipboard_Sep1";
            this.m_contextMenuStripClipboard_Sep1.Size = new System.Drawing.Size(191, 6);
            // 
            // m_contextMenuStripClipboard_Favorites
            // 
            this.m_contextMenuStripClipboard_Favorites.Name = "m_contextMenuStripClipboard_Favorites";
            this.m_contextMenuStripClipboard_Favorites.Size = new System.Drawing.Size(194, 22);
            this.m_contextMenuStripClipboard_Favorites.Text = "F&avorites";
            // 
            // m_contextMenuStripClipboard_Sep2
            // 
            this.m_contextMenuStripClipboard_Sep2.Name = "m_contextMenuStripClipboard_Sep2";
            this.m_contextMenuStripClipboard_Sep2.Size = new System.Drawing.Size(191, 6);
            // 
            // m_emptyToolStripMenuItem
            // 
            this.m_emptyToolStripMenuItem.Checked = true;
            this.m_emptyToolStripMenuItem.CheckOnClick = true;
            this.m_emptyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.m_emptyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("m_emptyToolStripMenuItem.Image")));
            this.m_emptyToolStripMenuItem.Name = "m_emptyToolStripMenuItem";
            this.m_emptyToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.m_emptyToolStripMenuItem.Text = "-- Empty --";
            this.m_emptyToolStripMenuItem.Click += new System.EventHandler(this.m_contextMenuStrip_ClipboardEntry_LeftClick);
            // 
            // m_ImageListSysMenu
            // 
            this.m_ImageListSysMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_ImageListSysMenu.ImageStream")));
            this.m_ImageListSysMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.m_ImageListSysMenu.Images.SetKeyName(0, "Favorites.ico");
            this.m_ImageListSysMenu.Images.SetKeyName(1, "RightArrow.ico");
            this.m_ImageListSysMenu.Images.SetKeyName(2, "Exit12.ico");
            this.m_ImageListSysMenu.Images.SetKeyName(3, "Hide12.ico");
            this.m_ImageListSysMenu.Images.SetKeyName(4, "Smile12.ico");
            // 
            // m_contextMenuStrip_ClipboardEntry
            // 
            this.m_contextMenuStrip_ClipboardEntry.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.m_contextMenuStrip_ClipboardEntry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_contextMenuStrip_ClipboardEntry_AddToFavorites,
            this.m_contextMenuStrip_ClipboardEntry_Edit,
            this.m_contextMenuStrip_ClipboardEntry_Remove});
            this.m_contextMenuStrip_ClipboardEntry.Name = "m_contextMenuStrip_ClipboardEntry";
            this.m_contextMenuStrip_ClipboardEntry.Size = new System.Drawing.Size(165, 82);
            this.m_contextMenuStrip_ClipboardEntry.Text = "Operations";
            this.m_contextMenuStrip_ClipboardEntry.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.m_contextMenuStrip_ClipboardEntry_Closed);
            // 
            // m_contextMenuStrip_ClipboardEntry_AddToFavorites
            // 
            this.m_contextMenuStrip_ClipboardEntry_AddToFavorites.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStrip_ClipboardEntry_AddToFavorites.Image")));
            this.m_contextMenuStrip_ClipboardEntry_AddToFavorites.Name = "m_contextMenuStrip_ClipboardEntry_AddToFavorites";
            this.m_contextMenuStrip_ClipboardEntry_AddToFavorites.Size = new System.Drawing.Size(164, 26);
            this.m_contextMenuStrip_ClipboardEntry_AddToFavorites.Text = "&Add to Favorites";
            this.m_contextMenuStrip_ClipboardEntry_AddToFavorites.Click += new System.EventHandler(this.m_contextMenuStrip_ClipboardEntry_AddToFavorites_Click);
            // 
            // m_contextMenuStrip_ClipboardEntry_Edit
            // 
            this.m_contextMenuStrip_ClipboardEntry_Edit.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStrip_ClipboardEntry_Edit.Image")));
            this.m_contextMenuStrip_ClipboardEntry_Edit.Name = "m_contextMenuStrip_ClipboardEntry_Edit";
            this.m_contextMenuStrip_ClipboardEntry_Edit.Size = new System.Drawing.Size(164, 26);
            this.m_contextMenuStrip_ClipboardEntry_Edit.Text = "&Edit";
            this.m_contextMenuStrip_ClipboardEntry_Edit.Click += new System.EventHandler(this.m_contextMenuStrip_ClipboardEntry_Edit_Click);
            // 
            // m_contextMenuStrip_ClipboardEntry_Remove
            // 
            this.m_contextMenuStrip_ClipboardEntry_Remove.Image = ((System.Drawing.Image)(resources.GetObject("m_contextMenuStrip_ClipboardEntry_Remove.Image")));
            this.m_contextMenuStrip_ClipboardEntry_Remove.Name = "m_contextMenuStrip_ClipboardEntry_Remove";
            this.m_contextMenuStrip_ClipboardEntry_Remove.Size = new System.Drawing.Size(164, 26);
            this.m_contextMenuStrip_ClipboardEntry_Remove.Text = "&Delete";
            this.m_contextMenuStrip_ClipboardEntry_Remove.Click += new System.EventHandler(this.m_contextMenuStrip_ClipboardEntry_Remove_Click);
            // 
            // m_ToolTip
            // 
            this.m_ToolTip.AutomaticDelay = 800;
            // 
            // m_SaveFileDialog
            // 
            this.m_SaveFileDialog.FileName = "Clipboard";
            this.m_SaveFileDialog.RestoreDirectory = true;
            // 
            // m_splitMain
            // 
            this.m_splitMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_splitMain.Location = new System.Drawing.Point(0, 24);
            this.m_splitMain.Name = "m_splitMain";
            // 
            // m_splitMain.Panel1
            // 
            this.m_splitMain.Panel1.Controls.Add(this.m_toolStripContainer);
            // 
            // m_splitMain.Panel2
            // 
            this.m_splitMain.Panel2.Controls.Add(this.m_listHistory);
            this.m_splitMain.Size = new System.Drawing.Size(984, 512);
            this.m_splitMain.SplitterDistance = 620;
            this.m_splitMain.TabIndex = 4;
            // 
            // m_listHistory
            // 
            this.m_listHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_listHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listHistory.FullRowSelect = true;
            this.m_listHistory.GridLines = true;
            this.m_listHistory.Location = new System.Drawing.Point(0, 0);
            this.m_listHistory.Name = "m_listHistory";
            this.m_listHistory.Size = new System.Drawing.Size(358, 510);
            this.m_listHistory.TabIndex = 0;
            this.m_listHistory.UseCompatibleStateImageBehavior = false;
            this.m_listHistory.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Clipboard History";
            this.columnHeader1.Width = 328;
            // 
            // FormClipboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 562);
            this.Controls.Add(this.m_splitMain);
            this.Controls.Add(this.m_statusStrip);
            this.Controls.Add(this.m_menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.m_menuStripMain;
            this.MinimumSize = new System.Drawing.Size(450, 300);
            this.Name = "FormClipboard";
            this.Text = "Clipboard Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClipboard_FormClosing);
            this.Load += new System.EventHandler(this.FormClipboard_Load);
            this.m_contextMenuStripTrayIcon.ResumeLayout(false);
            this.m_menuStripMain.ResumeLayout(false);
            this.m_menuStripMain.PerformLayout();
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            this.m_toolStripContainer.ContentPanel.ResumeLayout(false);
            this.m_toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.m_toolStripContainer.TopToolStripPanel.PerformLayout();
            this.m_toolStripContainer.ResumeLayout(false);
            this.m_toolStripContainer.PerformLayout();
            this.m_splitContainerClipboard.Panel1.ResumeLayout(false);
            this.m_splitContainerClipboard.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitContainerClipboard)).EndInit();
            this.m_splitContainerClipboard.ResumeLayout(false);
            this.m_pnlClipboard.ResumeLayout(false);
            this.m_contextMenuStrip_RichTextBox.ResumeLayout(false);
            this.m_pnlClipboardLeft.ResumeLayout(false);
            this.m_pnlClipboardLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_icoClipboardApp)).EndInit();
            this.m_pnlSnapShot.ResumeLayout(false);
            this.m_pnlSnapShotLeft.ResumeLayout(false);
            this.m_pnlSnapShotLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_icoSnapShotApp)).EndInit();
            this.m_pnlDebug.ResumeLayout(false);
            this.m_pnlDebugLeft.ResumeLayout(false);
            this.m_toolStrip.ResumeLayout(false);
            this.m_toolStrip.PerformLayout();
            this.m_contextMenuStripClipboard.ResumeLayout(false);
            this.m_contextMenuStrip_ClipboardEntry.ResumeLayout(false);
            this.m_splitMain.Panel1.ResumeLayout(false);
            this.m_splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitMain)).EndInit();
            this.m_splitMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NotifyIcon m_notifyIconCoodClip;
		private System.Windows.Forms.MenuStrip m_menuStripMain;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_File;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_File_Save;
		private System.Windows.Forms.StatusStrip m_statusStrip;
		private System.Windows.Forms.ToolStripContainer m_toolStripContainer;
		private System.Windows.Forms.ToolStrip m_toolStrip;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_History;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_OnTop;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_Copy;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_Cut;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_Paste;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_Print;
		private System.Windows.Forms.ContextMenuStrip m_contextMenuStripClipboard;
		private System.Windows.Forms.ToolStripMenuItem m_emptyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Edit;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Edit_Cut;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Edit_Copy;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Edit_Paste;

		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStrip_RichTextBox_Cut;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStrip_RichTextBox_Copy;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStrip_RichTextBox_Paste;

		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Tools;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Tools_Settings;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Help;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Help_About;
		private System.Windows.Forms.ContextMenuStrip m_contextMenuStripTrayIcon;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_Show;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_Settings;
		private System.Windows.Forms.ToolStripSeparator m_contextMenuStripTrayIcon_Sep1;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_Exit;
		private System.Windows.Forms.ToolStripSeparator m_ToolStripMenuItem_File_Sep1;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_File_Exit;
		private System.Windows.Forms.ImageList m_imageListClipboardTypes;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripClipboard_Current;
		private System.Windows.Forms.ToolStripSeparator m_contextMenuStripClipboard_Sep1;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_File_ClearHistory;
		private System.Windows.Forms.ToolStripSeparator m_ToolStripMenuItem_Edit_Sep1;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Edit_Test;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_File_ShowHistory;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_History;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_Wordwrap;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Favorites;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Favorites_Add;
		private System.Windows.Forms.ToolStripSeparator m_ToolStripMenuItem_Favorites_Sep1;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripClipboard_Favorites;
		private System.Windows.Forms.ToolStripSeparator m_contextMenuStripClipboard_Sep2;
		private System.Windows.Forms.ImageList m_ImageListSysMenu;
		private System.Windows.Forms.ContextMenuStrip m_contextMenuStrip_RichTextBox;
		private System.Windows.Forms.ContextMenuStrip m_contextMenuStrip_ClipboardEntry;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStrip_ClipboardEntry_AddToFavorites;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStrip_ClipboardEntry_Edit;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStrip_ClipboardEntry_Remove;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Favorites_Organize;
		private System.Windows.Forms.RichTextBox m_richTextBoxSnapShot;
		private System.Windows.Forms.Splitter m_HSplitterDebug;
		private System.Windows.Forms.ToolStripSplitButton m_ToolStripSplitButton_FontSize;
		private System.Windows.Forms.ToolStripMenuItem m_toolStripButton_FontSize_4;
		private System.Windows.Forms.ToolStripMenuItem m_toolStripButton_FontSize_3;
		private System.Windows.Forms.ToolStripMenuItem m_toolStripButton_FontSize_2;
		private System.Windows.Forms.ToolStripMenuItem m_toolStripButton_FontSize_1;
		private System.Windows.Forms.ToolStripMenuItem m_toolStripButton_FontSize_0;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_File_Hide;
		private System.Windows.Forms.Panel m_pnlSnapShot;
		private System.Windows.Forms.Panel m_pnlClipboard;
		private System.Windows.Forms.Panel m_pnlClipboardLeft;
		private System.Windows.Forms.Panel m_pnlSnapShotLeft;
		private System.Windows.Forms.Label m_lblClipboardType;
		private System.Windows.Forms.Label m_lblSnapShotType;
		private System.Windows.Forms.PictureBox m_icoSnapShotApp;
		private System.Windows.Forms.PictureBox m_icoClipboardApp;
		private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel m_toolStripStatusLabel3;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_View;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_View_Debug;
		private System.Windows.Forms.RichTextBox m_richTextBoxDebug;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_View_SnapShot;
		private System.Windows.Forms.Panel m_pnlDebug;
		private System.Windows.Forms.SplitContainer m_splitContainerClipboard;
		private System.Windows.Forms.ToolStripSeparator m_ToolStripMenuItem_Tools_Sep1;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Tools_ReverseChars;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Tools_Encoding;
		private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Tools_Encoding_Config;
		private System.Windows.Forms.ToolStripSeparator m_ToolStripMenuItem_Tools_Encoding_Sep1;
		private System.Windows.Forms.ToolStripComboBox m_ToolStripComboBox_CFormats;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator6;
		private System.Windows.Forms.Panel m_pnlDebugLeft;
		private System.Windows.Forms.Button m_btnDebugClear;
		private System.Windows.Forms.Button m_btnDebugCopy;
		private System.Windows.Forms.ToolTip m_ToolTip;
		private System.Windows.Forms.Button m_btnDebugScroll;
		private System.Windows.Forms.ImageList m_ImageList_Scroll;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_Reconnect;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_About;
		private System.Windows.Forms.ToolStripButton m_toolStripButton_Target;
		private System.Windows.Forms.ToolStripSeparator m_toolStripSeparator7;
		private CustomRichTextBox m_richTextBoxClipboard;
		private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_Spy;
		private System.Windows.Forms.SaveFileDialog m_SaveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_File_ClearLast;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Tools_Convert;
        private System.Windows.Forms.ToolStripMenuItem m_ToolStripMenuItem_Tools_UnescapeURI;
        private System.Windows.Forms.ToolStripSeparator m_contextMenuStripTrayIcon_Sep2;
        private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_Desktop;
        private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_DesktopSave;
        private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_DesktopRestore;
        private System.Windows.Forms.ToolStripSeparator m_contextMenuStripTrayIcon_Sep3;
        private System.Windows.Forms.ToolStripMenuItem m_contextMenuStripTrayIcon_UAC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton m_tbbtnDataFolder;
        private System.Windows.Forms.SplitContainer m_splitMain;
        private System.Windows.Forms.ListView m_listHistory;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}

