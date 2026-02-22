using HabbitApi.DTOs;
using HabbitApi.Model;

namespace HabbitApi.Interface;

public interface IHabitService
{
    Task AddHabitAsync (AddHabitRequestDTO requestDto);
    
    Task<IReadOnlyCollection<Habit>> GetAllHabitsAsync();
    
    Task<HabitResponseDTO> GetHabitByIdAsync(Guid habitId);
    
    Task UpdateHabitAsync(Guid habitId, UpdateHabitRequest request);
    
    Task DeleteHabitAsync(Guid habitId);
}