using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.GenreFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GenreFeatures.Queries.GetGenre
{
    public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, GenreDTO>
    {
        private readonly ISchoolLibraryContext _context;

        public GetGenreByIdQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<GenreDTO> Handle(GetGenreByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<Genre, GenreDTO>(await _context.Genres
                .AsNoTracking()
                .FirstAsync(g => g.GenreId == query.Id, cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(Author), query.Id);
        }
    }
}