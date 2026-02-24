namespace HabbitApi.DTOs;

public class HabitResponseDTO
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Category { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public DateTime CreatedOn { get; set; }
        
    public DateTime ModifiedOn { get; set; }
}