using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTOs.Message
{
    public class CreateMessageRequestDTO
    {
        public string Text { get; set; }

        public byte?[] ImageBytes { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ChatId { get; set; }
    }
}
