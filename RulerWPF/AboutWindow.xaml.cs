using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace RulerWPF
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
    }
}
