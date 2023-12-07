namespace OnyxScheduling.Models
{
    public class Technicians: User
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public ICollection<Invoices> Invoices { get; set; }
    }
}