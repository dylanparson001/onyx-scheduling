using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext _context;
        private readonly IInvoiceInvoiceItemRepository _invoiceInvoiceItemRepository;

        public InvoiceRepository(DataContext context, IInvoiceInvoiceItemRepository invoiceInvoiceItemRepository)
        {
            _context = context;
            _invoiceInvoiceItemRepository = invoiceInvoiceItemRepository;
        }

        public async Task AddInvoice(Invoices invoice, List<Invoice_Items> invoiceItemsList)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            foreach (var item in invoiceItemsList)
            {
                await _invoiceInvoiceItemRepository.AddInvoiceInvoiceItems(invoice.Id, item.Id);
            }
        }

        public async Task<List<Invoices>> GetInvoicesAsync()
        {
            List<Invoices> result =
                await _context.Invoices.AsNoTracking().OrderBy(x => x.CreatedDateTime).ToListAsync();

            return result;
        }

        public async Task<List<Invoices>> GetInvoicesByDate(DateTime date)
        {
            var result = await _context.Invoices
                .Where(x => x.FinishedDateTime.Value.Date == date.Date)
                .ToListAsync();

            return result;
        }

        public async Task<List<Invoices>> GetInvoicesByDateAndStatus(DateTime date, string status, int position, int take)
        {
            var result = new List<Invoices>();
            switch (status)
            {
                // If invoice hasn't been completed yet, get invoice by the scheduled start time
                case "Paid":
                case "Financed":
                    result = await _context.Invoices
                            
                        .Where(x => x.ScheduledStartDateTime.Month == date.Month &&
                                    x.ScheduledStartDateTime.Day == date.Day &&
                                    x.Processing_Status == status)
                        .OrderBy(x => x.ScheduledStartDateTime)
                        .Skip(position)
                        .Take(take)
                        .ToListAsync();
                    break;
                // If it has been completed, get the invoice by the finished date
                case "Unpaid":
                    result = await _context.Invoices
                        .Where(x => x.FinishedDateTime.Value.Month == date.Month &&
                                    x.FinishedDateTime.Value.Day == date.Day &&
                                    x.Processing_Status == status)
                        
                        .OrderBy(x => x.FinishedDateTime)
                        .Skip(position)
                        .Take(take)
                        .ToListAsync();
                    break;
                // If for some reason another option was sent, return null
                default:
                    return null;
            }

            return result;
        }

        public async Task<int> GetCountOfInvoicesByDateAndStatus(DateTime date, string status)
        {
            int result;
            switch (status)
            {
                // If invoice hasn't been completed yet, get invoice by the scheduled start time
                case "Paid":
                case "Financed":
                    result = await _context.Invoices

                        .Where(x => x.ScheduledStartDateTime.Month == date.Month &&
                                    x.ScheduledStartDateTime.Day == date.Day &&
                                    x.Processing_Status == status)
                        .CountAsync();
                        
                    break;
                // If it has been completed, get the invoice by the finished date
                case "Unpaid":
                    result = await _context.Invoices
                        .Where(x => x.FinishedDateTime.Value.Month == date.Month &&
                                    x.FinishedDateTime.Value.Month == date.Day &&
                                    x.Processing_Status == status)
                        .CountAsync();
                    break;
                // If for some reason another option was sent, return null
                default:
                    return 0;
            }

            return result;
        }

        public async Task<Invoices> GetInvoiceFromJobId(int jobId)
        {
            return await _context.Invoices.FirstOrDefaultAsync(x => x.JobId == jobId);
        }

        public async Task<Invoices> GetInvoiceFromInvoiceId(int invoiceId)
        {
            return await _context.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);
        }

        public async Task UpdateInvoiceFilePath(int invoiceId, string filePath)
        {
            var result = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);

            result.FilePath = filePath;
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetPdfFilePath(int invoiceId)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);

            return invoice.FilePath;
        }

        public async Task<List<Invoices>> GetInvoicesByTechnician(string technician_id)
        {
            List<Invoices> result = await _context.Invoices
                .Where(x => x.Assigned_Technician_Id == technician_id)
                .ToListAsync();

            return result;
        }


        public async Task RemoveInvoiceAsync(int invoiceId)
        {
            var invoices = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);

            _context.Invoices.Remove(invoices);

            await _context.SaveChangesAsync();
        }
    }
}