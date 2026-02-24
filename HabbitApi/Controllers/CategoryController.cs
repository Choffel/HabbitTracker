using HabbitApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HabbitApi.Controller;

[ApiController]
[Route("api/v1/[controller]/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [HttpPost("Create")]
    public async Task<IActionResult> CreateCategory([FromBody] string name)
    {
        var category = await _categoryService.AddCategoryAsync(name);
        
        return Ok(category);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        
        return Ok(categories);
    }

    [HttpGet("GetById/{categoryId}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId);
        
        return Ok(category);
    }

    [HttpPut("Update/{categoryId}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId, [FromBody] string newName)
    {
        await _categoryService.UpdateCategoryAsync(categoryId, newName);
        
        return NoContent();
    }

    [HttpDelete("Delete/{categoryId}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
    {
        var deletedCategory = await _categoryService.DeleteCategoryAsync(categoryId);
        
        return Ok(deletedCategory);
    }
    
}

