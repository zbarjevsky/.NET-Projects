using MZ.Tools;
using SimpleBackup;
using SimpleBackup.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MZ.Tools.FileUtils;

namespace SimpleBackup.Tools
{
    public class CalculateOccupiedSpaceTask
    {
        private FileProgress _fileProgress;
        private Thread _threadUpdateInfo = null;
        private BackupEntry _entry = null;

        public Action<long, long> OnThreadFinished = (size, count) => { };

        public CalculateOccupiedSpaceTask(FileProgress fileProgress)
        {
            _fileProgress = fileProgress;
        }

        //cancel task if running
        public void Abort()
        {
            if (_threadUpdateInfo != null && _threadUpdateInfo.IsAlive)
            {
                _fileProgress.Cancel = true; 
                Thread.Sleep(64);
                _threadUpdateInfo.Abort();
                _threadUpdateInfo.Join(64);
                _threadUpdateInfo = null;
            }
        }

        public void Start(BackupEntry entry)
        {
            if (_entry != null && entry.Selection.Equals(_entry.Selection))
                return; //avoid run on the same folder
            _entry = entry.Clone();

            Abort(); //cancel previous if running

            _threadUpdateInfo = new Thread(() =>
            {
                long size = 0;
                string prompt = string.Format("Collecting files for ({0}) ", _entry.FolderSrc);
                _fileProgress.ResetToMarquee(prompt);
                List<BackupFile> fileList = BackupEntry.CollectFiles(_entry, _fileProgress);
                if (_fileProgress.Cancel || fileList.Count == 0)
                {
                    OnThreadFinished(0, fileList.Count);
                    return;
                }

                int count = fileList.Count;
                prompt = string.Format("Calculating Folder Size ({0}) ", _entry.FolderSrc);
                _fileProgress.ResetToBlocks(prompt, count);
                foreach (BackupFile file in fileList)
                {
                    if (_fileProgress.Cancel)
                    {
                        OnThreadFinished(0, fileList.Count);
                        break;
                    }

                    _fileProgress.Value++;
                    if (file.SrcIfo.Exists)
                        size += file.SrcIfo.Length;
                }
                _fileProgress.Value = 0;
                fileList.Clear();
                GC.Collect();

                if (!_fileProgress.Cancel)
                    OnThreadFinished(size, count);
            });

            _threadUpdateInfo.IsBackground = true;
            _threadUpdateInfo.Name = "Update Info Thread";
            _threadUpdateInfo.Start();
        }
    }
}
