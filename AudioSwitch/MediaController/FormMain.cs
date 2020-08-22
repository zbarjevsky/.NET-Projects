﻿using System;
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
using Microsoft.WindowsAPICodePack.Taskbar;
using MZ.Tools;
using MZ.WPF;
using MZ.Media.ComObjects;
using MZ.Media.Device;
using MZ.Media.DeviceSwitch;
using MZ.Media.Interfaces;
using MZ.Media.Notifications;
using MZ.Media.Properties;
using MZ.Media.WPF;

namespace MZ.Media
{
    public partial class FormMain : Form
    {
        public const string TITLE = "Select Active Audio End Point";

        EnumeratorClient _mmd = new EnumeratorClient();
        readonly PopupVolumeInfoHelper _popupVolumeInfoWindow;

        public FormMain()
        {
            InitializeComponent();

            _mmd.DevicesChanged = OnDevicesChanged;
            _mmd.DefaultDeviceChanged = OnDefaultDeviceChanged;

            Rectangle rThis = new Rectangle(Properties.Settings.Default.MainLocation, this.Size);
            if(SystemInformation.VirtualScreen.IntersectsWith(rThis))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = Properties.Settings.Default.MainLocation;
            }

            TaskbarManagerHelper.Init(this.Handle);
            TaskbarManagerHelper.ButtonClicked = (index) => { m_DeviceListPlayback.SetActiveDevice(index); };

            m_DeviceListPlayback.RefreshDeviceList = (status) => { EnumDevices(status); };
            m_DeviceListRecording.RefreshDeviceList = (status) => { EnumDevices(status); };

            m_DeviceListPlayback.UpdateStatus = (status) => { UpdateUI(status); m_volumeControlSpk.UpdateUI(); };
            m_DeviceListRecording.UpdateStatus = (status) => { UpdateUI(status); m_volumeControlMic.UpdateUI(); };

            m_DeviceListPlayback.AlternateColorPalette = AlternateColorPalette.Cold;
            m_DeviceListRecording.AlternateColorPalette = AlternateColorPalette.Warm;

            m_volumeControlSpk.OnVolumeChanged = (volume) => { UpdateUI(null); UpdatePopupVolume(); };
            m_volumeControlMic.OnVolumeChanged = (volume) => { UpdateUI(null); };

            _popupVolumeInfoWindow = new PopupVolumeInfoHelper(
                Properties.Settings.Default.PopupLocation, 
                (volume) => { m_volumeControlSpk.Volume = volume; });

            this.Text = TITLE;
        }

        private void UpdatePopupVolume()
        {
            _popupVolumeInfoWindow.ShowPopupVolume(m_volumeControlSpk.Volume);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            EnumDevices("Loaded");
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.MainLocation = this.Location;
            Properties.Settings.Default.PopupLocation = _popupVolumeInfoWindow.Location;
            Properties.Settings.Default.Save();

            _popupVolumeInfoWindow.Close();

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
                    _popupVolumeInfoWindow.InfoText = activeSpk.FriendlyName;

                    m_DeviceListPlayback.UpdateDeviceList(devsPlayback, activeSpk);
                    m_volumeControlSpk.SetDevice(activeSpk);

                    MMDevice activeMic = _mmd.GetDefaultAudioEndpoint(EDataFlow.Capture, Role.Multimedia);
                    m_DeviceListRecording.UpdateDeviceList(devsRecording, activeMic);
                    m_volumeControlMic.SetDevice(activeMic);

                    this.Text = activeSpk.FriendlyName + " - " + TITLE;
 
                    UpdateTaskbarButtons();
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
            TaskbarManagerHelper.ShowButtons(activeGroup.Items, m_DeviceListPlayback.ActiveDevice.FriendlyName);

            //hide preview on click
            for (int i = 0; i < activeGroup.Items.Count; i++)
            {
                TaskbarManagerHelper.Button(i).DismissOnClick = true;
            }
        }

        private void UpdateUI(string status)
        {
            if(status != null)
                m_status1.Text = status.Replace("\n", " ");

            m_DeviceListRecording.UpdateActiveDeviceVolume(m_volumeControlMic.Volume / 100f);
            m_DeviceListPlayback.UpdateActiveDeviceVolume(m_volumeControlSpk.Volume / 100f);
        }

        public string Log(string format, params object [] parameters)
        {
            string log = string.Format("{0} - ", DateTime.Now.ToString("s"));
            log += string.Format(format, parameters);
            CommonUtils.ExecuteOnUIThread(() => { m_txtLog.Text = log + m_txtLog.Text; }, this);
            MZ.Tools.Trace.Debug(log);
            return log;
        }

        private void m_chkTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = m_chkTopMost.Checked;
        }
    }
}
