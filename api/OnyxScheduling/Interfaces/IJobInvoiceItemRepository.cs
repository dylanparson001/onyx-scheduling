using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces;

public interface IJobInvoiceItemRepository
{
    public Task<List<Invoice_Items>> GetItemsOfJob(int JobId);
    public Task AddItemsToJob(int jobId, int itemId, int quantity); 
}