using HabbitApi.Data;
using HabbitApi.Interface;
using HabbitApi.Model;
using Microsoft.EntityFrameworkCore;

namespace HabbitApi.Repository;

public class HabitRespository : IHabitRepository
{
    private readonly ApplicationDbContext _context;
    
    public HabitRespository(ApplicationDbContext context)
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
        return _context.habits.FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<IReadOnlyCollection<Habit>> GetAllAsync()
    {
        var habits = await _context.habits.ToListAsync();

        return habits.AsReadOnly();
    }

    public  Task UpdateAsync(Habit habit)
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