using Application.Common.Pagging.Entities;
using Application.Features.ReviewFeatures.Queries.Common;
using MediatR;

namespace Application.Features.ReviewFeatures.Queries.GetAllReviews
{
    public class GetAllReviewsQuery : IRequest<IEnumerable<ReviewDTO>>
    {
        public ReviewParameter _parameters { get; }

        public GetAllReviewsQuery(ReviewParameter parameters)
        {
            _parameters = parameters;
        }
    }
}