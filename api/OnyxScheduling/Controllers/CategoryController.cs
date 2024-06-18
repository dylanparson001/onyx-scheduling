using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        private string GetCompanyIdFromHeader()
        {            
            Request.Headers.TryGetValue("CompanyId", out var companyIdHeader);

            return companyIdHeader.FirstOrDefault();
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<ActionResult> AddCategory([FromBody]Category category)
        {
            if (category == null)
            {
                return BadRequest("A response was not sent");
            }
            if (await _categoryRepository.CategoryExists(category.Name, category.CompanyId))
            {
                return BadRequest("Category Exists");
            } 

            await _categoryRepository.AddCategory(category);

            return Ok("Category created");
            
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var companyId = GetCompanyIdFromHeader();
            return await _categoryRepository.GetAllCategories(companyId);
        }

        [HttpDelete]
        [Route("DeleteCategory")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var companyId = GetCompanyIdFromHeader();

            var category = await _categoryRepository.GetCategoryById(categoryId, companyId);

            await _categoryRepository.DeleteCategory(category);

            return NoContent();
        }
    }
}
