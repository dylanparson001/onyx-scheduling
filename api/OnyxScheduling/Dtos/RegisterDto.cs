using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace OnyxScheduling.Auth
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last name is required")] 
        public string LastName { get; set;}

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
