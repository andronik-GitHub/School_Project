using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.LoanFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanFeatures.Queries.GetLoan
{
    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanDTO>
    {
        private readonly ISchoolLibraryContext _context;

        public GetLoanByIdQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<LoanDTO> Handle(GetLoanByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<Loan, LoanDTO>(await _context.Loans
                .AsNoTracking()
                .Include(l => l.Book)
                .Include(l => l.User)
                .FirstAsync(l => l.LoanId == query.Id, cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(Publisher), query.Id);
        }
    }
}