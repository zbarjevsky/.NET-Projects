using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows;
using System;
using System.Diagnostics;

namespace DashCamGPSView.Controls
{
    /// <summary>
    /// Interaction logic for ThumbnailsUserControl.xaml
    /// </summary>
    public partial class ThumbnailsUserControl : UserControl
    {
        public class ThumbnailData
        {
            public BitmapSource bmp { get; set; }
            public string txt { get; set; }
            public double seconds { get; set; }

            public ThumbnailData(BitmapSource image, double sec)
            {
                bmp = image;
                txt = TimeSpan.FromSeconds(sec).ToString();
                seconds = sec;
            }
        }

        public Action<ThumbnailData> OnItemSelectedAction = (item) => { };

        public ObservableCollection<ThumbnailData> Images { get; set; } = new ObservableCollection<ThumbnailData>();

        public ThumbnailsUserControl()
        {
            InitializeComponent();

            DataContext = this;

            player.MediaOpened += Player_MediaOpened;
            player.MediaFailed += Player_MediaFailed;
        }

        private void Thumbnails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnItemSelectedAction(Thumbnails.SelectedItem as ThumbnailData);
        }

        private void Player_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Debug.WriteLine(e.ErrorException.ToString());

            player.Position = TimeSpan.Zero;
            player.Stop();
            player.Source = null;
        }

        private void Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            try
            {
                Images.Clear();
                player.Pause();
                if (player.NaturalDuration > TimeSpan.FromSeconds(8))
                {
                    player.UpdateLayout();
                    double seconds = player.NaturalDuration.TimeSpan.TotalSeconds;
                    for (int i = 0; i < seconds; i++)
                    {
                        player.Position = TimeSpan.FromSeconds(i);
                        player.Play();
                        player.Pause();
                        System.Threading.Thread.Sleep(1);
                        player.UpdateLayout();

                        BitmapSource bmp = Tools.Tools.UIElementToBitmap(player);
                        Images.Add(new ThumbnailData(bmp, i));
                    }
                    this.UpdateLayout();
                }
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.ToString());
            }
            finally
            {
                player.Position = TimeSpan.Zero;
                player.Stop();
                player.Source = null;
            }
        }

        public void StartCreateThumbnailsFromVideoFile(string fileName)
        {
            try
            {
                Images.Clear();
                player.Source = new System.Uri(fileName);
                player.Play();
            }
            catch (System.Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
    }
}
