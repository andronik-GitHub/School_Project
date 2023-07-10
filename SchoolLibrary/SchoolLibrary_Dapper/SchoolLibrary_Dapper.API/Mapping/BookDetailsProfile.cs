using AutoMapper;
using SchoolLibrary_Dapper.BLL.DTOs.BookDetailsDTOs;
using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.API.Mapping
{
    public class BookDetailsProfile : Profile
    {
        public BookDetailsProfile()
        {
            CreateMap<BookDetails, GetDTO_BookDetails>()
                .ForMember(dest => dest.BookDetailId, opt => opt.MapFrom(src => src.BookDetailId))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
                .ReverseMap();
            
            CreateMap<BookDetails, InsertDTO_BookDetails>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.Format))
                .ReverseMap();
            
            CreateMap<BookDetails, UpdateDTO_BookDetails>()
                .ForMember(dest => dest.BookDetailId, opt => opt.MapFrom(src => src.BookDetailId))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Pages, opt => opt.MapFrom(src => src.Pages))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Language))
                .ForMember(dest => dest.Format, opt => opt.MapFrom(src => src.Format))
                .ReverseMap();
        }
    }
}