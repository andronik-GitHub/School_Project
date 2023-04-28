using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PublisherFeatures.Queries
{
    public class GetPublisherByIdQuery : IRequest<Publisher>
    {
        public Guid Id { get; set; }
        
        public class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, Publisher>
        {
            private readonly ISchoolLibraryContext _context;

            public GetPublisherByIdQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Publisher> Handle(GetPublisherByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.Publishers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PublisherId == query.Id, cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(Publisher), query.Id);
            }
        }
    }
}