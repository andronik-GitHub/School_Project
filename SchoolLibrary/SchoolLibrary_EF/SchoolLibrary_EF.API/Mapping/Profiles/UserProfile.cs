using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolLibrary_EF.BLL.DTOs.UserDTOs;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.API.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                // .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                // .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                // .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                // .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                // .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                // .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => src.DateUpdated))
                .ForMember(dest => dest.DateDeleted, opt => opt.MapFrom(src => src.DateDeleted))
                .ForMember(dest => dest.AccessFailedCount, opt => opt.MapFrom(src => src.AccessFailedCount))
                .ForMember(dest => dest.ConcurrencyStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => src.LockoutEnabled))
                .ForMember(dest => dest.LockoutEnd, opt => opt.MapFrom(src => src.LockoutEnd))
                .ForMember(dest => dest.NormalizedEmail, opt => opt.MapFrom(src => src.NormalizedEmail))
                .ForMember(dest => dest.NormalizedUserName, opt => opt.MapFrom(src => src.NormalizedUserName))
                // .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                // .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.MapFrom(src => src.PhoneNumberConfirmed))
                .ForMember(dest => dest.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString("N").ToUpper()))
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => src.TwoFactorEnabled))
                // .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ReverseMap();
            
            CreateMap<User, GetDTO_User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Country}, {src.City}, {src.Street}"))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
            
            CreateMap<InsertDTO_User, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(
                    dest => dest.PasswordHash, 
                    opt => opt.MapFrom(src => new PasswordHasher<User>(null).HashPassword(null!, src.Password)))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                .ReverseMap();
            
            
            
            CreateMap<(string FirstName, string LastName, int BooksLoaned), GetDTO_NumBooksIssuedToUser>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BooksLoaned, opt => opt.MapFrom(src => src.BooksLoaned))
                .ReverseMap();
        }
    }
}
