using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MessengerMobile.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LastMessage { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
