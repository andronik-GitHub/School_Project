using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookAuthorFeatures.Queries
{
    public class GetBookAuthorByIdsQuery : IRequest<BookAuthors>
    {
        public Guid BookId { get; set; } = default!;
        public Guid AuthorId { get; set; } = default!;
        
        public class GetBookAuthorByIdsQueryHandler : IRequestHandler<GetBookAuthorByIdsQuery, BookAuthors>
        {
            private readonly ISchoolLibraryContext _context;

            public GetBookAuthorByIdsQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<BookAuthors> Handle(GetBookAuthorByIdsQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.BookAuthors
                    .AsNoTracking()
                    .Include(ba => ba.Book)
                    .Include(ba => ba.Author)
                    .FirstOrDefaultAsync(
                        ba => ba.BookId == query.BookId && ba.AuthorId == query.AuthorId, 
                        cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(BookAuthors), new { query.BookId, query.AuthorId });
            }
        }
    }
}