using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskCryptorHelper
{
    public class RecentFilesList
    {
        List<string> _files = new List<string>(5);

        public RecentFilesList()
        {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.VHD_FileNames))
            {
                string[] names = Properties.Settings.Default.VHD_FileNames.Trim(';').Split(';');
                _files.AddRange(names);
            }
        }

        public void AddRecent(string fileName, ToolStripDropDown menu, ComboBox cmb)
        {
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
                return;

            if (_files.Contains(fileName))
                _files.Remove(fileName);

            _files.Insert(0, fileName);

            if (_files.Count > 10)
                _files.RemoveAt(_files.Count - 1);

            Update(menu, cmb);
        }

        public void RemoveFromList(string fileName, ToolStripDropDown menu, ComboBox cmb)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            if (_files.Contains(fileName))
                _files.Remove(fileName);

            Update(menu, cmb);
        }

        public void Update(ToolStripDropDown menu, ComboBox cmb)
        {
            UpdateComboBox(cmb);
            UpdateMenu(menu, cmb);
            Save();
        }

        private void UpdateMenu(ToolStripDropDown menu, ComboBox cmb)
        {
            menu.Items.Clear();
            foreach (string fileName in _files)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(fileName);
                item.Click += (s, e) => { cmb.Text = item.Text; SingleInstanceHelper.GlobalShowWindow(FormMain.TITLE); };
                menu.Items.Add(item);
            }
        }

        private ToolStripItem Find(ToolStripItemCollection items, string fileName)
        {
            foreach (ToolStripMenuItem item in items)
            {
                if (item.Text == fileName)
                    return item;
            }
            return null;
        }

        private void UpdateComboBox(ComboBox cmb)
        {
            cmb.Items.Clear();
            cmb.Text = "";
            if (_files.Count == 0)
                return;

            foreach (string fileName in _files)
                cmb.Items.Add(fileName);

            cmb.Text = _files[0];
        }

        public void Save()
        {
            Properties.Settings.Default.VHD_FileNames = "";
            foreach (string file in _files)
                Properties.Settings.Default.VHD_FileNames += file + ";";

            Properties.Settings.Default.Save();
        }
    }
}
