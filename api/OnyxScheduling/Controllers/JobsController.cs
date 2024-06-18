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
        private readonly IAccountRepository _accountRepository;
        private readonly PdfService _pdfService;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceInvoiceItemRepository _invoiceInvoiceItemRepository;

        public JobsController(IJobsRepository jobsRepository,
            IJobInvoiceItemRepository jobInvoiceItemRepository,
            IAccountRepository accountRepository,
            PdfService pdfService,
            IInvoiceItemRepository invoiceItemRepository,
            IInvoiceRepository invoiceRepository,
            IInvoiceInvoiceItemRepository invoiceInvoiceItemRepository
            )
        {
            _jobsRepository = jobsRepository;
            _jobInvoiceItemRepository = jobInvoiceItemRepository;
            _accountRepository = accountRepository;
            _pdfService = pdfService;
            _invoiceItemRepository = invoiceItemRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceInvoiceItemRepository = invoiceInvoiceItemRepository;
        }
        
        private string GetCompanyIdFromHeader()
        {            
            Request.Headers.TryGetValue("CompanyId", out var companyIdHeader);

            return companyIdHeader.FirstOrDefault();
        }

        [HttpGet]
        [Route("GetJobsByDateAndStatus")]
        public async Task<ActionResult<List<Jobs>>> GetJobsByDateAndStatus(string setDate, string status, int position, int take)
        {
            var companyId = GetCompanyIdFromHeader();
            if (setDate == null || status == null)
            {
                return BadRequest(ModelState);
            }
            var parsedDate = DateTime.Parse(setDate);
            var result = await _jobsRepository.GetJobsByDateAndStatusAsync(parsedDate, status, position, take, companyId);

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
                InvoiceId = jobDto.InvoiceId
            };
            
            await _jobsRepository.AddJobs(newJob);

            return Ok(newJob);
        }

        [HttpPost]
        [Route("GenerateInvoiceFromJob")]
        public async Task<ActionResult> GenerateInvoiceFromJob(int jobId)
        {
            var jobResult = await _jobsRepository.GetJobById(jobId);
            
            if (jobResult == null) return BadRequest();
            if (jobResult.Processing_Status != "Closed")
            {
                await _jobsRepository.ChangeProcessingStatus(jobId, "Closed");
            };
            
            await GenerateInvoicePdfFromJob(jobResult);
            return NoContent();
        }

        [HttpPost]
        public async Task GenerateInvoicePdfFromJob(Jobs job)
        {
            if (job == null)
            {
                return;
            }

            var completedTimeNow = DateTime.Now;
            var customer = await  _accountRepository.GetCustomersFromCustomerId(job.Assigned_Customer_Id);
            var technician = await _accountRepository.GetTechnciainsFromTechId(job.Assigned_Technician_Id);
            var itemsFromJob = await _jobInvoiceItemRepository.GetItemsOfJob(job.Id);
            double tempPrice = 0.0;
            
            foreach (var jobItem in itemsFromJob)
            {
                var itemPrice = await _invoiceItemRepository.GetPriceOfItem(jobItem.Id);

                tempPrice += (itemPrice * jobItem.Quantity);
            }
            
            string fileName =
                $"./Invoices/{customer.FirstName} {customer.LastName} {job.ScheduledEndDateTime.ToLongDateString()}.pdf";
            if (!Directory.Exists("Invoices"))
            {
                Directory.CreateDirectory("./Invoices");
            }

        
            if (CheckFileExists(fileName))
            {
                string safeFirstName = customer.FirstName.Replace(":", "-").Replace("/", "-").Replace("\\", "-");
                string safeLastName = customer.LastName.Replace(":", "-").Replace("/", "-").Replace("\\", "-");
                string safeDate = job.ScheduledEndDateTime.ToLongDateString().Replace(":", "-").Replace("/", "-").Replace("\\", "-");
                string safeTime = job.ScheduledEndDateTime.ToShortTimeString().Replace(":", "-").Replace("/", "-").Replace("\\", "-");

                fileName = $"./Invoices/{safeFirstName} {safeLastName} {safeDate} {safeTime}.pdf";
            }
        
            var newInvoice = new Invoices()
            {
                CreatedDateTime = job.CreatedDateTime,
                FinishedDateTime = completedTimeNow,
                ScheduledStartDateTime = job.ScheduledStartDateTime,
                ScheduledEndDateTime = job.ScheduledEndDateTime,
                Assigned_Customer_Id = job.Assigned_Customer_Id,
                Assigned_Technician_Id = job.Assigned_Technician_Id,
                Processing_Status = "Paid",
                InvoiceNumber = job.InvoiceNumber,
                Total_Price = tempPrice,
                Address = job.Address,
                JobId = job.Id,
                FilePath = fileName,
                CompanyId = technician.CompanyId
            };
            
            
            _pdfService.GeneratePdf(newInvoice, customer, technician, itemsFromJob, fileName);
            
            await _invoiceRepository.AddInvoice(newInvoice, itemsFromJob);
        }
        
        private bool CheckFileExists(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                return true;
            }

            return false;
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
                "Cancelled"
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
    
    [HttpGet]
    [Route("GetJobById")]
    public async Task<ActionResult<Jobs>> GetJobById(int jobId)
    {
        var result = _jobsRepository.GetJobById(jobId);
        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPut]
    [Route("UpdateJobTimes")]
    public async Task<ActionResult> UpdateJobTimes(int jobId, string newStartTime, string newEndTime)
    {
        var newDateTimeStart = DateTime.Parse(newStartTime);
        var newDateTimeEnd = DateTime.Parse(newEndTime);

        await _jobsRepository.UpdateJobSchedule(jobId, newDateTimeStart, newDateTimeEnd);

        return Ok();
    }

    }
}
