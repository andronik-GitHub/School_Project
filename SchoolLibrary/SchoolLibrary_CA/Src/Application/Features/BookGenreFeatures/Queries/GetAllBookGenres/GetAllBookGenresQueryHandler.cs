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
        private readonly ISortHelper<BookGenres> _sortHelper;

        public GetAllBookGenresQueryHandler(ISchoolLibraryContext context, ISortHelper<BookGenres> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }


        public async Task<IEnumerable<BookGenresDTO>> Handle
            (GetAllBookGenresQuery query, CancellationToken cancellationToken)
        {
            var list = _context.BookGenres.AsNoTracking();

            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);

            // Paging
            return await MapsterFunctions.MapListSourceToDestination<BookGenres, BookGenresDTO>(
                list
                    //.OrderBy(bg => bg.BookId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize)
                    .Include(bg => bg.Book)
                    .Include(bg => bg.Genre))
                .ToListAsync(cancellationToken);
        }
    }
}