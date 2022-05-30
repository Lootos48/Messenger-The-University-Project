using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MessengerMobile.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string SendTime { get; set; }

        public string SendDate { get; set; }

        public string Text { get; set; }

        // nav property
        public byte[] Image { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public byte[] UserAvatar { get; set; }

        public int ChatId { get; set; }



        public bool IsOwner { get; set; }
        public bool HasPicture { get; set; }
        public bool HasMessage { get; set; }
        public string ImagePath { get; set; }

    }
}
