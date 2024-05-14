using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IInvoiceInvoiceItemRepository
    {
        public Task<List<Invoice_Items>> GetInvoiceItemsOfInvoice(int invoiceId);
        public Task AddInvoiceInvoiceItems(int invoiceId, int itemId);
    }
}
