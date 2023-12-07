using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category> GetCategoryById(int id);
        public Task AddCategory(Category category);
        public Task<Category> GetCategoryByName(string name);
        public Task<bool> CategoryExists(string name);
    }
}
