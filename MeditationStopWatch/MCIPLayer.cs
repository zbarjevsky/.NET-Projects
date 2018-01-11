using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace MeditationStopWatch
{
	public class MCIPLayer
	{
        //Random randomNumber = new Random();
        private StringBuilder m_sbErrorMessage;  // MCI Error message
        private StringBuilder m_sbReturnData;  // MCI return m_Data
        private bool result;
        private string _command, _file = string.Empty;  // String that holds the MCI command
        //private IList<FileInfo> playlist;  // ListView as a playlist with the song path
		//public int NowPlaying { get; set; }
		public bool Paused { get; set; }
		//public bool Loop { get; set; }
		//public bool Shuffle { get; set; }

        [DllImport("winmm.dll")]
        private static extern int mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        [DllImport("winmm.dll")]
        public static extern int mciGetErrorString(int errCode, StringBuilder errMsg, int buflen);

		private bool Command(string cmd, params object [] args)
		{
			if (_file == string.Empty)
				return true;

			_command = string.Format(cmd, args);
			int error = mciSendString(_command, null, 0, IntPtr.Zero);
			if (error == 0)
				return true;

			LogResult(error, _command);
			return false;
		}

		private string CommandGetData(string cmd, params object[] args)
		{
			if (_file == string.Empty)
				return string.Empty;

			_command = string.Format(cmd, args);
			int error = mciSendString(_command, m_sbReturnData, m_sbReturnData.Capacity, IntPtr.Zero);
			if (error == 0)
			{
				return m_sbReturnData.ToString();
			}

			LogResult(error, _command);
			return string.Empty;
		}

        // When creating a new player object you have to pass to it a ListView object
        // that will hold all the information about the songs in the playlist
		public MCIPLayer(/*IList<FileInfo> pl*/)
        {
            //playlist = pl;
            //NowPlaying = 0;
            //Loop = false;
            //Shuffle = true;
            Paused = false;
            m_sbErrorMessage = new StringBuilder(128);
            m_sbReturnData = new StringBuilder(128);
        }

        #region Operations

        public void Close()
        {
			Command("close {0}", _file);
			_file = string.Empty;
        }

        public bool Open(string sFileName, string sShortName)
        {
            Close();

            // Try to open as mpegvideo 
			_file = PrepareShortName(sShortName);
            result = Command("open \"{0}\" type mpegvideo alias {1}", sFileName, _file);
            if (!result)
            {
                // Let MCI deside which file type the song is
                result = Command("open \"{0}\" alias {1}", sFileName, _file);
                return result;
            }
            else
                return true;      
        }


		public bool Play(string sFileName, string sShortName)
        {
			if (Open(sFileName, sShortName))
            {
				result = Command("play {0}", _file);
                if (result)
                {
                    //NowPlaying = track;
                    return true;
                }
                else
                {
                    Close();
                    return false;
                }
            }
            else
                return false;
        }

        public void Pause()
        {
            if (Paused)
            {
                Resume();
                Paused = false;
            }
            else if(IsPlaying())
            {
				result = Command("pause {0}", _file);
                Paused = true;
            }
        }

        public void Stop()
        {
			result = Command("stop {0}", _file);
            Paused = false;
            //Close();
        }

        public void Resume()
        {
			result = Command("resume {0}", _file);
        }

        #endregion 

        #region Status

		public string StatusMode
		{
			get 
			{
				return CommandGetData("status {0} mode", _file);
			}
		}

        public bool IsPlaying()
        {
            string sMode = StatusMode;
			if (sMode.Length == 7 && sMode.Substring(0, 7) == "playing")
                return true;
            else
                return false;
        }

        public bool IsOpen()
        {
			string sMode = StatusMode;
			if (sMode.Length == 4 && sMode.Substring(0, 4) == "open")
                return true;
            else
                return false;
        }

        public bool IsPaused()
        {
			string sMode = StatusMode;
			if (sMode.Length == 6 && sMode.Substring(0, 6) == "paused")
                return true;
            else
                return false;
        }

        public bool IsStopped()
        {
			string sMode = StatusMode;
			if (sMode.Length == 7 && sMode.Substring(0, 7) == "stopped")
                return true;
            else
                return false;
        }
        #endregion

        #region Logic

        public int GetCurentMilisecond()
        {
			string sPosition = CommandGetData("status {0} position", _file);
			if (string.IsNullOrEmpty(sPosition))
				return 0;
			return int.Parse(sPosition);
        }

        public void SetPosition(int miliseconds)
        {
            if (IsPlaying())
            {
				result = Command("play {0} from {1}", _file, miliseconds);
            }
            else
            {
				result = Command("seek {0} to {1}", _file, miliseconds);
            }
        }

        public int GetSongLenght()
        {
            if (IsPlaying())
            {
				string sLength = CommandGetData("status {0} length", _file);
				return int.Parse(sLength);
            }
            else
                return 0;
        }

        #endregion

        #region Audio
        public bool SetVolume(int volume)
        {
            if (volume >= 0 && volume <= 1000)
            {
				result = Command("setaudio {0} volume to {1}", _file, volume);
                return result;
            }
            else
                return false;
        }

        public bool SetBalance(int balance, int volume)
        {
			const int max = 1000;
			double factor = volume / max; 

            if (balance >= 0 && balance <= max)
            {
				int left = (max - balance) * volume / max;
				result = Command("setaudio {0} left volume to {1}", _file, left);
				int right = balance * volume / max;
				result = Command("setaudio {0} right volume to {1}", _file, right);
                return true;
            }
            else
                return false;
        }

        #endregion

		private string GetErrorString(int error)
		{
			StringBuilder buffer = new StringBuilder(512);
			int returnValue = mciGetErrorString(error, buffer, buffer.Capacity);
			string err = buffer.ToString();
			return err;
		}

		private void LogResult(int error, string sCommand)
		{
			if (error != 0)
			{
				System.Diagnostics.Trace.WriteLine(sCommand);
				System.Diagnostics.Trace.WriteLine(GetErrorString(error));
			}
		}

		private string PrepareShortName(string sShortName)
		{
			return sShortName.Replace(' ', '_');
		}


		//public int GetNextSong(bool previous)
		//{
		//    if (Shuffle)
		//    {
		//        int i;
		//        if (playlist.Count == 1)
		//            return 0;
		//        while (true)
		//        {
		//            i = randomNumber.Next(playlist.Count);
		//            if (i != NowPlaying)
		//                return i;
		//        }
		//    }
		//    else if (Loop && !previous)
		//    {
		//        if (NowPlaying == playlist.Count - 1)
		//            return 0;
		//        else
		//            return NowPlaying + 1;
		//    }
		//    else if (Loop && previous)
		//    {
		//        if (NowPlaying == 0)
		//            return playlist.Count - 1;
		//        else
		//            return NowPlaying - 1;
		//    }
		//    else
		//    {
		//        if (previous)
		//        {
		//            if (NowPlaying != 0)
		//                return NowPlaying - 1;
		//            else
		//                return 0;
		//        }
		//        else
		//        {
		//            if (NowPlaying != playlist.Count - 1)
		//                return NowPlaying + 1;
		//            else
		//                return 0;
		//        }
		//    }
		//}

	}
}
