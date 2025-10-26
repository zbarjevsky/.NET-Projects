using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PdfMetadataEditor
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

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var input = openFileDialog.FileName;
                PdfFilePathTextBox.Text = input;

                PdfDocument document = PdfReader.Open(PdfFilePathTextBox.Text, PdfDocumentOpenMode.Modify);
                AuthorTextBox.Text = document.Info.Author;
                TitleTextBox.Text = document.Info.Title;
                SubjectTextBox.Text = document.Info.Subject;
                KeywordsTextBox.Text = document.Info.Keywords;
            }
        }

        private void UpdateMetadataButton_Click(object sender, RoutedEventArgs e)
        {
            PdfDocument document = PdfReader.Open(PdfFilePathTextBox.Text, PdfDocumentOpenMode.Modify);
            document.Info.Author = AuthorTextBox.Text;
            document.Info.Title = TitleTextBox.Text;
            document.Info.Subject = SubjectTextBox.Text;
            document.Info.Keywords = KeywordsTextBox.Text;
            document.Save(PdfFilePathTextBox.Text);
            MessageBox.Show("Metadata updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}