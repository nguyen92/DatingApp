using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOS
{
    public class UserRegisterForDto
    {
        [Required]
        
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength=4, ErrorMessage="password length should between 4 and 8")]
        public string Password { get; set; }
    }
}