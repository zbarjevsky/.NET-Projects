using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using MZ.Tools;

namespace SimpleBackup.Settings
{
    public class SelectedFoldersAndFilesList
    {
        public bool AllInSrcFolder { get; set; } = true;
        public string FolderSrc { get; set; }
        public List<string> Names { get; set; } = new List<string>();
    }

    public class BackupEntry
    {
        public string FolderDst { get; set; }
        public SearchOption IncludeSubfolders { get; set; }
        public string FolderIncludeTypes { get; set; }
        public string FolderExcludeTypes { get; set; }
        public BackupPriority Priority { get; set; }

        public SelectedFoldersAndFilesList SelectedFoldersAndFilesList { get; set; } = new SelectedFoldersAndFilesList();

        public string FolderSrc
        {
            set { SelectedFoldersAndFilesList.FolderSrc = value; }
            get { return SelectedFoldersAndFilesList.FolderSrc; }
        }

        [XmlIgnore]
        public bool IsSrcOnNetworkDrive 
        { 
            get 
            {
                string rootSrc = Path.GetPathRoot(SelectedFoldersAndFilesList.FolderSrc);
                DriveInfo drive = new DriveInfo(rootSrc);
                return drive.DriveType == DriveType.Network;
            }
        }

        public BackupEntry()
        {
            IncludeSubfolders = SearchOption.AllDirectories;
            Priority = BackupPriority.Normal;
            FolderIncludeTypes = "*.*";
            FolderExcludeTypes = "";
        }

        public bool IsValid(out string error)
        {
            error = "";
            if (!Directory.Exists(SelectedFoldersAndFilesList.FolderSrc))
            {
                error = "Error: Source does not exists: " + SelectedFoldersAndFilesList.FolderSrc;
                return false;
            }
            if (string.IsNullOrWhiteSpace(FolderDst))
            {
                error = "Error: Destination folder not set.";
                return false;
            }
            string rootSrc = Path.GetPathRoot(SelectedFoldersAndFilesList.FolderSrc);
            string rootDst = Path.GetPathRoot(FolderDst);
            if (rootDst == rootSrc)
            {
                error = "Error: Sorce and Destination are on same drive: " + rootSrc;
                return false;
            }

            return true;
        }

        public List<BackupFile> CollectFiles(FileUtils.FileProgress progress = null)
        {
            List<BackupFile> fileList = new List<BackupFile>();
            if (string.IsNullOrWhiteSpace(FolderSrc))
                return fileList;

            DirectoryInfo dir = new DirectoryInfo(FolderSrc);
            if (!dir.Exists)
                return fileList;

            try
            {
                if (progress != null)
                {
                    string prompt = string.Format("Discovering ({0}) ", FolderSrc);
                    progress.ResetToMarquee(prompt);
                }

                List<string> fileNames = new List<string>();
                if (SelectedFoldersAndFilesList.AllInSrcFolder)
                {
                    fileNames = FileUtils.GetFiles(dir.FullName, FolderIncludeTypes, IncludeSubfolders, progress).ToList();
                }
                else
                {
                    foreach (string file in SelectedFoldersAndFilesList.Names)
                    {
                        string fullPath = Path.Combine(FolderSrc, file);
                        if (Directory.Exists(fullPath))
                            fileNames.AddRange(FileUtils.GetFiles(fullPath, FolderIncludeTypes, IncludeSubfolders, progress));
                        else if (File.Exists(fullPath))
                            fileNames.Add(fullPath);
                    }
                }

                if (progress != null)
                {
                    if (progress.Cancel)
                    {
                        fileNames.Clear();
                        GC.Collect();
                        return fileList;
                    }

                    //report percentage only - may be too many files
                    string prompt = string.Format("Preparing Collected Files ({0}) ", SelectedFoldersAndFilesList.FolderSrc);
                    progress.ResetToBlocks(prompt, fileNames.Count);
                }

                for (int i = 0; i < fileNames.Count; i++)
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
                    fileList.Add(new BackupFile(i, fileNames[i], this));
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

    public class BackupGroup
    {
        private const string DATE_FORMAT = "yyyy_MMdd_HHmmss";

        public string Name { get; set; }

        public string Description { get; set; }

        public string LastBackupTimeStr { get; set; }

        [XmlIgnore]
        public DateTime LastBackupTime
        {
            get { return DateTime.ParseExact(LastBackupTimeStr, DATE_FORMAT, CultureInfo.InvariantCulture); }
            set { LastBackupTimeStr = value.ToString(DATE_FORMAT, CultureInfo.InvariantCulture); }
        }

        public List<BackupEntry> BackupList { get; set; } = new List<BackupEntry>();

        public BackupGroup(string name)
        {
            this.Name = name;
            Description = name;
        }

        public BackupGroup()
        {
            Name = "Default";
            Description = Name;
        }

        internal void Add(BackupEntry entry)
        {
            BackupEntry found = FindEntry(entry);
            if (found == null)
                BackupList.Add(entry);
            else
                MessageBox.Show("Already exists:\n" + entry.SelectedFoldersAndFilesList.FolderSrc);
        }

        internal void Remove(BackupEntry entry)
        {
            BackupList.Remove(entry);
        }

        internal BackupEntry FindEntry(BackupEntry entry)
        {
            return BackupList.SingleOrDefault(e => e.SelectedFoldersAndFilesList.FolderSrc == entry.SelectedFoldersAndFilesList.FolderSrc);
        }
    }

    public class BackupSettings
    {
        private const string _fileName = "SmartBackup.Config.xml";

        [XmlIgnore]
        public static string ConfigurationFileName { get; private set; }

        public List<BackupGroup> BackupGroups { get; set; } = new List<BackupGroup>();

        static BackupSettings()
        {
            string currentFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            ConfigurationFileName = Path.Combine(currentFolder, _fileName);
        }

        internal static BackupSettings Load()
        {
            BackupSettings cnf = SerializerHelper.Open<BackupSettings>(ConfigurationFileName);
            if (cnf.BackupGroups.Count == 0)
                cnf.BackupGroups.Add(new BackupGroup());
            return cnf;
        }

        internal void Save()
        {
            SerializerHelper.Save<BackupSettings>(ConfigurationFileName, this);
        }

        internal void Add(BackupGroup entry)
        {
            BackupGroup found = FindEntry(entry);
            if (found == null)
                BackupGroups.Add(entry);
            else
                MessageBox.Show("Already exists:\n" + entry.Name);
        }

        internal void Remove(BackupGroup entry)
        {
            BackupGroups.Remove(entry);
        }
    
        internal BackupGroup FindEntry(BackupGroup entry)
        {
            return BackupGroups.SingleOrDefault(e => e.Name == entry.Name);
        }
}
}
