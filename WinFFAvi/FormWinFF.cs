using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DUMeterMZ;

namespace WinFFAvi
{
    public partial class FormWinFF : Form
    {
        private string m_sCurrentFolder, m_sPreferencesFile;
		private IList<TreeFileInfo> m_vSelectedMovs = new List<TreeFileInfo>();
        private ProgramPreferences m_ProgramPreferences = new ProgramPreferences();

        public FormWinFF()
        {
            InitializeComponent();
        }

        private void FormWinFF_Load(object sender, EventArgs e)
        {
            m_sCurrentFolder = Application.StartupPath;
            m_sPreferencesFile = m_sCurrentFolder + "\\Preferences.xml";
            if (File.Exists(m_sPreferencesFile))
            {
                OptionsSerializer.Load(m_sPreferencesFile, m_ProgramPreferences);
            }
            UpdateMenus();

            //load list of last directories
            RecentlyUsedList.LoadFromRegistry(m_cmbOutFolder);

            //restore default setting
            m_btnResetOut_Click(sender, e);
			m_cmbOutSize.Text = m_ProgramPreferences.OutputSize;

			m_chkOutputSize_CheckedChanged(sender, e);

            //close options
            //m_chkUseOutOptions.Checked = false;

            this.Visible = true;

            CheckFFMpegExecutable(sender, e);

            //check command line
            AddToFileList(Environment.GetCommandLineArgs());
        }

        private void FormWinFF_FormClosing(object sender, FormClosingEventArgs e)
        {
            RecentlyUsedList.SaveToRegistry(m_cmbOutFolder);
        }

        private void FormWinFF_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void FormWinFF_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true) != true)
                return;

            try
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, true);
                AddToFileList(files);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error in DragDrop function: " + ex.Message);

                // don't show MessageBox here - Explorer is waiting !

            }
        }

        private void m_toolStripButton_BrowseMov_Click(object sender, EventArgs e)
        {
			m_openFileDialogMov.CheckFileExists = true;
			if (DialogResult.OK != m_openFileDialogMov.ShowDialog(this))
                return;

            AddToFileList(m_openFileDialogMov.FileNames);
        }

        private void m_toolStripButton_Remove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in m_listSelectedFiles.SelectedItems)
            {
				TreeFileInfo f = i.Tag as TreeFileInfo;
                m_vSelectedMovs.Remove(f);
            }

            FillDisplayFileList();
        }

		private void m_btnRemoveAll_Click(object sender, EventArgs e)
		{
			m_vSelectedMovs.Clear();
			FillDisplayFileList();
		}

        private void AddToFileList(string[] files)
        {
            foreach (string s in files)
            {
                FileInfo f = new FileInfo(s);
                if (!f.Exists)
                    continue;
                if (f.Extension.ToLower() == ".exe")
                    continue;

                int idx = IndexOf(s);
                if (idx >= 0)
                    continue;

                m_vSelectedMovs.Add(new TreeFileInfo(f));
            }

            FillDisplayFileList();
        }

        private int IndexOf(string sFileName)
        {
            for (int i = 0; i < m_vSelectedMovs.Count; i++)
            {
                if (m_vSelectedMovs[i].FullName.ToLower() == sFileName.ToLower())
                    return i;
            }
            return -1;
        }

        private void FillDisplayFileList()
        {
            m_listSelectedFiles.Items.Clear();
			foreach (TreeFileInfo f in m_vSelectedMovs)
            {
                ListViewItem i = m_listSelectedFiles.Items.Add(f.FullName);
				if (f.itm != null) i.Checked = f.Checked;
				else i.Checked = true;
				f.itm = i;
                i.SubItems.Add(string.Format("{0:N} KB", f.Length / 1024.0));
                i.Tag = f;
            }
            UpdateMenus();
        }

        private void m_toolStripButton_Convert_Click(object sender, EventArgs e)
        {
            CheckFFMpegExecutable(sender, e);
            ConvertMov();
        }

        private void m_btnBrowseFolder_Click(object sender, EventArgs e)
        {
            if (m_cmbOutFolder.Text.Trim() != "")
                m_folderBrowserDialog.SelectedPath = m_cmbOutFolder.Text;

            if (DialogResult.OK != m_folderBrowserDialog.ShowDialog(this))
                return;

            UpdateOutFolder(m_folderBrowserDialog.SelectedPath);
        }

        private void UpdateOutFolder(string sFolder)
        {
            if (sFolder == ".")
                return;

            int idx = IndexOfOutFolder(sFolder);
            if (idx >= 0)
                m_cmbOutFolder.Items.RemoveAt(idx);
            else if (m_cmbOutFolder.Items.Count > m_ProgramPreferences.MaximumFolders)
                m_cmbOutFolder.Items.RemoveAt(m_ProgramPreferences.MaximumFolders);

            m_cmbOutFolder.Items.Insert(1, sFolder);
            m_cmbOutFolder.Text = sFolder;
        }

        private int IndexOfOutFolder(string s1)
        {
            for (int i = 0; i < m_cmbOutFolder.Items.Count; i++)
            {
                string s2 = m_cmbOutFolder.Items[i].ToString().ToLower();
                if (s1.ToLower() == s2.ToLower())
                    return i;
            }
            return -1;
        }

        private void m_mnuFile_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConvertMov()
        {
            string sOutFolder = m_cmbOutFolder.Text.Trim();
            UpdateOutFolder(sOutFolder);

			string sBatchCommand = string.Format("title WinFF(MarkZ) Converting: {0} files\n", CheckedCount());
            int count = 1, checked_count = CheckedCount();
			foreach (TreeFileInfo f in m_vSelectedMovs)
            {
				if (!f.Checked)
					continue;

                sBatchCommand += string.Format("title {0}/{1} files - WinFF(MarkZ) - Converting: {2}\n",
				  count++, checked_count, f.Name);
                string sCmd = CreateBatchCommand(f.FullName, sOutFolder);
                sBatchCommand += sCmd + "\n";
				
				f.Checked = false;
            }
            sBatchCommand += "pause\n";
            StartBatchFile(sBatchCommand);
        }

		private int CheckedCount()
		{
			int count = 0;
			foreach (ListViewItem item in m_listSelectedFiles.Items)
			{
				if (item.Checked) count++;
			}
			return count;
		}

        private void CheckFFMpegExecutable(object sender, EventArgs e)
        {
            if (!File.Exists(m_ProgramPreferences.WinFF_Executable))
            {
                MessageBox.Show(this,
                  "File: " + m_ProgramPreferences.WinFF_Executable + " does not exists.\nPlease, configure ffmpeg.exe path",
                  "Converter",
                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_mnuTools_Preferences_Click(sender, e);
            }
        }

        private string CreateBatchCommand(string sInOrigFile, string sOutFolder)
        {
			string sOrigFolder = "";

            string sFFMpeg = "\"" + m_ProgramPreferences.WinFF_Executable + "\"";

            //{0} - FFMpeg
            //{1} - sMovFile
            //{2} - sFrameRate ( -r 29.97 )
            //{3} - resolution (640x480)
            //{4} - aspect (4:3)
            //{5} - Log File
			const string sFmtPreprocess = "{0} -threads 4 -i \"{1}\" -f avi {2} -vcodec libxvid -vtag XVID {3} {4} -maxrate 1800kb -b 1500kb -qmin 3 -qmax 5 -bufsize 4096 -mbd 2 -bf 2 -flags +4mv -trellis -aic -cmp 2 -subcmp 2 -g 300 -acodec libmp3lame -ar 48000 -ab 128kb -ac 2 -an -passlogfile \"{5}\" -pass 1 -y \"NUL.avi\"";

            //{0} - FFMpeg
            //{1} - sMovFile
            //{2} - sFrameRate ( -r 29.97 )
            //{3} - resolution (640x480)
            //{4} - aspect (4:3)
            //{5} - Log File
            //{6} - sOutAviFile
			const string sFmtConvert = "{0} -threads 4 -y -i \"{1}\" -f avi {2} -vcodec libxvid -vtag XVID {3} {4} -maxrate 1800kb -b 1500kb -qmin 3 -qmax 5 -bufsize 4096 -mbd 2 -bf 2 -flags +4mv -trellis -aic -cmp 2 -subcmp 2 -g 300 -acodec libmp3lame -ar 48000 -ab 128kb -ac 2 -passlogfile \"{5}\" -pass 2  \"{6}\"";

            FileInfo fOrigFile = new FileInfo(sInOrigFile);
			if (sOutFolder == ".")
			{
				sOutFolder = fOrigFile.DirectoryName;
				if (m_chkOrigFolder.Checked)
				{
					sOrigFolder = sOutFolder + "\\Orig";
					if (!Directory.Exists(sOrigFolder))
						Directory.CreateDirectory(sOrigFolder);
				}
			}

            DirectoryInfo fOutFolder = new DirectoryInfo(sOutFolder);
            if (!fOutFolder.Exists)
                fOutFolder.Create();

            m_sCurrentFolder = fOutFolder.FullName;

			string sInExt = Path.GetExtension(sInOrigFile).ToLower();
			string sInName = Path.GetFileNameWithoutExtension(sInOrigFile);
			string sOutName = sInName;

			string sOutAviFile = m_sCurrentFolder + "\\" + sOutName + ".avi";
			if (File.Exists(sOutAviFile))
				sOutName += string.Format("_{0:0}", new Random().Next(1, 9));

			sOutAviFile = m_sCurrentFolder + "\\" + sOutName + ".avi";
			string sLogFile = m_sCurrentFolder + "\\" + sOutName + ".log";
			string sThmFile = m_sCurrentFolder + "\\" + sInName + ".thm";

			string sResolution = "-s " + m_cmbOutSize.Text;
			string sAspect = "-aspect " + m_cmbAspect.Text;
			string sFrameRate = "-r " + m_cmbFrameRate.Text;

			if (!m_chkOutSize.Checked)
				sResolution = "";

			if (!m_chkAspect.Checked)
				sAspect = "";

			if (!m_chkFrameRate.Checked)
				sFrameRate = "";

            FileInfo fAviFile = new FileInfo(sOutAviFile);
            if (fAviFile.Exists)
                return "echo File Exists: " + sOutAviFile;

            string sCmdLine = string.Format(sFmtPreprocess, 
				sFFMpeg,
				sInOrigFile, sFrameRate, sResolution, sAspect, 
				sLogFile);
            sCmdLine += "\n";
            sCmdLine += string.Format(sFmtConvert,
				sFFMpeg,
				sInOrigFile, sFrameRate, sResolution, sAspect,
				sLogFile,
				sOutAviFile);

			if (m_chkUseOutOptions.Checked)
			{
				if (m_chkDeleteLog.Checked)
					sCmdLine += string.Format("\ndel \"{0}*.log\" ", sLogFile);
				else if (m_chkOrigFolder.Checked)
					sCmdLine += string.Format("\nmove \"{0}*.log\" \"{1}\"", sLogFile, sOrigFolder);

				//move original file to Orig dir
				if (m_chkOrigFolder.Checked)
					sCmdLine += string.Format("\nmove \"{0}\" \"{1}\"", sInOrigFile, sOrigFolder);
				else //make .EXT -> .ext
					sCmdLine += CmdMakeLowerExtension(sInOrigFile);

				sCmdLine += CmdMakeLowerExtension(sThmFile);
				if (sInName != sOutName) //different names - create copy of THM
					sCmdLine += string.Format("\ncopy \"{0}\" \"{1}\\{2}.thm\"", sThmFile, m_sCurrentFolder, sOutName);
				
				//copy THM file to Orig folder
				if (m_chkOrigFolder.Checked)
				{
					if (sInName != sOutName) //different names
						sCmdLine += string.Format("\nmove \"{0}\" \"{1}\"", sThmFile, sOrigFolder);
					else
						sCmdLine += string.Format("\ncopy \"{0}\" \"{1}\"", sThmFile, sOrigFolder);
				}
			}//end if

            return sCmdLine;
        }

		private string CmdMakeLowerExtension(string sFileName)
		{
			string sExt = Path.GetExtension(sFileName).ToLower();
			string sName = Path.GetFileNameWithoutExtension(sFileName);

			return string.Format("\nren \"{0}\" \"{1}{2}\"", sFileName, sName, sExt); 
		}

        private void StartBatchFile(string sBatchCommand)
        {
            DirectoryInfo fOutFolder = new DirectoryInfo(m_sCurrentFolder);

            string sBatFile = fOutFolder.FullName + string.Format("\\Go-{0}.bat", DateTime.Now.ToString("yyyyMMdd_HH-mm-ss"));
            File.WriteAllText(sBatFile, sBatchCommand);
            ProcessStartInfo procStartInfo = new ProcessStartInfo(sBatFile);

            procStartInfo.ErrorDialog = true;
            procStartInfo.ErrorDialogParentHandle = this.Handle;

            //procStartInfo.RedirectStandardOutput = true;
            //procStartInfo.RedirectStandardError = true;
            //procStartInfo.UseShellExecute = false;
            //procStartInfo.CreateNoWindow = true;

            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            bool bRes = proc.Start();

            //proc.WaitForExit();
            //while (!proc.StandardError.EndOfStream)
            //{
            //  string sResult = proc.StandardError.ReadLine();
            //  m_txtConsole.Text += sResult + "\n";
            //  Application.DoEvents();
            //}
        }//end StartBatchFile

        private void m_mnuTools_Preferences_Click(object sender, EventArgs e)
        {
            FormPreferences frm = new FormPreferences(m_ProgramPreferences);
            frm.ShowDialog();
            OptionsSerializer.Save(m_sPreferencesFile, m_ProgramPreferences);
        }

        private void m_ctxmnuOpenFile_Click(object sender, EventArgs e)
        {
            if (m_listSelectedFiles.SelectedItems == null || m_listSelectedFiles.SelectedItems.Count == 0)
                return;

            string sFileName = m_listSelectedFiles.SelectedItems[0].Text;
            Process.Start(sFileName);
        }

        private void m_ctxmnuOpenFolder_Click(object sender, EventArgs e)
        {
            if (m_listSelectedFiles.SelectedItems == null || m_listSelectedFiles.SelectedItems.Count == 0)
                return;

            string sFileName = m_listSelectedFiles.SelectedItems[0].Text;
            string sFolder = Path.GetDirectoryName(sFileName);
            Process.Start(sFolder);
        }

        private void m_listSelectedFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMenus();
        }

        private void UpdateMenus()
        {
            bool bSelected = (m_listSelectedFiles.SelectedItems != null && m_listSelectedFiles.SelectedItems.Count > 0);

            m_ctxmnuOpenFile.Enabled = bSelected;
            m_ctxmnuOpenFolder.Enabled = m_ctxmnuOpenFile.Enabled;

            m_ctxmnuRemove.Enabled = bSelected;
            m_toolStripButton_Remove.Enabled = m_ctxmnuRemove.Enabled;
            m_mnuFile_Remove.Enabled = m_ctxmnuRemove.Enabled;

            m_mnuFile_Convert.Enabled = m_listSelectedFiles.Items.Count > 0;
            m_toolStripButton_Convert.Enabled = m_mnuFile_Convert.Enabled;

            m_toolStripStatusLabel1.Text = bSelected ? m_listSelectedFiles.SelectedItems[0].Text : "Ready";
        }

        private void m_chkUseOutFormat_CheckedChanged(object sender, EventArgs e)
        {
            //m_pnlOptions.Visible = m_chkUseOutOptions.Checked;

            m_chkOutSize.Enabled = m_chkUseOutOptions.Checked;
			m_cmbOutSize.Enabled = m_chkUseOutOptions.Checked && m_chkOutSize.Checked;

			m_chkDeleteLog.Enabled = m_chkUseOutOptions.Checked;
			m_chkOrigFolder.Enabled = m_chkUseOutOptions.Checked;
		}

        private void m_btnResetOut_Click(object sender, EventArgs e)
        {
			m_cmbOutSize.Items.Clear();
			m_cmbOutSize.Items.Add("320x240");
			m_cmbOutSize.Items.Add("640x480");
			m_cmbOutSize.Items.Add("1024x768");
			m_cmbOutSize.Items.Add("1280x720");
			m_cmbOutSize.Items.Add("1920x1080");
			m_cmbOutSize.Items.Add("1920x1200");
			m_cmbOutSize.Text = ProgramPreferences.DEFAULT_SIZE;

			m_cmbAspect.Items.Clear();
			m_cmbAspect.Items.Add(AspectRatio.ASPECT_4x3);
			m_cmbAspect.Items.Add(AspectRatio.ASPECT_16x9);
			m_cmbAspect.Items.Add(AspectRatio.ASPECT_16x10);
			m_cmbAspect.Text = AspectRatio.ASPECT_16x9; //default

			m_cmbFrameRate.Items.Clear();
			m_cmbFrameRate.Items.Add("29.97");
			m_cmbFrameRate.SelectedIndex = 0;

			m_chkDeleteLog.Checked = true;
			m_chkOrigFolder.Checked = true;
			m_chkOutSize.Checked = false;
        }

        private void m_mnuHelp_About_Click(object sender, EventArgs e)
        {
            FormAbout frm = new FormAbout();
            frm.ShowDialog(this);
        }

		private void m_chkOutputSize_CheckedChanged(object sender, EventArgs e)
		{
			m_cmbOutSize.Enabled = m_chkOutSize.Checked;
		}

		private void m_cmbOutSize_TextUpdate(object sender, EventArgs e)
		{
			m_cmbAspect.Text = AspectRatio.ConvertResolutionToAspect(m_cmbOutSize.Text);
		}

		private void m_cmbOutSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_cmbOutSize_TextUpdate(sender, e);
		}

		private class TreeFileInfo
		{
			public FileInfo fi;
			public ListViewItem itm;

			public TreeFileInfo(FileInfo i)
			{
				fi = i;
			}

			public long Length { get { return fi.Length; } }
			public string Name { get { return fi.Name; } }
			public string FullName { get { return fi.FullName; } }
			public bool Checked { get { return itm.Checked; } set { itm.Checked = value; } }
		}

		private void m_btnFindFiles_Click(object sender, EventArgs e)
		{
			m_openFileDialogMov.CheckFileExists = false;
			if (DialogResult.OK != m_openFileDialogMov.ShowDialog(this))
				return;

			string sFileName = m_openFileDialogMov.FileName;
		}
    }//end class FormWinFF
}//end namespace WinFFAvi
