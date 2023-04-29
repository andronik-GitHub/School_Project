using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.ReviewFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReviewFeatures.Queries.GetAllReviews
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, IEnumerable<ReviewDTO>>
    {
        private readonly ISchoolLibraryContext _context;

        public GetAllReviewsQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<IEnumerable<ReviewDTO>> Handle(GetAllReviewsQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions.MapListSourceToDestination<Review, ReviewDTO>
                (await _context.Reviews
                    .AsNoTracking()
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}