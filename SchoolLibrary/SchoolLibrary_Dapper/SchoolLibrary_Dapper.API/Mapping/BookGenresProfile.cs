using AutoMapper;
using SchoolLibrary_Dapper.BLL.DTOs.BookGenreDTOs;
using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.API.Mapping
{
    public class BookGenresProfile : Profile
    {
        public BookGenresProfile()
        {
            CreateMap<BookGenres, GetDTO_BookGenres>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                .ReverseMap();
            
            CreateMap<BookGenres, InsertDTO_BookGenres>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                .ReverseMap();
            
            CreateMap<BookGenres, UpdateDTO_BookGenres>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                .ReverseMap();
            
            
            CreateMap<(Book, Genre), GetDTOInclude_BookGenres>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Item1.BookId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Item1.Title))
                .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.Item1.ISBN))
                .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.Item2.GenreId))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Item2.Name))
                .ReverseMap();
        }
    }
}