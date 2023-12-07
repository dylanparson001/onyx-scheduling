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

        [HttpPost]
        [Route("AddCategory")]
        public async Task<ActionResult> AddCategory([FromBody]Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }

            if (await _categoryRepository.CategoryExists(category.Name))
            {
                return BadRequest("Category Exists");
            } 

            await _categoryRepository.AddCategory(category);

            return Ok("Category Added");
            
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        } 
    }
}
