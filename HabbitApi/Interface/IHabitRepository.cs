using HabbitApi.Model;

namespace HabbitApi.Interface;

public interface IHabitRepository
{
    Task<Habit> AddAsync(Habit habit);
    
    Task<Habit?> GetByIdAsync(Guid id);
    
    Task<IReadOnlyCollection<Habit>> GetAllAsync();
    
    Task UpdateAsync(Habit habit);
    
    Task DeleteAsync(Guid id);
    
    Task<bool> ExistsAsync(Guid id);
}