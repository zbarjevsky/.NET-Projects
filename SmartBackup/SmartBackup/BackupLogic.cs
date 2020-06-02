using MZ.WinForms;
using MZ.Tools;
using SmartBackup.Settings;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartBackup
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

        private string _dstFolder = null;
        public string DstFolder 
        {
            get
            {
                if(_dstFolder == null)
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
            return (SrcIfo.Length > 12 * 1024 * 1024); //less than 13MB
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

        public BackupStatus PerformBackup(ProgressBar progress, Form owner, BackupOptions option = BackupOptions.OverwriteAllOlder)
        {
            try
            {
                Err = "OK";
                Status = BackupStatus.None;
                CommonUtils.ExecuteOnUIThread(() => { progress.Value = progress.Minimum; }, owner);

                if(!ValidateBackupNeeded(option, true))
                    return Status;

                Status = BackupStatus.InProgress;

                if (DstIfo.Exists && DstIfo.IsReadOnly)
                    DstIfo.IsReadOnly = false;

                if (IsBigFile()) //more than 12M
                    CopyWithProgress(progress, owner);
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
        private void CopyWithProgress(ProgressBar progress, Form owner)
        {
            int bufferSize = 1024 * 64;
            byte[] bytes = new byte[bufferSize];

            using (FileStream srcFile = new FileStream(Src, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan))
            {
                using (FileStream fileStream = new FileStream(Dst, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    int bytesRead = -1;
                    long totalWrite = 0;
                    while ((bytesRead = srcFile.Read(bytes, 0, bufferSize)) > 0)
                    {
                        fileStream.Write(bytes, 0, bytesRead);

                        totalWrite += bytesRead;

                        CommonUtils.ExecuteOnUIThread(() =>
                        {
                            progress.Value = (int)(totalWrite * 100 / SrcIfo.Length);
                        }, owner);
                    }
                }
            }
        }

        private string FindSubFolders(string folderSrc, string file)
        {
            List<string> newFolders = new List<string>();

            string folder = Path.GetDirectoryName(file);
            while (folderSrc.CompareTo(folder) != 0)
            {
                newFolders.Add(Path.GetFileName(folder));
                folder = Path.GetDirectoryName(folder);
            }
            newFolders.Add(Path.GetFileName(folder));

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

        public void Load(BackupPriority priority, FileUtils.FileProgress progress = null)
        {
            Clear();
            foreach (BackupEntry entry in _group.BackupList)
            {
                if (priority.HasFlag(entry.Priority))
                {
                    FileList.AddRange(CollectFiles(entry, progress));
                }
            }
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
            if(!Directory.Exists(root))
            {
                MessageBox.Show("Cannot access drive for backup: " + root);
                freeSpace = 0;
                return "Drive " + root + " is not accessible";
            }

            DriveInfo drive = GetDriveInfo(root);
            freeSpace = drive.TotalFreeSpace;
            return string.Format("Free Space on Destination Drive {0} is {1:###,##0.0} MB", root, freeSpace / i1MB);
        }

        public static DriveInfo GetDriveInfo(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive;
                }
            }
            return null;
        }

        public static List<BackupFile> CollectFiles(BackupEntry entry, FileUtils.FileProgress progress = null)
        {
            List<BackupFile> fileList = new List<BackupFile>();
            if (string.IsNullOrWhiteSpace(entry.FolderSrc))
                return fileList;

            DirectoryInfo dir = new DirectoryInfo(entry.FolderSrc);
            if (!dir.Exists)
                return fileList;

            try
            {
                if (progress != null)
                {
                    string prompt = string.Format("Discovering ({0}) ", entry.FolderSrc);
                    progress.ResetToMarquee(prompt);
                }

                List<string> files = FileUtils.GetFiles(dir.FullName, 
                    entry.FolderIncludeTypes, entry.IncludeSubfolders, progress).ToList();

                if (progress != null)
                {
                    if (progress.Cancel)
                    {
                        files.Clear();
                        GC.Collect();
                        return fileList;
                    }

                    //report percentage only - may be too many files
                    string prompt = string.Format("Preparing Collected Files ({0}) ", entry.FolderSrc);
                    progress.ResetToBlocks(prompt, files.Count);
                }

                for (int i = 0; i < files.Count; i++)
                {
                    if (progress != null)
                    {
                        if (progress.Cancel)
                        {
                            fileList.Clear();
                            break;
                        }
                        progress.Value = i;
                    }

                    //if (File.Exists(files[i]))
                        fileList.Add(new BackupFile(i, files[i], entry));
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine("Error enumerating files in: " + dir.FullName + ", Error: " + err);
            }

            GC.Collect();
            return fileList;
        }
    }
}
