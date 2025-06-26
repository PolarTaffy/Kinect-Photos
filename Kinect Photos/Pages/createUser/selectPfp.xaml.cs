using Dapper;
using Kinect_Photos.models;
using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Kinect_Photos.Pages.createUser
{
    /// <summary>
    /// Interaction logic for selectPfp.xaml
    /// </summary>
    public partial class selectPfp : Page
    {
        String userName = null;
        String pfpImage = null;
        public selectPfp()
        {
            InitializeComponent();

            userName = addUser.getName();
            nameLabel.Content = "Select a profile icon, " + userName + ".";

            initializePfps();
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            Login(pfpImage);
            //TODO: In the future, we need another screen where users set their photo paths, or at least confirm the default paths
        }

        private void initializePfps()
        {
            List<KinectTileButton> pictures = new List<KinectTileButton>();

            var pfps = Directory.GetFiles("./res/pfp");
            foreach (String pfpsrc in pfps)
            {
                KinectTileButton button = new KinectTileButton();
                button.HorizontalAlignment = HorizontalAlignment.Stretch;
                button.VerticalAlignment = VerticalAlignment.Stretch;

                ImageBrush curImage = new ImageBrush();
                curImage.ImageSource = new BitmapImage(new Uri(pfpsrc, UriKind.Relative));
                curImage.Stretch = Stretch.UniformToFill;
                button.Background = curImage;

                button.Click += (sender, args) =>
                {
                    pfpImage = pfpsrc;
                    //Login(pfpsrc);
                    
                };
                button.Margin = new Thickness(0);


                pictures.Add(button);
            }

            pictureContainer.ItemsSource = pictures;
        }

        private void Login(String pfpPath)
        {
            using (IDbConnection connection = DatabaseHelper.GetDbConnection())
            {
                connection.Open();
                //do we want to prevent duplicate names? nah
                //connection.Query<Users>("SELECT * FROM Users").ToList();

                String sql;
                if (pfpPath == null || pfpPath.Length > 0)
                {
                    sql = "INSERT INTO users (userName, pfpPath) VALUES (@userName, @pfpPath)";
                    connection.Execute(sql, new { userName, pfpPath });
                } else
                {
                    sql = "INSERT INTO users (userName) VALUES (@userName)";
                    connection.Execute(sql, new { userName });
                }

                

                ////TODO: Create filepaths dir for user
                //string folderPath = "C:/Users/cl672/Documents/Testing/sampleGallery";
                ////int userID = 
                //var sql1 = "INSERT INTO folderPaths (userID, folderPath) VALUES (@userID, @folderPath)";

            }
            NavigationService.Navigate(new Login());
        }



    }
}
