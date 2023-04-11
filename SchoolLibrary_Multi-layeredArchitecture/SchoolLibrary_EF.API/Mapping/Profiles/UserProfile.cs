using AutoMapper;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.API.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => ""));
        }
    }
}
