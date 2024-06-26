﻿using Microsoft.EntityFrameworkCore;
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

        public async Task AddInvoiceInvoiceItems(int invoiceId, int itemId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            var invoiceItem = await _context.Invoice_Items.FindAsync(itemId);

            var newItem = new InvoiceInvoice_Item()
            {
                InvoiceId = invoiceId,
                Invoice = invoice,
                InvoiceItemId = itemId,
                InvoiceItem = invoiceItem,
            };
            await _context.InvoiceInvoice_Item.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Invoice_Items>> GetInvoiceItemsOfInvoice(int invoiceId)
        {
            var invoiceInvoiceItems = await _context.InvoiceInvoice_Item.Where(x => x.InvoiceId == invoiceId).ToListAsync();

            List<Invoice_Items> result = new List<Invoice_Items>();
            if (invoiceInvoiceItems.Count == 0)
            {
                return result;
            }

            foreach (var item in invoiceInvoiceItems)
            {
                result.Add(await _context.Invoice_Items.FirstOrDefaultAsync(x => x.Id == item.InvoiceItemId));
            }

            return result;
        }
    }
}
