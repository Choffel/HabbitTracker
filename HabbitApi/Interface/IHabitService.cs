using HabbitApi.DTOs;
using HabbitApi.Model;

namespace HabbitApi.Interface;

public interface IHabitService
{
    Task<HabitResponseDTO> AddHabitAsync(AddHabitRequestDTO requestDto);
    
    Task<IReadOnlyCollection<HabitResponseDTO>> GetAllHabitsAsync();
    
    Task<HabitResponseDTO> GetHabitByIdAsync(Guid habitId);
    
    Task UpdateHabitAsync(Guid habitId, UpdateHabitRequest request);
    
    Task DeleteHabitAsync(Guid habitId);
    
    Task<HabitResponseDTO> CompleteHabitAsync(Guid habitId);
}