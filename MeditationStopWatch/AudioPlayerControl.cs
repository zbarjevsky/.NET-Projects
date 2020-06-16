using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media;
using System.Diagnostics;
using MZ.WPF.MessageBox;

namespace MeditationStopWatch
{
	public partial class AudioPlayerControl : UserControl
	{
		//private List<FileInfo> m_vSongsList = new List<FileInfo>();
		//private MCIPLayer m_Mp3Player = new MCIPLayer();
		private NETSoundPlayer m_Mp3Player = new NETSoundPlayer();
		private TimeSpan _tsLength;
		private Options m_Options;
		//private bool _bScreenSaverActive = false;
	
		public event EventHandler OnTimerTick;
		public event EventHandler<NETSoundPlayer.PlayingState> OnPlayerStateChanged;

		public AudioPlayerControl()
		{
			InitializeComponent();

			m_Mp3Player.OnMediaOpened += _player_MediaOpened;
			m_Mp3Player.OnMediaEnded += _player_MediaEnded;
			m_Mp3Player.OnMediaFailed += _player_MediaFailed;
			m_Mp3Player.OnStateChanged += _player_StateChanged;
		}

        private void AudioPlayerControl_Load(object sender, EventArgs e)
		{
			UpdateInfo();
            m_playLists.PL.OP.OnListChanged += () => { UpdateInfo(); };
            m_playLists.PL.OP.PlayAction += (selectedIdx) => { PlaySelected(selectedIdx); };
            m_playLists.PL.OP.PauseAction += () => { PauseResume(); };
			m_playLists.PL.OP.CloseAction += () => { Close(); };
			m_playLists.PL.OP.StopAction += () => { Stop(); };
			m_playLists.PL.OP.NextAction += () => { Next(); };
            m_playLists.PL.OP.PrevAction += () => { Prev(); };
        }

		public int Count { get { return m_playLists.PL.Count; } }
		public FileInfo PlayingFile 
		{ 
			get 
			{
				return m_playLists.PL.PlayingFile; 
			} 
		}

		public string[] PlayList
		{
			get
			{
				string[] playList = new string[m_Options.PlayListCollection[0].List.Count];
				for (int i = 0; i < m_Options.PlayListCollection[0].List.Count; i++)
				{
					playList[i] = m_Options.PlayListCollection[0].List[i];
				}
				return playList;
			}
		}

        public void AddToFileList(List<string> files, bool bPlayFirst)
        {
            m_playLists.PL.AddToFileList(files, bPlayFirst);
            m_playLists.PL.OP.OnListChanged();
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

		public void InitializeOptions(Options opt)
		{
            m_Options = opt;
            Volume = m_Options.Volume;
            m_progrReiKi.Initialize(m_Options);
            m_playLists.Initialize(m_Options);
            m_lblStatus.Font = m_Options.PlayListFont;
		}

		private void m_trackBarPosition_Scroll(object sender, EventArgs e)
		{
			m_Mp3Player.PositionMs = (m_trackBarPosition.Value * 1000);
			UpdateInfo();
		}

		private void _player_MediaOpened(object sender, EventArgs e)
		{
			_tsLength = m_Mp3Player.GetSongLenght();
			m_trackBarPosition.Maximum = (int)_tsLength.TotalSeconds;
			_tsLength = TimeSpan.FromSeconds(m_trackBarPosition.Maximum);
			m_playLists.PL.UpdateFileTime(m_playLists.PL.FileIndex, TimeString(_tsLength));

			UpdateInfo();
		}

		private void _player_MediaEnded(object sender, EventArgs e)
		{
			if (Loop) // && m_Mp3Player.GetStatusMode() == NETSoundPlayer.PlayingState.stopped && m_trackBarPosition.Value == m_trackBarPosition.Maximum)
				Play(m_playLists.PL.NextIdx());
		}


		private void _player_MediaFailed(object sender, ExceptionEventArgs e)
		{
			this.MessageError("Cannot open file: " + e.ErrorException.Message);
		}

		private void _player_StateChanged(object sender, NETSoundPlayer.PlayingState e)
		{
			OnPlayerStateChanged?.Invoke(sender, e);
		}


		private void m_timer1_Tick(object sender, EventArgs e)
		{
			//Debug.WriteLine("Timer: " + DateTime.Now.ToString("s"));
			if (m_Options == null)
				return;

			m_timer1.Stop();
            //if (Loop && m_Mp3Player.GetStatusMode() == NETSoundPlayer.PlayingState.stopped && m_trackBarPosition.Value == m_trackBarPosition.Maximum)
            //	Play(m_playLists.PL.NextIdx());

            OnTimerTick?.Invoke(sender, e);

            UpdateInfo();
			m_timer1.Start();
		}

		private void UpdateInfo()
		{
            try
            {
                var playMode = m_Mp3Player.State;
                m_lblStatus.Text = playMode + ": " + m_Mp3Player.FileName;
                UpdateButtonsState(playMode);
                DisableScreenSaver();
                UpdatePlayingFile();
                m_progrReiKi.Initialize(m_Options);

                int sec = m_Mp3Player.PositionMs / 1000;
                m_trackBarPosition.Value = sec;
                //m_prgr3Minute.Value = (int)(sec % 180);

                TimeSpan tsPos = TimeSpan.FromSeconds(sec);
                m_lblTime.Text = string.Format("{0} / {1}",
                    TimeString(tsPos), TimeString(_tsLength));

                m_toolStripTrackBarVolume.ToolTipText = "";
                //string.Format("V({0})", Volume);

                this.m_toolTip1.SetToolTip(this.m_toolStripTrackBarVolume.TrackBar,
                    string.Format("V({0})\nMouseScroll", Volume));

                this.m_toolTip1.SetToolTip(this.m_trackBarPosition,
                    string.Format("P({0})", TimeString(tsPos)));

            }
            catch (Exception err)
            {
				Debug.WriteLine(err);
            }		
		}

        private int _screenSaverTimerCount = 0;
		private void DisableScreenSaver()
		{
            if (m_Mp3Player.State == NETSoundPlayer.PlayingState.playing)
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
            m_playLists.PL.UpdatePlayingFile();
		}

		private void UpdateButtonsState(NETSoundPlayer.PlayingState status)
		{
			switch (status)
			{
				case NETSoundPlayer.PlayingState.playing:
					UpdatePlayerButtonsState(false, true, true, true, true, true);
					UpdatePlayerButtonsState(true, true, false, false, false, false);
					break;
				case NETSoundPlayer.PlayingState.paused:
					UpdatePlayerButtonsState(false, true, true, true, true, true);
					UpdatePlayerButtonsState(true, false, true, false, false, false);
					break;
				case NETSoundPlayer.PlayingState.open:
					UpdatePlayerButtonsState(false, true, true, true, true, true);
					UpdatePlayerButtonsState(true, false, false, false, false, false);
					break;
				case NETSoundPlayer.PlayingState.stopped:
					UpdatePlayerButtonsState(false, true, true, true, true, true);
					UpdatePlayerButtonsState(true, false, false, true, false, false);
					break;
				default:
					UpdatePlayerButtonsState(false, true, false, false, true, true);
					UpdatePlayerButtonsState(true, false, false, false, false, false);
					break;
			}
		}

		private void UpdatePlayerButtonsState(bool bUpdateCheckedState, bool bPlay, bool bPause, bool bStop, bool bPrev, bool bNext)
		{

			if (bUpdateCheckedState)
			{
				m_tbbtnPlay.Checked = bPlay;
				m_tbbtnPause.Checked = bPause;
				m_tbbtnStop.Checked = bStop;
				m_tbbtnPrev.Checked = bPrev;
				m_tbbtnNext.Checked = bNext;
			}
			else
			{
                bool bHasFiles = Count > 0;
                m_tbbtnPlay.Enabled = bPlay && bHasFiles;
				m_tbbtnPause.Enabled = bPause && bHasFiles;
				m_tbbtnStop.Enabled = bStop && bHasFiles;
				m_tbbtnPrev.Enabled = bPrev && Count>1;
				m_tbbtnNext.Enabled = bNext && Count>1;
			}

            m_playLists.PL.UpdatePlayerMenusState(bUpdateCheckedState, bPlay, bPause, bStop, bPrev, bNext);
		}

		//return first if not selected
		private int GetSelectedFile()
		{
			return m_playLists.PL.GetSelectedFile();
		}

		private void m_tbbtnPlay_Click(object sender, EventArgs e)
		{
            PlaySelected(GetSelectedFile());
		}

        private void PlaySelected(int selectedIndex = -1)
        {
            if (selectedIndex < 0)
            {
                //m_prgr3Minute.Value = 0;
                m_progrReiKi.Stop();
                return;
            }

            Play(selectedIndex);
            m_progrReiKi.Start();
        }

        private void Play(int idx)
		{
            FileInfo fiMP3 = m_playLists.PL.SelectPlayFile(idx);

			if(m_Mp3Player.State == NETSoundPlayer.PlayingState.paused)
            {
				m_Mp3Player.CmdResume();
            }
            else
            {
				m_Mp3Player.SetVolume(Volume);
				m_Mp3Player.Play(fiMP3.FullName, fiMP3.Name);
			}
		}

        public void PauseResume()
        {
			var mode = m_Mp3Player.State;

			if (mode == NETSoundPlayer.PlayingState.playing)
				m_Mp3Player.CmdPause();
			else if (mode == NETSoundPlayer.PlayingState.paused)
				m_Mp3Player.CmdResume();
			else if (mode == NETSoundPlayer.PlayingState.stopped)
				m_Mp3Player.CmdPlay();
			else if (mode == NETSoundPlayer.PlayingState.none)
				Next(); //play next

			mode = m_Mp3Player.State;
			if (mode == NETSoundPlayer.PlayingState.paused || mode == NETSoundPlayer.PlayingState.stopped)
                m_progrReiKi.Pause();
            else
                m_progrReiKi.Resume();
            UpdateInfo();
        }

		public void Close()
		{
			m_Mp3Player.CmdClose();
			m_progrReiKi.Stop();
			
			UpdateInfo();
		}

		public void Stop()
		{
			m_Mp3Player.CmdStop();
			m_progrReiKi.Stop();

			UpdateInfo();
		}

		public void Next()
        {
            if (Count == 0)
                return;

            PlaySelected(m_playLists.PL.NextIdx());
        }

        public void Prev()
        {
            if (Count == 0)
                return;

            PlaySelected(m_playLists.PL.PrevIdx());
        }

        private void m_tbbtnPause_Click(object sender, EventArgs e)
		{
			m_Mp3Player.CmdPause();
			UpdateInfo();
		}

		private void m_tbbtnStop_Click(object sender, EventArgs e)
		{
            Stop();
		}

		private void m_tbbtnNext_Click(object sender, EventArgs e)
		{
            Next();
		}

		private void m_tbbtnPrev_Click(object sender, EventArgs e)
		{
            Prev();
		}

		private void m_toolStripTrackBarVolume_ValueChanged(object sender, EventArgs e)
		{
            m_Options.Volume = Volume;
			m_Mp3Player.SetVolume(Volume);
			UpdateInfo();
		}

		private void m_chkLoop_Click(object sender, EventArgs e)
		{
			Loop = !Loop;
		}
    }
}
