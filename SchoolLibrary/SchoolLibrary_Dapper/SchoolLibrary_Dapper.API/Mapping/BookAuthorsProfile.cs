using AutoMapper;
using SchoolLibrary_Dapper.BLL.DTOs.BookAuthorDTOs;
using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.API.Mapping
{
    public class BookAuthorsProfile : Profile
    {
        public BookAuthorsProfile()
        {
            CreateMap<BookAuthors, GetDTO_BookAuthors>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ReverseMap();
            
            CreateMap<BookAuthors, InsertDTO_BookAuthors>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ReverseMap();
            
            CreateMap<BookAuthors, UpdateDTO_BookAuthors>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ReverseMap();
            
            
            CreateMap<(Book, Author), GetDTOInclude_BookAuthors>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Item1.BookId))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Item1.Title))
                .ForMember(dest => dest.BookISBN, opt => opt.MapFrom(src => src.Item1.ISBN))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Item2.AuthorId))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Item2.FirstName} {src.Item2.LastName}"))
                .ReverseMap();
        }
    }
}