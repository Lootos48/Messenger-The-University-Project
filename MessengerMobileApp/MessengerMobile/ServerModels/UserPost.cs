using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerMobile.ServerModels
{
    public class UserPost
    {
        public string username { get; set; }
        public string password { get; set; }
        public byte[] imagebytes { get; set; }
    }
}
