using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Interfaces;
using OnyxScheduling.Models;

namespace OnyxScheduling.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddCategory(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CategoryExists(string name)
        {
            return await _context.Category.AsNoTracking().AnyAsync(x => x.Name == name);
        }

        public async Task DeleteCategory(Category category)
        {
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Category.OrderBy(x => x.Name).ToListAsync();
            
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Category.FindAsync(id);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Category.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
        
    }
}
