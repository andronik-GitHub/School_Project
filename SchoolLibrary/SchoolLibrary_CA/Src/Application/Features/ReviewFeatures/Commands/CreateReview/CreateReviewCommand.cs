using MediatR;

namespace Application.Features.ReviewFeatures.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<Guid>
    {
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}