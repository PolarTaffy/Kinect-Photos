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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();

            this.sensorChooserUi.KinectSensorChooser = MainWindow.sensorChooser;
            //FIXME: The KinectSensorChooser appears blank

            

            demoProfileBtn.Click += (sender, args) =>
            {
                //MessageBox.Show(users[0].ToString());
                Window1 win = new Window1();
                win.Show();
                //Close();
            };
        }
    }
}


