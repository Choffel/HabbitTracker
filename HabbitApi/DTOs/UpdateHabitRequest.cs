namespace HabbitApi.DTOs;

public class UpdateHabitRequest
{
    public string Name { get; set; }
    
    public Guid CategoryId { get; set; }
}