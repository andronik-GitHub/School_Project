using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.BookDetailsFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Queries.GetAllBookDetails
{
    public class GetAllBookDetailsQueryHandler : IRequestHandler<GetAllBookDetailsQuery, IEnumerable<BookDetailsDTO>>
    {
        private readonly ISchoolLibraryContext _context;

        public GetAllBookDetailsQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<BookDetailsDTO>> Handle
            (GetAllBookDetailsQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions
                .MapListSourceToDestination<BookDetails, BookDetailsDTO>(await _context.BookDetails
                    .AsNoTracking()
                    .Include(bd => bd.Book)
                    .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}