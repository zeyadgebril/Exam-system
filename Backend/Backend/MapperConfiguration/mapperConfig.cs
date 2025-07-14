using AutoMapper;
using Backend.DTOs;
using Backend.Models;

namespace Backend.MapperConfiguration
{
    public class mapperConfig:Profile
    {
        public mapperConfig()
        {
            //implement the config here 
            //for example 
            //CreateMap<Sourse,Direction>
            //or
            //CreateMap<Sourse, Direction>().AfterMap((s, d) =>
            //{
            //    d.NumberOfStudent = s.Students.Count();
            //});
            CreateMap<ApplicationUser, UserDTO>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName));
        }
    }
}
