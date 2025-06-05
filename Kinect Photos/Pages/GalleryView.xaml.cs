using Dapper;
using Kinect_Photos.models;
using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.WebRequestMethods;

namespace Kinect_Photos
{
    /// <summary>
    /// Interaction logic for GalleryView.xaml
    /// </summary>
    public partial class GalleryView : Page
    {
        public GalleryView()
        {
            InitializeComponent();

            LoadGalleryImages();

            //TODO: Save user's last scroll position
            HandleTopMenuVisibility();
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e) { OverlayPanel.Visibility = Visibility.Visible; }

        private void OverlayPanel_Click(object sender, RoutedEventArgs e) { OverlayPanel.Visibility = Visibility.Collapsed; }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement Sign Out logic
            MessageBox.Show("Sign Out button clicked!"); 
            OverlayPanel.Visibility = Visibility.Collapsed; 
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement Settings navigation or logic
            MessageBox.Show("Settings button clicked!"); 
            OverlayPanel.Visibility = Visibility.Collapsed; 
        }

        private void CustomizationButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement Customization navigation or logic
            MessageBox.Show("Customization button clicked!"); 
            OverlayPanel.Visibility = Visibility.Collapsed; 
        }

        private void HandleTopMenuVisibility()
        {
            //we're gonna track the handpointer position and see if it's on the top half
            HandPointer pt = MainWindow.getHandPointer();

            DispatcherTimer handPosTimer = new DispatcherTimer();
            handPosTimer.Interval = TimeSpan.FromMilliseconds(100);
            handPosTimer.Tick += (sender, args) =>
            {
                pt.GetPosition(this);
                //Handle Menu Visibility
                Point menuPos = pt.GetPosition(menuBtn);
                if (!(menuPos.X >= 0 && menuPos.X <= menuBtn.ActualWidth && menuPos.Y >= 0 && menuPos.Y <= menuBtn.ActualHeight))
                {
                    //pointer isn't on menu
                }
            };

        }

        private void LoadGalleryImages() //For large galleries, this takes a while... consider segmenting this in the future where it generates more as you scroll?
        { //Also, have images saved or organized by date?


            //Index all files BY MONTH in a database
            //Load each one of them separated, and by month
            //Whenever you load, you just check if there are any more new files
            //Keeps everything in memory
            //Figure out how to cache things???

            List<String> directories = new List<String>();
            directories.Add("C:/Users/cl672/Documents/Testing/sampleGallery");

            using (IDbConnection connection = DatabaseHelper.GetDbConnection()) //TODO: Access the filepaths for the current user
            {
                connection.Open();
                //cur user
                int userID = MainWindow.getUserID(); 

                //get all databases in the table filepaths
                var sql = "SELECT * FROM folderPaths WHERE userID = (@userID);";
                List<folderPaths> paths = connection.Query<folderPaths>(sql, new { userID }).ToList();

                foreach (folderPaths path in paths)
                {
                    directories.Add(path.folderPath);
                }
            }


            List<KinectTileButton> images = new List<KinectTileButton>();

            foreach (String directory in directories)
            {
                String[] extensions = new[] { "*.png", "*.jpg", "*.jpeg", "*.bmp", "*.gif" };
                var imageFiles = extensions.SelectMany(ext => Directory.GetFiles(directory, ext)).ToList();
                foreach (String imgsrc in imageFiles)
                {

                    KinectTileButton button = new KinectTileButton();
                    // Changed HorizontalAlignment and VerticalAlignment to Stretch
                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
                    button.VerticalAlignment = VerticalAlignment.Stretch;

                    ImageBrush curImage = new ImageBrush();
                    curImage.ImageSource = new BitmapImage(new Uri(imgsrc, UriKind.Relative));
                    curImage.Stretch = Stretch.UniformToFill;
                    button.Background = curImage;

                    button.Click += (sender, args) =>
                    {
                        NavigationService.Navigate(new imageView(imgsrc, "hello"));
                    };
                    button.Margin = new Thickness(0);

                    images.Add(button);
                }
            }
            imageContainer.ItemsSource = images;
            //TODO: Resize imageGrid's columns/rows to increase or decrease scale or to make photo previews square/vertical

            //OR in the future have VARIED image preview dimensions/sizes to look really pretty
        }
    }
}
