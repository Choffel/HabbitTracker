using HabbitApi.Model;

namespace HabbitApi.Interface;

public interface IHabitLogRepository
{
    Task<HabitLog?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<HabitLog>> GetByHabitIdAsync(Guid habitId);
    
    Task AddAsync(HabitLog habitLog);
}