using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MeditationStopWatch
{
	public partial class AudioPlayerControl : UserControl
	{
		//private List<FileInfo> m_vSongsList = new List<FileInfo>();
		private MCIPLayer m_Mp3Player = new MCIPLayer();
		private TimeSpan _tsLength;
		private int _playingIdx = -1;
		private Options m_Options;
		//private bool _bScreenSaverActive = false;
	
		public AudioPlayerControl()
		{
			InitializeComponent();

			m_mnuPlay.Image = m_tbbtnPlay.Image;
			m_mnuPause.Image = m_tbbtnPause.Image;
			m_mnuStop.Image = m_tbbtnStop.Image;
			m_mnuPrev.Image = m_tbbtnPrev.Image;
			m_mnuNext.Image = m_tbbtnNext.Image;

			m_mnuUp.Image = m_toolStripButton_Up.Image;
			m_mnuDown.Image = m_toolStripButton_Down.Image;
			
			m_mnuAdd.Image = m_toolStripButton_AddFiles.Image;
			m_mnuRemove.Image = m_toolStripButton_Remove.Image;
			m_mnuRemoveAll.Image = m_toolStripButton_RemoveAll.Image;
		}
		public event EventHandler ValueChanged;
		public void Pause() 
        { 
            m_Mp3Player.Pause(); 
            if ( m_Mp3Player.Paused ) 
                m_progrReiKi.Pause(); 
            else 
                m_progrReiKi.Resume();
            UpdateInfo();
        }
		public int Count { get { return m_listFiles.Items.Count; } }
		public bool HasSelected { get { return m_listFiles.SelectedIndices.Count > 0; } }
		public FileInfo PlayingFile 
		{ 
			get 
			{
				if (_playingIdx < 0) return null;
				return (m_listFiles.Items[_playingIdx].Tag as FileInfo); 
			} 
		}

		public string[] PlayList
		{
			get
			{
				string[] playList = new string[Count];
				for (int i = 0; i < Count; i++)
				{
					playList[i] = (m_listFiles.Items[i].Tag as FileInfo).FullName;
				}
				return playList;
			}
		}

		public void AddToFileList(string[] files, bool bPlayFirst)
		{
			if (files == null || files.Length == 0)
				return;

			int iFirstIdx = -1;
			foreach (string s in files)
			{
				FileInfo f = new FileInfo(s);
				if (!f.Exists)
					continue;

				if (f.Extension.ToLower() != ".mp3")
					continue;

				int idx = IndexOf(s);
				if (idx >= 0)
				{
					if (iFirstIdx < 0) iFirstIdx = idx;
					continue;
				}

				ListViewItem i = m_listFiles.Items.Add(f.Name);
				i.ToolTipText = f.FullName;
				i.SubItems.Add(" --:-- ");
				i.SubItems.Add(string.Format("{0:N} MB", f.Length / (1024.0*1024.0)));
				i.Tag = f;
				if (iFirstIdx < 0) iFirstIdx = i.Index;
			}
			if (bPlayFirst) Play(iFirstIdx);
			UpdateInfo();
		}

		public void RemoveSelectedFiles()
		{
			for (int i = m_listFiles.SelectedIndices.Count - 1; i >= 0; i--)
			{
				int idx = m_listFiles.SelectedIndices[i];
				if (idx == _playingIdx)
				{
					m_Mp3Player.Close();
					_playingIdx = -1;
				}
				m_listFiles.SelectedItems[i].Remove();
			}
			_playingIdx = -1;
			UpdateInfo();
		}

		public void RemoveAll()
		{
			m_Mp3Player.Close();
			_playingIdx = -1;

			m_listFiles.Items.Clear();
			UpdateInfo();
		}

		public bool Loop
		{
			get { if(DesignMode) return true; return m_Options.Loop; }
			set 
			{ 
				if (m_Options == null) return; 
				m_Options.Loop = value; 
				m_chkLoop.Image = value ? m_imageList1.Images[10] : m_imageList1.Images[11]; 
			}
		}

		public int Volume
		{
			get { return m_toolStripTrackBarVolume.Value; }
			set 
			{
				if (m_Options == null)
					return; 
				m_Options.Volume = m_toolStripTrackBarVolume.Value = value; 
			}
		}

		public Options Options
		{
			set { m_Options = value; Volume = m_Options.Volume; m_progrReiKi.Options = value; }
		}

		private int IndexOf(string sFileName)
		{
			for (int i = 0; i < m_listFiles.Items.Count; i++)
			{
				FileInfo f = m_listFiles.Items[i].Tag as FileInfo;
				if (f.FullName.ToLower() == sFileName.ToLower())
					return i;
			}
			return -1;
		}

		//private void FillDisplayFileList()
		//{
		//    m_listFiles.Items.Clear();
		//    foreach (FileInfo f in m_vSongsList)
		//    {
		//        ListViewItem i = m_listFiles.Items.Add(f.FullName);
		//        i.SubItems.Add(" --:-- ");
		//        i.SubItems.Add(string.Format("{0:N} KB", f.Length / 1024.0));
		//        i.Tag = f;
		//    }
		//    UpdateInfo();
		//}

		private void AudioPlayerControl_Load(object sender, EventArgs e)
		{
			UpdateInfo();
		}

		private void m_listFiles_DoubleClick(object sender, System.EventArgs e)
		{
			m_tbbtnPlay_Click(sender, e);
		}

		private void m_trackBarPosition_Scroll(object sender, EventArgs e)
		{
			m_Mp3Player.SetPosition(m_trackBarPosition.Value * 1000);
			UpdateInfo();
		}

		private void m_timer1_Tick(object sender, EventArgs e)
		{
			if (m_Options == null)
				return;

			if (Loop && m_Mp3Player.IsStopped() && m_trackBarPosition.Value == m_trackBarPosition.Maximum)
				m_tbbtnNext_Click(sender, e);

			if (ValueChanged != null) 
				ValueChanged(sender, e);

			UpdateInfo();
		}

		private void UpdateInfo()
		{
			string sStatus = m_Mp3Player.StatusMode;
			m_lblStatus.Text = sStatus;
			UpdateButtonsState(sStatus);
			DisableScreenSaver();
			UpdatePlayingFile();

			int sec = m_Mp3Player.GetCurentMilisecond() / 1000;
			m_trackBarPosition.Value = sec;
			//m_prgr3Minute.Value = (int)(sec % 180);

			TimeSpan tsPos = TimeSpan.FromSeconds(sec);
			m_lblTime.Text = string.Format("{0} / {1}",
				TimeString(tsPos), TimeString(_tsLength));

			m_toolStripTrackBarVolume.ToolTipText = "";
				//string.Format("V({0})", Volume);

			this.m_toolTip1.SetToolTip(this.m_toolStripTrackBarVolume.TrackBar,
				string.Format("V({0})", Volume));

			this.m_toolTip1.SetToolTip(this.m_trackBarPosition,
				string.Format("P({0})", TimeString(tsPos)));
		}

        private int _screenSaverTimerCount = 0;
		private void DisableScreenSaver()
		{
            if (m_Mp3Player.IsPlaying())
            {
                if (_screenSaverTimerCount++ % 100 == 0)
                   ScreenSaver.ResetIdleTimer();
            }
        }

		private string TimeString(TimeSpan ts)
		{
			return string.Format("{0:00}:{1:00}", (int)ts.TotalMinutes, ts.Seconds);
		}

		private void UpdatePlayingFile()
		{
			foreach (ListViewItem item in m_listFiles.Items)
			{
				Color crText = item.Index == _playingIdx ? Color.Red : SystemColors.WindowText;
				item.ForeColor = crText;
			}
		}

		private void UpdateButtonsState(string status)
		{
			status = status.ToLower();
			switch (status)
			{
				case "playing":
					UpdateButtonsState(false, true, true, true, true, true);
					UpdateButtonsState(true, true, false, false, false, false);
					break;
				case "paused":
					UpdateButtonsState(false, true, true, true, true, true);
					UpdateButtonsState(true, false, true, false, false, false);
					break;
				case "open":
					UpdateButtonsState(false, true, true, true, true, true);
					UpdateButtonsState(true, false, false, false, false, false);
					break;
				case "stopped":
					UpdateButtonsState(false, true, true, true, true, true);
					UpdateButtonsState(true, false, false, true, false, false);
					break;
				default:
					UpdateButtonsState(false, true, false, false, true, true);
					UpdateButtonsState(true, false, false, false, false, false);
					break;
			}
		}

		private void UpdateButtonsState(bool bCheckState, bool bPlay, bool bPause, bool bStop, bool bPrev, bool bNext)
		{
			bool bHasFiles = Count > 0;

			if (bCheckState)
			{
				m_tbbtnPlay.Checked = bPlay;
				m_tbbtnPause.Checked = bPause;
				m_tbbtnStop.Checked = bStop;
				m_tbbtnPrev.Checked = bPrev;
				m_tbbtnNext.Checked = bNext;
			}
			else
			{
				m_tbbtnPlay.Enabled = bPlay && bHasFiles;
				m_tbbtnPause.Enabled = bPause && bHasFiles;
				m_tbbtnStop.Enabled = bStop && bHasFiles;
				m_tbbtnPrev.Enabled = bPrev && Count>1;
				m_tbbtnNext.Enabled = bNext && Count>1;
			}

			m_toolStripButton_Up.Enabled = EnableUp();
			m_toolStripButton_Down.Enabled = EnableDown();

			m_toolStripButton_Remove.Enabled = HasSelected;
			m_toolStripButton_RemoveAll.Enabled = bHasFiles;

			m_mnuPlay.Enabled = m_tbbtnPlay.Enabled;
			m_mnuPause.Enabled = m_tbbtnPause.Enabled;
			m_mnuStop.Enabled = m_tbbtnStop.Enabled;
			m_mnuPrev.Enabled = m_tbbtnPrev.Enabled;
			m_mnuNext.Enabled = m_tbbtnNext.Enabled;

			m_mnuUp.Enabled = m_toolStripButton_Up.Enabled;
			m_mnuDown.Enabled = m_toolStripButton_Down.Enabled;

			m_mnuAdd.Enabled = m_toolStripButton_AddFiles.Enabled;
			m_mnuRemove.Enabled = m_toolStripButton_Remove.Enabled;
			m_mnuRemoveAll.Enabled = m_toolStripButton_RemoveAll.Enabled;
		}

		//return first if not selected
		private int GetSelectedFile()
		{
			if (m_listFiles.Items.Count == 0)
				return -1;

			if (m_listFiles.SelectedIndices.Count == 0)
			{
				return 0;
			}

			return m_listFiles.SelectedIndices[0];
		}

		private void m_tbbtnPlay_Click(object sender, EventArgs e)
		{
			int idx = GetSelectedFile();
			if (idx < 0)
			{
				//m_prgr3Minute.Value = 0;
                m_progrReiKi.Stop();
				return;
			}
			
			Play(idx);

            m_progrReiKi.Start();
		}

		private void Play(int idx)
		{
			_playingIdx = idx;
			FileInfo fiMP3 = m_listFiles.Items[idx].Tag as FileInfo;

			//m_Mp3Player.Open(fiMP3.FullName, fiMP3.Name);
			m_Mp3Player.Play(fiMP3.FullName, fiMP3.Name);
			m_Mp3Player.SetVolume(Volume);

			m_trackBarPosition.Maximum = m_Mp3Player.GetSongLenght() / 1000;
			_tsLength = TimeSpan.FromSeconds(m_trackBarPosition.Maximum);
			m_listFiles.Items[idx].SubItems[1].Text = TimeString(_tsLength);

			UpdateInfo();
		}

		private void m_tbbtnPause_Click(object sender, EventArgs e)
		{
			Pause();
		}

		private void m_tbbtnStop_Click(object sender, EventArgs e)
		{
			m_Mp3Player.Stop();
			m_Mp3Player.SetPosition(0);
            m_progrReiKi.Stop();
			//m_Mp3Player.Close();
			UpdateInfo();
		}

		private void m_tbbtnPrev_Click(object sender, EventArgs e)
		{
			if (m_listFiles.Items.Count == 0) 
				return;

			int idx = _playingIdx - 1;
			if (idx < 0) idx = m_listFiles.Items.Count - 1;
			Play(idx);
		}

		private void m_tbbtnNext_Click(object sender, EventArgs e)
		{
			if (m_listFiles.Items.Count == 0)
				return;

			int idx = _playingIdx+1;
			if (idx >= m_listFiles.Items.Count) idx = 0;
			Play(idx);
		}

		private void m_toolStripTrackBarVolume_ValueChanged(object sender, EventArgs e)
		{
			m_Mp3Player.SetVolume(Volume);
			UpdateInfo();
		}

		private void m_chkLoop_Click(object sender, EventArgs e)
		{
			Loop = !Loop;
		}

		private void m_toolStripButton_AddFiles_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK != m_openFileDialog.ShowDialog(this))
				return;

			AddToFileList(m_openFileDialog.FileNames, true);
		}

		private void m_toolStripButton_RemoveAll_Click(object sender, EventArgs e)
		{
			RemoveAll();
		}

		private void m_toolStripButton_Remove_Click(object sender, EventArgs e)
		{
			RemoveSelectedFiles();
		}

		private void m_listFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateInfo();
		}

		private bool EnableUp()
		{
			if (m_listFiles.SelectedIndices.Count != 1)
				return false;
			if (m_listFiles.SelectedIndices[0] < 1)
				return false;
			return true;
		}

		private void m_toolStripButton_Up_Click(object sender, EventArgs e)
		{
			if (!EnableUp())
				return;

			int idx = m_listFiles.SelectedIndices[0];
			ListViewItem itm = m_listFiles.SelectedItems[0];
			m_listFiles.Items.RemoveAt(idx);
			m_listFiles.Items.Insert(idx - 1, itm);
			if (_playingIdx == idx) _playingIdx = idx - 1;
		}

		private bool EnableDown()
		{
			if (m_listFiles.SelectedIndices.Count != 1)
				return false;
			if (m_listFiles.SelectedIndices[0] > m_listFiles.Items.Count - 2)
				return false;
			return true;
		}

		private void m_toolStripButton_Down_Click(object sender, EventArgs e)
		{
			if (!EnableDown())
				return;

			int idx = m_listFiles.SelectedIndices[0];
			ListViewItem itm = m_listFiles.SelectedItems[0];
			m_listFiles.Items.RemoveAt(idx);
			m_listFiles.Items.Insert(idx + 1, itm);
			if (_playingIdx == idx) _playingIdx = idx + 1;
		}
	}
}
