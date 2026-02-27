namespace HabbitApi.Model;

public class HabitLog
{
    public Guid Id { get; set; }
    
    public Guid HabitId { get; set; }
    
    public Habit Habit { get; set; }
    
    public DateOnly LogDate { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public DateTime CreatedOn { get; set; } 
    
}