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
        }

        private Stopwatch _stopper = new Stopwatch();

        public long iBytesCount { get; set; } = 0;
        public TimeSpan ElapsedTime { get; set; } = TimeSpan.FromSeconds(0);

        private List<FileData> fileDataList = new List<FileData>(1024); 

        public void AddCount(long addBytes)
        {
            ElapsedTime += _stopper.Elapsed;
            iBytesCount += addBytes;

            fileDataList.Add(new FileData(addBytes, _stopper.Elapsed.TotalMilliseconds));

            _stopper.Restart();
        }

        public void StartCounting(int startIndex)
        {
            _stopper.Restart();

            if (startIndex == 0 || startIndex != fileDataList.Count)
            {
                iBytesCount = 0;
                ElapsedTime = TimeSpan.FromSeconds(0);
                fileDataList.Clear();
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
            for (int i = fileDataList.Count - 1; i >= 0; i--)
            {
                bytes += fileDataList[i].Length;
                milliseconds += fileDataList[i].ElapsedMilliseconds;
                if (milliseconds > interval.TotalMilliseconds)
                    break;
            }
            return bytes / milliseconds;
        }
    }
}
