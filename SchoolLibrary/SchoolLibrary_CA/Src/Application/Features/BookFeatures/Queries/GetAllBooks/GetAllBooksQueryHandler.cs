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
        private readonly ISortHelper<Book> _sortHelper;
            
        public GetAllBooksQueryHandler(ISchoolLibraryContext context, ISortHelper<Book> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }
            
            
        public async Task<IEnumerable<BookDTO>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
        {
            var list = _context.Books.AsNoTracking();

            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);
            
            // Paging
            return await MapsterFunctions.MapListSourceToDestination<Book, BookDTO>(
                list
                    //.OrderBy(b => b.BookId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize)
                    .Include(b => b.Publisher))
                .ToListAsync(cancellationToken);
        }
    }
}