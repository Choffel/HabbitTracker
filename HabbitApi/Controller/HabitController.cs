using HabbitApi.DTOs;
using HabbitApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HabbitApi.Controller;

[ApiController]
public class HabitController : ControllerBase
{
    private readonly IHabitService _habitService;
    
    public HabitController(IHabitService habitService)
    {
        _habitService = habitService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateHabit([FromBody] AddHabitRequestDTO request)
    {
        await _habitService.AddHabitAsync(request);
        
        return Ok();
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetHabitById([FromRoute] Guid habitId)
    {
        var find = await _habitService.GetHabitByIdAsync(habitId);
        
        return Ok(find);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllHabits()
    {
        var getAll = await _habitService.GetAllHabitsAsync();
        
        return Ok(getAll);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateHabit([FromRoute] Guid habitId, [FromBody] UpdateHabitRequest request)
    {
        await _habitService.UpdateHabitAsync(habitId, request); // исправил название

        return NoContent();
    }
    
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteHabit([FromRoute] Guid habitId)
    {
        await _habitService.DeleteHabitAsync(habitId);
        
        return NoContent();
    }
        
}