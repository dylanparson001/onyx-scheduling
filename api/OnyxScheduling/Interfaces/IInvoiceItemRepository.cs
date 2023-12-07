using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IInvoiceItemRepository
    {
        public Task<List<Invoice_Items>> GetAllInvoiceItems();
        public Task<List<Invoice_Items>> GetAllInvoiceItemsByCateogry(int categoryId);
        public Task AddInvoiceItems(Invoice_Items item);
        public Task<bool> InvoiceItemCategoryExists(int categoryId);
    }
}
