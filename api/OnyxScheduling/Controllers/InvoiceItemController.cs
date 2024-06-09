using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceItemController : ControllerBase
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IInvoiceInvoiceItemRepository _invoiceInvoiceItemRepository;

        public InvoiceItemController(IInvoiceItemRepository invoiceItemRepository, ICategoryRepository categoryRepository, IInvoiceInvoiceItemRepository invoiceInvoiceItemRepository)
        {
            _invoiceItemRepository = invoiceItemRepository;
            _categoryRepository = categoryRepository;
            _invoiceInvoiceItemRepository = invoiceInvoiceItemRepository;
        }

        [HttpGet]
        [Route("GetInvoiceItemsByCategory")]
        public async Task<ActionResult<List<Invoice_Items>>> GetInvoiceItemsByCategory(int categoryId)
        {
            if (!await _invoiceItemRepository.InvoiceItemCategoryExists(categoryId))
            {
                return NoContent();
            }

            return await _invoiceItemRepository.GetAllInvoiceItemsByCateogry(categoryId);
        }

        [HttpPost]
        [Route("AddInvoiceItem")]
        public async Task<ActionResult> AddInvoiceItem([FromBody]Invoice_Items item)
        {
            if (item == null) return BadRequest();


            await _invoiceItemRepository.AddInvoiceItems(item);
            return Ok();
        }
        [HttpGet]
        [Route("GetInvoiceItems")]
        public async Task<ActionResult<List<Invoice_Items>>> GetInvoiceItems()
        {
            return await _invoiceItemRepository.GetAllInvoiceItems();
        }

        [HttpGet]
        [Route("GetInvoiceItemsFromInvoice/{invoiceId}")]
        public async Task<ActionResult<Invoice_Items>> GetInvoiceItemsFromInvoice(int invoiceId)
        {
            var result =  await _invoiceInvoiceItemRepository.GetInvoiceItemsOfInvoice(invoiceId);

            return Ok(result);
        }
    }
}
