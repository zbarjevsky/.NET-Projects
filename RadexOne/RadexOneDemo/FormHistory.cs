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
        private List<RadiationDataPoint> _historyCached;
        private RadiationLog _historyRef; //reference to Main
        //private List<ChartPoint> _historyBuf; //currently displayed buffer

        public FormHistory(RadiationLog history)
        {
            _historyRef = history;

            InitializeComponent();

            m_btnStop1.Enabled = false;
        }

        private void FormHistory_Load(object sender, EventArgs e)
        {
            m_btnReload_Click(sender, e);
        }

        private void m_btnReload_Click(object sender, EventArgs e)
        {
            _historyCached = new List<RadiationDataPoint>(_historyRef.Log);

            m_chart1.ClearChart();
            m_trackBarZoom.Value = 0;
            if (_historyCached.Count > 0)
            {
                m_numMaxCPM.Value = (decimal)_historyCached.Last().Threshold;

                //calculate zoom value to show whole history
                m_trackBarZoom.Value = (int)Math.Ceiling(Math.Log(1 + _historyCached.Count / m_chart1.GraphWidth, 2));
            }

            m_trackBarZoom_ValueChanged(this, null);
        }

        private void LoadBuffer(List<RadiationDataPoint> history, bool scrollToLastBuffer)
        { 
            toolStripProgressBar1.Value = 0;
            this.Visible = true;

            _cancel = false;
            m_btnStop1.Enabled = true;

            m_chart1.Set(history, scrollToLastBuffer, true);

            UpdateLogText(history);

            m_status1.Text = Status(":");

            toolStripProgressBar1.Value = 0;
            m_btnStop1.Enabled = false;
        }

        private void UpdateLogText(List<RadiationDataPoint> history)
        {
            m_listLog.UpdateLog(history);
        }

        private string Status(string sep)
        {
            string dateFmt = string.Format("yyyy-MM-dd HH{0}mm{0}ss", sep);
            return string.Format("Total{0} {1}, From{2} {3}, To{4} {5}", sep, _historyCached.Count, 
                sep, _historyCached.First().date.ToString(dateFmt), sep, _historyCached.Last().date.ToString(dateFmt));
        }

        //Dillute - create one point per minute
        private static List<RadiationDataPoint> CompactHistoryByTime(List<RadiationDataPoint> input, int maxPoints)
        {
            if (input.Count <= maxPoints)
                return input;

            List<RadiationDataPoint> history = new List<RadiationDataPoint>();

            RadiationDataPoint pt = new RadiationDataPoint();
            List<RadiationDataPoint> bucket = new List<RadiationDataPoint>();

            TimeSpan range = input.Last().date - input.First().date;
            TimeSpan delta = TimeSpan.FromMinutes(range.TotalMinutes / maxPoints); //ensure has max buckets

            pt.date = input.First().date;
            for (int i = 0; i < input.Count; i++)
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

            return history;
        }

        //Dillute - create one point per minute
        private static List<RadiationDataPoint> CompactHistoryByPoints(List<RadiationDataPoint> input, int divider)
        {
            int maxPoints = (int)(input.Count / (double)divider);
            if (input.Count <= maxPoints)
                return input;

            List<RadiationDataPoint> history = new List<RadiationDataPoint>(maxPoints);

            List<RadiationDataPoint> bucket = new List<RadiationDataPoint>();

            for (int i = 0; i < input.Count; i++)
            {
                if (bucket.Count >= divider)
                {
                    if (bucket.Count > 0)
                    {
                        history.Add(MaxCPM_Point(bucket));
                        bucket.Clear();
                    }
                }

                bucket.Add(input[i]);
            }

            if (bucket.Count > 0) //last one
                history.Add(MaxCPM_Point(bucket));

            return history;
        }

        private static RadiationDataPoint MaxCPM_Point(List<RadiationDataPoint> history)
        {
            RadiationDataPoint pt = new RadiationDataPoint();
            foreach (RadiationDataPoint point in history)
            {
                if(point.CPM>pt.CPM)
                    pt = point;
            }

            return pt;
        }

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
            _historyRef = XmlHelper.Open<RadiationLog>(fileName);
            m_btnReload_Click(this, null);
            Cursor = Cursors.Arrow;
        }

        public void Save(string fileName)
        {
            Cursor = Cursors.WaitCursor;
            XmlHelper.Save(fileName, _historyRef);
            Cursor = Cursors.Arrow;
        }

        private void m_numMaxCPM_ValueChanged(object sender, EventArgs e)
        {
            m_chart1.UpdateThreshold((double)m_numMaxCPM.Value);
        }

        private void m_trackBarZoom_ValueChanged(object sender, EventArgs e)
        {
            int zoom = (int)Math.Pow(2, m_trackBarZoom.Value);
            m_lblZoom.Text = "Zoom Out: x" + zoom;

            List<RadiationDataPoint> history = CompactHistoryByPoints(_historyCached, zoom);
            LoadBuffer(history, false);
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
