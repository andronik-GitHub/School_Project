using AutoMapper;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ReverseMap()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => ""));
        }
    }
}
