using HabbitApi.DTOs;
using HabbitApi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HabbitApi.Controller;

[ApiController]
[Route("api/v1/[controller]")]
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
        var result = await _habitService.AddHabitAsync(request);
        
        return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var find = await _habitService.GetHabitByIdAsync(id);
        
        return Ok(find);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHabits()
    {
        var getAll = await _habitService.GetAllHabitsAsync();
        
        return Ok(getAll);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateHabit([FromRoute] Guid id, [FromBody] UpdateHabitRequest request)
    {
        await _habitService.UpdateHabitAsync(id, request); 

        return NoContent();
    }
    
    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteHabit([FromRoute] Guid id)
    {
        await _habitService.DeleteHabitAsync(id);
        
        return NoContent();
    }

    [HttpPatch("Complete/{id}")]
    public async Task<IActionResult> CompleteHabit([FromRoute] Guid id)
    {
        var habit = await _habitService.CompleteHabitAsync(id);
        
        return Ok(habit);
    }
}