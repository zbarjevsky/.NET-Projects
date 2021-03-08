﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

using MkZ.Media.Properties;
using MkZ.Media.Device;
using MkZ.WinForms;
using MkZ.Windows;

namespace MkZ.Media
{
    public partial class VolumeUserControl : UserControl
    {
        public Action<float> OnVolumeChanged = (volume) => { };

        public MMDevice Device { get; private set; }

        public string Title { get { return m_grpVolume.Text; } set { m_grpVolume.Text = value; } }

        public void EnableControls(bool bEnable)
        {
            m_btnMute.Enabled = bEnable;
            m_trackVolume.Enabled = bEnable;
            m_progrLevel.Enabled = bEnable;
            m_trackVolume.Value = 0;
        }

        public VolumeUserControl()
        {
            InitializeComponent();
        }

        private void VolumeUserControl_Load(object sender, EventArgs e)
        {
            m_imgListSpk.Images.Clear();
            m_imgListSpk.Images.AddStrip(Resources.SpeakerImgList);
        }

        public void SetDevice(MMDevice device)
        {
            if (Device != null)
                Device.Dispose();

            Device = device;
            EnableControls(Device != null);
            if (Device == null) //no device
                return;

            if (Device.DataFlow == EDataFlow.Capture)
            {
                m_btnMute.ImageList = m_imgListMic;
            }

            if (Device.DataFlow == EDataFlow.Render)
            {
                m_btnMute.ImageList = m_imgListSpk;
            }

            m_trackVolume.Value = (int)(100f * Device.AudioEndpointVolume.MasterVolumeLevelScalar);

            Device.AudioEndpointVolume.OnVolumeNotification = (notificationData) =>
            {
                CommonUtils.ExecuteOnUIThread(() => {
                    m_trackVolume.Value = (int)Math.Round(100f * notificationData.MasterVolume);
                }
                , this);
            };
        }

        [DisplayName("Color Theme")]
        public ColorBarsProgressBar.ThemeColorSet ColorTheme
        {
            get { return m_progrLevel.ColorTheme; }
            set { m_progrLevel.ColorTheme = value; Refresh(); }
        }

        //desired volume
        public int Volume
        {
            get { return (int)m_trackVolume.Value; }
            set { m_trackVolume.Value = value; UpdateDeviceVolume(); }
        }

        //actual device level
        public int Level
        {
            get { return m_progrLevel.Value; }
            set { m_progrLevel.Value = value; }
        }

        private void UpdateDeviceVolume()
        {
            if(Device != null)
                Device.AudioEndpointVolume.MasterVolumeLevelScalar = (float)m_trackVolume.Value / 100f;
        }

        private void m_trackVolume_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateDeviceVolume();
            OnVolumeChanged((float)m_trackVolume.Value / 100f);
            UpdateUI();
            //SystemSounds.Beep.Play();
        }

        private void m_trackVolume_ValueChanged(object sender, EventArgs e)
        {
            OnVolumeChanged((float)m_trackVolume.Value / 100f);
            UpdateUI();
        }

        private void m_btnMute_Click(object sender, EventArgs e)
        {
            Device.AudioEndpointVolume.Mute = !Device.AudioEndpointVolume.Mute;
            UpdateUI();
        }

        public void UpdateUI()
        {
            this.Text = "";
            this.m_lbl.Text = "N/A";
            if (Device == null) 
                return;
            
            this.Text = Device.FriendlyName;
            this.m_lbl.Text = m_trackVolume.Value + "%";

            bool isMuted = Device.AudioEndpointVolume.Mute;
            string mute = isMuted ? "Muted: " : "Mute: ";
            toolTip1.SetToolTip(m_btnMute, mute + Device.FriendlyName);

            //microphone
            if (Device.DataFlow == EDataFlow.Capture)
            {
                m_btnMute.ImageIndex = isMuted ? 1 : 0;
            }

            //speaker
            if (Device.DataFlow == EDataFlow.Render)
            {
                float volume = Device.AudioEndpointVolume.MasterVolumeLevelScalar;
                if (volume >= 0.7)
                    m_btnMute.ImageIndex = 0;
                else if (volume > 0.3 && volume < 0.7)
                    m_btnMute.ImageIndex = 1;
                else if (volume > 0 && volume <= 0.3)
                    m_btnMute.ImageIndex = 2;
                else
                    m_btnMute.ImageIndex = 4;

                if (isMuted)
                    m_btnMute.ImageIndex = 3;
            }

            m_progrLevel.Enabled = isMuted ? false : true;
        }

        private void m_timer_Tick(object sender, EventArgs e)
        {
            UpdatePeakLevel(m_progrLevel, Device);
        }

        private void UpdatePeakLevel(ColorBarsProgressBar progr, MMDevice dev)
        {
            if (dev == null || dev.State != EDeviceState.Active)
            {
                progr.Value = 0;
                return;
            }

            try
            {
                float peak = dev.AudioMeterInformation.MasterPeakValue * 100f;
                progr.Value = (int)peak;
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
            }        
        }
    }
}
