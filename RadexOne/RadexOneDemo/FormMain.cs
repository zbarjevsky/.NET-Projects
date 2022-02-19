using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RadexOneDemo.Docs;
using System.Threading;
using System.Runtime.InteropServices;
using RadexOneLib;

namespace RadexOneDemo
{
    public partial class FormMain : Form
    {
        private double _maxCPM = 0;
        private double _maxLive = 0;
        private DateTime _maxCPMTime, _maxDOSETime;

        public const string TITLE = "Radex Demo v1.01";

        private readonly RadexOneConnection _radexDevice = new RadexOneConnection();
        private List<RadexComPortDesc> _radexPorts = new List<RadexComPortDesc>();
        private readonly RadexOneConfig _radexConfig = new RadexOneConfig();

        private readonly Image _radiationOn, _radiationOff, _radiationWarning, _connectUsb;
        private readonly PlaySound _player;
        private Task _connectionCheckTask;
        //private Color _bkColor = Color.Green;
        private readonly AlertManager _alertManager = new AlertManager();

        private readonly RadiationLog _history = new RadiationLog();

        public FormMain()
        {
            LogReader.Test();

            InitializeComponent();

            this.Text = TITLE;

            _radiationOn = Properties.Resources.radiation_symbol;
            _radiationOff = Properties.Resources.OkEmoji;
            _radiationWarning = Properties.Resources.WarningEmoji;
            _connectUsb = Properties.Resources.UsbConnect3;

            m_picRadiationStatus.Image = _connectUsb;

            m_chkAutoRequestData.Checked = !_radexDevice.Pause;

            m_numMaxCPM.Value = Properties.Settings.Default.MaxCPM;// _radex.AlertCPM;
            m_chkAutoConnect.Checked = Properties.Settings.Default.AutoConnect;
            m_numInterval.Value = Properties.Settings.Default.Interval;
            m_trackAlertVolume.Value = Properties.Settings.Default.Volume;

            _player = new PlaySound(this, m_trackAlertVolume.Value, m_trackAlertVolume.Maximum);

            _alertManager.OnStateChanged = UpdateAlertState;

            richTextBox1.Rtf = Properties.Resources.How_much_is_dangerous;

            _radexConfig.PropertyChanged += RadexConfig_PropertyChanged;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _history.Load();
            m_chart1.Set(_history.Log, true, true);
            m_listLog.UpdateLog(_history.Log);

            _radexDevice.DataReceived = (cmd) =>
            {
                Utils.ExecuteOnUiThreadInvoke(this, () =>
                {
                    OnDataReceived(cmd);
                });
            };

            _radexDevice.VerReceived = (cmd) =>
            {
                Utils.ExecuteOnUiThreadInvoke(this, () =>
                {
                    _radexConfig.SetVersion(cmd);
                    _history.UpdateSerialNumber(cmd.SerialNumber);
                    m_lblSN.Text = _radexConfig.SerialNumber.ToString();
                });
            };

            _radexDevice.CfgReceived = (cmd) =>
            {
                Utils.ExecuteOnUiThreadInvoke(this, () =>
                {
                    _radexConfig.SetSettings(cmd);
                    _isSettingsChanged = false;
                });
            };

            _radexDevice.DisconnectEvent = (reason) =>
            {
                Utils.ExecuteOnUiThreadInvoke(this, () =>
                {
                    m_picRadiationStatus.Image = _connectUsb;
                    UpdateStatusBar();
                });
            };

            _connectionCheckTask = Task.Run(() =>
            {
                while (!_cancel)
                {
                    try
                    {
                        UpdateStatusBar();

                        _radexPorts = RadexComPortDesc.RadexPortInfos();

                        bool autoConnect = Utils.ExecuteOnUiThreadInvoke(this, () => { return m_chkAutoConnect.Checked; });
                        if(autoConnect)
                            ConnectToSelectedPort();
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine(err.ToString());
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private volatile bool _cancel = false;
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            _cancel = true;
            _connectionCheckTask.Wait(2000);

            //save settings/config
            Properties.Settings.Default.Interval = m_numInterval.Value;
            Properties.Settings.Default.MaxCPM = _alertManager.AlertCPM;
            Properties.Settings.Default.AutoConnect = m_chkAutoConnect.Checked;
            Properties.Settings.Default.Volume = m_trackAlertVolume.Value;

            Properties.Settings.Default.Save();

            _radexDevice.Close();
            _history.Save();
            Log.Close();
        }

        private void m_btnConnect_Click(object sender, EventArgs e)
        {
            ConnectToSelectedPort();
        }

        private void m_btnDisconnect_Click(object sender, EventArgs e)
        {
            m_chkAutoConnect.Checked = false;
            _radexDevice.Close();
            m_lblConnectStatus.Text = "Device Disconnected";
        }

        private void m_chkAutoConnect_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void m_cmbDevices_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (m_cmbDevices.SelectedItem == null)
                return;

            RadexComPortDesc selectedDevice = m_cmbDevices.SelectedItem as RadexComPortDesc;
            if (selectedDevice == null)
                return;

            if (_radexDevice.IsOpen && _radexDevice.PortName != selectedDevice.Port)
            {

            }
        }

        private void ConnectToSelectedPort()
        {
            Utils.ExecuteOnUiThreadInvoke(this, () =>
            {
                if (m_cmbDevices.SelectedItem != null)
                {
                    m_lblConnectStatus.Enabled = true;
                    RadexComPortDesc selectedDevice = m_cmbDevices.SelectedItem as RadexComPortDesc;

                    if (_radexDevice.IsOpen && selectedDevice.Port == _radexDevice.PortName)
                        return;

                    try
                    {
                        _radexDevice.Open(selectedDevice.Port);

                        m_btnRequest_Click(this, null);
                        m_btnGetVer_Click(this, null);
                        m_btnGetSett_Click(this, null);

                        m_lblConnectStatus.Text = "Connected Device: " + selectedDevice.Desc;

                        m_picRadiationStatus.Image = Properties.Resources.OkEmoji;
                    }
                    catch (Exception err)
                    {
                        Log.WriteLine("Open exception: " + err.Message);
                        m_lblConnectStatus.Text = "Error: " + err.Message;
                        m_picRadiationStatus.Image = _connectUsb;
                    }
                }
                else
                {
                    m_lblConnectStatus.Text = "Device Not Connected";
                }
            });
        }

        private void UpdateStatusBar()
        {
            Utils.ExecuteOnUiThreadInvoke(this, () =>
            {
                UpdateCmbPorts(_radexPorts);
                if (_radexPorts != null && _radexPorts.Count > 0)
                {
                    UpdateIfChanged(m_status1, "Device in: " + _radexPorts[0]);
                }
                else
                {
                    UpdateIfChanged(m_status1, "No Radex device plugged in.");
                }

                string port = _radexDevice.PortName;
                if (port != null)
                {
                    UpdateUI(_radexDevice.IsOpen);
                    UpdateIfChanged(m_status2, _radexDevice.IsOpen ? " - Connected." : " - Disconnected.");
                }
                else
                {
                    m_picRadiationStatus.Image = _connectUsb;
                    UpdateUI(false);
                    UpdateIfChanged(m_status2, "...");
                }
            });
        }

        private void UpdateAlertState(AlertManager.AlertState state)
        {
            if (state == AlertManager.AlertState.Good)
            {
                //m_progressMain.SetState(ModifyProgressBarColor.State.Green);

                m_lblVal.BackColor = Color.Chartreuse;
                m_lblCPM.BackColor = Color.LightGray;

                m_picRadiationStatus.Image = _radiationOff;

                _player.Stop();
            }
            else if (state == AlertManager.AlertState.Warning || state == AlertManager.AlertState.CoolingDown)
            {
                //m_progressMain.SetState(ModifyProgressBarColor.State.Yellow);

                m_lblVal.BackColor = Color.Chartreuse;
                m_lblCPM.BackColor = Color.Yellow;

                m_picRadiationStatus.Image = _radiationWarning;

                _player.Stop();
            }
            else //if(state == AlertManager.AlertState.Alert)
            {
                UpdateMaxRecord(false); //record alert value

                //m_progressMain.SetState(ModifyProgressBarColor.State.Red);

                m_lblVal.BackColor = Color.OrangeRed;
                m_lblCPM.BackColor = Color.OrangeRed;

                m_picRadiationStatus.Image = _radiationOn;

                _player.Play();
            }
        }

        private void UpdateUI(bool isDeviceConnected = true)
        {
            m_btnVer.Enabled = isDeviceConnected;
            m_btnReadConfig.Enabled = isDeviceConnected;
            m_btnRequestData.Enabled = isDeviceConnected;
            m_btnResetDose.Enabled = isDeviceConnected;
            m_btnTest.Enabled = isDeviceConnected;
            m_progressMain.Enabled = isDeviceConnected;
            m_chart1.Enabled = isDeviceConnected;
            m_lblSN.Enabled = isDeviceConnected;

            m_cmbDevices.Enabled = !isDeviceConnected; //disable when connected

            m_btnConnect.Enabled = !isDeviceConnected && m_cmbDevices.SelectedItem != null;
            m_btnDisconnect.Enabled = isDeviceConnected;
            m_btnDeviceConfig.Enabled = isDeviceConnected;
            m_mnuDeviceConfiguration.Enabled = isDeviceConnected;

            propertyGrid1.Enabled = isDeviceConnected;

            m_btnWriteConfig.Enabled = isDeviceConnected && _isSettingsChanged;

            if (isDeviceConnected == false)
            {
                m_progressMain.Value = 0;
                m_picRadiationStatus.BackColor = SystemColors.Control;
                m_splitContainerTools.Panel1.BackColor = SystemColors.Control;
            }
        }

        private void UpdateIfChanged(ToolStripStatusLabel txt, string text)
        {
            if(txt.Text == text)
                return;
            txt.Text = text;
        }

        private void UpdateCmbPorts(List<RadexComPortDesc> radexPorts)
        {
            bool bUpdated = false;
            if (radexPorts.Count == m_cmbDevices.Items.Count)
            {
                for (int i = 0; i < radexPorts.Count; i++)
                {
                    RadexComPortDesc desc = m_cmbDevices.Items[i] as RadexComPortDesc;
                    if (string.CompareOrdinal(radexPorts[i].Port, desc.Port) != 0)
                    {
                        m_cmbDevices.Items[i] = radexPorts[i];
                        bUpdated = true;
                    }
                }
            }
            else
            {
                m_cmbDevices.Items.Clear();
                m_cmbDevices.Text = "";
                m_cmbDevices.Items.AddRange(radexPorts.ToArray());
                bUpdated = true;
            }

            if (bUpdated && m_cmbDevices.Items.Count > 0)
                m_cmbDevices.SelectedIndex = 0;
        }

        private bool _isSettingsChanged = false;
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            _isSettingsChanged = true;
        }

        private void RadexConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            propertyGrid1.SelectedObject = _radexConfig;
        }

        private void m_btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                _radexDevice.SendRequestData();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "RequestData()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_btnGetVer_Click(object sender, EventArgs e)
        {
            try
            {
                _radexDevice.SendRequestVer();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "RequestVer()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_mnuDeviceConfiguration_Click(object sender, EventArgs e)
        {
            if (!_radexDevice.IsOpen)
                return;

            FormRadexOneConfig.ShowConfig(this, _radexDevice, _radexConfig);
        }

        private void m_btnGetSett_Click(object sender, EventArgs e)
        {
            try
            {
                _radexDevice.SendRequestGetSettings();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "RequestSettings0()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                _radexDevice.SendRequestSetSettings(_radexConfig.Sound, _radexConfig.Vibrate, _radexConfig.Threshold);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "RequestSetSettings()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_btnResetDose_Click(object sender, EventArgs e)
        {
            try
            {
                _radexDevice.RequestResetDose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "RequestResetDose()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            _radexDevice.Pause = !m_chkAutoRequestData.Checked;
        }

        private void m_btnClear_Click(object sender, EventArgs e)
        {
            m_listLog.Clear();
            _maxCPM = 0;
            _maxLive = 0;
            _history.Clear();
            m_chart1.ClearChart();
        }

        private void m_numMaxCPM_ValueChanged(object sender, EventArgs e)
        {
            _alertManager.AlertCPM = (int) m_numMaxCPM.Value;
        }

        private void m_numInterval_ValueChanged(object sender, EventArgs e)
        {
            _radexDevice.Interval = (int)(m_numInterval.Value * 1000);
            //ensure valid
            m_numInterval.Value = (decimal)(_radexDevice.Interval / 1000.0);
        }

        TimeSpan _interval = TimeSpan.FromMinutes(40);
        private void OnDataReceived(CommandGetData cmd)
        {
            if (cmd.CPM == 0 || cmd.RATE == 0)
                return;

            if (cmd.CPM > _maxCPM)
            {
                _maxCPM = cmd.CPM;
                _maxCPMTime = DateTime.Now;

                UpdateMaxRecord(true);
            }

            if (cmd.RATE > _maxLive)
            {
                _maxLive = cmd.RATE;
                _maxDOSETime = DateTime.Now;

                UpdateMaxRecord(true);
            }

            RadiationDataPoint pt = new RadiationDataPoint()
            {
                CPM = cmd.CPM,
                RATE = cmd.RATE,
                DOSE = cmd.DOSE,
                Threshold = (double)m_numMaxCPM.Value
            };

            _history.Log.Add(pt);
            if(_history.Log.Count > 1024 * 1024) //max size 1M
                _history.Log.RemoveAt(0);

            m_lblVal.Text = cmd.RATE.ToString("0.00");
            m_lblCPM.Text = cmd.CPM.ToString();

            if (m_chkUseConverter.Checked)
                radiationConverterControl1.ValueFrom = cmd.RATE;

            _radexConfig.Dose = cmd.DOSE;

            m_chart1.Set(_history.Log, true, true);

            int progress = (int)((100 * cmd.CPM) / (2 * m_numMaxCPM.Value));
            if (progress > 100) progress = 100;
            if (progress < 1) progress = 1;
            m_progressMain.Value = progress;

            m_picRadiationStatus.BackColor = Utils.GetBlendedColor(100 - progress);
            m_splitContainerTools.Panel1.BackColor = m_picRadiationStatus.BackColor;

            _alertManager.AnalyseSignal(cmd);

            string stat = string.Format("{0}  - {1:0000}. Rate: {2:0.00} µSv/h, CPM: {3}, Dose: {4:0.00} µSv, Level: {5}\r\n",
                pt.date.ToString("s"), cmd.cmdId, cmd.RATE, cmd.CPM, cmd.DOSE, _alertManager.AlertInfo);

            m_notifyIconSysTray.Text = string.Format("Rate: {0:0.00} µSv/h, CPM: {1}", cmd.RATE, cmd.CPM);

            m_txtStatus.Text = stat;
            m_txtStatus.Text += FormatMaxRecord(); 

            m_listLog.UpdateLog(_history.Log, m_chkAutoUpdateLog.Checked);
        }

        private string FormatMaxRecord()
        {
            const string DATE_FMT = "yyyy-MM-dd HH:mm:ss";

            if (_maxLive == 0.0 || _maxCPM == 0)
                return "";

            return string.Format("Max Rate: {0} µSv/h,\t\tMax CPM: {1}\r\n{2}\t\t{3}\n",
                _maxLive, _maxCPM, _maxDOSETime.ToString(DATE_FMT), _maxCPMTime.ToString(DATE_FMT));
        }

        private void m_btnHistory_Click(object sender, EventArgs e)
        {
            FormHistory frm = new FormHistory(_history);
            frm.ShowDialog(this);
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }

        private void m_trackAlertVolume_ValueChanged(object sender, EventArgs e)
        {
            m_lblAlertVolume.Text = string.Format("{0:0.0}%", m_trackAlertVolume.Value / 10.0);
            if (_player != null)
                _player.Volume = m_trackAlertVolume.Value;
        }

        private void m_mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_mnuShow_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
        }

        private DateTime _lastUpdateRecord = DateTime.MinValue;
        private void UpdateMaxRecord(bool isRecord) //new record or exceeded limit
        {
            TimeSpan ts = DateTime.Now - _lastUpdateRecord;
            _lastUpdateRecord = DateTime.Now;
            if(!isRecord && ts.TotalMilliseconds>100 && ts.TotalMilliseconds<60000)
                return; //do not update if in the same minute

            string record = FormatMaxRecord();
            if(string.IsNullOrEmpty(record))
                return;

            string prefix = isRecord?"+ ":"* ";
            m_txtRecords.Text = prefix + m_txtRecords.Text.Insert(0, record);
        }

        private void m_btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                _radexDevice.RequestTestCmd();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "RequestSettings0()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAboutLevels frm = new FormAboutLevels();
            frm.ShowDialog(this);
        }
    }
}
