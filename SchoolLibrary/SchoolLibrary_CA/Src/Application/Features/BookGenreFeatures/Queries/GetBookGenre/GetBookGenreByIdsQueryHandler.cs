using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.BookGenreFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookGenreFeatures.Queries.GetBookGenre
{
    public class GetBookGenreByIdsQueryHandler : IRequestHandler<GetBookGenreByIdsQuery, BookGenresDTO>
    {
        private readonly ISchoolLibraryContext _context;

        public GetBookGenreByIdsQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<BookGenresDTO> Handle(GetBookGenreByIdsQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<BookGenres, BookGenresDTO>(await _context.BookGenres
                .AsNoTracking()
                .Include(bg => bg.Book)
                .Include(bg => bg.Genre)
                .FirstAsync(bg => bg.BookId == query.BookId && bg.GenreId == query.GenreId, cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(BookGenres), new { query.BookId, query.GenreId });
        }
    }
}