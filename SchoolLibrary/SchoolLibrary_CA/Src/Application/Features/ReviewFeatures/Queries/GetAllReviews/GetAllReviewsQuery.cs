using Application.Common.Pagging.Entities;
using Application.Features.ReviewFeatures.Queries.Common;
using MediatR;

namespace Application.Features.ReviewFeatures.Queries.GetAllReviews
{
    public class GetAllReviewsQuery : IRequest<IEnumerable<ReviewDTO>>
    {
        public BaseParameter _parameters { get; }

        public GetAllReviewsQuery(BaseParameter parameters)
        {
            _parameters = parameters;
        }
    }
}