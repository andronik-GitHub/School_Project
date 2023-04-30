using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.GenreFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GenreFeatures.Queries.GetAllGenres
{
    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<GenreDTO>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<Genre> _sortHelper;

        public GetAllGenresQueryHandler(ISchoolLibraryContext context, ISortHelper<Genre> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }
            
            
        public async Task<IEnumerable<GenreDTO>> Handle(GetAllGenresQuery query, CancellationToken cancellationToken)
        {
            var list = _context.Genres.AsNoTracking();
            
            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);
            
            // Paging
            return await MapsterFunctions.MapListSourceToDestination<Genre, GenreDTO>(
                list
                    //.OrderBy(g => g.GenreId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize))
                .ToListAsync(cancellationToken);
        }
    }
}