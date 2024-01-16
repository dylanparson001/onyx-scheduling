using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnyxScheduling.Models
{
    public class Invoices
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string Processing_Status { get; set; }
        [JsonConverter(typeof(MyCustomDateTimeConverter))]
        public DateTime CreatedDateTime { get; set; }
        [JsonConverter(typeof(MyCustomDateTimeConverter))]
        public DateTime? FinishedDateTime { get; set; }
        [JsonConverter(typeof(MyCustomDateTimeConverter))]
        public DateTime ScheduledStartDateTime { get; set; }
        [JsonConverter(typeof(MyCustomDateTimeConverter))]
        public DateTime ScheduledEndDateTime { get; set; }
        public string Assigned_Technician_Id{ get; set; }
        public string Assigned_Customer_Id { get; set; }
        public double Total_Price { get; set; }
        public string InvoiceNumber { get; set; }
        public List<InvoiceInvoice_Item> InvoiceInvoice_Items{ get; set; }
        public int InvoiceId { get; internal set; }
    }
}
