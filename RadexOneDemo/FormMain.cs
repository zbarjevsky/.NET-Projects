using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using sD;
using RadexOneDemo.Docs;
using System.Threading;
using System.Runtime.InteropServices;

namespace RadexOneDemo
{
    public partial class FormMain : Form
    {
        private double _maxCPM = 0;
        private double _maxLive = 0;
        private DateTime _maxTimeCPM, _maxTimeDOSE;
        private readonly RadexOneConnection _radex = new RadexOneConnection();
        private readonly Image _radiationOn, _radiationOff, _radiationWarning, _connectUsb;
        private readonly PlaySound player;
        private Task _connectionCheckTask;
        //private Color _bkColor = Color.Green;

        private readonly List<ChartPoint> _history = new List<ChartPoint>(2048);

        public FormMain()
        {
            LogReader.Test();

            InitializeComponent();

            _radiationOn = Properties.Resources.radiation_symbol;
            _radiationOff = Properties.Resources.OkEmoji;
            _radiationWarning = Properties.Resources.WarningEmoji;
            _connectUsb = Properties.Resources.UsbConnect3;

            m_picYellowLight.Image = _connectUsb;

            m_chkPause.Checked = _radex.Pause;

            m_numMaxCPM.Value = Properties.Settings.Default.MaxCPM;// _radex.AlertCPM;
            m_chkAutoConnect.Checked = Properties.Settings.Default.AutoConnect;
            m_numInterval.Value = Properties.Settings.Default.Interval;

            player = new PlaySound();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _radex.LightOn = () =>
            {
                m_picYellowLight.Image = _radiationOn;
                player.Play();
            };

            _radex.LightOff = () =>
            {
                m_picYellowLight.Image = _radiationOff;
                player.Stop();
            };

            _radex.DataReceived = (cmd) =>
            {
                Utils.ExecuteOnUiThreadBeginInvoke(this, () =>
                {
                    OnDataReceived(cmd);
                });
            };

            _radex.VerReceived = (cmd) =>
            {
                Utils.ExecuteOnUiThreadBeginInvoke(this, () =>
                {
                    m_lblSN.Text = cmd.SerialNumber;
                });
            };

            _radex.CfgReceived = (cmd) =>
            {
                Utils.ExecuteOnUiThreadBeginInvoke(this, () =>
                {
                    m_chkSnd.Checked = cmd.Sound;
                    m_chkVib.Checked = cmd.Vibrate;
                    m_numLimit.Value = (decimal)cmd.Threshold;
                });
            };

            _radex.DisconnectEvent = (reason) =>
            {
                Utils.ExecuteOnUiThreadBeginInvoke(this, () =>
                {
                    m_picYellowLight.Image = _connectUsb;
                    UpdateStatus();
                    //if (!string.IsNullOrWhiteSpace(reason))
                    //    MessageBox.Show(this, "Disconnected\n" + reason, "Connection error",
                    //        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                });
            };

            _connectionCheckTask = Task.Run(() =>
            {
                while (_checkConnected)
                {
                    ConnectIfAvailable();
                    Thread.Sleep(1000);
                }
            });
        }

        private bool _checkConnected = true;
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _checkConnected = false;
            _connectionCheckTask.Wait(2000);

            //save settings/config
            Properties.Settings.Default.Interval = m_numInterval.Value;
            Properties.Settings.Default.MaxCPM = _radex.AlertCPM;
            Properties.Settings.Default.AutoConnect = m_chkAutoConnect.Checked;
            Properties.Settings.Default.Save();

            _radex.Close();
            Log.Close();
        }

        private void m_chkConnect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_chkConnect.Checked)
                {
                    ConnectIfAvailable();
                }
                else
                {
                    _radex.Close();
                    m_chkConnect.Text = "Closed";
                }
            }
            catch (Exception err)
            {
                m_chkConnect.Checked = false;
                MessageBox.Show(err.Message, "Connect()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            m_btnRequest.Enabled = m_chkConnect.Checked;
        }

        private void ConnectIfAvailable()
        {
            if (_radex.IsOpen)
                return;

            Utils.ExecuteOnUiThreadBeginInvoke(this, () =>
            {
                if (m_cmbDevices.SelectedItem != null)
                {
                    m_chkConnect.Enabled = true;
                    string device = m_cmbDevices.SelectedItem.ToString();
                    string comPort = device.Substring(0, 5).Trim();

                    //reconnect
                    if (_radex.RadexPort != comPort || (!_radex.IsOpen && m_chkAutoConnect.Checked))
                    {
                        try
                        {
                            _radex.Open(comPort);

                            m_btnRequest_Click(this, null);
                            m_btnGetVer_Click(this, null);
                            m_btnGetSett_Click(this, null);

                            m_chkConnect.Text = "Opened";
                            m_chkConnect.Checked = true;

                            m_picYellowLight.Image = Properties.Resources.OkEmoji;
                        }
                        catch (Exception err)
                        {
                            Log.WriteLine("Open exception: "+err.Message);
                            m_chkConnect.Checked = false;
                            m_chkConnect.Text = err.Message;
                            m_picYellowLight.Image = _connectUsb;
                        }
                    }
                }
                else
                {
                    m_chkConnect.Text = "Unavailable";
                    m_chkConnect.Checked = false;
                    m_chkConnect.Enabled = false;
                }

                UpdateStatus();
            });
        }

        private void UpdateStatus()
        {
            Utils.ExecuteOnUiThreadBeginInvoke(this, () =>
            {
                List<string> radexPorts = RadexComPort.RadexPortInfos();
                UpdateCmbPorts(radexPorts);
                if (radexPorts.Count > 0)
                {
                    UpdateIfChanged(m_status1, "Device in: " + radexPorts[0]);
                }
                else
                {
                    UpdateIfChanged(m_status1, "No Radex device plugged in.");
                }

                string port = _radex.RadexPort;
                if (port != null)
                {
                    EnableCommands(_radex.IsOpen);
                    UpdateIfChanged(m_status2, _radex.IsOpen ? " - Connected." : " - Disconnected.");
                }
                else
                {
                    m_picYellowLight.Image = _connectUsb;
                    EnableCommands(false);
                    UpdateIfChanged(m_status2, "...");
                }
            });
        }

        private void EnableCommands(bool bEnable = true)
        {
            m_btnVer.Enabled = bEnable;
            m_btnVer2.Enabled = bEnable;
            m_btnRequest.Enabled = bEnable;
            m_btnResetDose.Enabled = bEnable;
            m_btnSet.Enabled = bEnable;
            m_btnTest.Enabled = bEnable;
            m_chkSnd.Enabled = bEnable;
            m_chkVib.Enabled = bEnable;
            m_numLimit.Enabled = bEnable;
            m_progressMain.Enabled = bEnable;
            m_chart1.Enabled = bEnable;
            m_lblSN.Enabled = bEnable;

            if (bEnable == false)
            {
                m_progressMain.Value = 0;
                m_picYellowLight.BackColor = SystemColors.Control;
                m_splitContainerTools.Panel1.BackColor = SystemColors.Control;
            }
        }

        private void UpdateIfChanged(ToolStripStatusLabel txt, string text)
        {
            if(txt.Text == text)
                return;
            txt.Text = text;
        }

        private void UpdateCmbPorts(List<string> radexPorts)
        {
            bool bUpdated = false;
            if (radexPorts.Count == m_cmbDevices.Items.Count)
            {
                for (int i = 0; i < radexPorts.Count; i++)
                {
                    if (string.CompareOrdinal(radexPorts[i], m_cmbDevices.Items[i].ToString()) != 0)
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

        private void m_btnRequest_Click(object sender, EventArgs e)
        {
            try
            {
                _radex.RequestData();
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
                _radex.RequestVer();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "RequestVer()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_btnGetSett_Click(object sender, EventArgs e)
        {
            try
            {
                _radex.RequestSettings0();
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
                _radex.RequestSetSettings(m_chkSnd.Checked, m_chkVib.Checked, (double)(m_numLimit.Value));
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
                _radex.RequestResetDose();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "RequestResetDose()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void m_chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            _radex.Pause = m_chkPause.Checked;
        }

        private void m_btnClear_Click(object sender, EventArgs e)
        {
            m_txtLog.Text = "";
            _maxCPM = 0;
            _maxLive = 0;
            ChartHelper.ClearChart(m_chart1);
        }

        private void m_numMaxCPM_ValueChanged(object sender, EventArgs e)
        {
            _radex.AlertCPM = (int) m_numMaxCPM.Value;
        }

        private void m_numInterval_ValueChanged(object sender, EventArgs e)
        {
            _radex.Interval = (int)(m_numInterval.Value * 1000);
            //ensure valid
            m_numInterval.Value = (decimal)(_radex.Interval / 1000.0);
        }

        private void OnDataReceived(CommandGetData cmd)
        {
            if (cmd.CPM > _maxCPM)
            {
                _maxCPM = cmd.CPM;
                _maxTimeCPM = DateTime.Now;

                UpdateMaxRecord(true);
            }

            if (cmd.DOSE > _maxLive)
            {
                _maxLive = cmd.DOSE;
                _maxTimeDOSE = DateTime.Now;

                UpdateMaxRecord(true);
            }

            ChartPoint pt = new ChartPoint() {CPM = cmd.CPM, DOSE = cmd.DOSE};
            _history.Add(pt);
            if(_history.Count > 1024 * 1024) //max size 1M
                _history.RemoveAt(0);

            m_lblVal.Text = cmd.DOSE.ToString("0.00");
            m_lblWarn.Text = cmd.CPM.ToString();

            ChartHelper.AddPointXY(m_chart1, "SeriesCPM", pt.CPM, pt.date);
            ChartHelper.AddPointXY(m_chart1, "SeriesDOSE", pt.DOSE, pt.date);

            int progress = (int)((100 * cmd.CPM) / (2 * m_numMaxCPM.Value));
            if (progress > 100) progress = 100;
            if (progress < 1) progress = 1;
            m_progressMain.Value = progress;

            m_picYellowLight.BackColor = Utils.GetBlendedColor(100 - progress);
            m_splitContainerTools.Panel1.BackColor = m_picYellowLight.BackColor;

            if (cmd.CPM < (int)m_numMaxCPM.Value)
            {
                m_lblVal.BackColor = Color.Chartreuse;
                m_lblWarn.BackColor = Color.LightGray;
                m_picYellowLight.Image = _radiationOff;
                m_progressMain.SetState(ModifyProgressBarColor.State.Green);
            }
            else
            {
                UpdateMaxRecord(false);

                m_lblVal.BackColor = Color.Yellow;
                m_lblWarn.BackColor = m_lblWarn.BackColor == Color.Yellow ? Color.Gray : Color.Yellow;

                if (m_progressMain.Value < 75)
                {
                    m_progressMain.SetState(ModifyProgressBarColor.State.Yellow);
                    m_picYellowLight.Image = _radiationWarning;
                }
                else
                {
                    m_progressMain.SetState(ModifyProgressBarColor.State.Red);
                }
            }

            string stat = string.Format("{0:0000}. DOSE: {1:0.00} µSv/h, CPM: {2} min−1, Accumulated: {3:0.0} µSv, Alert: {4}\r\n",
                cmd.cmdId, cmd.DOSE, cmd.CPM, cmd.SUM, _radex.IsAlertOn);
            m_txtStatus.Text = stat;
            m_txtStatus.Text += FormatMaxRecord(); 

            if (m_chkShowLog.Checked)
            {
                stat += m_txtLog.Text;
                if (stat.Length > 4000)
                    stat = stat.Substring(0, 4000);
                m_txtLog.Text = stat;
            }
        }

        private string FormatMaxRecord()
        {
            const string DATE_FMT = "yyyy-MM-dd HH:mm:ss";

            if (_maxLive == 0.0 || _maxCPM == 0)
                return "";

            return string.Format("Max: {0} µSv/h,\t\tMax Rate: {1} pt/min\r\n{2}\t{3}\n",
                _maxLive, _maxCPM, _maxTimeDOSE.ToString(DATE_FMT), _maxTimeCPM.ToString(DATE_FMT));
        }

        private void m_btnHistory_Click(object sender, EventArgs e)
        {
            FormHistory frm = new FormHistory(_history);
            frm.ShowDialog(this);
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
                _radex.RequestTestCmd();
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
