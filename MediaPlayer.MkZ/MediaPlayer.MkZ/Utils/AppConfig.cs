﻿using System;
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
using MkZ.WPF.MessageBox;

namespace MkZ.MediaPlayer.Utils
{
    [Serializable]
    public class Configuration : NotifyPropertyChangedImpl
    {
        private string _backgroundImageFileName = "";
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BackgroundImageFileName
        {
            get { return _backgroundImageFileName; }
            set { SetProperty(ref _backgroundImageFileName, value); }
        }

        private bool _showTestControls = true;
        public bool ShowTestControls { get => _showTestControls; set => SetProperty(ref _showTestControls, value); }

        public List<string> SupportedImageExtensions { get; set; }

        public List<string> SupportedAudioExtensions { get; set; }

        public List<string> SupportedVideoExtensions { get; set; }

        private WPF.SimpleClockControl.ClockConfig _clockColors = new WPF.SimpleClockControl.ClockConfig();
        [Category("Clock"), TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("Clock Configuration")]
        public WPF.SimpleClockControl.ClockConfig ClockConfig
        {
            get { return _clockColors; }
            set { SetProperty(ref _clockColors, value); }
        }

        public void EnsureHasValues()
        {
            if (SupportedImageExtensions == null || SupportedImageExtensions.Count == 0)
                SupportedImageExtensions = new List<string>() { ".jpg", ".png", ".bmp", ".gif" };

            if (SupportedAudioExtensions == null || SupportedAudioExtensions.Count == 0)
                SupportedAudioExtensions = new List<string>() { ".mp3", ".wav", ".ogg" };

            if (SupportedVideoExtensions == null || SupportedVideoExtensions.Count == 0)
                SupportedVideoExtensions = new List<string>() { ".mpg", ".mpeg", ".mkv", ".mp4", ".webm" };
        }

        public void CopyFrom(Configuration config)
        {
            config.EnsureHasValues();

            BackgroundImageFileName = config.BackgroundImageFileName;

            ShowTestControls = config.ShowTestControls;
            if(config.ClockConfig.IsValid())
                ClockConfig = config.ClockConfig;

            SupportedImageExtensions = config.SupportedImageExtensions;
            SupportedAudioExtensions = config.SupportedAudioExtensions;
            SupportedVideoExtensions = config.SupportedVideoExtensions;
        }

        public string GetAllSupportedExtensions()
        {
            StringBuilder sb = new StringBuilder();

            SupportedImageExtensions.ForEach((ext) => { sb.Append("*" + ext + ";"); });
            SupportedAudioExtensions.ForEach((ext) => { sb.Append("*" + ext + ";"); });
            SupportedVideoExtensions.ForEach((ext) => { sb.Append("*" + ext + ";"); });

            return sb.ToString();
        }

        public string GetAllSupportedImagesExtensions()
        {
            StringBuilder sb = new StringBuilder();

            SupportedImageExtensions.ForEach((ext) => { sb.Append("*" + ext + ";"); });

            return sb.ToString();
        }

        public bool IsSupportedImageFile(string fileName)
        {
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            foreach (string ext1 in SupportedImageExtensions)
            {
                if (ext1 == ext) 
                    return true;
            } 
            return false;
        }

        public bool IsSupportedAudioFile(string fileName)
        {
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            foreach (string ext1 in SupportedAudioExtensions)
            {
                if (ext1 == ext)
                    return true;
            }
            return false;
        }

        public bool IsSupportedVideoFile(string fileName)
        {
            if (VideoPlayerContext.Instance.InDesignMode)
                return false;

            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            foreach (string ext1 in SupportedVideoExtensions)
            {
                if (ext1 == ext)
                    return true;
            }
            return false;
        }

        public bool IsSupportedMediaFile(string fileName)
        {
            return IsSupportedAudioFile(fileName) || IsSupportedVideoFile(fileName);
        }

        public override string ToString()
        {
            return string.Format("Configuration - Ext: {0}", GetAllSupportedExtensions());
        }
    }

    //directory tree style play list
    public enum ePlayMode
    {
        [Description("Play One")]
        PlayOne,
        [Description("Play All")]
        PlayAll,
        [Description("Repeat One")]
        RepeatOne,
        [Description("Repeat All")]
        RepeatAll,
        [Description("Shuffle")]
        Random
    }

    public class PlayList : NotifyPropertyChangedImpl
    {
        [XmlIgnore]
        public bool IsRoot => Owner == null;

        [XmlIgnore]
        public PlayList Owner { get; private set; }

        [XmlIgnore]
        public Action<PlayList> OnPlayListSelectionChangedAction = (list) => { };

        private string _name = "NewPlayList";
        [XmlAttribute]
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private bool _isSelectedPlayList = false;
        [XmlAttribute]
        public bool IsSelectedPlayList
        {
            get { return _isSelectedPlayList; }
            set 
            { 
                if(SetProperty(ref _isSelectedPlayList, value)) 
                    OnPlayListSelectionChangedAction(this); 
            }
        }

        private ePlayMode _playMode = ePlayMode.PlayOne;
        public ePlayMode PlayMode { get => _playMode; set => SetProperty(ref _playMode, value); }

        public bool HasSubLists { get { return PlayLists.Count > 0; } }

        public int SelectedMediaFileIndex { get; set; } = 0;

        public ObservableRangeCollection<MediaFileInfo> MediaFiles { get; set; } = new ObservableRangeCollection<MediaFileInfo>();

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

        public int AddNewMediaFile(string fileName, double volume)
        {
            MediaFileInfo info = MediaFiles.FirstOrDefault((file) => file.FileName == fileName);
            if (info == null)
            {
                MediaFiles.Insert(0, new MediaFileInfo()
                {
                    FileName = fileName,
                    MediaState = MediaState.Play,
                    Volume = volume
                });
                SelectedMediaFileIndex = 0;
            }
            else //file exists
            {
                SelectedMediaFileIndex = MediaFiles.IndexOf(info);
            }

            return SelectedMediaFileIndex;
        }

        public int RemoveMediaFileFromList(MediaFileInfo item)
        {
            int idx = MediaFiles.IndexOf(item);
            if (idx >= 0)
            {
                MediaFiles.RemoveAt(idx);
                if (idx >= MediaFiles.Count)
                    idx = MediaFiles.Count - 1; //last or -1
            }
            return idx;
        }

        public int SelectNextMediaFile(int indexFrom)
        {
            if (MediaFiles.Count == 0)
                return SelectedMediaFileIndex = -1;

            indexFrom++;
            if (indexFrom >= MediaFiles.Count)
                indexFrom = 0;

            return SelectedMediaFileIndex = indexFrom;
        }

        public int SelectPreviousMediaFile(int indexFrom)
        {
            if (MediaFiles.Count == 0)
                return SelectedMediaFileIndex = -1;

            indexFrom--;
            if (indexFrom >= MediaFiles.Count)
                indexFrom = 0;
            if (indexFrom < 0)
                indexFrom = MediaFiles.Count - 1; //last

            return SelectedMediaFileIndex = indexFrom;
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

        public PlayList FindPlayListContainingFile(string subString)
        {
            foreach (MediaFileInfo item in MediaFiles)
            {
                if (item.FileName.Contains(subString))
                    return this;
            }

            foreach (PlayList list in PlayLists)
            {
                PlayList info = list.FindPlayListContainingFile(subString);
                if (info != null)
                    return info;
            }

            return null;
        }

        public PlayList AddNewPlayList(string name)
        {
            PlayList newList = new PlayList() 
            { 
                Name = name, 
                Owner = this, 
                OnPlayListSelectionChangedAction = OnPlayListSelectionChangedAction, 
                IsSelectedPlayList = true 
            };
            
            PlayLists.Add(newList);
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
        public void InitPlayListTree(PlayList owner, Action<PlayList> OnPlayListSelectionChanged)
        {
            this.Owner = owner;
            this.OnPlayListSelectionChangedAction = OnPlayListSelectionChanged;
            foreach (PlayList list in PlayLists)
                list.InitPlayListTree(this, OnPlayListSelectionChanged);
        }

        public override string ToString()
        {
            return string.Format("PlayList: '{0}', MediaFiles: {1}, SelectedIndex: {2}, PlayLists: {3}",
                Name, MediaFiles.Count, SelectedMediaFileIndex, PlayLists.Count);
        }
    }

    public class MediaDatabaseInfo : NotifyPropertyChangedImpl
    {
        [Category("Media Database"), TypeConverter(typeof(ExpandableObjectConverter))]
        public PlayList RootList { get; set; } = new PlayList() { Name = "--ROOT--" };

        [XmlIgnore]
        public Action<PlayList> OnPlayListSelectionChangedAction = (list) => { };

        [XmlIgnore]
        public PlayList SelectedPlayList 
        {
            get
            {
                PlayList sel = PlayList.FindSelectedPlayList(RootList);
                if (sel != null)
                    return sel;

                if (RootList.PlayLists.Count == 0)
                    RootList.AddNewPlayList("Default Play List");

                RootList.SetSelectedPlayList(RootList.PlayLists[0]);

                return RootList.PlayLists[0];
            }
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
            return SelectedPlayList.MediaFiles;
        }

        [XmlIgnore]
        public int SelectedMediaFileIndex
        {
            get => SelectedPlayList.SelectedMediaFileIndex;
            set { SelectedPlayList.SelectedMediaFileIndex = value; NotifyPropertyChanged(); }
        }

        public int AddNewMediaFile(string fileName, double volume)
        {
            return SelectedPlayList.AddNewMediaFile(fileName, volume);
        }

        public void CopyFrom(MediaDatabaseInfo mediaDatabaseInfo)
        {
            RootList = mediaDatabaseInfo.RootList;
            RootList.Name = "--ROOT--";

            RootList.InitPlayListTree(null, OnPlayListSelectionChanged: (playList) => 
            {
                if (playList != null && playList.IsSelectedPlayList)
                {
                    playList.GetRootPlayList().SetSelectedPlayList(playList);
                    OnPlayListSelectionChangedAction(playList);
                }
                else //check if has no selection - set first selected
                {
                    var list = SelectedPlayList;
                }
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

            string debug = "";
#if DEBUG
            debug = "_debug";
#endif
            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("{0}_{1}{2}.xml", assemblyName, "Files", debug);
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
                try
                {
                    AppConfig appConfig = XmlHelper.Open<AppConfig>(_fileName);
                    this.CopyFrom(appConfig);
                }
                catch (Exception err)
                {
                    PopUp.Error(err.ToString(), "Cannot load Settings From File");
                }            
            }
            this.Configuration.EnsureHasValues();
        }
    }
}