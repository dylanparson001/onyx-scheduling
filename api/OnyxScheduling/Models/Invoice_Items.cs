using System.ComponentModel.DataAnnotations;

namespace OnyxScheduling.Models
{
    public class Invoice_Items
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Category is required")]
        public int Category_Id { get; set; }
        public string Item_Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<InvoiceInvoice_Item> InvoiceInvoiceItems { get; set; }
        public List<JobInvoice_Item> JobInvoiceItems { get; set; }

    }
}