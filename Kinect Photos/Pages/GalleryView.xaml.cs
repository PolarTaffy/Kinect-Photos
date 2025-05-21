using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
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

            List<KinectTileButton> images = new List<KinectTileButton>();
            //create all image files
            for (int i = 0; i < 50; i++)
            {
                KinectTileButton button = new KinectTileButton();
                button.HorizontalAlignment = HorizontalAlignment.Center;
                button.VerticalAlignment = VerticalAlignment.Center;

                ImageBrush curImage = new ImageBrush();
                curImage.ImageSource = new BitmapImage(new Uri("testImages/dance.png", UriKind.Relative));
                curImage.Stretch = Stretch.UniformToFill;
                button.Background = curImage;
                
                images.Add(button);
            }
            imageContainer.ItemsSource = images;

            //TODO: Save user's last scroll position
        }
    }
}
