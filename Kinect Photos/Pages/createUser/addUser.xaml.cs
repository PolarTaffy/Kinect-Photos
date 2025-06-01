using Dapper;
using Kinect_Photos.models;
using Kinect_Photos.Pages.createUser;
using Microsoft.Kinect.Toolkit.Controls;
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

namespace Kinect_Photos.Pages
{
    /// <summary>
    /// Interaction logic for addUser.xaml
    /// </summary>
    public partial class addUser : Page
    {
        Boolean kbCapitalized = true;
        static String name;
        public addUser()
        {
            InitializeComponent();
            inputBox.Text = string.Empty;
        }

        public static String getName()
        {
            return name; //This line seems a little problematic, revisit later
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            KinectTileButton key = sender as KinectTileButton;
            inputBox.Text += key.Content;
            if (kbCapitalized) { ChangeCase(); }
            name = inputBox.Text;

        }

        public void ChangeCase()
        {
            kbCapitalized = !kbCapitalized;
            foreach (StackPanel container in Keyboard.Children)
            {
                foreach (KinectTileButton key in container.Children)
                {
                    if (key.Content != null && key.Content.ToString().ToCharArray().Length == 1)
                    {
                        if (kbCapitalized) { key.Content = key.Content.ToString().ToUpper(); }
                        else { key.Content = key.Content.ToString().ToLower(); }
                    } 
                }
            }
        }
        private void ChangeCase(object sender, RoutedEventArgs e) { ChangeCase(); } //overload for XAML

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (inputBox.Text.Length > 0)
            {
                inputBox.Text = inputBox.Text.Substring(0, inputBox.Text.Length - 1);
            }
        }

        private void SubmitButtonClick(object sender, RoutedEventArgs e)
        {
            if (inputBox.Text.Length > 0)
            {
                //pass this into selectPfp
                Debug.WriteLine(inputBox.Text);
                NavigationService.Navigate(new selectPfp(), inputBox.Text);
            }
        }
    }
}
