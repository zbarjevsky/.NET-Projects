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

namespace MeditationStopWatch
{
	public partial class FormStopWatch : Form
	{
		public Options m_Options = new Options();
		private string m_sSettingsFile = "MeditationStopWatch.mz";
		private IList<string> m_FavoritesList = new List<string>();
		ImageFileUtil ImageInfo = new ImageFileUtil();
		ThumbnailCache m_ThumbnailCache;

		public FormStopWatch()
		{
			InitializeComponent();

			//designer problems
			this.m_splitContainerMain.Panel2MinSize = 220;

			string sDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			m_sSettingsFile = sDirectory + "\\" + m_sSettingsFile;

			m_ThumbnailCache = new ThumbnailCache(m_imageListThumbnails);
			m_ThumbnailCache.ProgressChanged += m_ThumbnailCache_ProgressChanged;
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

		private void FormStopWatch_Load(object sender, EventArgs e)
		{
			if ( File.Exists(m_sSettingsFile) )
				OptionsSerializer.Load(m_sSettingsFile, m_Options);

			InitializeFavorites();
			ApplyOptions();

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
			
			this.Visible = true;
			OpenImageDirectory(m_Options.LastImageFile);

			m_audioPlayerControl.ValueChanged += m_audioPlayerControl_ValueChanged;

            AutoCloseThumbnailsPanel();
		}

		private void FormStopWatch_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_ThumbnailCache.ProgressChanged -= m_ThumbnailCache_ProgressChanged;
			m_audioPlayerControl.ValueChanged -= m_audioPlayerControl_ValueChanged;
			m_ThumbnailCache.CancelLoadingThumbnails = true;
			
			SaveOptions();
		}

		private void FormStopWatch_FormClosed(object sender, FormClosedEventArgs e)
		{
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
				AddToFileList(files, true);
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Error in DragDrop function: " + ex.Message);

				// don't show MessageBox here - Explorer is waiting !

			}
		}

		private void AddToFileList(string[] files, bool bPlayFirst)
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

			ImageInfo.OpenImageDirectory(sFileName);
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
			m_pictureBox1.Load(image.FullName);
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
            //m_pnlClock.BackColor = m_Options.ClockBackground;

            ApplyClockColors(m_analogClock, m_Options);

            digitalClockCtrl1.ForeColor = m_Options.DigitalClockTextColor;
            digitalClockCtrl1.BackColor = m_Options.DigitalClockBackColor;
            digitalClockCtrl1.Font = m_Options.DigitalClockFont;

            m_audioPlayerControl.InitializeOptions(m_Options);
		}

        public static void ApplyClockColors(AnalogClock clock, Options options)
        {
            clock.BackColor         = options.ClockBackground;
            clock.HourHandColor     = options.HourHandColor;
            clock.MinuteHandColor   = options.MinuteHandColor;
            clock.SecondHandColor   = options.SecondHandColor;
            clock.TicksColor        = options.TicksColor;
        }

        private void SaveOptions()
		{
			m_Options.FavoritesList = m_FavoritesList.ToArray<string>();
            //m_Options.PlayList = m_audioPlayerControl.PlayList;
            m_Options.Volume = m_audioPlayerControl.Volume;
			m_Options.Loop = m_audioPlayerControl.Loop;

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
			m_audioPlayerControl.AddToFileList(new string [] {mnu.Text}, true);
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
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Space
                || keyData == Keys.Left || keyData == Keys.Right)
            {
                //if (CanUseArrorws())
                {
                    if (keyData == Keys.Space)
                        PauseResume();

                    if (keyData == Keys.Up)
                        AdjustVolume(1);

                    if (keyData == Keys.Down)
                        AdjustVolume(-1);

                    if (keyData == Keys.Left)
                        m_btnPrevImage_Click(this, null);

                    if (keyData == Keys.Right)
                        m_btnNextImage_Click(this, null);

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool CanUseArrorws()
        {
            List<Control> list = FindFocusedControls(this);
            if (list.Count != 1)
                return true;
            if (list[0] is ListView || list[0] is Button)
                return false;
            if (list[0] is SplitContainer && (list[0] as SplitContainer).Orientation == Orientation.Horizontal)
                return false;
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

        private void m_pictureBox1_Click(object sender, EventArgs e)
		{
			PauseResume();
			m_pictureBox1.Focus();
		}

        public void PauseResume()
        {
            m_audioPlayerControl.PauseResume();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
		{
			AdjustVolume(e.Delta);
			base.OnMouseWheel(e);
		}

		public void AdjustVolume(double delta)
		{
			delta /= Math.Abs(delta);

			int vol = m_audioPlayerControl.Volume;
            delta *= (1 + vol/10.0);

			vol += (int)delta;

			if (vol < 0) vol = 0;
			if (vol > 1000) vol = 1000;

            System.Diagnostics.Trace.WriteLine("Delta: " + delta + " Vol: " + vol);
			m_audioPlayerControl.Volume = vol;
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

		private void m_btnFitWindow_Click(object sender, EventArgs e)
		{
            FormFullScreenImage frm = new FormFullScreenImage(this);
            frm.Picture = m_pictureBox1.Image;
            frm.ShowDialog(this);
        }

		private void m_btnOrigSize_Click(object sender, EventArgs e)
		{

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
