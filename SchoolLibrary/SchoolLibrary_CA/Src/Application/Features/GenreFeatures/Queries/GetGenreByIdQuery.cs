using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GenreFeatures.Queries
{
    public class GetGenreByIdQuery : IRequest<Genre>
    {
        public Guid Id { get; set; } = default!;
        
        public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, Genre>
        {
            private readonly ISchoolLibraryContext _context;

            public GetGenreByIdQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<Genre> Handle(GetGenreByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.Genres
                    .AsNoTracking()
                    .FirstOrDefaultAsync(g => g.GenreId == query.Id, cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(Author), query.Id);
            }
        }
    }
}