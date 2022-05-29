using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerMobile.Services
{
    public class MessageModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public bool IsOwnerMessage { get; set; }

    }
}
