namespace OnyxScheduling.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime CompletedDateTime { get; set; }
        public int Assigned_Technician_Id { get; set; }
        public int Assigned_Customer_id { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
