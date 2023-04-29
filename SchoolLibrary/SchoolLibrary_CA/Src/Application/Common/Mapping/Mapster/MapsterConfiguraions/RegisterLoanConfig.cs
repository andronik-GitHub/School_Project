using Application.Features.LoanFeatures.Commands.CreateLoan;
using Application.Features.LoanFeatures.Commands.UpdateLoan;
using Application.Features.LoanFeatures.Queries.Common;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping.Mapster.MapsterConfiguraions
{
    public static class RegisterLoanConfig
    {
        public static void Registration()
        {
            RegisterCreateLoanCommandConfig();
            RegisterUpdateLoanCommandConfig();
            RegisterLoanDTOConfig();
        }

        private static void RegisterCreateLoanCommandConfig()
        {
            TypeAdapterConfig<CreateLoanCommand, Loan>
                .NewConfig()
                .Map(dest => dest.LoanDate, src => src.LoanDate)
                .Map(dest => dest.ReturnDate, src => src.ReturnDate)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
        private static void RegisterUpdateLoanCommandConfig()
        {
            TypeAdapterConfig<UpdateLoanCommand, Loan>
                .NewConfig()
                .Map(dest => dest.LoanId, src => src.Id)
                .Map(dest => dest.LoanDate, src => src.LoanDate)
                .Map(dest => dest.ReturnDate, src => src.ReturnDate)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
        private static void RegisterLoanDTOConfig()
        {
            TypeAdapterConfig<LoanDTO, Loan>
                .NewConfig()
                .Map(dest => dest.LoanId, src => src.LoanId)
                .Map(dest => dest.LoanDate, src => src.LoanDate)
                .Map(dest => dest.ReturnDate, src => src.ReturnDate)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
    }
}