using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IJobsRepository
    {
        public Task<List<Jobs>> GetJobsByDateAndStatusAsync(DateTime date, string status, int position, int take);
        public Task<List<Jobs>> GetJobsByTechnicianAsync(DateTime date, string technicianId );
        public Task AddJobs(Jobs job);
        public Task AddItemsToJob(int jobId, int invoiceItemId);
        
    }
}
