using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTOs.User
{
    public class UserAuthRequestDTO
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
