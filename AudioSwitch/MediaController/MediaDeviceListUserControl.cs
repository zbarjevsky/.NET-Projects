using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MkZ.Media.Device;
using System.Diagnostics;
using MkZ.Tools;
using MkZ.Media.DeviceSwitch;
using MkZ.WinForms;
using ListViewExtensions;

namespace MkZ.Media
{
    public partial class MediaDeviceListUserControl : UserControl
    {
        private Font _fontNorm;
        private Font _fontBold;
        private MMDevice _activeDevice = null;

        public MMDevice ActiveDevice { get { return _activeDevice; } }

        public Action<string> RefreshDeviceList = (status) => { };
        public Action<string> UpdateStatus = (status) => { };

        public AlternateColorPalette AlternateColorPalette { get; set; } = AlternateColorPalette.Cold;

        public Action<List<ListViewItem>> OnSortChanged { get; set; } = (group) => { };

        public MediaDeviceListUserControl()
        {
            InitializeComponent();

            _fontNorm = m_listDevices.Font;
            _fontBold = new Font(_fontNorm, FontStyle.Bold);

            m_listDevices.SmallImageList = new ImageList
            {
                ImageSize = new Size(32, 32),
                ColorDepth = ColorDepth.Depth32Bit
            };

            m_listDevices.SetDoubleBuffered(true);
            m_listDevices.SetListViewColumnSorter();
            m_listDevices.ColumnClick += ListDevices_ColumnClick; ;
        }

        private void ListDevices_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            List<ListViewItem> activeGroup = GetItemGroupSorted(EDeviceState.Active);
            OnSortChanged(activeGroup);
        }

        private void MediaDeviceListUserControl_Load(object sender, EventArgs e)
        {
            m_listDevices.CollapseAllGroups();
            m_listDevices.ExpandGroup(0);
        }

        public List<DeviceFullInfo> GetEnabledDevices()
        {
            List<DeviceFullInfo> list = new List<DeviceFullInfo>();

            foreach (ListViewItem item in m_listDevices.Items)
            {
                if(item.Tag is DeviceFullInfo dev)
                {
                    if (dev.State == EDeviceState.Active)
                        list.Add(dev);
                }
            }
            return list;
        }

        public void UpdateDeviceList(IReadOnlyCollection<DeviceFullInfo> devs, MMDevice activeDevice)
        {
            _activeDevice = activeDevice;

            m_listDevices.Items.Clear();

            m_listDevices.BeginUpdate();
            foreach (DeviceFullInfo dev in devs)
            {
                AddDeviceIconSmallImage(dev);

                Debug.WriteLine("Dev: " + dev.Name);
                ListViewItem lvi = m_listDevices.Items.Add(dev.FriendlyName);
                lvi.ImageKey = dev.IconPath;
                lvi.SubItems.Add("N/A");
                lvi.SubItems.Add("N/A");
                lvi.Tag = dev;
                lvi.Group = GetItemGroup(dev.State);
                lvi.ToolTipText = GetDeviceTooltip(dev);
            }

            SetActiveDeviceToBold(activeDevice);
            m_listDevices.EndUpdate();
        }

        /// <summary>
        /// Using the DeviceClassIconPath, get the Icon
        /// </summary>
        /// <param name="device"></param>
        /// <param name="listView"></param>
        private void AddDeviceIconSmallImage(DeviceFullInfo device)
        {
            if (!m_listDevices.SmallImageList.Images.ContainsKey(device.IconPath))
            {
                m_listDevices.SmallImageList.Images.Add(device.IconPath, device.LargeIcon);
            }
        }

        private string GetDeviceTooltip(DeviceFullInfo dev)
        {
            return dev.Name + "\n" + dev.State + "\n" + dev.ID;
        }

        public ListViewGroup GetItemGroup(EDeviceState state)
        {
            switch (state)
            {
                case EDeviceState.Active:
                    return m_listDevices.Groups[0];
                case EDeviceState.Unplugged:
                    return m_listDevices.Groups[1];
                case EDeviceState.Disabled:
                    return m_listDevices.Groups[2];
                case EDeviceState.NotPresent:
                default:
                    return m_listDevices.Groups[3];
            }
        }

        public List<ListViewItem> GetItemGroupSorted(EDeviceState state)
        {
            ListViewGroup activeGroup = GetItemGroup(EDeviceState.Active);
            List<ListViewItem> list = activeGroup.Items.OfType<ListViewItem>().Select(i => i).ToList();

            ListViewColumnSorter sorter = m_listDevices.ListViewItemSorter as ListViewColumnSorter;
            list.Sort((i1, i2) => ListViewColumnSorter.Compare(i1,i2, sorter.SortColumn, sorter.SortOrder));

            return list;
        }

        private void SetActiveDeviceToBold(MMDevice device)
        {
            AlternateColorTool altenateColor = new AlternateColorTool(AlternateColorPalette);

            string ID = device == null ? "" : device.ID;
            foreach (ListViewGroup group in m_listDevices.Groups)
            {
                foreach (ListViewItem item in group.Items)
                {
                    DeviceFullInfo dev = item.Tag as DeviceFullInfo;
                    bool isActive = dev.ID == ID;
                    item.Font = isActive ? _fontBold : _fontNorm;
                    item.SubItems[1].Text = isActive ? "Active" : "";
                    //item.SubItems[2].Text = dev.Device.AudioEndpointVolume.MasterVolumeLevelScalar.ToString("0%");
                    item.Selected = false; // dev.Id == sel.ID;
                    item.ForeColor = dev.State == EDeviceState.Active ? Color.Black : Color.DarkGray;
                    //alternate color inside each group
                    item.BackColor = altenateColor.GetColor(dev.Name);
                    //item.Group = GetItemGroup(dev);
                }
            }

            UpdateUI(null);
        }

        public void UpdateActiveDeviceVolume(float volume = 0)
        {
            if (ActiveDevice == null)
                return;

            foreach (ListViewItem item in m_listDevices.Items)
            {
                DeviceFullInfo dev = item.Tag as DeviceFullInfo;

                if (dev.ID == _activeDevice.ID)
                {
                    if (volume == 0)
                        volume = dev.Device.AudioEndpointVolume.MasterVolumeLevelScalar;

                    item.SubItems[2].Text = volume.ToString("0%");
                }
                else
                {
                    item.SubItems[2].Text = "---";
                }
            }
        }

        private void UpdateUI(string status)
        {
            UpdateStatus(status);

            UpdateActiveDeviceVolume();

            m_btnProperties.Enabled = false;
            m_btnActivate.Enabled = false;
            m_mnuActivate.Enabled = false;
            m_mnuActivate.Image = null;
            m_btnActivate.Image = null;

            string activateText = "No Device Selected...";

            if (m_listDevices.SelectedItems.Count > 0)
            {
                m_btnProperties.Enabled = true;

                m_mnuActivate.Image = m_listDevices.SmallImageList.Images[m_listDevices.SelectedItems[0].ImageKey];

                DeviceFullInfo device = GetSelectedDevice();

                if (m_listDevices.SelectedItems[0].Font.Bold) //is active
                {
                    activateText = "Active Device: " + device.FriendlyName;
                    m_mnuMute.Enabled = true;
                }
                else
                {
                    if (device.State == EDeviceState.Active)
                    {
                        m_mnuActivate.Enabled = true;
                        m_btnActivate.Enabled = true;
                        activateText = "Activate: " + device.FriendlyName;
                    }
                    else
                    {
                        activateText = "Inactive: " + device.FriendlyName;
                    }
                }
            }

            m_btnActivate.Text = activateText;
            m_mnuActivate.Text = activateText;
            m_btnActivate.Image = m_mnuActivate.Image;

            if (_activeDevice != null)
            {
                m_mnuMute.Enabled = true;
                m_mnuMute.Text = _activeDevice.AudioEndpointVolume.Mute ? "UnMute: " : "Mute: ";
                m_mnuMute.Text += _activeDevice.FriendlyName;
                m_mnuMute.Image = _activeDevice.AudioEndpointVolume.Mute ? m_imageListMute.Images[0] : m_imageListMute.Images[1];
            }
            else
            {
                m_mnuMute.Enabled = false;
                m_mnuMute.Text = "Mute";
                m_mnuMute.Image = null;
            }
        }

        private void m_listDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUI("Selected: " + GetSelectedName());
        }

        private DeviceFullInfo GetSelectedDevice()
        {
            if (m_listDevices.SelectedItems.Count > 0)
                return m_listDevices.SelectedItems[0].Tag as DeviceFullInfo;
            return null;
        }

        private string GetSelectedName()
        {
            if (m_listDevices.SelectedItems.Count > 0)
                return m_listDevices.SelectedItems[0].Text;
            return "N?A";
        }

        private void m_btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDeviceList("Refresh");
        }

        private void m_btnActivate_Click(object sender, EventArgs e)
        {
            SetActiveDevice();
        }

        private void m_listDevices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SetActiveDevice();
        }

        private void SetActiveDevice()
        {
            SetActiveDevice(GetSelectedDevice());
        }

        public void SetActiveDevice(int index)
        {
            List<ListViewItem> devices = GetItemGroupSorted(EDeviceState.Active);
            DeviceFullInfo dev = devices[index].Tag as DeviceFullInfo;
            SetActiveDevice(dev);
        }

        private void SetActiveDevice(DeviceFullInfo device)
        {
            try
            {
                if (device == null || device.State != EDeviceState.Active)
                    return;

                SoundDeviceManager.SetActiveDevice(device.ID, device.DeviceType);

                SetActiveDeviceToBold(device.Device);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void m_mnuActivate_Click(object sender, EventArgs e)
        {
            SetActiveDevice();
        }

        private void m_mnuProperties_Click(object sender, EventArgs e)
        {
            m_btnProperties_Click(sender, e);
        }

        private void m_mnuMute_Click(object sender, EventArgs e)
        {
            if (_activeDevice == null)
                return;

            string action = _activeDevice.AudioEndpointVolume.Mute ? "Unmute: " : "Mute: ";
            _activeDevice.AudioEndpointVolume.Mute = !_activeDevice.AudioEndpointVolume.Mute;
            UpdateUI(action + _activeDevice.FriendlyName);
            UpdateStatus(action +_activeDevice.FriendlyName);
        }

        private void m_btnProperties_Click(object sender, EventArgs e)
        {
            Process.Start("control.exe", $"mmsys.cpl sounds {_activeDevice.ID}");
        }

        private void m_btnSound_Click(object sender, EventArgs e)
        {
            Process.Start("control.exe", "mmsys.cpl sounds");
        }
    }
}
