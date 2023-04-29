using MediatR;

namespace Application.Features.ReviewFeatures.Commands.DeleteReview
{
    public class DeleteReviewCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
    }
}