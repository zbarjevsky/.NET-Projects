using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using MZ.Tools;

namespace SmartBackup.Settings
{
    public class BackupEntry
    {
        public string FolderSrc { get; set; }
        public string FolderDst { get; set; }
        public SearchOption IncludeSubfolders { get; set; }
        public string FolderIncludeTypes { get; set; }
        public string FolderExcludeTypes { get; set; }
        public BackupPriority Priority { get; set; }

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
            if (!Directory.Exists(FolderSrc))
            {
                error = "Error: Source does not exists: " + FolderSrc;
                return false;
            }
            if (string.IsNullOrWhiteSpace(FolderDst))
            {
                error = "Error: Destination folder not set.";
                return false;
            }
            string rootSrc = Path.GetPathRoot(FolderSrc);
            string rootDst = Path.GetPathRoot(FolderDst);
            if (rootDst == rootSrc)
            {
                error = "Error: Sorce and Destination are on same drive: " + rootSrc;
                return false;
            }

            return true;
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
                MessageBox.Show("Already exists:\n" + entry.FolderSrc);
        }

        internal void Remove(BackupEntry entry)
        {
            BackupList.Remove(entry);
        }

        internal BackupEntry FindEntry(BackupEntry entry)
        {
            return BackupList.SingleOrDefault(e => e.FolderSrc == entry.FolderSrc);
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
