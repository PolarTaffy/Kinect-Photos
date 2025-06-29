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
using System.Windows.Threading;

namespace Kinect_Photos.Pages.Gallery
{
    /// <summary>
    /// Interaction logic for Gallery.xaml
    /// </summary>
    public partial class Gallery : Page
    {

        public Gallery()
        {
            InitializeComponent();
            HandleTopMenuVisibility();
            //TODO: Add logic for switching the frame between photos and album

        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e) { OverlayPanel.Visibility = Visibility.Visible; }

        private void OverlayPanel_Click(object sender, RoutedEventArgs e) { OverlayPanel.Visibility = Visibility.Collapsed; }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sign Out button clicked!");
            OverlayPanel.Visibility = Visibility.Collapsed;
            MainWindow.setUserID(-1);
            NavigationService.Refresh();
            NavigationService.Navigate(new Login());
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
    }
}
