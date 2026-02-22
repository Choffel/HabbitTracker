using HabbitApi.Data;
using HabbitApi.Interface;
using HabbitApi.Model;
using Microsoft.EntityFrameworkCore;

namespace HabbitApi.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Category> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public async Task RemoveAsync(Guid id)
    {
        var category = await GetByIdAsync(id);

        if (category == null)
            return;  // Optionally, throw an exception or handle the case where the category is not found.

        _context.Categories.Remove(category);
    }

    public async Task<Category> GetByNameAsync(string name)
    {
        var get =  _context.Categories.FirstOrDefaultAsync(q => q.Name == name);
        
        return await get;
    }
}