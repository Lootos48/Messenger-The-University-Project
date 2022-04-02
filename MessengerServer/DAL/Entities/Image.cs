using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DAL.Entities
{
    public class Image
    {
        public string Id { get; set; }

        [Required]
        public string Path { get; set; }

        // nav property
        public int? UserId { get; set; }
        public User UserAvatar { get; set; }


        public int? MessageId { get; set; }
        public Message Message { get; set; }
    }
}