using MZ.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartBackup.Tools
{
    public class CalculateSpaceDifferenceTask
    {
        private Thread _threadCalculateSpace = null;
        private FileUtils.FileProgress _fileProgress;

        public Action<string, string> OnThreadFinished = (status, error) => { };

        public CalculateSpaceDifferenceTask(FileUtils.FileProgress fileProgress)
        {
            _fileProgress = fileProgress;
        }

        public void Abort()
        {
            if (_threadCalculateSpace != null && _threadCalculateSpace.IsAlive)
            {
                _fileProgress.Cancel = true;
                Thread.Sleep(64);
                _threadCalculateSpace.Abort();
                _threadCalculateSpace = null;
            }
        }

        public void Start(List<BackupFile> backupFilesList, 
            BackupStatus backupStatus = BackupStatus.Any, BackupOptions backupOptions = BackupOptions.OverwriteAllOlder)
        {
            if (backupFilesList.Count == 0 || _fileProgress.Cancel)
                return;

            Abort();

            _threadCalculateSpace = new Thread(() =>
            {
                if (backupFilesList.Count == 0 || _fileProgress.Cancel)
                {
                    OnThreadFinished("Calculate Needed Size Calculation - Aborted...", "Aborted");
                    return;
                }

                _fileProgress.ResetToBlocks("Calculate Space Needed: ", backupFilesList.Count);
                string sizeInfo = CalculateSpaceNeeded(backupFilesList, backupStatus, backupOptions, _fileProgress, out string error);
                OnThreadFinished(sizeInfo, error);
            });

            _threadCalculateSpace.IsBackground = true;
            _threadCalculateSpace.Name = "Calculate Space Thread";
            _threadCalculateSpace.Priority = ThreadPriority.Lowest;
            _threadCalculateSpace.Start();
        }

        public static string CalculateSpaceNeeded(List<BackupFile> backupFilesList, 
            BackupStatus backupStatus, BackupOptions overwriteOptions, FileUtils.FileProgress progress,
            out string error)
        {
            if(backupFilesList.Count == 0)
            {
                error = "File List is Empty...";
                return error;
            }

            string diskInfo = BackupLogic.GetDiskFreeSpace(backupFilesList[0].DstFolder, out long freeSpace);

            if (progress != null)
                progress.Maximum = backupFilesList.Count;

            long sizeDst = 0, sizeSrc = 0;
            for (int i = 0; i < backupFilesList.Count; i++)
            {
                BackupFile file = backupFilesList[i];

                if (progress != null)
                {
                    if (progress.Cancel)
                    {
                        error = "Needed Size Calculation - Aborted..., " + diskInfo;
                        return error;
                    }

                    progress.Value++;
                }

                if (backupStatus.HasFlag(file.Status))
                {
                    if (!file.SrcIfo.Exists)
                        continue;

                    sizeSrc += file.SrcIfo.Length;
                    if (!file.ValidateBackupNeeded(overwriteOptions))
                        continue; //will not overwrite

                    sizeDst += file.SrcIfo.Length;
                    if (file.DstIfo.Exists)
                        sizeDst -= file.DstIfo.Length;
                }
            }

            error = freeSpace > sizeDst ? "" : "Not Enough Space on Destination Drive: " + backupFilesList[0].DstFolder;
            return string.Format("Filter: {0} - Source size {1:###,##0.0} MB, Estimated Space Needed: {2:###,##0.0} MB, {3}",
                backupStatus, sizeSrc / BackupLogic.i1MB, sizeDst / BackupLogic.i1MB, diskInfo);
        }
    }
}
