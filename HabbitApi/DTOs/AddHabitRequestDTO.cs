namespace HabbitApi.DTOs;

public class AddHabitRequestDTO
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    
}