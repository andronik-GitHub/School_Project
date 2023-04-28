using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookAuthorFeatures.Queries
{
    public class GetAllBookAuthorsQuery : IRequest<IEnumerable<BookAuthors>>
    {
        public class GetAllBookAuthorsQueryHandler : IRequestHandler<GetAllBookAuthorsQuery, IEnumerable<BookAuthors>>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAllBookAuthorsQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<IEnumerable<BookAuthors>> Handle
                (GetAllBookAuthorsQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.BookAuthors
                    .AsNoTracking()
                    .Include(ba => ba.Book)
                    .Include(ba => ba.Author)
                    .ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}