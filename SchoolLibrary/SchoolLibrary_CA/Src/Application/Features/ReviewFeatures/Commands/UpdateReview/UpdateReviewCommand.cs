using MediatR;

namespace Application.Features.ReviewFeatures.Commands.UpdateReview
{
    public class UpdateReviewCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}