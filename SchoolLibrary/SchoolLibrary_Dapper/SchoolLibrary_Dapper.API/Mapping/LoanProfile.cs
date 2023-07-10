using AutoMapper;
using SchoolLibrary_Dapper.BLL.DTOs.LoanDTOs;
using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.API.Mapping
{
    public class LoanProfile : Profile
    {
        public LoanProfile()
        {
            CreateMap<Loan, GetDTO_Loan>()
                .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.LoanId))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
                .ReverseMap();
            
            CreateMap<Loan, InsertDTO_Loan>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
                .ReverseMap();
            
            CreateMap<Loan, UpdateDTO_Loan>()
                .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.LoanId))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => src.LoanDate))
                .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
                .ReverseMap();
        }
    }
}