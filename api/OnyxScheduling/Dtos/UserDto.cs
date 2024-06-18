using System.Text.Json.Serialization;

namespace OnyxScheduling.Dtos
{
    public class UserDto
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        public string Token { get; set; }
        public string CompanyId { get; set; }


    }
}
