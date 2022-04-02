using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DAL.Entities
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
