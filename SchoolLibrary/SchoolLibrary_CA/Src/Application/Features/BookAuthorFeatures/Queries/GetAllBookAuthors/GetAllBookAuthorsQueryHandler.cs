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
        private readonly ISortHelper<BookAuthors> _sortHelper;

        public GetAllBookAuthorsQueryHandler(ISchoolLibraryContext context, ISortHelper<BookAuthors> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }


        public async Task<IEnumerable<BookAuthorsDTO>> Handle
            (GetAllBookAuthorsQuery query, CancellationToken cancellationToken)
        {
            var list = _context.BookAuthors.AsNoTracking();

            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);
            
            // Paging
            return await MapsterFunctions.MapListSourceToDestination<BookAuthors, BookAuthorsDTO>(
                list
                    //.OrderBy(ba => ba.BookId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize)
                    .Include(ba => ba.Book)
                    .Include(ba => ba.Author))
                .ToListAsync(cancellationToken);
        }
    }
}