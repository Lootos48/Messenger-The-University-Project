using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerMobile.ServerModels
{
    public class MessageEditPost
    {
        public int Id { get; set; }
        public string text { get; set; }
        public byte[] imageBytes { get; set; }
    }
}
