using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTOs.Chat
{
    public class ChatEditRequestDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Max title length is 100 characters")]
        public string Title { get; set; }
    }
}
