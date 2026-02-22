using HabbitApi.DTOs;
using HabbitApi.Model;

namespace HabbitApi.Interface;

public interface IHabbitService
{
    Task AddHabbit (AddHabbitRequest request);
    
    Task<IReadOnlyCollection<Habbit>> GetAllHabbitsAsync();
    
    Task<> GetHabbitByIdAsync(Guid habbitId);
    
    Task UpdateHabbitAsync(Guid habbitId, UpdateHabbitRequest request);
    
    Task DeleteHabbitAsync(Guid habbitId);
}