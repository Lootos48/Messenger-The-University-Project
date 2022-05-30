using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerMobile.ServerModels
{
    public class UserEditPost
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public byte[] imageBytes { get; set; }
    }
}
