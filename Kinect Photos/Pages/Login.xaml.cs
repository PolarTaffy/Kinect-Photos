using Dapper;
using Kinect_Photos.models;
using Kinect_Photos.Pages;
using Kinect_Photos.Pages.Gallery;
using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kinect_Photos
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        //replace array with a database or something idk
        User myUser = null;
        List<Users> localUsers = new List<Users>();

        public Login()
        {
            InitializeComponent();
            //this.sensorChooserUi.KinectSensorChooser = MainWindow.sensorChooser;
            //FIXME: The KinectSensorChooser appears blank
            loadProfiles();
        }

        private void loadProfiles()
        {
            List<Grid> profileIcons = new List<Grid>();
            using (IDbConnection connection = DatabaseHelper.GetDbConnection())
            {
                connection.Open();
                localUsers = connection.Query<Users>("SELECT * FROM Users").ToList();
            }

            foreach (Users profile in localUsers) {
                KinectTileButton profileTile = new KinectTileButton();

                ImageBrush brush = new ImageBrush();

                BitmapImage profileImg = null;
                try
                {
                    profileImg = new BitmapImage(new Uri(profile.pfpPath, UriKind.Relative));
                }
                catch (Exception)
                {
                    profileImg = new BitmapImage(new Uri("res/user.png", UriKind.Relative));
                    //throw;
                } finally
                {
                    brush.ImageSource = profileImg;
                    brush.Stretch = Stretch.UniformToFill;
                    profileTile.Background = brush;
                }

                //Button - Functionality
                profileTile.Click += (sender, args) =>
                {
                    MainWindow.setUserID(profile.userID);
                    NavigationService.Navigate(new Gallery());
                };
                
                //Label
                Label usernameLabel = new Label();
                usernameLabel.Content = profile.userName;
                usernameLabel.HorizontalAlignment = HorizontalAlignment.Center;
                
                StackPanel profilePanel = new StackPanel();
                profilePanel.Children.Add(profileTile);
                profilePanel.Children.Add(usernameLabel);

                //TODO: Extract method for these buttons?
                //Delete Button
                KinectCircleButton delete = new KinectCircleButton();
                delete.VerticalAlignment = VerticalAlignment.Top;
                delete.HorizontalAlignment = HorizontalAlignment.Right;
                delete.Width = 70; delete.Height = 70;
                delete.Content = "❌";
                delete.Foreground = new SolidColorBrush(Colors.Red);
                delete.Margin = new Thickness(-5);
                

                //Edit Button
                KinectCircleButton edit = new KinectCircleButton();
                edit.VerticalAlignment = VerticalAlignment.Top;
                edit.HorizontalAlignment = HorizontalAlignment.Right;
                edit.Width = 70; edit.Height = 70;
                edit.Content = "✏️";
                edit.Foreground = new SolidColorBrush(Colors.Green);
                edit.Margin = new Thickness(-5);

                //Wrap Panel
                WrapPanel profileControlBtns = new WrapPanel();
                profileControlBtns.HorizontalAlignment = HorizontalAlignment.Right;
                profileControlBtns.FlowDirection = FlowDirection.RightToLeft;
                profileControlBtns.Orientation = Orientation.Horizontal;
                profileControlBtns.Margin = new Thickness(0, 5, 5, 0);
                profileControlBtns.Visibility = Visibility.Hidden;

                profileControlBtns.Children.Add(delete);
                profileControlBtns.Children.Add(edit);

                //Grid
                Grid fullProfile = new Grid();
                fullProfile.Children.Add(profilePanel);
                fullProfile.Children.Add(profileControlBtns);
                

                profileIcons.Add(fullProfile);

                delete.Click += (sender, args) =>
                {
                    //TODO: Add "Are you sure?" popup

                    using (IDbConnection connection = DatabaseHelper.GetDbConnection())
                    {
                        connection.Open();
                        int userID = profile.userID;
                        var sql = "DELETE FROM users WHERE userID = (@userID)";
                        connection.Execute(sql, new { userID });
                    }

                    profileIcons.Remove(fullProfile);
                    Profiles.ItemsSource = new List<Grid>(profileIcons);

                    //TODO: Remove all user filepaths from filepaths table

                };
            }

            //New profile creation option
            KinectTileButton newProfile = new KinectTileButton();
            Label makeProfileLabel = new Label();
            makeProfileLabel.Content = "Create Profile";
            makeProfileLabel.HorizontalAlignment = HorizontalAlignment.Center;
            newProfile.Click += (sender, args) =>
            {
                NavigationService.Navigate(new addUser());
            };
            StackPanel addAccPanel = new StackPanel();
            addAccPanel.Children.Add(newProfile);
            addAccPanel.Children.Add(makeProfileLabel);

            Grid createProfile = new Grid();
            createProfile.Children.Add(addAccPanel);
            profileIcons.Add(createProfile);

            Profiles.ItemsSource = profileIcons; 
        }

        private void EditModeBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Grid profile in  Profiles.ItemsSource)
            {
                UIElementCollection controls = profile.Children;
                List<UIElement> elementList = controls.Cast<UIElement>().ToList();
                foreach (var item in elementList)
                {
                    if (typeof(WrapPanel).Equals(item.GetType()))
                    {
                        WrapPanel panel = (WrapPanel) item;
                        if (panel.Visibility == Visibility.Visible) { panel.Visibility = Visibility.Hidden; }
                        else { panel.Visibility = Visibility.Visible; }
                    }
                }
            }
        }
    }
}


