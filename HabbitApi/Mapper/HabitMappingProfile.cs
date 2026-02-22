using AutoMapper;
using HabbitApi.DTOs;
using HabbitApi.Interface;
using HabbitApi.Model;

namespace HabbitApi.Mapper;

public class HabitMappingProfile : Profile
{
    public HabitMappingProfile()
    {
        CreateMap<Habit, HabitResponseDTO>();
        
        CreateMap<AddHabitRequestDTO, Habit>();
        
        CreateMap<UpdateHabitRequest, Habit>();
    }
}