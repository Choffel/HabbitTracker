using HabbitApi.Data;
using HabbitApi.Interface;
using HabbitApi.Model;
using Microsoft.EntityFrameworkCore;

namespace HabbitApi.Repository;

public class HabitLogRepository : IHabitLogRepository
{
    private readonly ApplicationDbContext _context;
    
    public async Task<HabitLog?> GetByIdAsync(Guid id)
    {
        return await _context.habitLogs.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async  Task<IEnumerable<HabitLog>> GetByHabitIdAsync(Guid habitId)
    {
        return await _context.habitLogs.Where(a => a.HabitId == habitId)
            .OrderBy(a => a.LogDate)
            .ToListAsync();
    }

    public async Task AddAsync(HabitLog habitLog)
    {
        await _context.habitLogs.AddAsync(habitLog);
    }
}