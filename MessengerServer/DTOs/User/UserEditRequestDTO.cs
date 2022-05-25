using System.ComponentModel.DataAnnotations;

namespace MessengerServer.DTOs.User
{
    public class UserEditRequestDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Max user name length is 20 characters")]
        public string Username { get; set; }

        [Required]
        [Compare("ConfirmPassword", ErrorMessage = "Entered passwords must be equal")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
