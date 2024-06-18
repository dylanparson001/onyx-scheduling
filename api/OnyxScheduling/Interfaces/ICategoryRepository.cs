using OnyxScheduling.Models;

namespace OnyxScheduling.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllCategories(string companyId);
        public Task<Category> GetCategoryById(int id, string companyId);
        public Task AddCategory(Category category);
        public Task<Category> GetCategoryByName(string name, string companyId);
        public Task<bool> CategoryExists(string name,string companyId);
        public Task DeleteCategory(Category category);
    }
}
