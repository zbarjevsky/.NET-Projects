using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using MeditationStopWatch.Tools;
using Microsoft.WindowsAPICodePack.Taskbar;
using MZ.Tools.WinForms;
using MZ.ControlsWinForms;
using MZ.Tools;

namespace MeditationStopWatch
{
	public partial class FormStopWatch : Form
	{
        internal const string TITLE = "Meditation...";

        public Options m_Options = new Options();
		private string m_sSettingsFile = "MeditationStopWatch.mz";
		private IList<string> m_FavoritesList = new List<string>();
		ImageFileUtil ImageInfo = new ImageFileUtil();
		ThumbnailCache m_ThumbnailCache;

		public FormStopWatch()
		{
            GlobalMessageFilter gmh = new GlobalMessageFilter();
            gmh.MouseMovedAction = (point) => OnMouseMove(point);
            Application.AddMessageFilter(gmh);

            InitializeComponent();

            this.Text = TITLE;

			//designer problems
			this.m_splitContainerMain.Panel2MinSize = 220;

			string sDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			m_sSettingsFile = sDirectory + "\\" + m_sSettingsFile;

			m_ThumbnailCache = new ThumbnailCache(m_imageListThumbnails);
			m_ThumbnailCache.ProgressChanged += m_ThumbnailCache_ProgressChanged;

            m_Options.AnalogClockSettings = m_analogClock.Settings;
		}

		private bool _isInitialized = false;
		private void FormStopWatch_Load(object sender, EventArgs e)
		{
			if (File.Exists(m_sSettingsFile))
			{
				OptionsSerializer.Load(m_sSettingsFile, m_Options);

				//check files exists - may be network disk is unmounted
			CheckFiles:
				DialogResult res = CheckFilesExist();
				if (res == DialogResult.Abort)
				{
					this.Close();
					return;
				}
				else if (res == DialogResult.Retry)
				{
					goto CheckFiles;
				}
			}

            m_analogClock.Settings = m_Options.AnalogClockSettings;

            m_lblVolume.Parent = m_pictureBox1.PictureBox;
            m_lblVolume.Draggable(true);
            if (!DesignMode)
                m_lblVolume.Visible = false;
            m_pictureBox1.OnSizeChangedAction = (bounds) =>
            {
				m_pictureBox1.EnsureVisible(m_lblVolume, AnchorStyles.Top | AnchorStyles.Right, 50);
            };
            m_pictureBox1.OnClickAction = () =>
            {
                PauseResume();
                m_pictureBox1.Focus();
            };
            m_pictureBox1.ShowControlsAction = (show) => { m_btnHideSumbnails.Visible = show; };

            InitializeFavorites();

            //restore position
            if (m_Options.AppRectangle != null)
            {
                if(System.Windows.SystemParameters.VirtualScreenLeft < m_Options.AppRectangle.Location.X)
                    this.Location = m_Options.AppRectangle.Location;
                this.Size = m_Options.AppRectangle.Size;
            }

			if (m_Options.WindowState != FormWindowState.Minimized)
				WindowState = m_Options.WindowState;

			if (m_Options.PictureWidth > 25)
				m_splitContainerMain.SplitterDistance = m_Options.PictureWidth;

			if (m_Options.ClockHeight > 25)
				m_splitContainerTools.SplitterDistance = m_Options.ClockHeight;
			
			ApplyOptions();

			m_cmbAudioOutDevices.Items.AddRange(SoundUtils.GetOutDevices().ToArray<object>());
			m_cmbAudioOutDevices.SelectedIndex = 0;

			this.Visible = true;
			OpenImageDirectory(m_Options.LastImageFile);

			m_audioPlayerControl.ValueChanged += m_audioPlayerControl_ValueChanged;

            AutoCloseThumbnailsPanel();
			InitThumbnailToolBarButtons();

			_isInitialized = true;
		}

		private void FormStopWatch_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!_isInitialized)
				return;

			m_ThumbnailCache.ProgressChanged -= m_ThumbnailCache_ProgressChanged;
			m_audioPlayerControl.ValueChanged -= m_audioPlayerControl_ValueChanged;
			m_ThumbnailCache.CancelLoadingThumbnails = true;
			
			SaveOptions();
		}

		private void FormStopWatch_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		private DialogResult CheckFilesExist()
		{
			foreach (PlayList list in m_Options.PlayListCollection.Collection)
			{
				foreach (string file in list.List)
				{
					if (!File.Exists(file))
					{
						DialogResult res = MessageBox.Show(this, "Cannot find file:\n" + file, "Load",
							MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Asterisk);
						return res;
					}
				}
			}
			return DialogResult.Ignore;
		}

		private void InitThumbnailToolBarButtons()
		{
			TaskbarManagerHelper.Init(this.Handle);
			TaskbarManagerHelper.ShowButtons(
				new List<string>() {"Previous", "Play/Pause",  "Next"}, 
				new List<Icon>() { Properties.Resources.previus_on, Properties.Resources.pause_on, Properties.Resources.next_on});
			
			TaskbarManagerHelper.ButtonClicked = (index) => 
			{
				if (index == 0)
					m_audioPlayerControl.Prev();
				if (index == 1)
					m_audioPlayerControl.PauseResume();
				if (index == 2)
					m_audioPlayerControl.Next();
			};
		}

		private void m_ThumbnailCache_ProgressChanged(object sender, CacheEventArgs e)
		{
			UpdateStatus(e.Percent, e.Status);
		}

		private void UpdateStatus(int percent, string status)
		{
			if (this.Disposing || this.IsDisposed) return;

			if (this.InvokeRequired)
			{
				BeginInvoke(new MethodInvoker(delegate() { UpdateStatus(percent, status); }));
			}
			else
			{
				if (m_ThumbnailCache.CancelLoadingThumbnails)
				{
					m_toolStripStatusLabel1.Text = "Ready";
					m_toolStripStatusLabel2.Text = "";
					//m_toolStripProgressBar1.Value = 0;
					return;
				}

				m_toolStripStatusLabel1.Text = status;
				if (percent > 0)
					m_toolStripStatusLabel2.Text =
						percent + " % (" + percent * ImageInfo.AllImages.Count / 100 + " of " + ImageInfo.AllImages.Count + ")";
				else
					m_toolStripStatusLabel2.Text = "";

				m_toolStripProgressBar1.Value = percent;

                m_listThumbnails.Invalidate();
				
				Application.DoEvents();
			}
		}

        private void AutoCloseThumbnailsPanel(int delayMs = 2000)
        {
            //auto collapse thumbnails panel after 'delayMs' msec
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1200;
            timer.Tick += (s, a) => {
                timer.Stop();
                if (!m_splitContainerImage.Panel2Collapsed)
                    m_btnHideSumbnails_Click(s, a);
            };
            timer.Start();
        }

        private int _iImageTimeoutCount = 0;
		private void m_audioPlayerControl_ValueChanged(object sender, EventArgs e)
		{
			//m_lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
			if ( _iImageTimeoutCount++ % m_Options.SlideShowTimeOut == 0) //every 6 seconds
			{
				if (m_btnSlideShow.Checked)
					m_btnNextImage_Click(sender, e);
			}
		}

		private void FormStopWatch_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void FormStopWatch_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop, true) != true)
				return;

			try
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, true);
				AddToFileList(new List<string>(files), true);
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Error in DragDrop function: " + ex.Message);

				// don't show MessageBox here - Explorer is waiting !

			}
		}

		private void AddToFileList(List<string> files, bool bPlayFirst)
		{
			m_audioPlayerControl.AddToFileList(files, bPlayFirst);
		}

		private void m_btnOpenImage_Click(object sender, EventArgs e)
		{
			if (m_openFileDialog.ShowDialog(this) != DialogResult.OK)
				return;

			OpenImageDirectory(m_openFileDialog.FileName);
		}

		private void OpenImageDirectory(string sFileName)
		{
			if (string.IsNullOrEmpty(sFileName) || !File.Exists(sFileName))
				return;

			string[] extentions = m_openFileDialog.Filter.Split('|');
			extentions = extentions[1].Replace("*", "").Split(';');

			ImageInfo.OpenImageDirectory(sFileName, extentions);
			m_listThumbnails.VirtualListSize = ImageInfo.AllImages.Count;
			OpenImage(ImageInfo.ImageInfo);

			m_ThumbnailCache.CancelLoadingThumbnails = true;
			m_ThumbnailCache.InitCache(ImageInfo.AllImages);

			EnsureVisible();
			UpdateStatusText();
		}


		private void OpenImage(FileInfo image)
		{
			_iImageTimeoutCount = 1;
			m_Options.LastImageFile = image.FullName;
			m_pictureBox1.LoadImage(image.FullName);
            m_pictureBox1.Focus();
        }

		private void m_mnuFile_Open_Click(object sender, EventArgs e)
		{
			m_btnOpenImage_Click(sender, e);
		}

		private void m_mnuFile_Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void m_mnuTools_Options_Click(object sender, EventArgs e)
		{
			m_Options.Volume = m_audioPlayerControl.Volume;
			m_Options.Loop = m_audioPlayerControl.Loop;
			
			FormOptions frm = new FormOptions(m_Options, (propertyName) => { ApplyOptions(); });
			frm.ShowDialog(this);
			
			ApplyOptions();
			SaveOptions();
		}

		private void m_mnuHelp_About_Click(object sender, EventArgs e)
		{
			FormAbout frm = new FormAbout();
			frm.ShowDialog(this);
		}

		private void ApplyOptions()
		{
            digitalClockCtrl1.ForeColor = m_Options.DigitalClockTextColor;
            digitalClockCtrl1.BackColor = m_Options.DigitalClockBackColor;
            digitalClockCtrl1.Font = m_Options.DigitalClockFont;

            m_audioPlayerControl.InitializeOptions(m_Options);
            m_lblVolume.Bounds = m_Options.SoudVolumeLabelBounds;
		}

        private void SaveOptions()
		{
			m_Options.FavoritesList = m_FavoritesList.ToArray<string>();
            //m_Options.PlayList = m_audioPlayerControl.PlayList;
            m_Options.Volume = m_audioPlayerControl.Volume;
			m_Options.Loop = m_audioPlayerControl.Loop;
            m_Options.SoudVolumeLabelBounds = m_lblVolume.Bounds;

            if (WindowState != FormWindowState.Minimized)
				m_Options.WindowState = WindowState;
			else
				m_Options.WindowState = FormWindowState.Normal;

            m_Options.AppRectangle = new Rectangle(this.Location, this.Size);

			m_Options.PictureWidth = m_splitContainerMain.SplitterDistance;
			m_Options.ClockHeight = m_splitContainerTools.SplitterDistance;

            OptionsSerializer.Save(m_sSettingsFile, m_Options);
		}

		private void InitializeFavorites()
		{
			if (m_Options.FavoritesList == null)
				return;

			m_FavoritesList = new List<string>(m_Options.FavoritesList);
			foreach (string file in m_FavoritesList)
			{
				AppendFavoritesMenu(file);
			}
		}

		private void AppendFavoritesMenu(string file)
		{
			ToolStripMenuItem mnu = new ToolStripMenuItem(file);
			mnu.Click += FavoriteItem_Click;
			m_mnuFavorites.DropDownItems.Add(mnu);
		}

		private void FavoriteItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem mnu = sender as ToolStripMenuItem;
			m_audioPlayerControl.AddToFileList(new List<string>() {mnu.Text}, true);
		}

		private void m_mnuFavorites_Add_Click(object sender, EventArgs e)
		{
            if (m_audioPlayerControl.PlayingFile == null)
            {
                MessageBox.Show(this, "No file is playing..", "Favorities", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

			string add_name = m_audioPlayerControl.PlayingFile.FullName;

			foreach (string file in m_FavoritesList)
			{
				if (file == add_name)
					return; //already exists
			}

			m_FavoritesList.Add(add_name);
			AppendFavoritesMenu(add_name);
		}

		private void m_mnuFavorites_Organize_Click(object sender, EventArgs e)
		{
            MessageBox.Show(this, "Not Implemented...", "Favorities", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (CanUseArrorws())
            {
                switch (keyData)
                {
                    case Keys.Space:
                        PauseResume();
                        return true;
                    case Keys.Up:
                        AdjustVolume(1);
                        return true;
                    case Keys.Down:
                        AdjustVolume(-1);
                        return true;
                    case Keys.Left:
                        ShowPrevImage();
                        return true;
                    case Keys.Right:
                        ShowNextImage();
                        return true;
                    default:
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool CanUseArrorws()
        {
            List<Control> ctrls_in_focus = FindFocusedControls(this);
            if (ctrls_in_focus.Count != 1)
                return true;
            if (ctrls_in_focus[0] is ListView) // || ctrls_in_focus[0] is Button)
                return false;
            //if (list[0] is SplitContainer && (list[0] as SplitContainer).Orientation == Orientation.Horizontal)
            //    return false;
            return true;
        }

        private List<Control> FindFocusedControls(Control c)
        {
            List<Control> list = new List<Control>();
            foreach (Control ctrl in c.Controls)
            {
                if (ctrl.Focused)
                    list.Add(ctrl);
                list.AddRange(FindFocusedControls(ctrl));
            }
            return list;
        }

        public void PauseResume()
        {
            m_audioPlayerControl.PauseResume();
        }

        private Point _prevMouseMovePoint = new Point();

        private void OnMouseMove(Point pt)
        {
            int x = Math.Abs(pt.X - _prevMouseMovePoint.X);
            int y = Math.Abs(pt.Y - _prevMouseMovePoint.Y);
            _prevMouseMovePoint = pt;

            if(x>1 || y>1)
                CursorHandler.IsCursorVisible = true;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
		{
            if (!ModifierKeys.HasFlag(Keys.Control))
                AdjustVolume(e.Delta);
            base.OnMouseWheel(e);
		}

        public string AdjustVolume(double delta)
		{
			delta /= Math.Abs(delta);

			int vol = m_audioPlayerControl.Volume;
            if ((vol + delta) >= 10 && (vol + delta) <= 100)
            {
                vol -= (vol % 10); //round to nearest 10
                delta *= 10;
            }
            else if ((vol + delta) > 100)
            {
                vol -= (vol % 100); //round to nearest 100
                delta *= 100;
            }

            vol += (int)delta;

			if (vol < 0) vol = 0;
			if (vol > 1000) vol = 1000;

            System.Diagnostics.Trace.WriteLine("Delta: " + delta + " Vol: " + vol);
			m_audioPlayerControl.Volume = vol;

            string fmt = (vol < 10) ? "0.0" : "0";

			m_pictureBox1.EnsureVisible(m_lblVolume, AnchorStyles.Top | AnchorStyles.Right, 50);
            m_lblVolume.Show(string.Format("Volume: {0} %", (vol /10.0).ToString(fmt)), 4000);

            return m_lblVolume.Text;
		}

        private void m_btnPrevImage_Click(object sender, EventArgs e)
		{
            ShowPrevImage();
		}

        public FileInfo ShowPrevImage()
        {
            FileInfo ifo = ImageInfo.Prev();
            OpenImage(ifo);
            EnsureVisible();
            UpdateStatusText();
            return ifo;
        }

        private void m_btnNextImage_Click(object sender, EventArgs e)
		{
            ShowNextImage();
		}

        public FileInfo ShowNextImage()
        {
            FileInfo ifo = ImageInfo.Next();
            OpenImage(ifo);
            EnsureVisible();
            UpdateStatusText();
            return ifo;
        }

        private void UpdateStatusText()
		{
			int idx = ImageInfo.IndexOf(ImageInfo.ImageInfo);
			m_lblImageDesc.Text = string.Format("{0} of {1}", idx + 1, ImageInfo.AllImages.Count);
			m_txtImageIndex.Text = string.Format("{0}", idx + 1);
			//m_lblFileName.Text = ImageInfo.ImageInfo.FullName;
			//m_lblFileName.Visible = m_Options.ShowFileName;
			m_tsTxt_FileName.Text = ImageInfo.ImageInfo.FullName;
			m_toolStrip_Picture.Visible = m_Options.ShowFileName; ;
		}

		private void EnsureVisible()
		{
			int idx = ImageInfo.IndexOf(ImageInfo.ImageInfo);
			if (idx < 0)
				return;
			m_listThumbnails.Items[idx].Selected = true;
			m_listThumbnails.EnsureVisible(idx);
		}

		private void m_btnSlideShow_Click(object sender, EventArgs e)
		{
			m_btnSlideShow.Checked = !m_btnSlideShow.Checked;
		}

		private void m_listThumbnails_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
				int imgIdx = m_ThumbnailCache.GetImageIdx(e.ItemIndex);
				e.Item = new ListViewItem((e.ItemIndex + 1) + ". " + ImageInfo.AllImages[e.ItemIndex].Name, imgIdx);
		}

		//int m_iFirstVisibleThumb = -1;
		//bool m_bRetrievingItem = false;
		//private void GetFirstVisibleThumbnailIdx(int idx)
		//{
		//    if (m_bRetrievingItem == false)
		//    {
		//        m_bRetrievingItem = true;
		//        m_listThumbnails.GetItemAt(10, 10);
		//        m_bRetrievingItem = false;
		//    }
		//    else
		//    {
		//        System.Diagnostics.Trace.WriteLine("First: " + idx);
		//        m_iFirstVisibleThumb = idx;
		//    }
		//}

		private void m_listThumbnails_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (m_listThumbnails.SelectedIndices == null || m_listThumbnails.SelectedIndices.Count == 0)
				return;

			int idx = m_listThumbnails.SelectedIndices[0];
			ImageInfo.ImageInfo = ImageInfo.AllImages[idx];
			OpenImage(ImageInfo.ImageInfo);
			UpdateStatusText();
		}

        private void m_mnuViewFullScreen_Click(object sender, EventArgs e)
        {
            FormFullScreenImage frm = new FormFullScreenImage(this);
            frm.Picture = m_pictureBox1.PictureBox.Image;
            frm.ShowDialog(this);
        }

        private void m_btnFullScreen_Click(object sender, EventArgs e)
		{
            m_mnuViewFullScreen_Click(sender, e);
        }

		private void m_txtImageIndex_TextChanged(object sender, EventArgs e)
		{
			string idx = m_txtImageIndex.Text;
			if ( string.IsNullOrEmpty(idx))
				return;

			if (idx[idx.Length - 1] == '\n')
			{
			}
		}

        private void m_btnHideSumbnails_Click(object sender, EventArgs e)
        {
            m_splitContainerImage.Panel2Collapsed = !m_splitContainerImage.Panel2Collapsed;
            m_btnHideSumbnails.Text = m_splitContainerImage.Panel2Collapsed ? "Show Thumbnails" : "Hide Thumbnails";
            m_btnHideSumbnails.ImageIndex = m_splitContainerImage.Panel2Collapsed ? 1 : 0;
        }
    }
}
