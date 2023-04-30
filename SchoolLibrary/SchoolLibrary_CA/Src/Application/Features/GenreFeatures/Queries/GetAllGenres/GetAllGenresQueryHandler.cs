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

        public GetAllGenresQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<IEnumerable<GenreDTO>> Handle(GetAllGenresQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions.MapListSourceToDestination<Genre, GenreDTO>(await _context.Genres
                .AsNoTracking()
                .OrderBy(g => g.GenreId)
                .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                .Take(query._parameters.PageSize)
                .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}