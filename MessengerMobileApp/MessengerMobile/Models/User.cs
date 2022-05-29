using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MessengerMobile.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Max user name length is 20 characters")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // nav property
        public byte[] Avatar { get; set; }

        public IEnumerable<Chat> Chats { get; set; } = new List<Chat>();
        public IEnumerable<Message> Messages { get; set; } = new List<Message>();


        public string ImagePath { get; set; }
    }
}
