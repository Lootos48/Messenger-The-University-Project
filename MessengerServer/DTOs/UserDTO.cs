using MessengerServer.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTOs
{
    public class UserDTO
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

        public IEnumerable<ChatDTO> Chats { get; set; } = new List<ChatDTO>();
        public IEnumerable<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
    }
}
