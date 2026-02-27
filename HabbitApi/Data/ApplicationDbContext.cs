using HabbitApi.Model;
using Microsoft.EntityFrameworkCore;

namespace HabbitApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Habit> habits { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<HabitLog> habitLogs { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Habit>()
                .HasOne(h => h.Category)         
                .WithMany(c => c.Habits)          
                .HasForeignKey(h => h.CategoryId) 
                .OnDelete(DeleteBehavior.Cascade);
        }
}