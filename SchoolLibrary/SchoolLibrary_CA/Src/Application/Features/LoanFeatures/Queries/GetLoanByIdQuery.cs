using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanFeatures.Queries
{
    public class GetLoanByIdQuery : IRequest<Loan>
    {
        public Guid Id { get; set; }
        
        public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, Loan>
        {
            private readonly ISchoolLibraryContext _context;

            public GetLoanByIdQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<Loan> Handle(GetLoanByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.Loans
                    .AsNoTracking()
                    .Include(l => l.Book)
                    .Include(l => l.User)
                    .FirstOrDefaultAsync(l => l.LoanId == query.Id, cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(Publisher), query.Id);
            }
        }
    }
}