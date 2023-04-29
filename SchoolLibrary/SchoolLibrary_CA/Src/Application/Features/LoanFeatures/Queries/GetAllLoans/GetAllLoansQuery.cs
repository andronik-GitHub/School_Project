using Application.Features.LoanFeatures.Queries.Common;
using MediatR;

namespace Application.Features.LoanFeatures.Queries.GetAllLoans
{
    public class GetAllLoansQuery : IRequest<IEnumerable<LoanDTO>>
    {
    }
}