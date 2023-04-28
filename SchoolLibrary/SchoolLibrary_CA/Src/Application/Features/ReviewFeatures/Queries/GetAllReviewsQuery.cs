using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReviewFeatures.Queries
{
    public class GetAllReviewsQuery : IRequest<IEnumerable<Review>>
    {
        public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, IEnumerable<Review>>
        {
            private readonly ISchoolLibraryContext _context;

            public GetAllReviewsQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query, CancellationToken cancellationToken)
            {
                var list = await _context.Reviews
                    .AsNoTracking()
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .ToListAsync(cancellationToken);

                return list.AsReadOnly();
            }
        }
    }
}