using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnyxScheduling.Dtos;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*    [Authorize(Roles = "Office, Field, Admin")]*/
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;

        public InvoicesController(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository )
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
        }

        [HttpGet]
        [Route("GetAllInvoices")]
        public async Task<ActionResult<List<Invoices>>> GetAllInvoices()
        {

            var result = await _invoiceRepository.GetInvoicesAsync();
            
            if ( result == null )
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

            return result;
        }

        [HttpGet]
        [Route("GetInvoicesByDate")]
        public async Task<ActionResult<List<Invoices>>> GetInvoicesByDate(string setDate)
        {
            var parsedDate = DateTime.Parse(setDate);
            var result = await _invoiceRepository.GetInvoicesByDate(parsedDate);

            if (result == null)
            {
                return BadRequest(ModelState);
            }


            return Ok(result);
        }

        [HttpPost]
        [Route("AddInvoice")]
        public async Task<ActionResult> AddInvoice(Invoices invoice)
        {
            if (invoice == null)
            {
                return BadRequest();
            }


 /*           foreach(var item in invoice.InvoiceInvoice_Items)
            {
                var price = await _invoiceItemRepository.GetPriceOfItem(item.InvoiceItemId);
                invoice.Total_Price += price * item.Quantity;
            }*/

            await _invoiceRepository.AddInvoice(invoice);

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
                
        
    }
}
