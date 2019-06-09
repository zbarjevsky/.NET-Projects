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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnInfo_OnClick(object sender, RoutedEventArgs e)
        {
            MZ.WPF.MessageBox.PopUp.Information("Information.");
        }

        private void btnWarn_OnClick(object sender, RoutedEventArgs e)
        {
            MZ.WPF.MessageBox.PopUp.Exclamation("Exclamation!");
        }

        private void btnError_OnClick(object sender, RoutedEventArgs e)
        {
            MZ.WPF.MessageBox.PopUp.Error("Error!!!");
        }

        private void btnQuestionr_OnClick(object sender, RoutedEventArgs e)
        {
            MZ.WPF.MessageBox.PopUp.Question("Question!");
        }

        private void CYNQuestion_Click(object sender, RoutedEventArgs e)
        {
            MZ.WPF.MessageBox.PopUp.Question("Question!", this.Title, MessageBoxImage.Question, TextAlignment.Center,
                MZ.WPF.MessageBox.PopUp.PopUpButtonsType.CancelNoYes);
        }
    }
}
