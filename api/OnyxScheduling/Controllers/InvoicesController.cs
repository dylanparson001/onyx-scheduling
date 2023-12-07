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

        public InvoicesController(IInvoiceRepository invoiceRepository )
        {
            _invoiceRepository = invoiceRepository;
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
        public async Task<ActionResult<List<Invoices>>> GetInvoicesByTechnician(int technicianId)
        {
            var result = await _invoiceRepository.GetInvoicesByTechnician(technicianId);

            return result;
        }

        [HttpPost]
        [Route("AddInvoice")]
        public async Task<ActionResult> AddInvoice(Invoices invoice)
        {
            if (invoice == null)
            {
                return BadRequest();
            }


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
