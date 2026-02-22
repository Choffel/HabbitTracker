namespace HabbitApi.Model;

public class Habit
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Category { get; set; }
    
    public bool IsComplete { get; set; }
    
    public DateTime LastUpdate { get; set; }
    
}