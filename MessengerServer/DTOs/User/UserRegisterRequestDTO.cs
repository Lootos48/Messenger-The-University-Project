using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTOs.User
{
    public class UserRegisterRequestDTO
    {
        [Required]
        [StringLength(20, ErrorMessage = "Max username length is 20 characters")]
        public string username { get; set; }

        [Required]
        public string password { get; set; }

        public byte[] ImageBytes { get; set; }
    }
}
