using Newtonsoft.Json;

namespace OnyxScheduling.Models;

public class JobInvoice_Item
{
    [JsonIgnore]
    public int JobId { get; set; }
    [JsonIgnore]
    public Jobs Job { get; set; }
    public int InvoiceItemId { get; set; }
    public Invoice_Items InvoiceItems { get; set; }
    public int Quantity { get; set; }
    public string CompanyId { get; set; }

}