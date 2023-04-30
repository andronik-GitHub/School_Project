using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.BookGenreFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookGenreFeatures.Queries.GetAllBookGenres
{
    public class GetAllBookGenresQueryHandler : IRequestHandler<GetAllBookGenresQuery, IEnumerable<BookGenresDTO>>
    {
        private readonly ISchoolLibraryContext _context;

        public GetAllBookGenresQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<BookGenresDTO>> Handle
            (GetAllBookGenresQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions.MapListSourceToDestination<BookGenres, BookGenresDTO>(await _context.BookGenres
                .AsNoTracking()
                .OrderBy(bg => bg.BookId)
                .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                .Take(query._parameters.PageSize)
                .Include(bg => bg.Book)
                .Include(bg => bg.Genre)
                .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}