using HabbitApi.DTOs;

namespace HabbitApi.Model;

public class Habit
{
    public Guid Id { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public Category Category { get; set; }
    
    public string Name { get; set; }
    
    public bool IsComplete { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public DateTime LastUpdate { get; set; }
    
    public ICollection<HabitLog> Logs { get; set; }
    
}