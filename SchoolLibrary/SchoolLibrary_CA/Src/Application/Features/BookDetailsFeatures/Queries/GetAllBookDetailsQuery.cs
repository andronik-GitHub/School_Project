using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Queries
{
    public class GetAllBookDetailsQuery : IRequest<IEnumerable<BookDetails>>
    {
        public class GetAllBookDetailsQueryHandler : IRequestHandler<GetAllBookDetailsQuery, IEnumerable<BookDetails>>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAllBookDetailsQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<IEnumerable<BookDetails>> Handle
                (GetAllBookDetailsQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.BookDetails
                    .AsNoTracking()
                    .Include(bd => bd.Book)
                    .ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}