using MkZ.Tools;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Media;
using System.Reflection;
using System.Windows.Forms;

namespace KeyClickSound
{
    public partial class FormMain : Form
    {
        string _settingsFileName = "KeySoundSettings.xml";
        public Settings Settings = new Settings();
        private Dictionary<string, SoundPlayer> _players = new Dictionary<string, SoundPlayer>();
        ConcurrentQueue<int> _keysQueue = new ConcurrentQueue<int>();
        bool _isWorking = false;
        private int _lastKeyCode = 0;

        public FormMain()
        {
            InitializeComponent();

            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var assemblyPath = Assembly.GetExecutingAssembly().Location;

            _settingsFileName = Path.Combine(Path.GetDirectoryName(assemblyPath), _settingsFileName);
            if (File.Exists(_settingsFileName))
            {
                Settings = XmlHelper.Open<Settings>(_settingsFileName);
            }

            if (Settings == null)
                Settings = new Settings();

            _isWorking = true;
            Task.Factory.StartNew(async () => 
            {
                while (_isWorking)
                {
                    while (_keysQueue.Count > 3)
                        _keysQueue.TryDequeue(out int vkDummyCode);

                    if (_keysQueue.TryDequeue(out int vkCode))
                    {
                        Debug.WriteLine($"<-- Queue size: {_keysQueue.Count},  Code from queue: {(Keys)vkCode}");
                        PlaySoundForKey(vkCode);
                    }
                    else
                    {
                        _lastKeyCode = 0;
                        await Task.Delay(33);
                    }
                }
            });
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            List<string> keyNames = Enum.GetValues(typeof(Keys))
                .Cast<Keys>()
                .Select(k => k.ToString())
                .Distinct() // remove duplicates like "Control" and "ControlKey"
                .OrderBy(k => k)
                .ToList();

            foreach (string key in keyNames)
            {
                ListViewItem item = new ListViewItem(key);

                var keySetting = Settings.Keys.FirstOrDefault(k => k.Key == key);
                if (keySetting != null)
                    item.SubItems.Add(keySetting.Path);
                else
                    item.SubItems.Add("");

                m_listKeys.Items.Add(item);
            }

            UpdateSettings();

            EnableButtons(false);

            KeyboardHook.Connect();
            KeyboardHook.OnKeyPressed = (vkCode) => 
            { 
                Debug.WriteLine($"--> Queue Size: {_keysQueue.Count}, Key Code: {(Keys)vkCode}" );
                if (vkCode != _lastKeyCode || _keysQueue.Count == 0)
                {
                    _lastKeyCode = vkCode;
                    _keysQueue.Enqueue(vkCode);
                }
            };
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _isWorking = false;

            KeyboardHook.Disconnect();

            UpdateSettings();
            XmlHelper.Save(_settingsFileName, Settings);
        }

        private void UpdateSettings()
        {
            Settings.Keys.Clear();
            for (int i = 0; i < m_listKeys.Items.Count; i++)
            {
                string key = m_listKeys.Items[i].SubItems[0].Text;
                string path = m_listKeys.Items[i].SubItems[1].Text;

                Settings.Keys.Add(new KeyPath() { Key = key, Path = path });
            }
        }

        private void m_chkSoundOn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void m_btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                ListViewItem item = m_listKeys.SelectedItems[0];
                item.SubItems[1].Text = fileName;
                UpdateSettings();
            }
        }

        private void m_btnPlay_Click(object sender, EventArgs e)
        {
            ListViewItem item = m_listKeys.SelectedItems[0];
            string fileName = item.SubItems[1].Text;
            PlaySound(m_listKeys.SelectedIndices[0]);
        }

        private void m_btnClear_Click(object sender, EventArgs e)
        {
            ListViewItem item = m_listKeys.SelectedItems[0];
            item.SubItems[1].Text = "";
        }

        private void m_listKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_listKeys.SelectedItems.Count > 0)
            {
                ListViewItem item = m_listKeys.SelectedItems[0];

                EnableButtons(true);
            }
            else
            {
                EnableButtons(false);
            }
        }

        private void EnableButtons(bool enable)
        {
            m_btnBrowse.Enabled = enable;
            m_btnPlay.Enabled = enable;
            m_btnClear.Enabled = enable;
        }

        private void m_listKeys_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void m_listKeys_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0) return;

            Point clientPoint = m_listKeys.PointToClient(new Point(e.X, e.Y));
            ListViewItem item = m_listKeys.GetItemAt(clientPoint.X, clientPoint.Y);

            if (item != null)
            {
                // Now determine the subitem (cell) clicked
                int subItemIndex = GetSubItemIndex(item, clientPoint);
                if (subItemIndex >= 0 && subItemIndex < item.SubItems.Count)
                {
                    item.SubItems[subItemIndex].Text = files[0];
                }
            }

            UpdateSettings();
        }
        private int GetSubItemIndex(ListViewItem item, Point point)
        {
            int x = 0;
            for (int i = 0; i < m_listKeys.Columns.Count; i++)
            {
                x += m_listKeys.Columns[i].Width;
                if (point.X < x)
                    return i;
            }
            return -1;
        }

        private System.Windows.Forms.Keys KeyFromCode(int vkCode)
        {
            System.Windows.Forms.Keys key = (System.Windows.Forms.Keys)vkCode;
            return key;
        }

        private int GetListIndexForKey(int vkCode)
        {
            System.Windows.Forms.Keys key = (System.Windows.Forms.Keys)vkCode;

            for (int i = 0; i <= Settings.Keys.Count; i++)
            {
                string keyText = Settings.Keys[i].Key;
                if (keyText == key.ToString())
                { 
                    return i; 
                }
            }
            return -1;
        }

        private void PlaySoundForKey(int vkCode)
        {
            if (m_chkSoundOn.Checked)
            {
                int listIndex = GetListIndexForKey(vkCode);
                PlaySound(listIndex);
            }
        }

        private void PlaySound(int listIndex)
        {
            if (listIndex < 0 || listIndex >= Settings.Keys.Count) { return; }

            string fileName = Settings.Keys[listIndex].Path;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                try
                {
                    if (!_players.ContainsKey(fileName))
                    {
                        _players[fileName] = new SoundPlayer(fileName);
                    }
                    _players[fileName].Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }
    }
}
