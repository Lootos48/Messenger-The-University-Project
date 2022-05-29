using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MessengerMobile.Models
{
    public class UserPicture
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        // nav property
        public int? UserId { get; set; }
        public User PictureOwner { get; set; }
    }
}
