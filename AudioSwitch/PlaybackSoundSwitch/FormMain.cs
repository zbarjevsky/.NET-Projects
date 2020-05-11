using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkZ.Tools;
using Microsoft.WindowsAPICodePack.Taskbar;
using MZ.Tools;
using PlaybackSoundSwitch.ComObjects;
using PlaybackSoundSwitch.Device;
using PlaybackSoundSwitch.DeviceSwitch;
using PlaybackSoundSwitch.Interfaces;
using PlaybackSoundSwitch.Notifications;
using PlaybackSoundSwitch.Properties;
using PlaybackSoundSwitch.Tools;

namespace PlaybackSoundSwitch
{
    public partial class FormMain : Form
    {
        public const string TITLE = "Select Active Audio End Point";

        private Font _fontNorm;
        private Font _fontBold;
        MMDeviceEnumerator _mmd = new MMDeviceEnumerator();
        MMDevice _activeDevice = null;

        //http://www.java2s.com/Open-Source/CSharp_Free_Code/API/Download_C4F_Managed_Taskbar_Sample.htm
        private ThumbnailToolbarButton _btnNext, _btnRefresh;

        public FormMain()
        {
            InitializeComponent();

            _fontNorm = m_listDevices.Font;
            _fontBold = new Font(_fontNorm, FontStyle.Bold);

            m_listDevices.SmallImageList = new ImageList
            {
                ImageSize = new Size(32, 32),
                ColorDepth = ColorDepth.Depth32Bit
            };

            m_imageListSpeakers.Images.AddStrip(Resources.SpeakerImgList);

            m_listDevices.SetDoubleBuffered(true);

            _mmd.DevicesChanged = OnDevicesChanged;
            _mmd.DefaultDeviceChanged = OnDefaultDeviceChanged;

            Icon icoRefresh = Icon.FromHandle(Properties.Resources.Refresh1.GetHicon());
            _btnRefresh = new ThumbnailToolbarButton(icoRefresh, "Refresh Device List");
            _btnRefresh.Click += (s, e) => { EnumDevices("Refresh from Toolbar"); };
            _btnRefresh.Visible = true;

            Icon next = Icon.FromHandle(Properties.Resources.defaultSpeakers.GetHicon());
            _btnNext = new ThumbnailToolbarButton(next, "Next Device");
            _btnNext.Click += (s, e) => { ActivateNextDevice(); };
            _btnNext.Visible = true;

            TaskbarManager.Instance.ThumbnailToolbars.AddButtons(this.Handle, _btnRefresh, _btnNext);

            this.Text = TITLE;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_listDevices.CollapseAllGroups();
            m_listDevices.ExpandGroup(0);

            EnumDevices("Loaded");
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mmd.Dispose();
        }

        private void OnDevicesChanged(MMDevice device, object additionalData = null)
        {
            string log = Log("Device Changed: {0}, Data: {1}\n", device, additionalData);
            EnumDevices(log);
        }

        private void OnDefaultDeviceChanged(MMDevice device)
        {
            string log = Log("Default Device Changed: {0}\n", device);
            EnumDevices(log);
        }

        public string Log(string format, params object [] parameters)
        {
            string log = string.Format("{0} - ", DateTime.Now.ToString("s"));
            log += string.Format(format, parameters);
            CommonUtils.ExecuteOnUIThread(() => { m_txtLog.Text = log + m_txtLog.Text; }, this);
            MZ.Tools.Trace.Debug(log);
            return log;
        }

        bool _isEnumerating = false;
        private void EnumDevices(string status)
        {
            CommonUtils.ExecuteOnUIThread(() => {
                try
                {
                    m_status1.Text = status.Trim();

                    if (_isEnumerating)
                        return;
                    _isEnumerating = true;

                    this.Cursor = Cursors.WaitCursor;
                    m_btnRefresh.Enabled = false;

                    MMDeviceCollection coll = _mmd.EnumerateAudioEndPoints(EDataFlow.Render, DeviceState.All);
                    IReadOnlyCollection<DeviceFullInfo> devs = CreateDeviceList(coll);

                    Application.DoEvents();
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
                        lvi.Group = GetItemGroup(dev);
                        lvi.ToolTipText = GetDeviceTooltip(dev);
                    }

                    SetActiveDeviceToBold();
                    m_listDevices.EndUpdate();
                }
                catch (Exception err)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(err.Message);
                }
                this.Cursor = Cursors.Arrow;
                m_btnRefresh.Enabled = true;
                _isEnumerating = false;
            }, this);
        }

        private void ActivateNextDevice()
        {
            SetActiveDevice(FindNextActiveDevice());
        }

        private DeviceFullInfo FindNextActiveDevice()
        {
            int count = m_listDevices.Groups[0].Items.Count;
            if (count > 1)
            {
                for (int i = 0; i < count; i++)
                {
                    if (m_listDevices.Groups[0].Items[i].Font.Bold)
                    {
                        if (i == count - 1)
                            i = -1;
                        i++;
                        DeviceFullInfo dev = m_listDevices.Groups[0].Items[i].Tag as DeviceFullInfo;
                        return dev;
                    }
                }
            }
            return null;
        }

        private string GetDeviceTooltip(DeviceFullInfo dev)
        {
            return dev.Name + "\n" + dev.State + "\n" + dev.Id;
        }

        private ListViewGroup GetItemGroup(DeviceFullInfo dev)
        {
            switch (dev.State)
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

        private void SetActiveDeviceToBold()
        {
            _activeDevice = _mmd.GetDefaultAudioEndpoint(EDataFlow.Render, Role.Multimedia);
            _activeDevice.AudioEndpointVolume.OnVolumeNotification = (notificationData) =>
            {
                CommonUtils.ExecuteOnUIThread(() => {
                    m_trackVolume.Value = (int)(100f * notificationData.MasterVolume);
                }
                , this);
            };

            this.Text = _activeDevice.FriendlyName + " - " + TITLE;

            AlternateColorTool altenateColor = new AlternateColorTool();

            foreach (ListViewGroup group in m_listDevices.Groups)
            {
                foreach (ListViewItem item in group.Items)
                {
                    DeviceFullInfo dev = item.Tag as DeviceFullInfo;
                    bool isActive = dev.Id == _activeDevice.ID;
                    item.Font = isActive ? _fontBold : _fontNorm;
                    item.SubItems[1].Text = isActive ? "Active" : "";
                    item.Selected = false; // dev.Id == sel.ID;
                    item.ForeColor = dev.State == EDeviceState.Active ? Color.Black : Color.DarkGray;
                    //alternate color inside each group
                    item.BackColor = altenateColor.GetColor(dev.Name);
                    //item.Group = GetItemGroup(dev);
                }
            }

            DeviceFullInfo next = FindNextActiveDevice();
            if (next != null)
                _btnNext.Tooltip = "Next Device: " + next.FriendlyName;
            else
                _btnNext.Tooltip = "N/A";


            m_trackVolume.Value = (int)(100f * _activeDevice.AudioEndpointVolume.MasterVolumeLevelScalar);
            UpdateUI(null);
        }

        private static IReadOnlyCollection<DeviceFullInfo> CreateDeviceList(MMDeviceCollection collection)
        {
            var sortedDevices = new List<DeviceFullInfo>();
            foreach (var device in collection)
            {
                try
                {
                    MMDevice d = new MMDevice(device);
                    var deviceInfo = new DeviceFullInfo(d);
                    if (string.IsNullOrEmpty(deviceInfo.Name))
                    {
                        continue;
                    }

                    sortedDevices.Add(deviceInfo);
                }
                catch (Exception e)
                {
                    //Log.Warning("Can't get name of device {device}", device.ID);
                    throw;
                }
            }

            return sortedDevices.OrderBy(dev => dev.Name).ThenBy(dev => dev.FriendlyName).ToArray();
        }

        private void m_btnRefresh_Click(object sender, EventArgs e)
        {
            EnumDevices("Refresh");
        }

        private void m_trackVolume_Scroll(object sender, EventArgs e)
        {
            _activeDevice.AudioEndpointVolume.MasterVolumeLevelScalar = m_trackVolume.Value / 100f;
            //SystemSounds.Beep.Play();
        }

        private void m_trackVolume_ValueChanged(object sender, EventArgs e)
        {
            UpdateUI("Volume: "+m_trackVolume.Value);
        }

        private void m_btnMute_Click(object sender, EventArgs e)
        {
            _activeDevice.AudioEndpointVolume.Mute = !_activeDevice.AudioEndpointVolume.Mute;
            UpdateUI("Mute");
        }

        private void UpdateUI(string status)
        {
            if(status != null)
                m_status1.Text = status;

            float volume = m_trackVolume.Value / 100f;
            foreach (ListViewItem item in m_listDevices.Items)
            {
                if(item.Font.Bold)
                {
                    item.SubItems[2].Text = volume.ToString("0%");
                }
                else
                {
                    item.SubItems[2].Text = "---";
                }
            }

            if (volume >= 0.7)
                m_btnMute.ImageIndex = 0;
            else if (volume > 0.3 && volume < 0.7)
                m_btnMute.ImageIndex = 1;
            else if (volume > 0 && volume <= 0.3)
                m_btnMute.ImageIndex = 2;
            else
                m_btnMute.ImageIndex = 4;

            if (_activeDevice.AudioEndpointVolume.Mute)
                m_btnMute.ImageIndex = 3;

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
            UpdateUI("Selected: "+ GetSelectedName());
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

        private void SetActiveDevice(DeviceFullInfo device)
        {
            if (_isEnumerating)
                return;

            try
            {
                if (device == null || device.State != EDeviceState.Active)
                    return;

                SoundDeviceManager.SetActiveDevice(device);

                SetActiveDeviceToBold();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
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
    }
}
