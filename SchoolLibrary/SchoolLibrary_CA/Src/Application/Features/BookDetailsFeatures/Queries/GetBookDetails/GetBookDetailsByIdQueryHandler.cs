using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.BookDetailsFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Queries.GetBookDetails
{
    public class GetBookDetailsByIdQueryHandler : IRequestHandler<GetBookDetailsByIdQuery, BookDetailsDTO>
    {
        private readonly ISchoolLibraryContext _context;

        public GetBookDetailsByIdQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<BookDetailsDTO> Handle(GetBookDetailsByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<BookDetails, BookDetailsDTO>(await _context.BookDetails
                .AsNoTracking()
                .Include(bd => bd.Book)
                .FirstAsync(bd => bd.BookDetailId == query.Id, cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(BookDetails), query.Id);
        }
    }
}