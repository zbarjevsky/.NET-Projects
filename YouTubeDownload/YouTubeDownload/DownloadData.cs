using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;


using MZ.Tools;

namespace YouTubeDownload
{
    public enum eDownloadState
    {
        None,
        InQueue,
        Working,
        Skipped,
        Succsess,
        Failed,
        Stopped,
    }

    [Serializable]
    public class DownloadData
    {
        public eDownloadState State { get; set; } = eDownloadState.None;
        public string OutputFolder { get; set; } = "";
        public string Description { get; set; } = "";
        public string PlayListDescription { get; set; } = "";
        public string PlayListProgress { get; set; } = "";
        public string FileNameTemplate { get; set; } = "%(title)s-%(id)s.%(ext)s";
        public bool NoPlayList { get; set; } = true;
        public bool AudioOnly { get; set; } = false;
        public string Url { get; set; } = "";
        public string AdditionalParameters { get; set; } = "";
        [XmlIgnore]
        public double Progress { get; set; } = 0;
        [XmlIgnore]
        public Encoding Encoding 
        { 
            get { return Encoding.GetEncoding(EncodingName); } 
            set{ EncodingName = value.WebName; } 
        }
        //Cannot serialize Encoding with generic serialize - use string instead
        public string EncodingName { get; set; } = Encoding.UTF8.WebName;

        public DownloadData Clone()
        {
            return new DownloadData()
            {
                State = State,
                OutputFolder = OutputFolder,
                Description = Description,
                PlayListDescription = PlayListDescription,
                PlayListProgress = PlayListProgress,
                FileNameTemplate = FileNameTemplate,
                NoPlayList = NoPlayList,
                AudioOnly = AudioOnly,
                Url = Url,
                AdditionalParameters = AdditionalParameters,
                Progress = Progress,
                Encoding = Encoding
            };
        }
    }

    [Serializable]
    public class DownloadList
    {
        public static string _outPath;
        public readonly List<DownloadData> Downloads = new List<DownloadData>();

        static DownloadList()
        {
            string commonPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _outPath = Path.Combine(commonPath, "MKZ", "YTD");
            Directory.CreateDirectory(_outPath);
        }

        public string FileName
        {
            get
            {
                return Path.Combine(_outPath, "DownloadsList.xml");
            }
        }

        public static void Save(ListView ctrl)
        {
            if (ctrl.Items.Count == 0)
                return;

            string date = DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
            string fileName = string.Format("{0}_{1}.xml", "DownloadsList", date);
            string fName = Path.Combine(_outPath, fileName);

            DownloadList list = new DownloadList();

            foreach (ListViewItem item in ctrl.Items)
            {
                list.Downloads.Add(item.Tag as DownloadData);
            }
            
            list.Save(fName);
        }

        public bool Load(string fileName)
        {
            try
            {
                DownloadList list = XmlHelper.Open<DownloadList>(fileName);
                Downloads.Clear();
                Downloads.AddRange(list.Downloads);
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show("File: "+ fileName + "\n"+ err.Message, "YouTube Downloader Load", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }        
        }

        public bool Save(string fileName)
        {
            try
            { 
                XmlHelper.Save(fileName, this);
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show("File: " + fileName + "\n" + err.Message, "YouTube Downloader Save",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
