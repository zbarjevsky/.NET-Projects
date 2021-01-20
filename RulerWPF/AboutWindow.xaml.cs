using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MkZ.WPF.RulerWPF
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.AboutRulerWPF)))
            {
                txtAbout.Selection.Load(stream, DataFormats.Rtf);
            }

            _version.Text = "Version: " + Assembly.GetEntryAssembly().GetName().Version.ToString();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void CenterTo(FrameworkElement owner)
        {
            Point ownerLocation = owner.PointToScreen(new Point());
            this.Left = ownerLocation.X;
            this.Top = ownerLocation.Y + 60;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            // open URL
            Hyperlink source = sender as Hyperlink;
            if (source != null)
            {
                System.Diagnostics.Process.Start(source.NavigateUri.ToString());
            }
        }
    }
}
