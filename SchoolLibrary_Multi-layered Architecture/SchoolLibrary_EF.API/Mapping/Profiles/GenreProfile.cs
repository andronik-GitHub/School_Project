using AutoMapper;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.API.Mapping.MappingProfiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, GenreDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => "undefined"))
                .ForMember(dest => dest.Rating, opt =>
                    opt.MapFrom(src => Math.Round((decimal)new Random().NextDouble() * 9 + 1, 2)))
                .ReverseMap();
        }
    }
}
