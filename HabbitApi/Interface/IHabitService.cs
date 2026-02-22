using HabbitApi.DTOs;
using HabbitApi.Model;

namespace HabbitApi.Interface;

public interface IHabitService
{
    Task AddHabitAsync (AddHabitRequestDTO requestDto);
    
    Task<IReadOnlyCollection<Habit>> GetAllHabitsAsync();
    
    Task<HabitResponseDTO> GetHabitByIdAsync(Guid habitId);
    
    Task UpdateHabbitAsync(Guid habitId, UpdateHabitRequest request);
    
    Task DeleteHabbitAsync(Guid habitId);
}