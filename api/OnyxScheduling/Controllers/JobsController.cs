using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnyxScheduling.Data.Repositories;
using OnyxScheduling.Dtos;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;
using System;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using OnyxScheduling.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private readonly IEmailService _emailService;

        public JobsController(IJobsRepository jobsRepository,
            IJobInvoiceItemRepository jobInvoiceItemRepository,
            IAccountRepository accountRepository,
            PdfService pdfService,
            IInvoiceItemRepository invoiceItemRepository,
            IInvoiceRepository invoiceRepository,
            IInvoiceInvoiceItemRepository invoiceInvoiceItemRepository,
            IEmailService emailService
            )
        {
            _jobsRepository = jobsRepository;
            _jobInvoiceItemRepository = jobInvoiceItemRepository;
            _accountRepository = accountRepository;
            _pdfService = pdfService;
            _invoiceItemRepository = invoiceItemRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceInvoiceItemRepository = invoiceInvoiceItemRepository;
            _emailService = emailService;

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
                InvoiceId = jobDto.InvoiceId,
                Description = jobDto.Description,
            };

            await _jobsRepository.AddJobs(newJob);

            var customer = await _accountRepository.GetCustomersFromCustomerId(jobDto.Assigned_Customer_Id);
            var technician = await _accountRepository.GetTechnciainsFromTechId(jobDto.Assigned_Technician_Id);

            string customerEmailHtml = $@" 
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Appointment Confirmation</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                    }}
                    .header {{
                        text-align: center;
                        padding-bottom: 20px;
                        border-bottom: 1px solid #dddddd;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 24px;
                        color: #333333;
                    }}
                    .content {{
                        padding: 20px 0;
                    }}
                    .content p {{
                        font-size: 16px;
                        color: #555555;
                        line-height: 1.6;
                    }}
                    .content .highlight {{
                        font-weight: bold;
                        color: #333333;
                    }}
                    .footer {{
                        text-align: center;
                        padding-top: 20px;
                        border-top: 1px solid #dddddd;
                    }}
                    .footer p {{
                        font-size: 14px;
                        color: #999999;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Appointment Confirmation</h1>
                    </div>
                    <div class=""content"">
                        <p>Hello, <span class=""highlight"">{customer.FirstName} {customer.LastName}</span></p>
                        <p>Your appointment has successfully been scheduled for <span class=""highlight"">{parsedScheduledStartDate}</span>.</p>
                        <p>Your technician is: <span class=""highlight"">{technician.FirstName} {technician.LastName}</span> and will call you up to an hour prior to confirm the arrival time.</p>
                        <p>Thank you.</p>
                    </div>
                    <div class=""footer"">
                        <p>&copy; 2024 Onyx Solutions. All rights reserved.</p>
                    </div>
                </div>
            </body>
            </html>";

            string technicianEmailHtml = $@" 
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Appointment Confirmation</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                    }}
                    .header {{
                        text-align: center;
                        padding-bottom: 20px;
                        border-bottom: 1px solid #dddddd;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 24px;
                        color: #333333;
                    }}
                    .content {{
                        padding: 20px 0;
                    }}
                    .content p {{
                        font-size: 16px;
                        color: #555555;
                        line-height: 1.6;
                    }}
                    .content .highlight {{
                        font-weight: bold;
                        color: #333333;
                    }}
                    .footer {{
                        text-align: center;
                        padding-top: 20px;
                        border-top: 1px solid #dddddd;
                    }}
                    .footer p {{
                        font-size: 14px;
                        color: #999999;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>New Job</h1>
                    </div>
                    <div class=""content"">
                        <p>Hello, <span class=""highlight"">{technician.FirstName} {technician.LastName}</span></p>
                        <p>New job has been scheduled for  <span class=""highlight"">{parsedScheduledStartDate}</span> to <span class=""highlight"">{parsedScheduledEndDate}</span>.</p>
                        <p>Description: {newJob.Description}</p>
                        <p>Address: {newJob.Address}</p>
                        <p>Thank you.</p>
                    </div>
                    <div class=""footer"">
                        <p>&copy; 2024 Onyx Solutions. All rights reserved.</p>
                    </div>
                </div>
            </body>
            </html>";

            _emailService.SendEmail(
                $"{customer.Email}",
                $"New Job {parsedScheduledStartDate}",
                customerEmailHtml
                );
            _emailService.SendEmail(
                $"{technician.Email}",
                $"New Job {parsedScheduledStartDate} - {parsedScheduledEndDate}",
                technicianEmailHtml
                );

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
        public async System.Threading.Tasks.Task GenerateInvoicePdfFromJob(Jobs job)
        {

            // Need to make this adjustable to different time zones
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime utcTime = DateTime.UtcNow;

            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, easternZone);
            var completedTimeNow = easternTime;

            var customer = await _accountRepository.GetCustomersFromCustomerId(job.Assigned_Customer_Id);
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

            string customerEmailHtml = $@" 
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Appointment Confirmation</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                    }}
                    .header {{
                        text-align: center;
                        padding-bottom: 20px;
                        border-bottom: 1px solid #dddddd;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 24px;
                        color: #333333;
                    }}
                    .content {{
                        padding: 20px 0;
                    }}
                    .content p {{
                        font-size: 16px;
                        color: #555555;
                        line-height: 1.6;
                    }}
                    .content .highlight {{
                        font-weight: bold;
                        color: #333333;
                    }}
                    .footer {{
                        text-align: center;
                        padding-top: 20px;
                        border-top: 1px solid #dddddd;
                    }}
                    .footer p {{
                        font-size: 14px;
                        color: #999999;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Appointment Completed</h1>
                    </div>
                    <div class=""content"">
                        <p>Hello, <span class=""highlight"">{customer.FirstName} {customer.LastName}</span></p>
                        <p>Your appointment has successfully been completed at <span class=""highlight"">{job.FinishedDateTime}</span>.</p>
                        <p>Your technician was: <span class=""highlight"">{technician.FirstName} {technician.LastName}</span> .</p>
                        <p>Thank you for you business and please provide a review on Google.</p>
                        <p>Thank you</p>
                    </div>
                    <div class=""footer"">
                        <p>&copy; 2024 Onyx Solutions. All rights reserved.</p>
                    </div>
                </div>
            </body>
            </html>";

            string technicianEmailHtml = $@" 
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Appointment Confirmation</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 0;
                        padding: 0;
                        background-color: #f4f4f4;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 20px auto;
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                    }}
                    .header {{
                        text-align: center;
                        padding-bottom: 20px;
                        border-bottom: 1px solid #dddddd;
                    }}
                    .header h1 {{
                        margin: 0;
                        font-size: 24px;
                        color: #333333;
                    }}
                    .content {{
                        padding: 20px 0;
                    }}
                    .content p {{
                        font-size: 16px;
                        color: #555555;
                        line-height: 1.6;
                    }}
                    .content .highlight {{
                        font-weight: bold;
                        color: #333333;
                    }}
                    .footer {{
                        text-align: center;
                        padding-top: 20px;
                        border-top: 1px solid #dddddd;
                    }}
                    .footer p {{
                        font-size: 14px;
                        color: #999999;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <div class=""header"">
                        <h1>Appointment Completed</h1>
                    </div>
                    <div class=""content"">
                        <p>Hello, <span class=""highlight"">{technician.FirstName} {technician.LastName}</span></p>
                        <p>Your appointment has successfully been completed at <span class=""highlight"">{job.FinishedDateTime}</span>.</p>
                        <p>Your customer was: <span class=""highlight"">{customer.FirstName} {customer.LastName}</span> .</p>
                        <p>Your total sale was: ${newInvoice.Total_Price}</p>
                        <p>Thank you</p>
                    </div>
                    <div class=""footer"">
                        <p>&copy; 2024 Onyx Solutions. All rights reserved.</p>
                    </div>
                </div>
            </body>
            </html>";

            //var pdfPath = await _invoiceRepository.GetPdfFilePath(newInvoice.InvoiceId);

            //if (string.IsNullOrEmpty(pdfPath) || !System.IO.File.Exists(pdfPath))
            //{
            //    return;
            //}
            //byte[] pdfBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);
            //var pdfStream = new MemoryStream(pdfBytes);

            //var invoiceToSend = new FileStreamResult(pdfStream, "application/pdf")
            //{
            //    FileDownloadName = $"Invoice_{newInvoice.InvoiceId}.pdf"
            //};

            _emailService.SendEmail(
                 customer.Email,
                 $"Appointment Completed",
                 customerEmailHtml
                 
                 );
            _emailService.SendEmail(
                 technician.Email,
                 $"Appointment Completed",
                 technicianEmailHtml
                 
                 );
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
        public async Task<ActionResult> AddItemsToJob(int jobId, List<Invoice_Items> invoiceItems)
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
