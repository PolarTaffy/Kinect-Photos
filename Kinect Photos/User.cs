using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Kinect_Photos
{
    public class User
    {
        String name;
        Image profileImage;
        public User()
        {
            name = "New Profile";
            this.profileImage = new Image();
        }

        public User(String userName)
        {
            this.name = userName;
            this.profileImage = new Image();
        }

    }
}
