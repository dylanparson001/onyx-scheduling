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
        private readonly IJobInvoiceItemRepository _jobInvoiceItemRepository;

        public JobsController(IJobsRepository jobsRepository, IJobInvoiceItemRepository jobInvoiceItemRepository)
        {
            _jobsRepository = jobsRepository;
            _jobInvoiceItemRepository = jobInvoiceItemRepository;
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
                Processing_Status = jobDto.processing_Status,
                CreatedDateTime = parsedCreatedDate,
                ScheduledStartDateTime = parsedScheduledStartDate,
                ScheduledEndDateTime = parsedScheduledEndDate,
                FinishedDateTime = parsedFinishedDateTime,
                Assigned_Technician_Id = jobDto.Assigned_Technician_Id,
                Assigned_Customer_Id = jobDto.Assigned_Customer_Id,
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

        [HttpGet]
        [Route("GetJobsByTechnician")]
        public async Task<ActionResult<List<Jobs>>> GetTechnicianJobsByDate(string date, string technicianId)
        {
            var requestedDate = DateTime.Parse(date);
            
            
            var result = await _jobsRepository.GetJobsByTechnicianAsync(requestedDate, technicianId);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("AddItemsToJob")]
        public async Task<ActionResult> AddItemsToJob(int jobId,  List<Invoice_Items> invoiceItems)
        {
            foreach (var item in invoiceItems)
            {
                await _jobInvoiceItemRepository.AddItemsToJob(jobId, item.Id, item.Quantity);
            }

            return Ok();
        }

        [HttpGet]
        [Route("GetItemsFromJob")]
        public async Task<ActionResult<List<InvoiceItemDto>>> GetItemsFromJob(int jobId)
        {
            var result = await _jobInvoiceItemRepository.GetItemsOfJob(jobId);

            var itemDtos = result.Select(item => new InvoiceItemDto()
            {
                Id = item.Id,
                Category_Id = item.Category_Id,
                Item_Name = item.Item_Name,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList();
            return Ok(itemDtos);
        }

        [HttpDelete]
        [Route("RemoveItemsFromJob")]
        public async Task<ActionResult> RemoveItemsFromJob(int jobId, int itemToDelete)
        {
            await _jobInvoiceItemRepository.RemoveItemsFromJob(jobId, itemToDelete);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateJobStatus")]
        public async Task<ActionResult> UpdateStatus(int jobId, string newStatus)
        {
            if (newStatus != null)
            {
                await _jobsRepository.ChangeProcessingStatus(jobId, newStatus);
            }

            return NoContent();
        }
    }
    
}
