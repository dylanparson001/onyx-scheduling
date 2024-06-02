using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using OnyxScheduling.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnyxScheduling.Dtos
{
    public class CustomerDto
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        public List<Invoices> Invoices { get; set; }
    }
}
