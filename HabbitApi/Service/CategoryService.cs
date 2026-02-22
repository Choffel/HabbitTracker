using HabbitApi.DTOs;
using HabbitApi.Interface;
using HabbitApi.Model;

namespace HabbitApi.Service;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    
    public CategoryService(IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }


    public async  Task<CategoryDTO> AddCategoryAsync(string name)
    {
        var category = new Category
        {
            Id =  Guid.NewGuid(),
            Name = name,
        };
        
        await _categoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
        
        return new CategoryDTO
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
    {
       var categories = await _categoryRepository.GetAllAsync();

       return categories.Select(c => new CategoryDTO
       {
           Id = c.Id,
           Name = c.Name
       });
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(Guid id)
    {
        var get = await _categoryRepository.GetByIdAsync(id);
        
        if (get == null)
        {
            throw new Exception("Category not found");
        }
        
        return new CategoryDTO
        {
            Id = get.Id,
            Name = get.Name
        };
    }

    public  async Task UpdateCategoryAsync(Guid id, string newName)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        
        category.Name = newName;
        
        await _unitOfWork.SaveChangesAsync();
    }

    
    //  return to id 
    public async Task<Category> DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        if (category == null)
        {
            throw new Exception("Category not found");
        }
        
        await  _categoryRepository.RemoveAsync(id);

        await _unitOfWork.SaveChangesAsync();

        return category;
    }
}