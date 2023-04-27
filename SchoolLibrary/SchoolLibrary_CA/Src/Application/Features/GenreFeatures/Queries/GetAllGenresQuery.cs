using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GenreFeatures.Queries
{
    public class GetAllGenresQuery : IRequest<IEnumerable<Genre>>
    {
        public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<Genre>>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAllGenresQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<IEnumerable<Genre>> Handle(GetAllGenresQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.Genres.AsNoTracking().ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}