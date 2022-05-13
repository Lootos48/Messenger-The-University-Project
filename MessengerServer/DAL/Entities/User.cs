using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DAL.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(20, ErrorMessage = "Max user name length is 20 characters")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // nav property
        public int? UserPictureId { get; set; }
        public UserPicture Avatar { get; set; }

        public IEnumerable<ChatsUsers> Chats { get; set; } = new List<ChatsUsers>();
        public IEnumerable<Message> Messages { get; set; } = new List<Message>();
    }
}
