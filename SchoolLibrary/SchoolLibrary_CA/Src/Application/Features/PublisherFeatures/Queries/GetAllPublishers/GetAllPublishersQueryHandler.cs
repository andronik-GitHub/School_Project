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

        public GetAllPublishersQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<IEnumerable<PublisherDTO>> Handle
            (GetAllPublishersQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions.MapListSourceToDestination<Publisher, PublisherDTO>(await _context.Publishers
                .AsNoTracking()
                .OrderBy(p => p.PublisherId)
                .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                .Take(query._parameters.PageSize)
                .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}