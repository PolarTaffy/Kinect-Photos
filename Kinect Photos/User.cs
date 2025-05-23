using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Kinect_Photos
{
    
    public class User
    {
        readonly String DEFAULT_PROFILE_IMG = "res/user.png";
        String name;
        BitmapImage profileImage;
        List<String> photoDirectories;

        public User()
        {
            this.name = "New Profile";
            String imgsrc = String.Copy(DEFAULT_PROFILE_IMG);
            this.profileImage = new BitmapImage(new Uri(imgsrc, UriKind.Relative));
            
            this.photoDirectories = new List<String>();
            this.photoDirectories.Add("C:/Users/cl672/Documents/Testing/sampleGallery");
        }

        public User(String userName)
        {
            this.name = userName;
            String imgsrc = String.Copy(DEFAULT_PROFILE_IMG);
            this.profileImage = new BitmapImage(new Uri(imgsrc, UriKind.Relative));
            this.photoDirectories = new List<String>();
            this.photoDirectories.Add("C:/Users/cl672/Documents/Testing/sampleGallery");

        }

        public User(String userName, String imgPath)
        {
            this.name = userName;
            this.profileImage = new BitmapImage(new Uri(imgPath, UriKind.RelativeOrAbsolute));
            this.photoDirectories = new List<String>();
            this.photoDirectories.Add("C:/Users/cl672/Documents/Testing/sampleGallery");
        }

        public String getName()
        {
            return name;
        }

        public BitmapImage getProfileImage()
        {
            return profileImage;
        }

        public List<String> getImageDirectories()
        {
            return this.photoDirectories;
        }

        public void setName(String newName)
        {
            this.name = newName;
        }

        public void setProfileImage(String imgPath)
        {
            this.profileImage = new BitmapImage(new Uri(imgPath, UriKind.Absolute));
        }

        public void addImgDirectory(String imgPath)
        {
            //TODO: Handle verifying the validity of it
            this.photoDirectories.Add(imgPath);
        }

    }
}
