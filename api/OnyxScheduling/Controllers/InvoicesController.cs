using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnyxScheduling.Dtos;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Office, Field, Admin")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly PdfService _pdfService;
        private readonly IAccountRepository _accountRepository;
        private readonly UserController _userController;


        public InvoicesController(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository, PdfService pdfService, IAccountRepository accountRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _pdfService = pdfService;
            _accountRepository = accountRepository;
        }
        private string GetCompanyIdFromHeader()
        {            
            Request.Headers.TryGetValue("CompanyId", out var companyIdHeader);

            return companyIdHeader.FirstOrDefault();
        }

        [HttpGet]
        [Route("GetAllInvoices")]
        public async Task<ActionResult<List<Invoices>>> GetAllInvoices()
        {
            var companyId = GetCompanyIdFromHeader();

            var result = await _invoiceRepository.GetInvoicesAsync(companyId);

            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetInvoicesByTechnician")]
        public async Task<ActionResult<List<Invoices>>> GetInvoicesByTechnician(string technicianId)
        {
            var result = await _invoiceRepository.GetInvoicesByTechnician(technicianId);

            return Ok(result);
        }

        [HttpGet]
        [Route("GetInvoicesByDate")]
        public async Task<ActionResult<List<Invoices>>> GetInvoicesByDate(string setDate, string status, int position, int take)
        {
            if (setDate == null || status == null)
            {
                return BadRequest(ModelState);
            }
            var companyId = GetCompanyIdFromHeader();

            var parsedDate = DateTime.Parse(setDate);
            var result = await _invoiceRepository.GetInvoicesByDateAndStatus(parsedDate, status,
                position, take, companyId);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            result = result.OrderBy(x => x.CreatedDateTime).ToList();

            return Ok(result);
        }
        
        [HttpGet]
        [Route("GetCountOfInvoicesByDate")]
        public async Task<ActionResult<List<Invoices>>> GetCountOfInvoicesByDate(string setDate, string status)
        {
            if (setDate == null || status == null)
            {
                return BadRequest(ModelState);
            }
            var companyId = GetCompanyIdFromHeader();

            var parsedDate = DateTime.Parse(setDate);
            var result = await _invoiceRepository.GetCountOfInvoicesByDateAndStatus(parsedDate, status, companyId);

            return Ok(result);
        }
        [HttpPost]
        [Route("AddInvoice")]
        public async Task<ActionResult> AddInvoice(InvoiceDto invoice)
        {
            if (invoice == null)
            {
                return BadRequest();
            }

            var parsedDate = DateTime.Parse(invoice.CreatedDateTime);
            DateTime? parsedFinishedDate = DateTime.Parse(invoice.CreatedDateTime);
            var parsedScheduledStartDate = DateTime.Parse(invoice.ScheduledStartDateTime);
            var parsedScheduledEndDate = DateTime.Parse(invoice.ScheduledEndDateTime);

            if (invoice.Processing_Status == "Paid")
            {
                parsedFinishedDate = DateTime.Now;
            } else
            {
                parsedFinishedDate = null;
            }


            var newInvoice = new Invoices()
            {
                CreatedDateTime = parsedDate,
                FinishedDateTime = parsedFinishedDate,
                ScheduledStartDateTime = parsedScheduledStartDate,
                ScheduledEndDateTime = parsedScheduledEndDate,
                Assigned_Customer_Id = invoice.Assigned_Customer_id,
                Assigned_Technician_Id = invoice.Assigned_Technician_Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Total_Price = 0.0,
                Processing_Status = invoice.Processing_Status,
                Address = invoice.Address,
                JobId = invoice.JobId
            };


            return NoContent();
        }

        [HttpGet]
        [Route("GetPdfFromInvoice")]
        public async Task<ActionResult> GetInvoicePdf(int invoiceId)
        {
            var pdfPath =await  _invoiceRepository.GetPdfFilePath(invoiceId);
            
            if (string.IsNullOrEmpty(pdfPath) || !System.IO.File.Exists(pdfPath))
            {
                return NotFound();
            }
            byte[] pdfBytes = await System.IO.File.ReadAllBytesAsync(pdfPath);
            var pdfStream = new MemoryStream(pdfBytes);
        
            return new FileStreamResult(pdfStream, "application/pdf")
            {
                FileDownloadName = $"Invoice_{invoiceId}.pdf"
            };
        }
       

        [HttpDelete]
        [Route("RemoveInvoice")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemoveInvoice(int invoiceId)
        {
            await _invoiceRepository.RemoveInvoiceAsync(invoiceId);

            return Ok();
        }

        [HttpGet]
        [Route("GetProcessingStatuses")]
        public ActionResult<string[]> GetProcessingStatuses()
        {
            string[] result = { "Paid",
                "Unpaid",
                "Financed"};

            return Ok(result);
        }

    }
}
