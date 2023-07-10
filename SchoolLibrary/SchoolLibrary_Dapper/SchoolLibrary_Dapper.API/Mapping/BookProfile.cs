using AutoMapper;
using SchoolLibrary_Dapper.BLL.DTOs.BookDTOs;
using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.API.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, GetDTO_Book>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.PublisherId))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ReverseMap();
            
            CreateMap<Book, InsertDTO_Book>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PublishingYear, opt => opt.MapFrom(src => src.PublishingYear))
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.PublisherId))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ReverseMap();
            
            CreateMap<Book, UpdateDTO_Book>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.PublishingYear, opt => opt.MapFrom(src => src.PublishingYear))
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.PublisherId))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
                .ReverseMap();
            
            
            CreateMap<(Book, Author, Genre), GetDTO_BookWithAuthorsAndGenres>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Item1.BookId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Item1.Title))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.Item1.ISBN))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Item2.AuthorId))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Item2.FirstName} {src.Item2.LastName}"))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Item3.GenreId))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Item3.Name))
                .ReverseMap();
        }
    }
}