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
using System.Windows.Threading;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Kinect.Toolkit.Interaction;

namespace Kinect_Photos
{
    /// <summary>
    /// Interaction logic for imageView.xaml
    /// </summary>
    /// 
    public partial class imageView : Page
    {
        double prevHandDistance = 0;
        double curHandDistance = 0;
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

            initializeZoomControls();

            

        }

        void initializeZoomControls() //maybe this should just be onPropertyChanged? not sure how to manage that hough
        {
            HandPointer hp = MainWindow.getHandPointer();

            DispatcherTimer gripCheckTimer = new DispatcherTimer();
            gripCheckTimer.Interval = TimeSpan.FromMilliseconds(10);
            gripCheckTimer.Tick += (sender, args) =>
            {
                if (hp != null && hp.IsInGripInteraction)
                {
                    String output = "Hand is gripped! ";

                    prevHandDistance = curHandDistance;
                    curHandDistance = hp.PressExtent;

                    output += curHandDistance; //PressExtent seems to be from a range of 0 to 4
                    zoomEnabledIndicator.Content = output;

                    //In order to determine if we're zooming in or out, we're going to just do some math (subtraction & multiplication)
                    //zoom in means negative change, zoom out is positive change
                    double change = curHandDistance - prevHandDistance;

                    ImageScaleTransform.ScaleX -= change;
                    ImageScaleTransform.ScaleY -= change; //VERY unstable scrolling


                }
                else
                {
                    zoomEnabledIndicator.Content = "Hand is not gripped!";
                    prevHandDistance = 0;
                    curHandDistance = 0;
                }

            };
            gripCheckTimer.Start();
        }

    }
}
