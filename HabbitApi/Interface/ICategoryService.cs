using HabbitApi.DTOs;
using HabbitApi.Model;

namespace HabbitApi.Interface;

public interface ICategoryService
{
    Task<CategoryDTO> AddCategoryAsync(string name);
    
    Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    
    Task<CategoryDTO> GetCategoryByIdAsync(Guid id);
    
    Task UpdateCategoryAsync(Guid id, string newName);
    
    Task<Category> DeleteCategoryAsync(Guid id);
}