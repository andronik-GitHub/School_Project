using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Queries
{
    public class GetAllBooksQuery : IRequest<IEnumerable<Book>>
    {
        public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
        {
            private readonly ISchoolLibraryContext _context;
            
            public GetAllBooksQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.Books
                    .AsNoTracking()
                    .Include(b => b.Publisher)
                    .ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}