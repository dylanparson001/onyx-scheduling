using System.ComponentModel.DataAnnotations;

namespace OnyxScheduling.Auth
{
    public class LoginDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string CompanyId { get; set; }
    }
}
