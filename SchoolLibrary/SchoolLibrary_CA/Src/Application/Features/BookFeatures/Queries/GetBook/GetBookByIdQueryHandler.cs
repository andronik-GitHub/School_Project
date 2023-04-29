using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.BookFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Queries.GetBook
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDTO>
    {
        private readonly ISchoolLibraryContext _context;
            
        public GetBookByIdQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<BookDTO> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<Book, BookDTO>(await _context.Books
                .AsNoTracking()
                .Include(b => b.Publisher)
                .FirstAsync(b => b.BookId == query.Id, cancellationToken: cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(Book), query.Id);
        }
    }
}