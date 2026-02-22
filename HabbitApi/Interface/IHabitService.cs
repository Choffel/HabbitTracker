using HabbitApi.DTOs;
using HabbitApi.Model;

namespace HabbitApi.Interface;

public interface IHabitService
{
    Task AddHabit (AddHabitRequest request);
    
    Task<IReadOnlyCollection<Habit>> GetAllHabitsAsync();
    
    Task<HabitResponseDTO> GetHabitByIdAsync(Guid habbitId);
    
    Task UpdateHabbitAsync(Guid habitId, UpdateHabitRequest request);
    
    Task DeleteHabbitAsync(Guid habitId);
}