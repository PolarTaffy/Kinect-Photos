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
        public User()
        {
            this.name = "New Profile";
            String imgsrc = String.Copy(DEFAULT_PROFILE_IMG);
            this.profileImage = new BitmapImage(new Uri(imgsrc, UriKind.Relative));

        }

        public User(String userName)
        {
            this.name = userName;
            String imgsrc = String.Copy(DEFAULT_PROFILE_IMG);
            this.profileImage = new BitmapImage(new Uri(imgsrc, UriKind.Relative));

        }

        public User(String userName, String imgPath)
        {
            this.name = userName;
            this.profileImage = new BitmapImage(new Uri(imgPath, UriKind.RelativeOrAbsolute));
        }

        public String getName()
        {
            return name;
        }

        public BitmapImage getProfileImage()
        {
            return profileImage;
        }

        public void setName(String newName)
        {
            this.name = newName;
        }
    }
}
