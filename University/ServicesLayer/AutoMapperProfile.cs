using AutoMapper;
using EntityLayer.Dto;
using EntityLayer.Entities;

namespace ServicesLayer;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
      CreateMap<Course, CourseDto>()
      .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseName));  
    }
}