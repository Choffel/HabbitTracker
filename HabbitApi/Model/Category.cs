using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;

namespace HabbitApi.Model;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public ICollection<Habit> Habits { get; set; } = new List<Habit>();
}