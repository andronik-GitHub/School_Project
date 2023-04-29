using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.PublisherFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PublisherFeatures.Queries.GetPublisher
{
    public class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, PublisherDTO>
    {
        private readonly ISchoolLibraryContext _context;

        public GetPublisherByIdQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<PublisherDTO> Handle(GetPublisherByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<Publisher, PublisherDTO>(await _context.Publishers
                .AsNoTracking()
                .FirstAsync(p => p.PublisherId == query.Id, cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(Publisher), query.Id);
        }
    }
}