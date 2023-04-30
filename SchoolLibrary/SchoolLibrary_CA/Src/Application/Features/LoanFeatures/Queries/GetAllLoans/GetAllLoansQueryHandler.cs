using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.LoanFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanFeatures.Queries.GetAllLoans
{
    public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQuery, IEnumerable<LoanDTO>>
    {
        private readonly ISchoolLibraryContext _context;

        public GetAllLoansQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }

            
        public async Task<IEnumerable<LoanDTO>> Handle(GetAllLoansQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions.MapListSourceToDestination<Loan, LoanDTO>(await _context.Loans
                .AsNoTracking()
                .OrderBy(l => l.LoanId)
                .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                .Take(query._parameters.PageSize)
                .Include(l => l.Book)
                .Include(l => l.User)
                .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}