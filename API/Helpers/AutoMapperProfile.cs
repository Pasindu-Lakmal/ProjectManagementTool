using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src=> 
                    src.Photos.FirstOrDefault(x=>x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, AppUser>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<NameDto, AppUser>();
            CreateMap<Work, WorkDetailsDto>();  
            CreateMap<Work, WorkDto>(); 
            CreateMap<Todo, TodoDto>()
             .ForMember(dest => dest.WorkName, opt => opt.MapFrom(src =>src.Works.WorkName));
        }
    }
}