using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

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
                    .FindAsync(new object?[] { query.Id, cancellationToken }, cancellationToken: cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(Author), query.Id);
            }
        }
    }
}