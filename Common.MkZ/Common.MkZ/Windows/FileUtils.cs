using MZ.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MZ.Tools
{
    [Flags]
    public enum NotifyOptions
    {
        NotifyValueChange = 1,
        NotifyPercentChange = 2
    }

    public static class FileUtils
    {
        public class FileProgress
        {
            private Stopwatch _stopper = new Stopwatch();

            private Action OnValueChange = () => { };
            private Action OnPercentChange = () => { };
            public Action<string> OnChange = (status) => { };

            public NotifyOptions ReportOption = NotifyOptions.NotifyPercentChange;

            public string Message { get; set; } = "";
            public string SubStatus { get; set; } = "";

            private long _max = 100;
            public long Maximum { get { return _max; } set { Validate(_min, value, _val); _max = value; } }
            
            private long _min = 100;
            public long Minimum { get { return _min; } set { Validate(value, _max, _val); _min = value; } }

            //circular progress
            //set it to 
            private long _marqueeElapsed = 0;
            private long _marqueeTimeoutMs = 0;
            public long MarqueeTimeoutMs { get { return _marqueeTimeoutMs; } set { _marqueeTimeoutMs = value; } }

            private long _val = 0;
            public long Value 
            { 
                get { return _val; } 
                set 
                {
                    if(Style == ProgressBarStyle.Marquee)
                    {
                        if (ReportOption.HasFlag(NotifyOptions.NotifyValueChange))
                        {
                            long delta = (long)Elapsed.TotalMilliseconds - _marqueeElapsed;
                            if (delta > _marqueeTimeoutMs)
                            {
                                _marqueeElapsed = 0;
                                OnValueChange();
                            }
                        }
                    }
                    else
                    {
                        int prevPercent = Percent;
                        if (_val == value)
                            return;
                        _val = value;

                        //report percent only if changed
                        if (ReportOption.HasFlag(NotifyOptions.NotifyPercentChange))
                            if (prevPercent != Percent)
                                OnPercentChange();

                        if (ReportOption.HasFlag(NotifyOptions.NotifyValueChange))
                            OnValueChange();
                    }
                } 
            }

            public int Percent { get { return (int)(100 * (_val - Minimum) / (Maximum - Minimum + 1)); } }

            public ProgressBarStyle Style { get; set; } = ProgressBarStyle.Blocks;

            public TimeSpan Elapsed { get { return _stopper.Elapsed; } }

            private volatile bool _cancel = false;
            public bool Cancel
            {
                get { return _cancel; }
                set
                {
                    _cancel = value;
                    _val = 0;
                    _marqueeTimeoutMs = 0;
                }
            }

            private readonly ColorBarsProgressBar _ctrlProgress;
            private readonly Form _formOwner;

            public FileProgress(ColorBarsProgressBar ctrlProgress, Form owner)
            {
                _formOwner = owner;
                _ctrlProgress = ctrlProgress;

                this.OnPercentChange = () =>
                {
                    CommonUtils.ExecuteOnUIThread(() =>
                    {
                        _ctrlProgress.Style = Style;
                        _ctrlProgress.Value = Percent;
                        OnChange(this.ToString());
                    }, _formOwner);
                    Application.DoEvents();
                };

                this.OnValueChange = () =>
                {
                    CommonUtils.ExecuteOnUIThread(() =>
                    {
                        _ctrlProgress.Style = Style;
                        if (Style == ProgressBarStyle.Marquee)
                            _ctrlProgress.MarqueeNext();
                        else
                            _ctrlProgress.Value = Percent;
                        OnChange(this.ToString());
                    }, _formOwner);
                    Application.DoEvents();
                };
            }

            public void ResetToBlocks(string message = "Progress: ",
                long max = 100, long min = 0,
                NotifyOptions options = NotifyOptions.NotifyPercentChange)
            {
                Reset(message, max, 0, ProgressBarStyle.Blocks, options);
            }

            public void ResetToMarquee(string message = "Progress: ", long marqueeTimeoutMs = 256)
            {
                MarqueeTimeoutMs = marqueeTimeoutMs;
                Reset(message, 100, 0, ProgressBarStyle.Marquee, NotifyOptions.NotifyValueChange);
            }

            private void Reset(string message, long max, long min, ProgressBarStyle style, NotifyOptions options)
            {
                Validate(min, max, 0);

                _val = 0;
                _marqueeTimeoutMs = 0;
                _min = min;
                _max = max;

                ReportOption = options;
                Message = message;
                Cancel = false;
                Style = style;

                CommonUtils.ExecuteOnUIThread(() =>
                {
                    _ctrlProgress.Maximum = 100;
                    _ctrlProgress.Minimum = 0;
                    _ctrlProgress.Value = 0;
                    _ctrlProgress.Style = style;
                }, _formOwner);

                _stopper.Restart();
            }

            public override string ToString()
            {
                if(Style == ProgressBarStyle.Marquee)
                {
                    return string.Format("{0} {1} - Elapsed: {2}",
                        Message, SubStatus, Elapsed.ToString("mm':'ss'.'f"));
                }
                else
                {
                    return string.Format("{0} Progress: {1:#0}% - {2:###,##0} - Elapsed: {3}",
                        Message, Percent, Value, Elapsed.ToString("mm':'ss'.'f"));
                }
            }

            private void Validate(long min, long max, long val)
            {
                if (min >= max)
                    throw new ArgumentOutOfRangeException("invalid min/max parameter");
                if (val < min || val > max)
                    throw new ArgumentOutOfRangeException("invalid value");
            }
        }

        //https://stackoverflow.com/questions/4986293/access-to-the-path-is-denied-when-using-directory-getfiles
        //
        public static List<string> GetFiles(string root, string searchPattern, 
            SearchOption option = SearchOption.AllDirectories, FileProgress fileProgress = null)
        {
            Stack<string> pending = new Stack<string>();
            pending.Push(root);

            List<string> listFiles = new List<string>();

            int totalFoldersCount = 1; //root folder
            while (pending.Count != 0)
            {
                string path = pending.Pop();

                if (fileProgress != null)
                {
                    fileProgress.Value++;
                    if (fileProgress.Cancel)
                    {
                        listFiles.Clear();
                        break;
                    }
                    fileProgress.SubStatus = string.Format("Folders: ? {0:###,##0}, Files: ? {1:###,##0}", totalFoldersCount, listFiles.Count);
                }

                string[] fileList = null;
                try
                {
                    fileList = Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
                }
                catch (Exception e) { Debug.WriteLine("Get Files Path: {0} - Error: {1}", path, e); }

                if (fileList != null && fileList.Length != 0)
                {
                    listFiles.AddRange(fileList);
                    //foreach (string file in fileList)
                    //    yield return file;
                }
                
                try
                {
                    if (option == SearchOption.AllDirectories)
                    {
                        string [] folders = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
                        foreach (string subdir in folders)
                            pending.Push(subdir);

                        totalFoldersCount += folders.Length;
                    }
                }
                catch (Exception e) { Debug.WriteLine("Get Folders Path: {0} - Error: {1}", path, e); }
            }

            return listFiles;
        }

        public static void CopyFileWithSystemProgressDialog(string sourceFileName, string destinationFileName)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(sourceFileName, destinationFileName,
                Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
        }

        public static void CopyDirectoryWithSystemProgressDialog(string sourceDirectoryName, string destinationDirectoryName)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceDirectoryName, destinationDirectoryName,
                Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
        }

        public static void MoveDirectoryWithSystemProgressDialog(string sourceDirectoryName, string destinationDirectoryName)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(sourceDirectoryName, destinationDirectoryName,
                Microsoft.VisualBasic.FileIO.UIOption.AllDialogs);
        }

        public static void RenameDirectoryWithSystemProgressDialog(string sourceDirectoryName, string destinationDirectoryName)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(sourceDirectoryName, destinationDirectoryName);
        }

        public static void DeleteDirectoryWithSystemProgressDialog(string directoryName)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(directoryName, 
                Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }
    }
}
