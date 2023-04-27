using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PublisherFeatures.Queries
{
    public class GetAllPublishersQuery : IRequest<IEnumerable<Publisher>>
    {
        public class GetAllPublishersQueryHandler : IRequestHandler<GetAllPublishersQuery, IEnumerable<Publisher>>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAllPublishersQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<IEnumerable<Publisher>> Handle
                (GetAllPublishersQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.Publishers.AsNoTracking().ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}