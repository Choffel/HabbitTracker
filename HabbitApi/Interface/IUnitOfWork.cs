namespace HabbitApi.Interface;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}