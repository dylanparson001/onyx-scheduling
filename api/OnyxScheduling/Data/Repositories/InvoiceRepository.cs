using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext _context;

        public InvoiceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddInvoice(Invoices invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Invoices>> GetInvoicesAsync()
        {
            List<Invoices> result = await _context.Invoices.ToListAsync();

            return result;
        }

        public async Task<List<Invoices>> GetInvoicesByTechnician(int technician_id)
        {
            List<Invoices> result = await _context.Invoices.Where(x => x.Assigned_Technician_Id == technician_id).ToListAsync();

            return result;
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task RemoveInvoiceAsync(int invoiceId)
        {
            var invoices = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);

            _context.Invoices.Remove(invoices);

            await _context.SaveChangesAsync();

            
        }
    }
}
