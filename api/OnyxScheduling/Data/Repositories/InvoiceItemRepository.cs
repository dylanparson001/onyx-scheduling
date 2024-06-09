using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Data.Repositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly DataContext _context;

        public InvoiceItemRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddInvoiceItems(Invoice_Items item)
        {
            await _context.Invoice_Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Invoice_Items>> GetAllInvoiceItems()
        {
            return await _context.Invoice_Items.ToListAsync();
        }

        public async Task<List<Invoice_Items>> GetAllInvoiceItemsByCateogry(int categoryId)
        {
            return await _context.Invoice_Items.Where(x => x.Category_Id == categoryId).ToListAsync();
        }

        public async Task<Invoice_Items> GetItemById(int id)
        {
            return await _context.Invoice_Items.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<double> GetPriceOfItem(int id)
        {
            var result =  await _context.Invoice_Items.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return 0.0;
            }

            return result.Price;
        }

        public async Task<bool> InvoiceItemCategoryExists(int categoryId)
        {
            var category = await _context.Category.AsNoTracking().FirstOrDefaultAsync(x => x.Id== categoryId);
            if (category== null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<Category>> GetCategories()
        {
            var result = await _context.Category.ToListAsync();

            return result;
        }
    }
}
