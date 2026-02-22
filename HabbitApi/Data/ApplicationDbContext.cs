using HabbitApi.Model;
using Microsoft.EntityFrameworkCore;

namespace HabbitApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Habit> habits { get; set; }
    
}