using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<List<Invoices>> GetInvoicesAsync();
        public Task<List<Invoices>> GetInvoicesByTechnician(int technicianId);
        public Task AddInvoice(Invoices invoice);
        public Task RemoveInvoiceAsync(int invoiceId);
    }
}
