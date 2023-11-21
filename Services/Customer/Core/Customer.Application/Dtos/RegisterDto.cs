using System.ComponentModel.DataAnnotations;

namespace Customer.Application.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage ="Email is required.")]
        [EmailAddress(ErrorMessage = "Email format is wrong.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]  
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
    }
}
