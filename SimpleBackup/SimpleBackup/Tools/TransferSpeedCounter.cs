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
        public int Count { get { return _fileDataList.Count; } }

        private List<FileData> _fileDataList = new List<FileData>(1024); 

        public void AddCount(long addBytes)
        {
            lock (_fileDataList)
            {
                ElapsedTime += _stopper.Elapsed;
                iBytesCount += addBytes;

                _fileDataList.Add(new FileData(addBytes, _stopper.Elapsed.TotalMilliseconds));
            }

            _stopper.Restart();
        }

        public void StartCounting(int startIndex)
        {
            _stopper.Restart();

            if (startIndex == 0 || (startIndex+1) != _fileDataList.Count)
            {
                iBytesCount = 0;
                ElapsedTime = TimeSpan.FromSeconds(0);
                _fileDataList.Clear();
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
            lock (_fileDataList)
            {
                for (int i = _fileDataList.Count - 1; i >= 0; i--)
                {
                    bytes += _fileDataList[i].Length;
                    milliseconds += _fileDataList[i].ElapsedMilliseconds;
                    if (milliseconds > interval.TotalMilliseconds)
                        break;
                }
            }
            return bytes / milliseconds;
        }

        public List<double> SpeedHistory(int total, ref int max)
        {
            List<double> progress = new List<double>();
            lock (_fileDataList)
            {
                if (total <= max)
                {
                    max = total;
                    for (int i = 0; i < _fileDataList.Count; i++)
                    {
                        progress.Add(_fileDataList[i].SpeedKBs); //KB/s
                    }
                    return progress;
                }
                else
                {
                    double countMax = total;
                    double countVal = _fileDataList.Count;

                    double filesPerBucket = countMax / max;

                    double currentFile = filesPerBucket;
                    List<FileData> data = new List<FileData>();

                    FileData currentBucket = new FileData(0, 0);
                    for (int i = 0; i < _fileDataList.Count; i++)
                    {
                        if (i < currentFile)
                        {
                            currentBucket += _fileDataList[i];
                        }
                        else
                        {
                            progress.Add(currentBucket.SpeedKBs);
                            currentBucket = _fileDataList[i];
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
