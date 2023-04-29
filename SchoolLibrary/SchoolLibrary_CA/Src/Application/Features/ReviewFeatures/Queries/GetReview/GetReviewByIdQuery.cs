using Application.Features.ReviewFeatures.Queries.Common;
using MediatR;

namespace Application.Features.ReviewFeatures.Queries.GetReview
{
    public class GetReviewByIdQuery : IRequest<ReviewDTO>
    {
        public Guid Id { get; set; } = default!;
    }
}