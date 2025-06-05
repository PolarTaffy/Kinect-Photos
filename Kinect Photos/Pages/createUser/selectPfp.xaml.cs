using Dapper;
using Kinect_Photos.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace Kinect_Photos.Pages.createUser
{
    /// <summary>
    /// Interaction logic for selectPfp.xaml
    /// </summary>
    public partial class selectPfp : Page
    {
        String userName = null;
        public selectPfp()
        {
            InitializeComponent();

            userName = addUser.getName();
            nameLabel.Content = userName;
            Debug.WriteLine(userName);
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {

            using (IDbConnection connection = DatabaseHelper.GetDbConnection())
            {
                connection.Open();
                //do we want to prevent duplicate names? nah
                //connection.Query<Users>("SELECT * FROM Users").ToList();

                var sql = "INSERT INTO users (userName) VALUES (@userName)";
                connection.Execute(sql, new {userName});

                ////TODO: Create filepaths dir for user
                //string folderPath = "C:/Users/cl672/Documents/Testing/sampleGallery";
                ////int userID = 
                //var sql1 = "INSERT INTO folderPaths (userID, folderPath) VALUES (@userID, @folderPath)";
                
            }

            NavigationService.Navigate(new Login());
            //TODO: In the future, we need another screen where users set their photo paths, or at least confirm the default paths
        }
    }
}
