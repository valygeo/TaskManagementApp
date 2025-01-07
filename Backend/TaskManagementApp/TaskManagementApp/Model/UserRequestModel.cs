using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Model
{
    public class UserRequestModel
    {
        [Required(ErrorMessage = "Username required!")]
        [MaxLength(50, ErrorMessage = "Username too long!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email required!")]
        [MinLength(5, ErrorMessage = "Email must be at least 5 characters long!")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required!")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long!")]
        public string Password { get; set; }
    }
}
