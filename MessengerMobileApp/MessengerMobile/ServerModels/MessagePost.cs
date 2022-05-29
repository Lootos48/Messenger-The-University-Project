using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MessengerMobile.ServerModels
{
    public class MessagePost
    {
        public string text { get; set; }
        public byte[] imagebytes { get; set; }
        public int userId { get; set; }
        public int chatId { get; set; }
    }
}
