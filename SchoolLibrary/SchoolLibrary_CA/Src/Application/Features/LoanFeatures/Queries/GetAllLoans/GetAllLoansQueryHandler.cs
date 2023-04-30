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
        private readonly ISortHelper<Loan> _sortHelper;

        public GetAllLoansQueryHandler(ISchoolLibraryContext context, ISortHelper<Loan> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }

            
        public async Task<IEnumerable<LoanDTO>> Handle(GetAllLoansQuery query, CancellationToken cancellationToken)
        {
            var list = _context.Loans.AsNoTracking();

            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);
            
            // Paging
            return await MapsterFunctions.MapListSourceToDestination<Loan, LoanDTO>(
                list
                    //.OrderBy(l => l.LoanId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize)
                    .Include(l => l.Book)
                    .Include(l => l.User))
                .ToListAsync(cancellationToken);
        }
    }
}