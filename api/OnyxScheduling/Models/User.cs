using Microsoft.AspNetCore.Identity;

namespace OnyxScheduling.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string CompanyId { get; set; }
    }
}
