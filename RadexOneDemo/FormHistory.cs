using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RadexOneDemo
{
    public partial class FormHistory : Form
    {
        private ChartPoint[] _history;

        public FormHistory(List<ChartPoint> history)
        {
            _history = new ChartPoint[history.Count];
            history.CopyTo(_history);

            InitializeComponent();

            m_btnStop1.Enabled = false;
        }

        private void FormHistory_Load(object sender, EventArgs e)
        {
            ChartPoint[] history = CompactHistory(_history);
            LoadHistory(history);
        }

        private void LoadHistory(ChartPoint[] history)
        { 
            const int maxHistory = 1000000;

            toolStripProgressBar1.Value = 0;
            this.Visible = true;

            _cancel = false;
            m_btnStop1.Enabled = true;
            m_chart1.SuspendLayout();

            m_chart1.Series[0].Points.Clear();
            m_chart1.Series[1].Points.Clear();

            int i;
            for (i = 0; i < history.Length && !_cancel; i++)
            {
                ChartHelper.AddPointXY(m_chart1, "SeriesCPM", history[i].CPM, history[i].date, maxHistory);
                ChartHelper.AddPointXY(m_chart1, "SeriesDOSE", history[i].RATE, history[i].date, maxHistory);

                if (history.Length<1000 || i % 100 == 0)
                {
                    m_status1.Text = "Loading " + i + " of " + history.Length;
                    toolStripProgressBar1.Value = 100 * i / history.Length;
                    Application.DoEvents();
                }
            }

            m_status1.Text = Status(":");

            m_chart1.ResumeLayout();
            toolStripProgressBar1.Value = 0;
            m_btnStop1.Enabled = false;
        }

        private string Status(string sep)
        {
            string dateFmt = string.Format("yyyy-MM-dd HH{0}mm{0}ss", sep);
            return string.Format("Total{0} {1}, From{2} {3}, To{4} {5}", sep, _history.Length, 
                sep, _history.First().date.ToString(dateFmt), sep, _history.Last().date.ToString(dateFmt));
        }

        //Dillute - create one point per minute
        private static ChartPoint[] CompactHistory(ChartPoint[] input)
        {
            const int max = 5000;
            if (input.Length < max)
                return input;

            List<ChartPoint> history = new List<ChartPoint>();

            ChartPoint pt = new ChartPoint();
            List<ChartPoint> bucket = new List<ChartPoint>();

            TimeSpan range = input.Last().date - input.First().date;
            TimeSpan delta = TimeSpan.FromMinutes(range.TotalMinutes / max); //ensure has max buckets

            pt.date = input.First().date;
            for (int i = 0; i < input.Length; i++)
            {
                if (!IsInTimeInterval(input[i].date, pt.date, delta))
                {
                    if (bucket.Count > 0)
                    {
                        history.Add(MaxCPM_Point(bucket));
                        bucket.Clear();
                    }

                    pt = input[i];
                }

                bucket.Add(input[i]);
            }

            if (bucket.Count > 0) //last one
                history.Add(MaxCPM_Point(bucket));

            return history.ToArray();
        }

        private static ChartPoint MaxCPM_Point(List<ChartPoint> history)
        {
            ChartPoint pt = new ChartPoint();
            foreach (ChartPoint point in history)
            {
                if(point.CPM>pt.CPM)
                    pt = point;
            }

            return pt;
        }

        //private ChartPoint AveragePoint(List<ChartPoint> history)
        //{
        //    ChartPoint pt = new ChartPoint();
        //    foreach (ChartPoint point in history)
        //    {
        //        pt.date = point.date;
        //        pt.CPM += point.CPM;
        //        pt.DOSE += point.DOSE;
        //    }
        //    pt.CPM /= history.Count;
        //    pt.DOSE /= history.Count;

        //    return pt;
        //}

        private static bool IsInTimeInterval(DateTime t1, DateTime t2, TimeSpan delta)
        {
            return (t1-t2) <= delta;
        }

        private bool _cancel = false;
        private void m_btnStop1_Click(object sender, EventArgs e)
        {
            _cancel = true;
        }

        private void m_btnOpen_Click(object sender, EventArgs e)
        {
            if (m_openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            m_btnOpen.Enabled = false;
            Open(m_openFileDialog.FileName);
            m_btnOpen.Enabled = true;
        }

        private void m_btnSave_Click(object sender, EventArgs e)
        {
            m_saveFileDialog.FileName = Status("-") + ".hist";
            if ( m_saveFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            m_btnSave.Enabled = false;
            Save(m_saveFileDialog.FileName);
            m_btnSave.Enabled = true;
        }

        private void Open(string fileName)
        {
            Cursor = Cursors.WaitCursor;
            _history = XmlHelper.Open<ChartPoint[]>(fileName);
            ChartPoint[] history = CompactHistory(_history);
            LoadHistory(history);
            Cursor = Cursors.Arrow;
        }

        public void Save(string fileName)
        {
            Cursor = Cursors.WaitCursor;
            XmlHelper.Save(fileName, _history);
            Cursor = Cursors.Arrow;
        }
    }

    public class XmlHelper
    {
        public static T Open<T>(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                return (T)serializer.Deserialize(streamReader);
            }
        }

        public static void Save<T>(string fileName, T o)
        {
            using (StreamWriter streamReader = new StreamWriter(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(streamReader, o);
            }
        }
    }
}
