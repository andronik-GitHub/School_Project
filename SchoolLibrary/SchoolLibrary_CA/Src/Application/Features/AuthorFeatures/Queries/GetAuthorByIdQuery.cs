using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries
{
    public class GetAuthorByIdQuery : IRequest<Author>
    {
        public Guid Id { get; set; }
        
        public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAuthorByIdQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Author> Handle(GetAuthorByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.Authors
                    .FindAsync(new object?[] { query.Id }, cancellationToken: cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(Author), query.Id);
            }
        }
    }
}