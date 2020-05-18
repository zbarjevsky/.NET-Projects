using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZ.Tools
{
    public static class FileUtils
    {
        public class FileProgress
        {
            [Flags]
            public enum ReportOptions
            {
                ReportValueChange = 1,
                ReportPercentChange = 2,
                ReportAll = ReportValueChange | ReportPercentChange
            }

            private Stopwatch _stopper = new Stopwatch();

            public Action OnValueChange = () => { };
            public Action<int> OnPercentChange = (percent) => { };

            public ReportOptions ReportOption = FileProgress.ReportOptions.ReportAll;

            public string Message { get; set; } = "";

            private long _max = 100;
            public long Max { get { return _max; } set { Validate(_min, value, _val); _max = value; } }
            
            private long _min = 100;
            public long Min { get { return _min; } set { Validate(value, _max, _val); _min = value; } }

            private long _val = 0;
            public long Val 
            { 
                get { return _val; } 
                set 
                {
                    if (_val == value)
                        return;

                    int prevPercent = Percent;
                    
                    _val = value;

                    //report percent only if changed
                    if(ReportOption.HasFlag(ReportOptions.ReportPercentChange))
                        if (prevPercent != Percent)
                            OnPercentChange(Percent);

                    if(ReportOption.HasFlag(ReportOptions.ReportValueChange))
                        OnValueChange();
                } 
            }

            public int Percent { get { return (int)(100 * (_val - Min) / (Max - Min)); } }

            public TimeSpan Elapsed { get { return _stopper.Elapsed; } }

            public volatile bool IsCancel = false;

            public void Reset(string message = "Progress: ", 
                long max = 100, long min = 0, 
                ReportOptions options = ReportOptions.ReportAll )
            {
                Val = 0;
                Min = min;
                Max = max;
                ReportOption = options;
                Message = message;
                IsCancel = false;

                _stopper.Restart();
            }

            public override string ToString()
            {
                return string.Format(" {0}{1}% - {2:###,##0} - Elapsed: {3}", 
                    Message, Percent, Val, Elapsed.ToString("mm':'ss'.'f"));

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
        public static IEnumerable<string> GetFiles(string root, string searchPattern, FileProgress progress = null, SearchOption option = SearchOption.AllDirectories)
        {
            Stack<string> pending = new Stack<string>();
            pending.Push(root);

            int totalFoldersCount = 1; //root folder
            while (pending.Count != 0)
            {
                if(progress != null)
                {
                    if (progress.IsCancel)
                        break;
                    progress.Val++;
                }

                string path = pending.Pop();
                string[] next = null;
                try
                {
                    next = Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
                }
                catch (Exception e) { Debug.WriteLine("Get Files Path: {0} - Error: {1}", path, e); }
                
                if (next != null && next.Length != 0)
                    foreach (string file in next) 
                        yield return file;
                
                try
                {
                    if (option == SearchOption.AllDirectories)
                    {
                        string [] folders = Directory.GetDirectories(path);
                        foreach (string subdir in folders)
                            pending.Push(subdir);

                        totalFoldersCount += folders.Length;
                        
                        if (progress != null)
                            progress.Max = totalFoldersCount;
                    }
                }
                catch (Exception e) { Debug.WriteLine("Get Folders Path: {0} - Error: {1}", path, e); }
            }
        }
    }
}
