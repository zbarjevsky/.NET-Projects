using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadexOneDemo
{
    public class RadiationLogListView : ListView
    {
        private List<RadiationDataPoint> _radiationLog = new List<RadiationDataPoint>();

        public RadiationLogListView()
        {
            this.DoubleBuffered = true;

            this.VirtualMode = true;
            this.View = View.Details;
            this.GridLines = true;
            this.FullRowSelect = true;
            this.LabelEdit = true;

            ColumnHeader columnHeader1 = new ColumnHeader();
            columnHeader1.Text = "Log";
            columnHeader1.Width = -2;
            this.Columns.Add(columnHeader1);
            this.HeaderStyle = ColumnHeaderStyle.None;

            this.SizeChanged += RadiationLogListView_SizeChanged;
            this.RetrieveVirtualItem += RadiationLogListView_RetrieveVirtualItem;
        }

        private void RadiationLogListView_SizeChanged(object sender, EventArgs e)
        {
            this.Columns[0].Width = this.Width - 30;
        }

        public void UpdateLog(List<RadiationDataPoint> log, bool update = true)
        {
            if (update)
            {
                _radiationLog = new List<RadiationDataPoint>(log);
                this.VirtualListSize = log.Count;
                this.SelectedIndices.Clear();
                if(_radiationLog.Count > 0)
                    this.EnsureVisible(0);
            }
        }

        private void RadiationLogListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if(e.ItemIndex < _radiationLog.Count)
            {
                //reverse list order
                int idx = _radiationLog.Count - e.ItemIndex - 1;
                e.Item = new ListViewItem(_radiationLog[idx].ToString());
            }
        }
    }
}
