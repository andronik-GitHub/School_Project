using AutoMapper;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.API.Mapping.MappingProfiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Nationality))
                .ReverseMap()
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Country));
        }
    }
}
