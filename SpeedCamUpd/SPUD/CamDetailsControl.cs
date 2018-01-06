using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SPUD
{
  public partial class CamDetailsControl : UserControl
  {
    public CamDetailsControl()
    {
      InitializeComponent();
    }

    private void CamDetailsControl_Load(object sender, EventArgs e)
    {

    }

    [Category("Properties")]
    public double Longtitude
    {
      get { return Convert.ToDouble(m_txtLon.Text); }
      set { m_txtLon.Text = value.ToString(); }
    }

    [Category("Properties")]
    public double Latitude
    {
      get { return Convert.ToDouble(m_txtLat.Text); }
      set { m_txtLat.Text = value.ToString(); }
    }

    [Category("Properties")]
    public SPUD.SpeedCam.RecordTypes State
    {
      get { return SpeedCam.RecordTypes.New; }// m_cmbState.Text; }
      set { m_cmbState.Text = value.ToString(); }
    }

    [Category("Properties")]
    public SPUD.SpeedCam.CameraTypes Type
    {
      get { return SpeedCam.CameraTypes.FixedSpeedcam; }//m_cmbType.Text; }
      set { m_cmbType.Text = value.ToString(); }
    }

    [Category("Properties")]
    public SPUD.SpeedCam.CameraDirection Direction
    {
      get { return SpeedCam.CameraDirection.AllDirections; }//m_cmbDirection.Text; }
      set { m_cmbDirection.Text = value.ToString(); }
    }
  }//end class CamDetailsControl
}//end namespace SPUD
