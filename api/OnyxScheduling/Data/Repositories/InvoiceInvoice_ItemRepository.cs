using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Data.Repositories
{
    public class InvoiceInvoice_ItemRepository : IInvoiceInvoiceItemRepository
    {
        private readonly DataContext _context;

        public InvoiceInvoice_ItemRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice_Items>> GetInvoiceItemsOfInvoice(int invoiceId)
        {
            var invoiceInvoiceItems = await _context.InvoiceInvoice_Item.Where(x => x.InvoiceId == invoiceId).ToListAsync();

            List<Invoice_Items> result = new List<Invoice_Items>();

            foreach (var item in invoiceInvoiceItems)
            {
                result.Add(await _context.Invoice_Items.FirstOrDefaultAsync(x => x.Id == item.InvoiceItemId));
            }

            return result;
        }
    }
}
