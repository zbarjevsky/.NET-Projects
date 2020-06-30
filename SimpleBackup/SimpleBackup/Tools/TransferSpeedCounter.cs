using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleBackup.Tools
{
    public class TransferSpeedCounter
    {
        public class FileData
        {
            public long Length = 0;
            public double  ElapsedMilliseconds = 0;

            public FileData(long length, double elapsed)
            {
                Length = length;
                ElapsedMilliseconds = elapsed;
            }

            public double SpeedKBs { get { return Length / ElapsedMilliseconds; } }

            public static FileData operator +(FileData d1, FileData d2)
            {
                return new FileData(d1.Length + d2.Length, d1.ElapsedMilliseconds + d2.ElapsedMilliseconds);
            }

            public static FileData operator +(FileData d1)
            {
                return d1;
            }
        }

        private Stopwatch _stopper = new Stopwatch();

        public long iBytesCount { get; set; } = 0;
        public TimeSpan ElapsedTime { get; set; } = TimeSpan.FromSeconds(0);
        public int Count { get { return _fileDataListMax.Count; } }

        private List<FileData> _fileDataListMax = new List<FileData>(1024);
        private List<FileData> _fileDataList100 = new List<FileData>(1024);

        public void AddCount(long addBytes, int currentIndex, int total)
        {
            lock (_fileDataListMax)
            {
                ElapsedTime += _stopper.Elapsed;
                iBytesCount += addBytes;

                FileData data = new FileData(addBytes, _stopper.Elapsed.TotalMilliseconds);
                _fileDataListMax.Add(data);

                int i = 100 * currentIndex / total;
                _fileDataList100[i] += data;
            }

            _stopper.Restart();
        }

        public void InitCounting(int startIndex, int total)
        {
            _stopper.Restart();

            if (startIndex == 0)
            {
                iBytesCount = 0;
                ElapsedTime = TimeSpan.FromSeconds(0);

                _fileDataListMax.Clear();
                _fileDataList100.Clear();
                
                for (int i = 0; i < 100; i++)
                {
                    _fileDataList100.Add(new FileData(0, 0));
                }
            }
        }

        public void StopCounting()
        {
            _stopper.Stop();
        }

        public TimeSpan EstimatedTotal(ProgressBar progress)
        {
            return EstimatedTotal(progress.Value, progress.Maximum, progress.Minimum);
        }

        public TimeSpan EstimatedTotal(long val, long max, long min)
        {
            if (val < min)
                throw new ArgumentOutOfRangeException("val");

            if (val == min) //not started yet
                return TimeSpan.FromDays(0);

            return TimeSpan.FromMilliseconds((max-min) * ElapsedTime.TotalMilliseconds / ((val - min) + 1));
        }

        public double SpeedNowKB()
        {
            return SpeedInervalKB(TimeSpan.FromSeconds(0));
        }

        public double SpeedAvgKB()
        {
            return SpeedInervalKB(ElapsedTime);
        }

        public double SpeedInervalKB(TimeSpan interval)
        {
            long bytes = 0;
            double milliseconds = 0;
            lock (_fileDataListMax)
            {
                for (int i = _fileDataListMax.Count - 1; i >= 0; i--)
                {
                    bytes += _fileDataListMax[i].Length;
                    milliseconds += _fileDataListMax[i].ElapsedMilliseconds;
                    if (milliseconds > interval.TotalMilliseconds)
                        break;
                }
            }
            return bytes / milliseconds;
        }

        public List<double> SpeedHistory(int total, ref int max)
        {
            List<double> progress = new List<double>();
            lock (_fileDataListMax)
            {
                if (total <= max)
                {
                    max = total;
                    for (int i = 0; i < _fileDataListMax.Count; i++)
                    {
                        progress.Add(_fileDataListMax[i].SpeedKBs); //KB/s
                    }
                    return progress;
                }
                else
                {
                    double countMax = total;
                    double countVal = _fileDataListMax.Count;

                    double filesPerBucket = countMax / max;

                    double currentFile = filesPerBucket;
                    List<FileData> data = new List<FileData>();

                    FileData currentBucket = new FileData(0, 0);
                    for (int i = 0; i < _fileDataListMax.Count; i++)
                    {
                        if (i < currentFile)
                        {
                            currentBucket += _fileDataListMax[i];
                        }
                        else
                        {
                            progress.Add(currentBucket.SpeedKBs);
                            currentBucket = _fileDataListMax[i];
                            currentFile += filesPerBucket;
                        }
                    }

                    if (currentBucket.SpeedKBs > 0)
                        progress.Add(currentBucket.SpeedKBs);
                }

                return progress;
            }
        }
    }
}
