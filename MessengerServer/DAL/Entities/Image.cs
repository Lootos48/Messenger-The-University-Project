using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DAL.Entities
{
    public class Image : BaseEntity
    {
        [Required]
        public string Path { get; set; }

        // nav property
        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}