namespace OnyxScheduling.Dtos;

public class JobsDto
{
    public int Id { get; set; }
    public string CreatedDateTime { get; set; }
    public string CompletedDateTime { get; set; }
    public string ScheduledStartDateTime { get; set; }
    public string ScheduledEndDateTime { get; set; }
    public string Assigned_Technician_Id { get; set; }
    public string Assigned_Customer_Id { get; set; }
    public string InvoiceNumber { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string processing_Status { get; set; }
    public int InvoiceId { get; set; }
}