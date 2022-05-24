using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTOs.ChatsUsers
{
    public class UserChatInteractRequestDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ChatId { get; set; }
    }
}
