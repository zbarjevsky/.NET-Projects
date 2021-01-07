using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

using MkZ.Tools;
using MkZ.Windows;

namespace MkZ.MediaPlayer.Utils
{
    [Serializable]
    public class Configuration
    {
        public ePlayMode PlayMode { get; set; } = ePlayMode.RepeatOne;

        public void CopyFrom(Configuration config)
        {
            PlayMode = config.PlayMode;
        }

        public override string ToString()
        {
            return string.Format("Configuration, PlayMode: {0}", PlayMode);
        }
    }

    //directory tree style play list
    public class PlayList : NotifyPropertyChangedImpl
    {
        [XmlIgnore]
        public bool IsRoot => Owner == null;

        [XmlIgnore]
        public PlayList Owner { get; private set; }

        [XmlIgnore]
        public Action<PlayList> OnPlayListSelectedAction = (l) => { };

        private string _name = "NewPlayList";
        [XmlAttribute]
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private bool _isSelectedPlayList = false;
        [XmlAttribute]
        public bool IsSelectedPlayList
        {
            get { return _isSelectedPlayList; }
            set { if(SetProperty(ref _isSelectedPlayList, value) && value) OnPlayListSelectedAction(this); }
        }

        public bool HasSubLists { get { return PlayLists.Count > 0; } }

        public int SelectedMediaFileIndex { get; set; } = 0;

        public ObservableCollection<MediaFileInfo> MediaFiles { get; set; } = new ObservableCollection<MediaFileInfo>();

        public ObservableCollection<PlayList> PlayLists { get; set; } = new ObservableCollection<PlayList>();

        public PlayList GetPlayList(params int[] treePath)
        {
            if (treePath == null || treePath.Length == 0)
                return this;

            int idx = treePath[0];
            if (idx < PlayLists.Count)
            {
                int[] subPath = new int[treePath.Length - 1];
                Array.Copy(treePath, 1, subPath, 0, treePath.Length - 1);

                return PlayLists[idx].GetPlayList(subPath);
            }

            throw new IndexOutOfRangeException("PlayList not found, idx: " + idx);
        }

        public int AddNewMediaFile(string fileName)
        {
            MediaFiles.Insert(0, new MediaFileInfo() { FileName = fileName, MediaState = MediaState.Play });
            SelectedMediaFileIndex = 0;
            return SelectedMediaFileIndex;
        }

        public int DeleteMediaFile(MediaFileInfo item)
        {
            MediaFiles.Remove(item);
            if (MediaFiles.Count > 0)
                SelectedMediaFileIndex = 0;
            else
                SelectedMediaFileIndex = -1;

            return SelectedMediaFileIndex;
        }

        public MediaFileInfo FindFile(string subString)
        {
            foreach (MediaFileInfo item in MediaFiles)
            {
                if (item.FileName.Contains(subString))
                    return item;
            }

            foreach (PlayList list in PlayLists)
            {
                MediaFileInfo info = list.FindFile(subString);
                if (info != null)
                    return info;
            }

            return null;
        }

        public PlayList AddNewPlayList(string name)
        {
            PlayList newList = new PlayList() { Name = name };

            GetRootPlayList().SetSelectedPlayList(newList);

            return newList;
        }

        public PlayList GetRootPlayList()
        {
            PlayList list = this;
            while (list.Owner != null)
                list = list.Owner;
            return list;
        }

        public static PlayList FindSelectedPlayList(PlayList root)
        {
            if (root.IsSelectedPlayList)
                return root;

            for (int i = 0; i < root.PlayLists.Count; i++)
            {
                PlayList list = FindSelectedPlayList(root.PlayLists[i]);
                if (list != null)
                    return list;
            }

            return null;
        }

        public void SetSelectedPlayList(PlayList list2select)
        {
            SetSelectedPlayList(this, list2select);
        }

        public static void SetSelectedPlayList(PlayList root, PlayList list2select)
        {
            for (int i = 0; i < root.PlayLists.Count; i++)
            {
                root.PlayLists[i].IsSelectedPlayList = (object.ReferenceEquals(root.PlayLists[i], list2select));

                SetSelectedPlayList(root.PlayLists[i], list2select);
            }
        }

        public bool DeleteSubList(PlayList list)
        {
            return PlayListOp(this, list, (parent, found, index) =>
            {
                parent.PlayLists.RemoveAt(index);
            });
        }

        public bool PlayListOp(PlayList root, PlayList listToFind, Action<PlayList, PlayList, int> action)
        {
            if (action == null)
                return false;

            for (int i = 0; i < root.PlayLists.Count; i++)
            {
                if (object.ReferenceEquals(root.PlayLists[i], listToFind))
                {
                    action(root, listToFind, i);
                    return true;
                }

                if (PlayListOp(root.PlayLists[i], listToFind, action))
                    return true;
            }

            return false;
        }

        //initialize play list tree
        public void InitPlayListTree(PlayList owner, Action<PlayList> OnPlayListSelected)
        {
            this.Owner = owner;
            this.OnPlayListSelectedAction = OnPlayListSelected;
            foreach (PlayList list in PlayLists)
                list.InitPlayListTree(this, OnPlayListSelected);
        }

        public override string ToString()
        {
            return string.Format(" MediaFiles {0}, SelectedIndex: {1}, PlayLists {2}",
                MediaFiles.Count, SelectedMediaFileIndex, PlayLists.Count);
        }
    }

    public class MediaDatabaseInfo : NotifyPropertyChangedImpl
    {
        [Category("Media Database"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PlayList RootList { get; set; } = new PlayList() { Name = "--ROOT--" };

        [XmlIgnore]
        public Action<PlayList> OnPlayListSelectedAction = (l) => { };

        public PlayList GetSelectedPlayList() 
        {
            PlayList sel = PlayList.FindSelectedPlayList(RootList);
            if (sel != null)
                return sel;

            if (RootList.PlayLists.Count == 0)
                RootList.AddNewPlayList("Default Play List");

            RootList.SetSelectedPlayList(RootList.PlayLists[0]);
            
            return RootList.PlayLists[0];
        }

        public void RemovePlayList(PlayList list)
        {
            RootList.DeleteSubList(list);

            //if no PlayLists left - insert default empty list and mark it as selected
            if (RootList.PlayLists.Count == 0)
            {
                RootList.PlayLists.Add(new PlayList() { IsSelectedPlayList = true });
            }
        }

        public ObservableCollection<MediaFileInfo> GetSelectedMediaFilesList()
        {
            return GetSelectedPlayList().MediaFiles;
        }

        [XmlIgnore]
        public int SelectedMediaFileIndex
        {
            get => GetSelectedPlayList().SelectedMediaFileIndex;
            set { GetSelectedPlayList().SelectedMediaFileIndex = value; NotifyPropertyChanged(); }
        }

        public int AddNewMediaFile(string fileName)
        {
            return GetSelectedPlayList().AddNewMediaFile(fileName);
        }

        public void CopyFrom(MediaDatabaseInfo mediaDatabaseInfo)
        {
            RootList = mediaDatabaseInfo.RootList;

            RootList.InitPlayListTree(null, (playList) => 
            { 
                playList.GetRootPlayList().SetSelectedPlayList(playList);
                OnPlayListSelectedAction(playList);
            });

            if (RootList.PlayLists.Count == 0)
            {
                //if no PlayLists defined/loaded - insert default empty list and mark it as selected
                RootList.PlayLists.Add(new PlayList() { IsSelectedPlayList = true });
            }
        }

        public void AddNewPlayList(string name, PlayList owner)
        {
            if (owner != null)
                owner.AddNewPlayList(name);
            else
                RootList.AddNewPlayList(name);
        }
    }

    public class AppConfig
    {
        private string _dataFolder;
        private string _fileName;

        [Category("Config"), TypeConverter(typeof(ExpandableObjectConverter))]
        public Configuration Configuration { get; set; } = new Configuration();

        [Category("Media Database"), TypeConverter(typeof(ExpandableObjectConverter))]
        public MediaDatabaseInfo MediaDatabaseInfo { get; set; } = new MediaDatabaseInfo();

        public AppConfig()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string commonPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _dataFolder = Path.Combine(commonPath, "MarkZ", assemblyName);
            Directory.CreateDirectory(_dataFolder);

            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("{0}_{1}.xml", assemblyName, "Files");
            _fileName = Path.Combine(_dataFolder, fileName);
        }

        public void CopyFrom(AppConfig config)
        {
            MediaDatabaseInfo.CopyFrom(config.MediaDatabaseInfo);
            Configuration.CopyFrom(config.Configuration);
        }

        public void Save()
        {
            XmlHelper.Save(_fileName, this);
        }

        public void Load()
        {
            if (File.Exists(_fileName))
            {
                AppConfig appConfig = XmlHelper.Open<AppConfig>(_fileName);
                this.CopyFrom(appConfig);
            }
        }
    }
}
