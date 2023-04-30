using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.PublisherFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PublisherFeatures.Queries.GetAllPublishers
{
    public class GetAllPublishersQueryHandler : IRequestHandler<GetAllPublishersQuery, IEnumerable<PublisherDTO>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<Publisher> _sortHelper;

        public GetAllPublishersQueryHandler(ISchoolLibraryContext context, ISortHelper<Publisher> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }
            
            
        public async Task<IEnumerable<PublisherDTO>> Handle
            (GetAllPublishersQuery query, CancellationToken cancellationToken)
        {
            var list = _context.Publishers.AsNoTracking();

            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);
            
            // Paging
            return await MapsterFunctions.MapListSourceToDestination<Publisher, PublisherDTO>(
                list
                    //.OrderBy(p => p.PublisherId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize))
                .ToListAsync(cancellationToken);
        }
    }
}