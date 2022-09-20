﻿using System.Windows.Controls;
using DynamicMap.NET;
//using Demo.WindowsForms;
using DashCamGPSView;

namespace DashCamGPSView.Controls
{
   /// <summary>
   /// Interaction logic for TrolleyTooltip.xaml
   /// </summary>
   public partial class TrolleyTooltip : UserControl
   {
      public TrolleyTooltip()
      {
         InitializeComponent();
      }

      public void SetValues(string type, VehicleData vl)
      {
         Device.Text = vl.Id.ToString();
         LineNum.Text = type + " " + vl.Line;
         StopName.Text = vl.LastStop;
         TrackType.Text = vl.TrackType;
         TimeGps.Text = vl.Time;
         Area.Text = vl.AreaName;
         Street.Text = vl.StreetName;
      }
   }
}
