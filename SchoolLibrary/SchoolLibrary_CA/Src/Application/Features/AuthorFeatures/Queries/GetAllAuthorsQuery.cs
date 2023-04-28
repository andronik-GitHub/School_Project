using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthorFeatures.Queries
{
    public class GetAllAuthorsQuery : IRequest<IEnumerable<Author>>
    {
        public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<Author>>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAllAuthorsQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<IEnumerable<Author>> Handle(GetAllAuthorsQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.Authors
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}