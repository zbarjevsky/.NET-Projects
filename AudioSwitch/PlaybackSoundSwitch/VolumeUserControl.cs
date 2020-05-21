using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlaybackSoundSwitch.Properties;
using PlaybackSoundSwitch.Device;
using MZ.Controls;

namespace PlaybackSoundSwitch
{
    public partial class VolumeUserControl : UserControl
    {
        public Action<float> OnVolumeChanged = (volume) => { };

        public MMDevice Device { get; private set; }

        public string Title { get { return m_grpVolume.Text; } set { m_grpVolume.Text = value; } }

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
                MZ.Tools.CommonUtils.ExecuteOnUIThread(() => {
                    m_trackVolume.Value = (int)Math.Ceiling(100f * notificationData.MasterVolume);
                }
                , this);
            };
        }

        public ColorBarsProgressBar.ColorsThemeType ColorTheme
        {
            get { return m_progrLevel.ColorThemeType; }
            set { m_progrLevel.ColorThemeType = value; }
        }

        //desired volume
        public int Volume
        {
            get { return m_trackVolume.Value; }
            set { m_trackVolume.Value = value; }
        }

        //actual device level
        public int Level
        {
            get { return m_progrLevel.Value; }
            set { m_progrLevel.Value = value; }
        }

        private void m_trackVolume_Scroll(object sender, EventArgs e)
        {
            Device.AudioEndpointVolume.MasterVolumeLevelScalar = m_trackVolume.Value / 100f;
            OnVolumeChanged(m_trackVolume.Value / 100f);
            UpdateUI();
            //SystemSounds.Beep.Play();
        }

        private void m_trackVolume_ValueChanged(object sender, EventArgs e)
        {
            OnVolumeChanged(m_trackVolume.Value / 100f);
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

            string mute = Device.AudioEndpointVolume.Mute ? "Muted: " : "Mute: ";
            toolTip1.SetToolTip(m_btnMute, mute + Device.FriendlyName);

            //microphone
            if (Device.DataFlow == EDataFlow.Capture)
            {
                m_btnMute.ImageIndex = Device.AudioEndpointVolume.Mute ? 1 : 0;
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

                if (Device.AudioEndpointVolume.Mute)
                    m_btnMute.ImageIndex = 3;
            }
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
