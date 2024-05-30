using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface IJobsRepository
    {
        public Task<List<Jobs>> GetJobsByDateAndStatusAsync(DateTime date, string status, int position, int take);
        public Task<List<Jobs>> GetJobsByTechnicianAsync(DateTime date, string technicianId );
        public Task AddJobs(Jobs job);
        public Task ChangeProcessingStatus(int jobId, string processingStatus);
        public Task<Jobs> GetJobById(int jobId);
        public Task UpdateJobSchedule(int jobId, DateTime startTime, DateTime endTime);

    }
}
