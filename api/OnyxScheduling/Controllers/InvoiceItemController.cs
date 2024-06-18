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

        private string GetCompanyIdFromHeader()
        {            
            Request.Headers.TryGetValue("CompanyId", out var companyIdHeader);

            return companyIdHeader.FirstOrDefault();
        }

        [HttpGet]
        [Route("GetInvoiceItemsByCategory")]
        public async Task<ActionResult<List<Invoice_Items>>> GetInvoiceItemsByCategory(int categoryId)
        {
            var companyId = GetCompanyIdFromHeader();
            
            if (!await _invoiceItemRepository.InvoiceItemCategoryExists(categoryId))
            {
                return NoContent();
            }
            return await _invoiceItemRepository.GetAllInvoiceItemsByCateogry(categoryId, companyId);
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
            var companyId = GetCompanyIdFromHeader();

            return await _invoiceItemRepository.GetAllInvoiceItems(companyId);
        }

        [HttpGet]
        [Route("GetInvoiceItemsFromInvoice/{invoiceId}")]
        public async Task<ActionResult<Invoice_Items>> GetInvoiceItemsFromInvoice(int invoiceId)
        {
            var result =  await _invoiceInvoiceItemRepository.GetInvoiceItemsOfInvoice(invoiceId);

            return Ok(result);
        }

        [HttpDelete]
        [Route("DeleteInvoiceItem")]
        public async Task<ActionResult> DeleteItem(int itemId)
        {
            var item = await _invoiceItemRepository.GetItemById(itemId);

            if (item == null)
            {
                return BadRequest();
            }

            await _invoiceItemRepository.DeleteItem(item);
            return NoContent();
        }
    }
}
