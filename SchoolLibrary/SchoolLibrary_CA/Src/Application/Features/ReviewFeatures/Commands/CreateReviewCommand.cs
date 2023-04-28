using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.ReviewFeatures.Commands
{
    public class CreateReviewCommand : IRequest<Guid>
    {
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        
        public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public CreateReviewCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<Guid> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
            {
                var entity = new Review()
                {
                    ReviewId = Guid.NewGuid(),
                    Rating = command.Rating,
                    ReviewText = command.ReviewText,
                    UserId = command.UserId,
                    BookId = command.BookId
                };

                await _context.Reviews.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.ReviewId;
            }
        }
    }
}