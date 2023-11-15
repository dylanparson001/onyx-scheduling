using Microsoft.AspNetCore.Identity;

namespace OnyxScheduling.Models
{
    public class User: IdentityUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

    }
}
