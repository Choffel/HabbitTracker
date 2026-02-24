using HabbitApi.Data;
using HabbitApi.Interface;
using HabbitApi.Model;
using Microsoft.EntityFrameworkCore;

namespace HabbitApi.Repository;

public class HabitRepository : IHabitRepository
{
    private readonly ApplicationDbContext _context;
    
    public HabitRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Habit> AddAsync(Habit habit)
    {
       await _context.habits.AddAsync(habit);
       
         return habit;
    }

    public Task<Habit?> GetByIdAsync(Guid id)
    {
        return _context.habits.Include(h => h.Category).FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<IReadOnlyCollection<Habit>> GetAllAsync()
    {
        return await _context.habits
            .Include(h => h.Category)
            .ToListAsync();
    }

    public Task UpdateAsync(Habit habit)
    {
        _context.habits.Update(habit);
        
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    { 
        var habit = await GetByIdAsync(id);
        
        _context.habits.Remove(habit);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _context.habits.AnyAsync(h => h.Id == id);
    }
}