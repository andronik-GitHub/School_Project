using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public Guid Id { get; set; } = default!;
        
        public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
        {
            private readonly ISchoolLibraryContext _context;
            
            public GetBookByIdQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Book> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.Books
                    .AsNoTracking()
                    .Include(b => b.Publisher)
                    .FirstOrDefaultAsync(b => b.BookId == query.Id, cancellationToken: cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(Book), query.Id);
            }
        }
    }
}