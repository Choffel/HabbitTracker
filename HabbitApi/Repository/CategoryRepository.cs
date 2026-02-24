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
        return await _context.Categories.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public async Task<int> RemoveAsync(Guid id)
    {
        var affected = await _context.Categories
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        if (affected == 0)
            throw new KeyNotFoundException("Category not found");
        
        return affected;
    }
}