using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlaybackSoundSwitch.Device;
using System.Diagnostics;
using PlaybackSoundSwitch.Tools;
using MZ.Tools;
using PlaybackSoundSwitch.DeviceSwitch;
using MarkZ.Tools;

namespace PlaybackSoundSwitch
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
        }

        private void MediaDeviceListUserControl_Load(object sender, EventArgs e)
        {
            m_listDevices.CollapseAllGroups();
            m_listDevices.ExpandGroup(0);
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

            SetActiveDeviceToBold(activeDevice.ID);
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

        private void SetActiveDeviceToBold(string deviceId)
        {
            AlternateColorTool altenateColor = new AlternateColorTool(AlternateColorPalette);

            foreach (ListViewGroup group in m_listDevices.Groups)
            {
                foreach (ListViewItem item in group.Items)
                {
                    DeviceFullInfo dev = item.Tag as DeviceFullInfo;
                    bool isActive = dev.ID == deviceId;
                    item.Font = isActive ? _fontBold : _fontNorm;
                    item.SubItems[1].Text = isActive ? "Active" : "";
                    item.Selected = false; // dev.Id == sel.ID;
                    item.ForeColor = dev.State == EDeviceState.Active ? Color.Black : Color.DarkGray;
                    //alternate color inside each group
                    item.BackColor = altenateColor.GetColor(dev.Name);
                    //item.Group = GetItemGroup(dev);
                }
            }

            UpdateUI(null);
        }

        public void UpdateActiveDeviceVolume(float volume)
        {
            if (ActiveDevice == null)
                return;

            if(volume == 0)
                volume = ActiveDevice.AudioEndpointVolume.MasterVolumeLevelScalar;

            foreach (ListViewItem item in m_listDevices.Items)
            {
                if (item.Font.Bold)
                {
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

            UpdateActiveDeviceVolume(0);

            if (m_listDevices.SelectedItems.Count > 0)
            {
                var device = GetSelectedDevice();
                m_btnActivate.Enabled = (m_listDevices.SelectedItems[0].Font.Bold == false && device.State == EDeviceState.Active);
                m_btnActivate.Text = "Set Active: " + GetSelectedName();
            }
            else
            {
                m_btnActivate.Enabled = false;
                m_btnActivate.Text = "Select Device to Activate...";
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

        public void SetActiveDevice(string friendlyName)
        {
            ListViewGroup activeGroup = m_listDevices.Groups[0];
            for (int i = 0; i < activeGroup.Items.Count; i++)
            {
                ListViewItem item = activeGroup.Items[i];
                DeviceFullInfo dev = item.Tag as DeviceFullInfo;
                if (friendlyName == dev.FriendlyName)
                    SetActiveDevice(dev);
            }
        }

        private void SetActiveDevice(DeviceFullInfo device)
        {
            try
            {
                if (device == null || device.State != EDeviceState.Active)
                    return;

                SoundDeviceManager.SetActiveDevice(device.ID, device.DeviceType);

                SetActiveDeviceToBold(device.ID);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
