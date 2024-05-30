using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<List<Invoices>> GetInvoicesAsync();
        public Task<List<Invoices>> GetInvoicesByTechnician(string technicianId);
        public Task AddInvoice(Invoices invoice,List<Invoice_Items> invoiceItemsList);
        public Task<List<Invoices>> GetInvoicesByDate(DateTime date);
        public Task RemoveInvoiceAsync(int invoiceId);
        public Task<List<Invoices>> GetInvoicesByDateAndStatus(DateTime date, string status, int position, int take);
        public Task<int> GetCountOfInvoicesByDateAndStatus(DateTime date, string status);
        public Task<Invoices> GetInvoiceFromJobId(int jobId);
    }
}
