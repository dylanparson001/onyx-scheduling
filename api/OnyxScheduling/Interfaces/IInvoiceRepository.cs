using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<List<Invoices>> GetInvoicesAsync();
        public Task<List<Invoices>> GetInvoicesByTechnician(string technicianId);
        public Task AddInvoice(Invoices invoice);
        public Task<List<Invoices>> GetInvoicesByDate(DateTime date);
        public Task RemoveInvoiceAsync(int invoiceId);
    }
}
