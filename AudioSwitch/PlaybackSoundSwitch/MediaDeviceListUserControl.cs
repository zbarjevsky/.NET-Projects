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
        private DeviceFullInfo _activeDevice = null;

        public MMDevice ActiveDevice { get { if (_activeDevice == null) return null; return _activeDevice.Device; } }

        public Action<string> RefreshDeviceList = (status) => { };

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

        public void UpdateDeviceList(IReadOnlyCollection<DeviceFullInfo> devs, DeviceFullInfo activeDevice)
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
            return dev.Name + "\n" + dev.State + "\n" + dev.Id;
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

        private void SetActiveDeviceToBold(DeviceFullInfo activeDevice)
        {
            //_activeDevice = _mmd.GetDefaultAudioEndpoint(EDataFlow.Render, Role.Multimedia);
            //_activeDevice.AudioEndpointVolume.OnVolumeNotification = (notificationData) =>
            //{
            //    CommonUtils.ExecuteOnUIThread(() => {
            //        m_trackVolume.Value = (int)(100f * notificationData.MasterVolume);
            //    }
            //    , this);
            //};

            ////MMDeviceCollection micList = _mmd.EnumerateAudioEndPoints(EDataFlow.Capture, DeviceState.Active);
            //_activeMic = _mmd.GetDefaultAudioEndpoint(EDataFlow.Capture, Role.Multimedia);

            AlternateColorTool altenateColor = new AlternateColorTool();

            foreach (ListViewGroup group in m_listDevices.Groups)
            {
                foreach (ListViewItem item in group.Items)
                {
                    DeviceFullInfo dev = item.Tag as DeviceFullInfo;
                    bool isActive = dev.Id == activeDevice.Id;
                    item.Font = isActive ? _fontBold : _fontNorm;
                    item.SubItems[1].Text = isActive ? "Active" : "";
                    item.Selected = false; // dev.Id == sel.ID;
                    item.ForeColor = dev.State == EDeviceState.Active ? Color.Black : Color.DarkGray;
                    //alternate color inside each group
                    item.BackColor = altenateColor.GetColor(dev.Name);
                    //item.Group = GetItemGroup(dev);
                }
            }

            //UpdateTaskbarButtons(_activeDevice.FriendlyName);

            //m_trackVolume.Value = (int)(100f * _activeDevice.AudioEndpointVolume.MasterVolumeLevelScalar);
            UpdateUI(null);
        }

        private void UpdateTaskbarButtons(string activeName)
        {
            List<string> deviceNames = new List<string>();

            ListViewGroup activeGroup = m_listDevices.Groups[0];
            TaskbarManagerHelper.UpdateButtons(activeGroup.Items, activeName);
        }

        private void UpdateUI(string status)
        {
            //if (status != null)
            //    m_status1.Text = status;

            //this.Text = _activeDevice.FriendlyName + " - " + TITLE;

            //string mute = _activeDevice.AudioEndpointVolume.Mute ? "Muted: " : "Mute: ";
            //toolTip1.SetToolTip(m_btnMute, mute + _activeDevice.FriendlyName);

            //mute = _activeMic.AudioEndpointVolume.Mute ? "Muted: " : "Mute: ";
            //toolTip1.SetToolTip(m_btnMicMute, mute + _activeMic.FriendlyName);

            float volume = _activeDevice.Device.AudioEndpointVolume.MasterVolumeLevelScalar;
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

            //m_btnMicMute.ImageIndex = _activeMic.AudioEndpointVolume.Mute ? 1 : 0;

            //if (volume >= 0.7)
            //    m_btnMute.ImageIndex = 0;
            //else if (volume > 0.3 && volume < 0.7)
            //    m_btnMute.ImageIndex = 1;
            //else if (volume > 0 && volume <= 0.3)
            //    m_btnMute.ImageIndex = 2;
            //else
            //    m_btnMute.ImageIndex = 4;

            //if (_activeDevice.AudioEndpointVolume.Mute)
            //    m_btnMute.ImageIndex = 3;

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
            //if (_isEnumerating)
            //    return;

            try
            {
                if (device == null || device.State != EDeviceState.Active)
                    return;

                SoundDeviceManager.SetActiveDevice(device);

                SetActiveDeviceToBold(device);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
