using AutoMapper;
using SchoolLibrary_Dapper.BLL.DTOs.ReviewDTOs;
using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.API.Mapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, GetDTO_Review>()
                .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.ReviewId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ReverseMap();
            
            CreateMap<Review, InsertDTO_Review>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ReverseMap();
            
            CreateMap<Review, UpdateDTO_Review>()
                .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.ReviewId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
                .ReverseMap();
            
            
            CreateMap<(Review, User, Book), GetDTO_ReviewWithUserAndBook>()
                .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(src => src.Item1.ReviewId))
                .ForMember(dest => dest.ReviewRating, opt => opt.MapFrom(src => src.Item1.Rating))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Item2.Id))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => $"{src.Item2.FirstName} {src.Item2.LastName}"))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Item3.BookId))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Item3.Title))
                .ForMember(dest => dest.BookISBN, opt => opt.MapFrom(src => src.Item3.ISBN))
                .ReverseMap();
        }
    }
}