using AutoMapper;
using HabbitApi.DTOs;
using HabbitApi.Interface;
using HabbitApi.Model;

namespace HabbitApi.Mapper;

public class HabitMappingProfile : Profile
{
    public HabitMappingProfile()
    {
        CreateMap<Habit, HabitResponseDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsComplete))
            .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => src.LastUpdate));
        
        CreateMap<AddHabitRequestDTO, Habit>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
            .ForMember(dest => dest.LastUpdate, opt => opt.Ignore())
            .ForMember(dest => dest.IsComplete, opt => opt.MapFrom(src => false));

        
        CreateMap<UpdateHabitRequest, Habit>();
    }
}