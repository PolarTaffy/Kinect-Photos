using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Dapper;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Kinect.Toolkit.Interaction;

namespace Kinect_Photos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static KinectSensorChooser sensorChooser;
        public InteractionStream interactionStream;
        private static KinectRegion curKinectRegion;
        private static int CUR_USER_ID;

        public MainWindow()
        {
            InitializeComponent();

            //TODO: In settings, allow user to toggle: NavigationUIVisibility="Hidden"

            // initialize the sensor chooser and UI
            sensorChooser = new KinectSensorChooser();
            sensorChooser.KinectChanged += SensorChooserOnKinectChanged;

            sensorChooser.Start();

            curKinectRegion = this.kinectRegion;

            // Bind the sensor chooser's current sensor to the KinectRegion
            var regionSensorBinding = new Binding("Kinect") { Source = sensorChooser };
            BindingOperations.SetBinding(this.kinectRegion, KinectRegion.KinectSensorProperty, regionSensorBinding);

            //Database init
            if (!File.Exists("kinectPhotos.db")) { SQLiteConnection.CreateFile("kinectPhotos.db"); }
            DatabaseHelper.initializeDatabase();
        }

        public static int getUserID()
        {
            return CUR_USER_ID;
        }
        public static void setUserID(int userID)
        {
            CUR_USER_ID = userID;
        }

        public static HandPointer getHandPointer()
        {
            return curKinectRegion.HandPointers.FirstOrDefault(handPointer => handPointer.IsPrimaryUser && handPointer.IsPrimaryHandOfUser);
        }

        /// Handles New Kinect Sensors
        private static void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Range = DepthRange.Default;
                    args.OldSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                    try
                    {
                        args.NewSensor.DepthStream.Range = DepthRange.Near;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = true;
                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                    }
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }
        }
    }

    
}
