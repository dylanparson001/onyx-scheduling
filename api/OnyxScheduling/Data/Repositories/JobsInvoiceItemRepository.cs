using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Data.Repositories;

public class JobsInvoiceItemRepository: IJobInvoiceItemRepository
{
    private readonly DataContext _context;

    public JobsInvoiceItemRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<List<Invoice_Items>> GetItemsOfJob(int JobId)
    {
        var jobInvoiceItemResult = await _context.JobInvoice_Item.Where(x => x.JobId == JobId).ToListAsync();
        var invoiceItemResult = new List<Invoice_Items>();

        foreach (var jobInvoiceItem in jobInvoiceItemResult)
        {
            var itemResult = await _context.Invoice_Items.FirstOrDefaultAsync(x => x.Id == jobInvoiceItem.InvoiceItemId);

            if (itemResult != null)
            {
                invoiceItemResult.Add(itemResult);
            }
        }
        
        return invoiceItemResult;
        
    }

    public async Task AddItemsToJob(int jobId, int itemId, int quantity)
    {
        var existingItem =
           await _context.JobInvoice_Item.FirstOrDefaultAsync(x => x.JobId == jobId && x.InvoiceItemId == itemId);

        // if item has been added to this job already, set the new quantity
        if (existingItem != null)
        {
            existingItem.Quantity = quantity;
            await _context.SaveChangesAsync();
            return;
        }
        var newJobInvoiceItem = new JobInvoice_Item()
        {
            JobId = jobId,
            InvoiceItemId = itemId,
            Quantity = quantity
        };

        await _context.JobInvoice_Item.AddAsync(newJobInvoiceItem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveItemsFromJob(int jobId, int itemId)
    {
        var existingItem =
            await _context.JobInvoice_Item.FirstOrDefaultAsync(x => x.JobId == jobId && x.InvoiceItemId == itemId);

        if (existingItem == null)
        {
            return;
        }

        _context.JobInvoice_Item.Remove(existingItem);
        await _context.SaveChangesAsync();
    }
}