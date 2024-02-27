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

        public InvoicesController(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
        }

        [HttpGet]
        [Route("GetAllInvoices")]
        public async Task<ActionResult<List<Invoices>>> GetAllInvoices()
        {

            var result = await _invoiceRepository.GetInvoicesAsync();

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
        public async Task<ActionResult<List<Invoices>>> GetInvoicesByDate(string setDate, string status)
        {
            if (setDate == null || status == null)
            {
                return BadRequest(ModelState);
            }
            var parsedDate = DateTime.Parse(setDate);
            var result = await _invoiceRepository.GetInvoicesByDateAndStatus(parsedDate, status);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            result = result.OrderBy(x => x.CreatedDateTime).ToList();

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

            if (invoice.Processing_Status == "Completed")
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
                InvoiceInvoice_Items = new List<InvoiceInvoice_Item>(),
                Processing_Status = invoice.Processing_Status,
                Address = invoice.Address
            };

            if (newInvoice.InvoiceInvoice_Items.Count != 0)
            {

                foreach (var item in newInvoice.InvoiceInvoice_Items)
                {
                    var price = await _invoiceItemRepository.GetPriceOfItem(item.InvoiceItemId);
                    newInvoice.Total_Price += price * item.Quantity;
                }

            }

            await _invoiceRepository.AddInvoice(newInvoice);

            return NoContent();
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
            string[] result = { "Open",
                "Pending",
                "Started",
                "Completed"};

            return Ok(result);
        }

    }
}
