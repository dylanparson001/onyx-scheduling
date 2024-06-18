namespace OnyxScheduling.Dtos;

public class JobInvoiceItemDto
{
    public int InvoiceItemId { get; set; }
    public InvoiceItemDto InvoiceItems { get; set; }
    public int Quantity { get; set; }
    public string CompanyId { get; set; }

}