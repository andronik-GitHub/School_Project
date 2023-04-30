using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.BookDetailsFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Queries.GetAllBookDetails
{
    public class GetAllBookDetailsQueryHandler : IRequestHandler<GetAllBookDetailsQuery, IEnumerable<BookDetailsDTO>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<BookDetails> _sortHelper;

        public GetAllBookDetailsQueryHandler(ISchoolLibraryContext context, ISortHelper<BookDetails> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }


        public async Task<IEnumerable<BookDetailsDTO>> Handle
            (GetAllBookDetailsQuery query, CancellationToken cancellationToken)
        {
            var list = _context.BookDetails.AsNoTracking();

            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);

            // Paging
            return await MapsterFunctions.MapListSourceToDestination<BookDetails, BookDetailsDTO>(
                list
                    //.OrderBy(bd => bd.BookDetailId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize)
                    .Include(bd => bd.Book))
                .ToListAsync(cancellationToken);
        }
    }
}