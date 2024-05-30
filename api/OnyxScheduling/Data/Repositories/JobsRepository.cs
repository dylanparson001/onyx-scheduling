using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Data.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly DataContext _context;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IInvoiceInvoiceItemRepository _invoiceInvoiceItemRepository;

        public JobsRepository(DataContext context, IInvoiceItemRepository invoiceItemRepository, IInvoiceInvoiceItemRepository invoiceInvoiceItemRepository)
        {
            _context = context;
            _invoiceItemRepository = invoiceItemRepository;
            _invoiceInvoiceItemRepository = invoiceInvoiceItemRepository;
        }

        public async Task AddJobs(Jobs job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
        }
        
        public async Task ChangeProcessingStatus(int jobId, string processingStatus)
        {
            var result = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == jobId);

            result.Processing_Status = processingStatus;
            await _context.SaveChangesAsync();
        }

        public async Task<Jobs> GetJobById(int jobId)
        {
            var result = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == jobId);

            return result;
        }

        public async Task UpdateJobSchedule(int jobId, DateTime startTime, DateTime endTime)
        {
            var jobResult = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == jobId);

            jobResult.ScheduledStartDateTime = startTime;
            jobResult.ScheduledEndDateTime = endTime;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Jobs>> GetJobsByDateAndStatusAsync(DateTime date, string status, int position, int take)
        {
            var result = new List<Jobs>();
            switch (status)
            {
                case "Open":
                case "Pending":
                case "Started":
                    result = await _context.Jobs
                        .Where(x => x.ScheduledStartDateTime.Month == date.Month &&
                                    x.ScheduledStartDateTime.Day == date.Day &&
                                    x.Processing_Status == status)
                        .OrderBy(x => x.ScheduledStartDateTime)
                        .Skip(position)
                        .Take(take)
                        .ToListAsync();

                    break;
                case "Completed":
                    result = await _context.Jobs
                        .Where(x => x.FinishedDateTime.Value.Month == date.Month &&
                                    x.FinishedDateTime.Value.Month == date.Day &&
                                    x.Processing_Status == status)
                        .OrderBy(x => x.FinishedDateTime)
                        .Skip(position)
                        .Take(take)
                        .ToListAsync();
                    break;

            }

            return result;
        }

        public async Task<List<Jobs>> GetJobsByTechnicianAsync(DateTime date, string technicianId)
        {
            var result = await _context.Jobs
                .Where(x => x.Assigned_Technician_Id == technicianId &&
                            x.ScheduledStartDateTime.Month == date.Month &&
                            x.ScheduledStartDateTime.Day == date.Day)
                .OrderBy(x => x.ScheduledStartDateTime)
                .ToListAsync();

            return result;
        }
        
    }
}
