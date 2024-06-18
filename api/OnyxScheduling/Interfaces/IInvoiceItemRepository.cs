using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IInvoiceItemRepository
    {
        public Task<List<Invoice_Items>> GetAllInvoiceItems(string companyId);
        public Task<Invoice_Items> GetItemById(int id);
        public Task<List<Invoice_Items>> GetAllInvoiceItemsByCateogry(int categoryId, string companyId);
        public Task AddInvoiceItems(Invoice_Items item);
        public Task<double> GetPriceOfItem(int id);
        public Task<bool> InvoiceItemCategoryExists(int categoryId);
        public Task<List<Category>> GetCategories();
        public Task DeleteItem(Invoice_Items item);

        public Task<int> GetCountOfItemsByCategory(int categoryId);
        public Task<List<Invoice_Items>> GetItemsTakeAndPosition(int categoryId, int take, int position);
    }
}
