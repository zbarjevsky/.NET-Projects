using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
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
        //MMDevice _activeDevice = null;
        //MMDevice _activeMic = null;

        public FormMain()
        {
            InitializeComponent();

            m_imageListSpeakers.Images.Clear();
            m_imageListSpeakers.Images.AddStrip(Resources.SpeakerImgList);

            _mmd.DevicesChanged = OnDevicesChanged;
            _mmd.DefaultDeviceChanged = OnDefaultDeviceChanged;

            TaskbarManagerHelper.Init(this.Handle);
            TaskbarManagerHelper.ButtonClicked = (friendlyName) => { m_DeviceListPlayback.SetActiveDevice(friendlyName); };

            m_DeviceListPlayback.RefreshDeviceList = (status) => { EnumDevices(status); };
            m_DeviceListRecording.RefreshDeviceList = (status) => { EnumDevices(status); };

            m_DeviceListPlayback.UpdateStatus = (status) => { UpdateUI(status); };
            m_DeviceListRecording.UpdateStatus = (status) => { UpdateUI(status); };

            this.Text = TITLE;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            m_progrLevelsMic.Value = 0;
            m_progrLevelsMic.SetColorYellow();

            m_progrLevelSpk.Value = 0;
            m_progrLevelSpk.SetColorGreen();

            EnumDevices("Loaded");
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mmd.Dispose();
        }

        private void m_timer_Tick(object sender, EventArgs e)
        {
            UpdatePeakLevel(m_progrLevelSpk, m_DeviceListPlayback.ActiveDevice);
            UpdatePeakLevel(m_progrLevelsMic, m_DeviceListRecording.ActiveDevice);
        }

        private void UpdatePeakLevel(ProgressBar progr, MMDevice dev)
        {
            if (dev == null || dev.State != EDeviceState.Active)
            {
                progr.Value = 0;
                return;
            }

            float peak = dev.AudioMeterInformation.MasterPeakValue * 100f;
            progr.Value = (int)peak;
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

        private void m_trackVolume_Scroll(object sender, EventArgs e)
        {
            m_DeviceListPlayback.ActiveDevice.AudioEndpointVolume.MasterVolumeLevelScalar = m_trackVolume.Value / 100f;
            //SystemSounds.Beep.Play();
        }

        private void m_trackVolume_ValueChanged(object sender, EventArgs e)
        {
            UpdateUI("Volume: "+m_trackVolume.Value);
        }

        private void m_btnMute_Click(object sender, EventArgs e)
        {
            m_DeviceListPlayback.ActiveDevice.AudioEndpointVolume.Mute = !m_DeviceListPlayback.ActiveDevice.AudioEndpointVolume.Mute;
            UpdateUI("Mute: "+ m_DeviceListPlayback.ActiveDevice.FriendlyName);
        }

        private void m_btnMicMute_Click(object sender, EventArgs e)
        {
            m_DeviceListRecording.ActiveDevice.AudioEndpointVolume.Mute = !m_DeviceListRecording.ActiveDevice.AudioEndpointVolume.Mute;
            UpdateUI("MicMute: "+ m_DeviceListRecording.ActiveDevice.FriendlyName);
        }

        bool _isEnumerating = false;
        private void EnumDevices(string status)
        {
            CommonUtils.ExecuteOnUIThread(() => 
            {
                try
                {
                    if (_isEnumerating)
                        return;
                    _isEnumerating = true;

                    this.Cursor = Cursors.WaitCursor;
                    //m_btnRefresh.Enabled = false;

                    IReadOnlyCollection<DeviceFullInfo> devsPlayback = _mmd.EnumerateAudioEndPoints(EDataFlow.Render, DeviceState.All);

                    IReadOnlyCollection<DeviceFullInfo> devsRecording = _mmd.EnumerateAudioEndPoints(EDataFlow.Capture, DeviceState.All);

                    Application.DoEvents();

                    MMDevice activeSpk = _mmd.GetDefaultAudioEndpoint(EDataFlow.Render, Role.Multimedia);
                    m_DeviceListPlayback.UpdateDeviceList(devsPlayback, activeSpk);

                    MMDevice activeMic = _mmd.GetDefaultAudioEndpoint(EDataFlow.Capture, Role.Multimedia);
                    m_DeviceListRecording.UpdateDeviceList(devsRecording, activeMic);
                    
                    m_DeviceListPlayback.ActiveDevice.AudioEndpointVolume.OnVolumeNotification = (notificationData) =>
                    {
                        CommonUtils.ExecuteOnUIThread(() => {
                            m_trackVolume.Value = (int)(100f * notificationData.MasterVolume);
                        }
                        , this);
                    };

                    UpdateTaskbarButtons();

                    m_trackVolume.Value = (int)(100f * m_DeviceListPlayback.ActiveDevice.AudioEndpointVolume.MasterVolumeLevelScalar);
                }
                catch (Exception err)
                {
                    this.Cursor = Cursors.Arrow;
                    MessageBox.Show(err.Message, "Enum Devices", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                UpdateUI(status);
                this.Cursor = Cursors.Arrow;
                //m_btnRefresh.Enabled = true;
                _isEnumerating = false;
            }, this);
        }

        private void UpdateTaskbarButtons()
        {
            ListViewGroup activeGroup = m_DeviceListPlayback.GetItemGroup(EDeviceState.Active);
            TaskbarManagerHelper.UpdateButtons(activeGroup.Items, m_DeviceListPlayback.ActiveDevice.FriendlyName);
        }

        private void UpdateUI(string status)
        {
            if(status != null)
                m_status1.Text = status;

            //microphone
            if (m_DeviceListRecording.ActiveDevice != null)
            {
                string muteMic = m_DeviceListRecording.ActiveDevice.AudioEndpointVolume.Mute ? "Muted: " : "Mute: ";
                toolTip1.SetToolTip(m_btnMicMute, muteMic + m_DeviceListRecording.ActiveDevice.FriendlyName);

                m_btnMicMute.ImageIndex = m_DeviceListRecording.ActiveDevice.AudioEndpointVolume.Mute ? 1 : 0;
            }

            //speaker
            if (m_DeviceListPlayback.ActiveDevice != null)
            {
                this.Text = m_DeviceListPlayback.ActiveDevice.FriendlyName + " - " + TITLE;

                string muteSpk = m_DeviceListPlayback.ActiveDevice.AudioEndpointVolume.Mute ? "Muted: " : "Mute: ";
                toolTip1.SetToolTip(m_btnMute, muteSpk + m_DeviceListPlayback.ActiveDevice.FriendlyName);

                float volume = m_DeviceListPlayback.ActiveDevice.AudioEndpointVolume.MasterVolumeLevelScalar;
                if (volume >= 0.7)
                    m_btnMute.ImageIndex = 0;
                else if (volume > 0.3 && volume < 0.7)
                    m_btnMute.ImageIndex = 1;
                else if (volume > 0 && volume <= 0.3)
                    m_btnMute.ImageIndex = 2;
                else
                    m_btnMute.ImageIndex = 4;

                if (m_DeviceListPlayback.ActiveDevice.AudioEndpointVolume.Mute)
                    m_btnMute.ImageIndex = 3;
            }
        }

        public string Log(string format, params object [] parameters)
        {
            string log = string.Format("{0} - ", DateTime.Now.ToString("s"));
            log += string.Format(format, parameters);
            CommonUtils.ExecuteOnUIThread(() => { m_txtLog.Text = log + m_txtLog.Text; }, this);
            MZ.Tools.Trace.Debug(log);
            return log;
        }
    }
}
