using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnyxScheduling.Data.Repositories;
using OnyxScheduling.Dtos;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Office, Field, Admin")]
    public class JobsController : ControllerBase
    {
        private readonly IJobsRepository _jobsRepository;

        public JobsController(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        [HttpGet]
        [Route("GetJobsByDateAndStatus")]
        public async Task<ActionResult<List<Jobs>>> GetJobsByDateAndStatus(string setDate, string status, int position, int take)
        {
            if (setDate == null || status == null)
            {
                return BadRequest(ModelState);
            }
            var parsedDate = DateTime.Parse(setDate);
            var result = await _jobsRepository.GetJobsByDateAndStatusAsync(parsedDate, status, position, take);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            result = result.OrderBy(x => x.CreatedDateTime).ToList();

            return Ok(result);
        }

        [HttpPost]
        [Route("AddJob")]
        public async Task<ActionResult> AddJob(JobsDto jobDto)
        {
            var parsedCreatedDate = DateTime.Parse(jobDto.CreatedDateTime);
            var parsedScheduledStartDate = DateTime.Parse(jobDto.ScheduledStartDateTime);
            var parsedScheduledEndDate = DateTime.Parse(jobDto.ScheduledEndDateTime);
            var parsedFinishedDateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(jobDto.CompletedDateTime))
            {
                parsedFinishedDateTime = DateTime.Parse(jobDto.CompletedDateTime);
            }

            Jobs newJob = new Jobs()
            {
                Id = jobDto.Id,
                Address = jobDto.Address,
                //City = jobDto.City,
                Processing_Status = jobDto.Processing_Status,
                CreatedDateTime = parsedCreatedDate,
                ScheduledStartDateTime = parsedScheduledStartDate,
                ScheduledEndDateTime = parsedScheduledEndDate,
                FinishedDateTime = parsedFinishedDateTime,
                Assigned_Technician_Id = jobDto.Assigned_Technician_Id,
                Assigned_Customer_Id = jobDto.Assigned_Customer_id,
                Total_Price = 0.0,
                InvoiceNumber = jobDto.InvoiceNumber,
                InvoiceId = jobDto.InvoiceId
            };
            
            await _jobsRepository.AddJobs(newJob);

            return Ok(newJob);
        }

        [HttpGet]
        [Route("GetJobStatusList")]
        public async Task<ActionResult<List<string>>> GetJobStatusList()
        {
            var statuses = new List<string>()
            {
                "Open",
                "Pending",
                "Started",
                "Closed"
            };
            return statuses;
        }
    }
}
