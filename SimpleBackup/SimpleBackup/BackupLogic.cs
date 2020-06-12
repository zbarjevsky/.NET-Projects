using MZ.WinForms;
using MZ.Tools;
using SimpleBackup.Settings;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MZ.Tools.FileUtils;

namespace SimpleBackup
{
    [Flags]
    public enum BackupStatus : int
    {
        None = 1,
        InProgress = 2,
        Done = 4,
        Error = 8,
        Any = None|InProgress|Done|Error
    }

    [Flags]
    public enum BackupOptions
    {
        OverwriteAll = 1,
        OverwriteAllOlder = 2,
        SkipExisting = 4,
        SkipReadonly = 8
    }

    [Flags]
    public enum BackupPriority : int
    {
        High = 1,
        Normal = 2,
        Low = 4,
        All = 7
    }

    public class BackupFile
    {
        public string Src = "";
        public string Err = "OK";
        public BackupStatus Status = BackupStatus.None;

        public long Index { get; }

        private string _dst = null;
        public string Dst 
        {
            get
            {
                if (_dst == null)
                {
                    string fileName = Path.GetFileName(Src);
                    _dst = Path.Combine(DstFolder, fileName);
                }
                return _dst;
            }
        }

        /// <summary>
        /// If source drive on network - decrease big file size
        /// </summary>
        public double BigFileSizeThreshold = 12 * BackupLogic.i1MB;

        private string _dstFolder = null;
        //private string _dstFolder = null;
        public string DstFolder 
        {
            get
            {
                if (_dstFolder == null)
                {
                    string subFolders = FindSubFolders(_entry.FolderSrc, Src);
                    _dstFolder = Path.Combine(_entry.FolderDst, subFolders);
                }
                return _dstFolder;
            }
        }

        private FileInfo _srcIfo = null;
        public FileInfo SrcIfo 
        {
            get 
            {
                if (_srcIfo == null)
                    _srcIfo = new FileInfo(Src);
                return _srcIfo;
            } 
        }

        private FileInfo _dstIfo = null;
        public FileInfo DstIfo
        {
            get
            {
                if (_dstIfo == null)
                    _dstIfo = new FileInfo(Dst);
                return _dstIfo;
            }
            private set
            {
                _dstIfo = value;
            }
        }

        public bool IsBigFile()
        {
            return (SrcIfo.Length > BigFileSizeThreshold); 
        }

        private readonly BackupEntry _entry;

        public BackupFile(long index, string file, BackupEntry entry)
        {
            Index = index;
            _entry = entry;
            Status = BackupStatus.None;
            Src = file;
        }

        public bool ValidateBackupNeeded(BackupOptions option, bool createDstFolderIfNotExists = false)
        {
            if(!SrcIfo.Exists)
            { Status = BackupStatus.Error; return false; }

            if (!DstIfo.Exists)
            {
                if(createDstFolderIfNotExists)
                    Directory.CreateDirectory(DstFolder);
                return true;
            }
            else if (!option.HasFlag(BackupOptions.OverwriteAll)) //check overwrite options
            {
                if (option.HasFlag(BackupOptions.OverwriteAllOlder) && SrcIfo.CreationTimeUtc <= DstIfo.CreationTimeUtc)
                { Status = BackupStatus.Done; return false; }

                if (option.HasFlag(BackupOptions.SkipExisting) && DstIfo.Exists)
                { Status = BackupStatus.Done; return false; }

                if (option.HasFlag(BackupOptions.SkipReadonly) && DstIfo.Exists && DstIfo.IsReadOnly)
                { Status = BackupStatus.Done; return false; }
            }

            return true;
        }

        public BackupStatus PerformBackup(FileProgress progress, BackupOptions option = BackupOptions.OverwriteAllOlder)
        {
            try
            {
                Err = "OK";
                Status = BackupStatus.None;
                progress.Value = progress.Minimum; //reset

                if(!ValidateBackupNeeded(option, true))
                    return Status;

                Status = BackupStatus.InProgress;

                if (DstIfo.Exists && DstIfo.IsReadOnly)
                    DstIfo.IsReadOnly = false;

                if (IsBigFile()) //more than 12M
                    CopyWithProgress(Src, Dst, SrcIfo.Length, progress);
                else
                    File.Copy(Src, Dst, true);

                RestoreTimestamp();

                return Status = BackupStatus.Done;
            }
            catch (Exception err)
            {
                Err = "Copy Err: " + err.Message;
                return Status = BackupStatus.Error;
            }        
        }

        public void ResetStatus()
        {
            _dstIfo = null;
            Err = "";
            Status = BackupStatus.None;
        }

        private BackupStatus RestoreTimestamp()
        {
            try
            {
                DstIfo = new FileInfo(Dst);
                if (DstIfo.IsReadOnly)
                    DstIfo.IsReadOnly = false;

                if(DstIfo.CreationTime != SrcIfo.CreationTime)
                    DstIfo.CreationTime = SrcIfo.CreationTime;
                if(DstIfo.LastWriteTime != SrcIfo.LastWriteTime)
                    DstIfo.LastWriteTime = SrcIfo.LastWriteTime;
                if(DstIfo.LastAccessTime != SrcIfo.LastAccessTime)
                    DstIfo.LastAccessTime = SrcIfo.LastAccessTime;

                return Status;
            }
            catch (Exception err)
            {
                Err = "Timestamp Err: " + err.Message;
                return Status = BackupStatus.Error;
            }
        }

        //http://www.pinvoke.net/default.aspx/kernel32.CopyFileEx
        private static void CopyWithProgress(string src, string dst, long length, FileProgress progress)
        {
            const int bufferSize = 4 * 1024; //4k - best size
            byte[] bytes = new byte[bufferSize];

            progress.ResetToBlocks("Progress: ", length, 0, NotifyOptions.NotifyValueChange);

            try
            {
                using (FileStream srcFile = new FileStream(src, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan))
                {
                    using (FileStream fileStream = new FileStream(dst, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        int bytesRead = -1;
                        long totalWrite = 0;
                        while ((bytesRead = srcFile.Read(bytes, 0, bufferSize)) > 0)
                        {
                            fileStream.Write(bytes, 0, bytesRead);

                            totalWrite += bytesRead;

                            if (progress.Cancel)
                                throw new Exception("Abort copy file: " + src);

                            progress.Value = totalWrite;
                        }
                    }
                }
                progress.Value = 0;
            }
            catch (Exception)
            {
                progress.Value = 0;
                if (File.Exists(dst)) //delete partially copied file
                    File.Delete(dst);
                throw;
            }        
        }

        //go up from 'file' folder until found 'folderSrc'
        //return all 'missing' folders on the way
        private string FindSubFolders(string folderSrc, string file)
        {
            List<string> newFolders = new List<string>();

            string folder = Path.GetDirectoryName(file);
            while (folderSrc.CompareTo(folder) != 0)
            {
                newFolders.Add(Path.GetFileName(folder));
                folder = Path.GetDirectoryName(folder);
            }
            //newFolders.Add(Path.GetFileName(folder));

            newFolders.Reverse();
            return Path.Combine(newFolders.ToArray());
        }
    }

    internal class BackupLogic
    {
        private readonly BackupGroup _group;

        private readonly List<BackupFile> FileList = new List<BackupFile>();

        public BackupLogic(BackupGroup group)
        {
            _group = group;

        }

        public void Load(BackupPriority priority, FileUtils.FileProgress progress, Action<string> onFinished = null)
        {
            Clear();
            Task.Factory.StartNew(() => 
            {
                string error = "";
                try
                {
                    foreach (BackupEntry entry in _group.BackupList)
                    {
                        if (priority.HasFlag(entry.Priority))
                        {
                            FileList.AddRange(CollectFiles(entry, progress));
                        }
                        
                        if (progress != null && progress.Cancel)
                            throw new Exception("Operation Aborted");
                    }
                }
                catch (Exception err)
                {
                    error = err.Message;
                }

                if (onFinished != null)
                    onFinished(error);
            });
        }

        public void Clear()
        {
            FileList.Clear();
            GC.Collect();
        }

        public List<BackupFile> FilteredFileList(BackupOptions options, BackupStatus statusFilter = BackupStatus.Any)
        {
            if (statusFilter == BackupStatus.Any)
                return FileList; //improve performance

            List<BackupFile> list = new List<BackupFile>();
            foreach (BackupFile file in FileList)
            {
                file.ValidateBackupNeeded(options);
                if (statusFilter.HasFlag(file.Status))
                    list.Add(file);
            }
            return list;
        }

        public void ResetStatus()
        {
            foreach (BackupFile file in FileList)
            {
                file.ResetStatus();
            }
        }

        public const double i1MB = 1024 * 1024;

        public static string GetDiskFreeSpace(string path, out long freeSpace)
        {
            string root = Path.GetPathRoot(path);
            DriveInfo drive = GetDriveInfo(root);
            if(drive == null)
            {
                string message = "Cannot access drive for backup: " + root;
                if (string.IsNullOrWhiteSpace(path))
                    message = "Destination Drive is not Defined";

                MessageBox.Show(message);
                freeSpace = 0;
                return "Drive " + root + " is not accessible";
            }

            freeSpace = drive.TotalFreeSpace;
            return string.Format("Free Space on Destination Drive {0} is {1:###,##0.0} MB", root, freeSpace / i1MB);
        }

        public static DriveInfo GetDriveInfo(string path)
        {
            string root = Path.GetPathRoot(path);
            if (string.IsNullOrWhiteSpace(root))
                return null;

            DriveInfo drive = new DriveInfo(root);
            if(drive != null && drive.IsReady)
                return drive;
            return null;
        }

        public static List<BackupFile> CollectFiles(BackupEntry entry, FileUtils.FileProgress progress = null)
        {
            List<BackupFile> fileList = entry.CollectFiles(progress);
            return fileList;
        }
    }
}
