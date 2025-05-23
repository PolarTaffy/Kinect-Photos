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
    /// Interaction logic for imageView.xaml
    /// </summary>
    public partial class imageView : Page
    {
        public imageView(String imgUrl, String folder)
        {
            InitializeComponent();

            image.Source = new BitmapImage(new Uri(imgUrl, UriKind.Absolute));

            //scrollviewer.ScrollToVerticalOffset(scrollviewer.ScrollableHeight / 2);
            //scrollviewer.ScrollToHorizontalOffset(scrollviewer.ScrollableWidth / 2); //allegedly used for centering


            backBtn.Click += (sender, args) =>
            {
                NavigationService.GoBack();
            };

        }
    }
}
