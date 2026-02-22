using HabbitApi.Model;

namespace HabbitApi.Interface;

public interface ICategoryRepository
{
    Task<Category> GetByIdAsync(Guid id);
    
    Task<IEnumerable<Category>> GetAllAsync();
    
    Task AddAsync(Category category);
    
    void Remove(Category category);
}