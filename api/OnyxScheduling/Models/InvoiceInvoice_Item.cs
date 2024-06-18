using System.Text.Json.Serialization;

namespace OnyxScheduling.Models
{
    public class InvoiceInvoice_Item
    {
        public int InvoiceId { get; set; }
        [JsonIgnore]
        public Invoices Invoice { get; set; }

        public int InvoiceItemId { get; set; }
        [JsonIgnore]
        public Invoice_Items InvoiceItem { get; set; }
        public int Quantity { get; set; }
        public string CompanyId { get; set; }

    }
}
