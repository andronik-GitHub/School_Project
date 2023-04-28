using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookGenreFeatures.Queries
{
    public class GetAllBookGenresQuery : IRequest<IEnumerable<BookGenres>>
    {
        public class GetAllBookGenresQueryHandler : IRequestHandler<GetAllBookGenresQuery, IEnumerable<BookGenres>>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAllBookGenresQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<IEnumerable<BookGenres>> Handle
                (GetAllBookGenresQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.BookGenres
                    .AsNoTracking()
                    .Include(bg => bg.Book)
                    .Include(bg => bg.Genre)
                    .ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}