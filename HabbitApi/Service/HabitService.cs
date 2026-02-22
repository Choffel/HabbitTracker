using AutoMapper;
using HabbitApi.DTOs;
using HabbitApi.Interface;
using HabbitApi.Model;

namespace HabbitApi.Service;

public class HabitService : IHabitService
{
    private readonly IHabitRepository _habitRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uof;
    
    public HabitService(IHabitRepository habitRepository, IUnitOfWork uof, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _uof = uof;
        _habitRepository = habitRepository;
    }
    
    public async Task AddHabitAsync(AddHabitRequestDTO requestDto)
    {
        var category =  await _categoryRepository.GetByIdAsync(requestDto.CategoryId);

        if (category == null)
        {
            throw new Exception("Category not found");
        }
        
        
        var habit = new Habit()
        {
            Id = Guid.NewGuid(),
            Name = requestDto.Name,
            CategoryId = category.Id,
            Category = category,
            CreatedOn = DateTime.UtcNow,
            LastUpdate = DateTime.UtcNow
        };
        
        await _habitRepository.AddAsync(habit);
        
        await _uof.SaveChangesAsync();
    }

    public Task<IReadOnlyCollection<Habit>> GetAllHabitsAsync()
    {
        var habits = _habitRepository.GetAllAsync();
        
        return habits;
    }

    public async Task<HabitResponseDTO> GetHabitByIdAsync(Guid habitId)
    {
        var findHabit =  await _habitRepository.GetByIdAsync(habitId);
        
        if (findHabit == null)
        {
            throw new Exception("Habit not found");
        }
        
        return _mapper.Map<HabitResponseDTO>(findHabit);
    }

    public async Task UpdateHabbitAsync(Guid habitId, UpdateHabitRequest request)
    {
        //get habit 
        var findHabit = await _habitRepository.GetByIdAsync(habitId);
        
        if (findHabit == null)
        {
            throw new Exception("Habit not found");
        }
        
        var category =  await _categoryRepository.GetByNameAsync(request.Category);
        
        if (category == null)
            throw new Exception("Category not found");
        
        findHabit.Name = request.Name;
        findHabit.Category = category;
        findHabit.LastUpdate = DateTime.UtcNow;
    
        await _habitRepository.UpdateAsync(findHabit);
        await _uof.SaveChangesAsync();
    }

    public Task DeleteHabbitAsync(Guid habitId)
    {
        var findHabit = _habitRepository.GetByIdAsync(habitId);
    
        if (findHabit == null)
        {
            throw new Exception("Habit not found");
        }
    
        return _habitRepository.DeleteAsync(habitId);
    }
}