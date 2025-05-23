using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            //Initialize Menu Buttons
            ChangeUserBtn.Click += (sender, args) =>
            {
                NavigationService.Navigate(new Login());
            };

            SettingsBtn.Click += (sender, args) =>
            {
                //NavigationService.Navigate(...);
            };

            LoadGalleryImages();

            //TODO: Save user's last scroll position
        }



        private void LoadGalleryImages() //For large galleries, this takes a while... consider segmenting this in the future where it generates more as you scroll?
        { //Also, have images saved or organized by date?

            //FIXME: Get current user
            User curUser = new User("Ralphie");

            //Get user directory
            List<KinectTileButton> images = new List<KinectTileButton>();

            foreach (String directory in curUser.getImageDirectories())
            {
                String[] extensions = new[] { "*.png", "*.jpg", "*.jpeg", "*.bmp", "*.gif" };
                var imageFiles = extensions.SelectMany(ext => Directory.GetFiles(directory, ext)).ToList();
                foreach (String imgsrc in imageFiles)
                {

                    KinectTileButton button = new KinectTileButton();
                    button.HorizontalAlignment = HorizontalAlignment.Center;
                    button.VerticalAlignment = VerticalAlignment.Center;

                    ImageBrush curImage = new ImageBrush();
                    curImage.ImageSource = new BitmapImage(new Uri(imgsrc, UriKind.Relative));
                    curImage.Stretch = Stretch.UniformToFill;
                    button.Background = curImage;

                    button.Click += (sender, args) =>
                    {
                        NavigationService.Navigate(new imageView(imgsrc, "hello"));
                    };

                    images.Add(button);
                }
            }
            imageContainer.ItemsSource = images;
        }
    }
}
