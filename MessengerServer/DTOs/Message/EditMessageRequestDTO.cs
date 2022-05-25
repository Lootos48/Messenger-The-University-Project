using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTOs.Message
{
    public class EditMessageRequestDTO
    {
        [Required]
        public int Id { get; set; }
        public string Text { get; set; }
        public byte?[] ImageBytes { get; set; }
    }
}
