using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.BookAuthorFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookAuthorFeatures.Queries.GetAllBookAuthors
{
    public class GetAllBookAuthorsQueryHandler : IRequestHandler<GetAllBookAuthorsQuery, IEnumerable<BookAuthorsDTO>>
    {
        private readonly ISchoolLibraryContext _context;

        public GetAllBookAuthorsQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<BookAuthorsDTO>> Handle
            (GetAllBookAuthorsQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions
                .MapListSourceToDestination<BookAuthors, BookAuthorsDTO>(await _context.BookAuthors
                    .AsNoTracking()
                    .OrderBy(ba => ba.BookId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize)
                    .Include(ba => ba.Book)
                    .Include(ba => ba.Author)
                    .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}