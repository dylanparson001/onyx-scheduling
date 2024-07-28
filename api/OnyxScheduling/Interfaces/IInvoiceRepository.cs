using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<List<Invoices>> GetInvoicesAsync(string companyId);
        public Task<List<Invoices>> GetInvoicesByTechnician(string technicianId);
        public Task<List<Invoices>> GetInvoicesByTechnicianByDate(string technicianId, DateTime date);
        public Task AddInvoice(Invoices invoice,List<Invoice_Items> invoiceItemsList);
        public Task<List<Invoices>> GetInvoicesByDate(DateTime date, string companyId);
        public Task RemoveInvoiceAsync(int invoiceId);
        public Task<List<Invoices>> GetInvoicesByDateAndStatus(DateTime date, string status, int position, int take, string companyId);
        public Task<int> GetCountOfInvoicesByDateAndStatus(DateTime date, string status, string companyId);
        public Task<Invoices> GetInvoiceFromJobId(int jobId);
        public Task<Invoices> GetInvoiceFromInvoiceId(int invoiceId);
        public Task UpdateInvoiceFilePath(int invoiceId,string filePath);
        public Task<string> GetPdfFilePath(int invoiceId);
    }
}
