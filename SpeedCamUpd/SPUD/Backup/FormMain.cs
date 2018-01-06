using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Collections;

namespace SPUD
{
  public partial class FormMain : Form
  {
    private static CultureInfo m_CultureInfo = new CultureInfo("en-US");

    public FormMain()
    {
      InitializeComponent();
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
      m_dataGridViewSpeed.DataSource = new BindingList<SpeedCam>();
      for (int col = 0; col < m_dataGridViewSpeed.ColumnCount; col++)
      {
        m_dataGridViewSpeed.Columns[col].Width = SpeedCam.GetColumnWidth(col);
      }//end for

      //
      m_cmbState.Items.Add(SpeedCam.RecordTypes.New);
      m_cmbState.Items.Add(SpeedCam.RecordTypes.Edited);
      m_cmbState.Items.Add(SpeedCam.RecordTypes.Deleted);

      //
      m_cmbType.Items.Add(SpeedCam.CameraTypes.FixedSpeedcam);
      m_cmbType.Items.Add(SpeedCam.CameraTypes.MobileSpeedcam);
      m_cmbType.Items.Add(SpeedCam.CameraTypes.RedLightCam);
      m_cmbType.Items.Add(SpeedCam.CameraTypes.SectionSpeedcam);
      m_cmbType.Items.Add(SpeedCam.CameraTypes.TrafficLight);

      //
      m_cmbDirection.Items.Add(SpeedCam.CameraDirection.AllDirections);
      m_cmbDirection.Items.Add(SpeedCam.CameraDirection.Unidirectional);
      m_cmbDirection.Items.Add(SpeedCam.CameraDirection.BiDirectional);
    }

    private bool Browse()
    {
      m_openFileDialog.FileName = m_txtFileName.Text;
      if (DialogResult.OK != m_openFileDialog.ShowDialog(this))
        return false;
      m_txtFileName.Text = m_openFileDialog.FileName;
      return true;
    }

    private void m_mnuOpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if ( !Browse() )
        return;

      List<SpeedCam> list = spud.Import_MioMap(m_txtFileName.Text);
      m_dataGridViewSpeed.DataSource = new BindingList<SpeedCam>(list);
    }

    private void m_mnuSaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(m_txtFileName.Text))
        m_saveFileDialog.FileName = m_txtFileName.Text;
      else
        m_saveFileDialog.FileName = "SpeedcamUpdates.spud";

      if (DialogResult.OK != m_saveFileDialog.ShowDialog(this))
        return;
      m_txtFileName.Text = m_saveFileDialog.FileName;

      BindingList<SpeedCam> list = m_dataGridViewSpeed.DataSource as BindingList<SpeedCam>;
      try
      {
        spud.Save_MioMap(list, m_txtFileName.Text, m_chkDeleted.Checked);
      }//end try
      catch (Exception err)
      {
        MessageBox.Show(err.Message, "Unexpected save error", 
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }//end catch
    }

    private void m_mnu_AddToolStripMenuItem_Click(object sender, EventArgs e)
    {
      BindingList<SpeedCam> list = m_dataGridViewSpeed.DataSource as BindingList<SpeedCam>;
      if (list == null)
        return;

      list.Add(new SpeedCam(-1));
      m_dataGridViewSpeed.CurrentCell = m_dataGridViewSpeed.Rows[list.Count-1].Cells[3];
    }

    private void m_mnuDeleteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      BindingList<SpeedCam> list = m_dataGridViewSpeed.DataSource as BindingList<SpeedCam>;
      if (list == null || m_dataGridViewSpeed.CurrentRow == null)
        return;

      int idx = m_dataGridViewSpeed.CurrentRow.Index;
      if (idx < 0 || idx > list.Count - 1 )
        return;

      list.RemoveAt(idx);
    }

    private void m_dataGridViewSpeed_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {

    }

    /// <summary>
    /// Save selected record with new data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void m_btnSaveRow_Click(object sender, EventArgs e)
    {
      UpdateDataFromControls(false);
    }

    /// <summary>
    /// Add new record with filled data from current selection
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void m_btnAddRow_Click(object sender, EventArgs e)
    {
      UpdateDataFromControls(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>row index</returns>
    private SpeedCam UpdateDataFromControls(bool bCreateNew)
    {
      if (m_dataGridViewSpeed.CurrentCell == null)
        return null;

      try
      {
        double lon = Convert.ToDouble(m_txtLong.Text, m_CultureInfo);
        double lat = Convert.ToDouble(m_txtLat.Text, m_CultureInfo);

        BindingList<SpeedCam> list = m_dataGridViewSpeed.DataSource as BindingList<SpeedCam>;
        SpeedCam cam = list[m_dataGridViewSpeed.CurrentCell.RowIndex];
        if ( bCreateNew ) cam = new SpeedCam(cam);

        cam.sFlag = (SpeedCam.RecordTypes)m_cmbState.SelectedItem;

        cam.Latitude = lat;
        cam.Longtitude = lon;

        cam.sType = (SpeedCam.CameraTypes)m_cmbType.SelectedItem;
        cam.sDirection = (SpeedCam.CameraDirection)m_cmbDirection.SelectedItem; //????

        if (bCreateNew)
        {
          list.Add(cam);
          int idxLastRow = m_dataGridViewSpeed.Rows.Count - 1;
          m_dataGridViewSpeed.CurrentCell = m_dataGridViewSpeed.Rows[idxLastRow].Cells[3];
        }//end if

        m_dataGridViewSpeed.Refresh();
       
        return (cam);
      }//end try
      catch (Exception err)
      {
        System.Diagnostics.Debug.WriteLine("GetDataFromControls() Error: " + err.Message, "Error");
        return null;
      }//end catch
    }

    private void m_dataGridViewSpeed_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
      OnSelectedRowChanged(e.RowIndex);
    }

    private void OnSelectedRowChanged(int row)
    {
      m_lblState.Text = "State";
      BindingList<SpeedCam> list = m_dataGridViewSpeed.DataSource as BindingList<SpeedCam>;
      if (list == null || row < 0)
        return;

      SpeedCam cam = list[row];
      m_lblState.Text = string.Format("State of index {0}", cam.Index);
      m_cmbState.SelectedItem = cam.sFlag;

      m_txtLat.Text = cam.Latitude.ToString(m_CultureInfo);
      m_txtLong.Text = cam.Longtitude.ToString(m_CultureInfo);

      m_cmbType.SelectedItem = cam.sType;
      m_cmbDirection.SelectedItem = cam.sDirection;
    }

    private void m_txtLong_TextChanged(object sender, EventArgs e)
    {
      ShowPointOnMap();
    }

    private void m_txtLat_TextChanged(object sender, EventArgs e)
    {
      ShowPointOnMap();
    }

    private void ShowPointOnMap()
    {
      try
      {
        double lon = Convert.ToDouble(m_txtLong.Text, m_CultureInfo);
        double lat = Convert.ToDouble(m_txtLat.Text, m_CultureInfo);

        m_lblPos.Location = LocationFromLongLat(lon, lat);
      }//end try
      catch (Exception err)
      {
        System.Diagnostics.Debug.WriteLine("ShowPointOnMap Error: " + err.Message);
      }//end catch
    }

    private Point LocationFromLongLat(double longtitude, double latitude)
    {
      double x = 34.00;
      double kX = 122.5;
      
      double y = 33.28;
      double kY = 137.6;

      Point pt = new Point((int)((longtitude-x)*kX), (int)((y-latitude)*kY));
      return pt;
    }

    private void m_lblPos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      try
      {
        string sUrlFmt = @"http://maps.google.com/maps?f=q&source=s_q&hl=en&q={0},+{1}&vps=3&jsv=160h&sll={1},{0}&sspn=0.716963,1.219482&ie=UTF8&lr=lang_en&geocode=FWYj9AEdWGMVAg&split=0";
        double lon = Convert.ToDouble(m_txtLong.Text, m_CultureInfo);
        double lat = Convert.ToDouble(m_txtLat.Text, m_CultureInfo);

        OpenUrl(string.Format(sUrlFmt, lat.ToString(m_CultureInfo), lon.ToString(m_CultureInfo)));
      }//end try
      catch (Exception err)
      {
        System.Diagnostics.Debug.WriteLine("Pos_LinkClicked Error: " + err.Message);
      }//end catch
    }

    public static void OpenUrl(string sUrl)
    {
      System.Diagnostics.Process proc = new System.Diagnostics.Process();
      proc.StartInfo.FileName = "iexplore";
      proc.StartInfo.Arguments = sUrl;
      proc.StartInfo.UseShellExecute = true;
      proc.Start();
    }

    private void m_mnu_AboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      new FormAbout().ShowDialog();
    }

    private void m_mnuExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    //sort
    private void m_dataGridViewSpeed_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      BindingList<SpeedCam> list = m_dataGridViewSpeed.DataSource as BindingList<SpeedCam>;
      if (list == null)
        return;

      //reset Glyph on previous column
      m_dataGridViewSpeed.Columns[SpeedCamComparer.m_sortColumn].HeaderCell.SortGlyphDirection = SortOrder.None;

      //set new column Glyph
      SpeedCamComparer comparer = new SpeedCamComparer(e.ColumnIndex);
      m_dataGridViewSpeed.Columns[SpeedCamComparer.m_sortColumn].HeaderCell.SortGlyphDirection = SpeedCamComparer.m_sortOrder;

      //remember cell before sort
      int row = m_dataGridViewSpeed.CurrentCell.RowIndex;
      int col = m_dataGridViewSpeed.CurrentCell.ColumnIndex;
      int index = list[row].Index;
      
      ArrayList.Adapter(list).Sort(comparer);
      
      //restore selection after sort
      row = FindSpeedCamIndex(index, list);
      m_dataGridViewSpeed.CurrentCell = m_dataGridViewSpeed.Rows[row].Cells[col];
    }

    private int FindSpeedCamIndex(int index, BindingList<SpeedCam> list)
    {
      for (int i = 0; i < list.Count; i++)
      {
        if (list[i].Index == index)
          return i;
      }//end for
      return -1;
    }

  }
}