using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookGenreFeatures.Queries
{
    public class GetBookGenreByIdsQuery : IRequest<BookGenres>
    {
        public Guid BookId { get; set; } = default!;
        public Guid GenreId { get; set; } = default!;
        
        public class GetBookGenreByIdsQueryHandler : IRequestHandler<GetBookGenreByIdsQuery, BookGenres>
        {
            private readonly ISchoolLibraryContext _context;

            public GetBookGenreByIdsQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<BookGenres> Handle(GetBookGenreByIdsQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.BookGenres
                    .AsNoTracking()
                    .Include(bg => bg.Book)
                    .Include(bg => bg.Genre)
                    .FirstOrDefaultAsync(
                        bg => bg.BookId == query.BookId && bg.GenreId == query.GenreId, 
                        cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(BookGenres), new { query.BookId, query.GenreId });
            }
        }
    }
}