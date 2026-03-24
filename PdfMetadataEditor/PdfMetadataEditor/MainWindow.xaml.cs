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
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                PdfFilesListBox.Items.Clear();
                foreach (var file in openFileDialog.FileNames)  
                    PdfFilesListBox.Items.Add(file);

                PdfFilesListBox.SelectedItem = openFileDialog.FileName;
            }
        }

        private void PdfFilesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedFile = PdfFilesListBox.SelectedItem as string;
            if (selectedFile != null)
            {
                try
                {
                    PdfFilePathTextBox.Text = selectedFile;
                    PdfDocument document = PdfReader.Open(PdfFilePathTextBox.Text, PdfDocumentOpenMode.Import);
                    AuthorTextBox.Text = document.Info.Author;
                    TitleTextBox.Text = document.Info.Title;
                    SubjectTextBox.Text = document.Info.Subject;
                    KeywordsTextBox.Text = document.Info.Keywords;
                    document.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading PDF metadata: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }            
            }
        }

        private void UpdateMetadataButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UpdateAllButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var file in PdfFilesListBox.Items)
                UpdateMetadata(file.ToString());
        }

        private void UpdateSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateMetadata(PdfFilePathTextBox.Text);
        }

        private void UpdateMetadata(string filePath)
        {
            try
            {
                PdfDocument document = PdfReader.Open(filePath, PdfDocumentOpenMode.Modify);
                
                if (AuthorCheckBox.IsChecked == true)
                    document.Info.Author = AuthorTextBox.Text;
                if (TitleCheckBox.IsChecked == true)
                    document.Info.Title = TitleTextBox.Text;
                if (SubjectCheckBox.IsChecked == true)
                    document.Info.Subject = SubjectTextBox.Text;
                if (KeywordsCheckBox.IsChecked == true)
                    document.Info.Keywords = KeywordsTextBox.Text;
                
                document.Save(filePath);
                document.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating PDF metadata for file " + filePath + ": " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}