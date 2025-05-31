using Dapper;
using Kinect_Photos.models;
using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
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
            List<StackPanel> profileTiles = new List<StackPanel>();
            GalleryView galleryPage = new GalleryView();

            using (IDbConnection connection = DatabaseHelper.GetDbConnection())
            {
                connection.Open();
                Console.WriteLine(connection.ConnectionString);
                localUsers = connection.Query<Users>("SELECT * FROM Users").ToList();

                var raw = connection.Query("SELECT * FROM users").ToList();
                Console.WriteLine($"Raw rows: {raw.Count}"); // Should not be 0
            }


            Console.WriteLine(localUsers.ToString());


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
                    //TODO: Make a function to actually like handle signing in
                    NavigationService.Navigate(galleryPage);
                };
                
                //Label
                Label usernameLabel = new Label();
                usernameLabel.Content = profile.userName;
                usernameLabel.HorizontalAlignment = HorizontalAlignment.Center;
                
                StackPanel profilePanel = new StackPanel();
                profilePanel.Children.Add(profileTile);
                profilePanel.Children.Add(usernameLabel);


                profileTiles.Add(profilePanel);
            }

            //New profile creation option
            KinectTileButton newProfile = new KinectTileButton();
            Label makeProfileLabel = new Label();
            makeProfileLabel.Content = "Create Profile";
            makeProfileLabel.HorizontalAlignment = HorizontalAlignment.Center;
            newProfile.Click += (sender, args) =>
            {
                //TODO: Handle Navigation to Create Profile Screen
            };
            StackPanel addAccPanel = new StackPanel();
            addAccPanel.Children.Add(newProfile);
            addAccPanel.Children.Add(makeProfileLabel);
            profileTiles.Add(addAccPanel); 

            
            Profiles.ItemsSource = profileTiles; 
        }
    }
}


