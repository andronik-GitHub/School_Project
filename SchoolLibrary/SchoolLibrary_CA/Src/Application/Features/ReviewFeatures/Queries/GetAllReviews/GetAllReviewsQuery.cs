using Application.Features.ReviewFeatures.Queries.Common;
using MediatR;

namespace Application.Features.ReviewFeatures.Queries.GetAllReviews
{
    public class GetAllReviewsQuery : IRequest<IEnumerable<ReviewDTO>>
    {
    }
}