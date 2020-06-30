using MZ.WPF.MessageBox;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFMessageBoxTestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowTestWpfMesageBox : Window
    {
        public WindowTestWpfMesageBox()
        {
            InitializeComponent();
        }

        private void btnInfo_OnClick(object sender, RoutedEventArgs e)
        {
            this.MessageInfo("Information.");
        }

        private void btnWarn_OnClick(object sender, RoutedEventArgs e)
        {
            this.MessageWarning("Exclamation!");
        }

        private void btnError_OnClick(object sender, RoutedEventArgs e)
        {
            this.MessageError("Error!!!");
        }

        private void btnQuestionr_OnClick(object sender, RoutedEventArgs e)
        {
            this.MessageQuestion("?Question?");
        }

        private void CYNQuestion_Click(object sender, RoutedEventArgs e)
        {
            this.MessageQuestion("Question???", this.Title, PopUp.PopUpButtonsType.CancelNoYes);
        }
    }
}
