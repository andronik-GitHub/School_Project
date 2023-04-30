using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.BookFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Queries.GetAllBooks
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookDTO>>
    {
        private readonly ISchoolLibraryContext _context;
            
        public GetAllBooksQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<IEnumerable<BookDTO>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions.MapListSourceToDestination<Book, BookDTO>(await _context.Books
                .AsNoTracking()
                .OrderBy(b => b.BookId)
                .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                .Take(query._parameters.PageSize)
                .Include(b => b.Publisher)
                .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}