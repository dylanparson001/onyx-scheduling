﻿using OnyxScheduling.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnyxScheduling.Dtos
{
    public class TechnicianDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }
        public List<Invoices> Invoices { get; set; }
        [JsonPropertyName("Id")]
        public string Id { get; internal set; }
        public string CompanyId { get; set; }
        public double DailyTotal { get; set; }
        
    }
}
