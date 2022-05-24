using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTOs.Chat
{
    public class ChatCreateRequestDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Max title length is 100 characters")]
        public string Title { get; set; }
    }
}
