using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.AuthorFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthorFeatures.Queries.GetAuthor
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorDTO>
    {
        private readonly ISchoolLibraryContext _context;

        public GetAuthorByIdQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<AuthorDTO> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<Author, AuthorDTO>(await _context.Authors
                .AsNoTracking()
                .FirstAsync(a => a.AuthorId == query.Id, cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(Author), query.Id);
        }
    }
}