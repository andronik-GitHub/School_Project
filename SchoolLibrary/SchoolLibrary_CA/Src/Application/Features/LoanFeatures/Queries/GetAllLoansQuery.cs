using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanFeatures.Queries
{
    public class GetAllLoansQuery : IRequest<IEnumerable<Loan>>
    {
        public class GetAllLoansQueryHandler : IRequestHandler<GetAllLoansQuery, IEnumerable<Loan>>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAllLoansQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }

            
            public async Task<IEnumerable<Loan>> Handle(GetAllLoansQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.Loans
                    .AsNoTracking()
                    .Include(l => l.Book)
                    .Include(l => l.User)
                    .ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}