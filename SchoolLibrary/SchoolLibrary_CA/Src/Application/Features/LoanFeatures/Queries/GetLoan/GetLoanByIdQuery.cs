using Application.Features.LoanFeatures.Queries.Common;
using MediatR;

namespace Application.Features.LoanFeatures.Queries.GetLoan
{
    public class GetLoanByIdQuery : IRequest<LoanDTO>
    {
        public Guid Id { get; set; }
    }
}