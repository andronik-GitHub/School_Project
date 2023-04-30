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
        private readonly ISortHelper<Review> _sortHelper;

        public GetAllReviewsQueryHandler(ISchoolLibraryContext context, ISortHelper<Review> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }
            
            
        public async Task<IEnumerable<ReviewDTO>> Handle(GetAllReviewsQuery query, CancellationToken cancellationToken)
        {
            var list = _context.Reviews.AsNoTracking();

            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);
            
            // Paging
            return await MapsterFunctions.MapListSourceToDestination<Review, ReviewDTO>(
                list
                    //.OrderBy(r => r.ReviewId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize)
                    .Include(r => r.Book)
                    .Include(r => r.User))
                .ToListAsync(cancellationToken);
        }
    }
}