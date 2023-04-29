using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.BookAuthorFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookAuthorFeatures.Queries.GetBookAuthor
{
    public class GetBookAuthorByIdsQueryHandler : IRequestHandler<GetBookAuthorByIdsQuery, BookAuthorsDTO>
    {
        private readonly ISchoolLibraryContext _context;

        public GetBookAuthorByIdsQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<BookAuthorsDTO> Handle(GetBookAuthorByIdsQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<BookAuthors, BookAuthorsDTO>(await _context.BookAuthors
                .AsNoTracking()
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .FirstAsync(
                    ba => ba.BookId == query.BookId && ba.AuthorId == query.AuthorId, 
                    cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(BookAuthors), new { query.BookId, query.AuthorId });
        }
    }
}