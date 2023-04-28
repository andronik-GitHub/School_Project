using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReviewFeatures.Commands
{
    public class UpdateReviewCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        
        public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public UpdateReviewCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<Guid> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Reviews
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.ReviewId == command.Id, cancellationToken: cancellationToken);
                
                if (entity == null) throw new NotFoundException(nameof(Review), command.Id);

                entity.Rating = command.Rating;
                entity.ReviewText = command.ReviewText;
                entity.UserId = command.UserId;
                entity.BookId = command.BookId;

                await _context.SaveChangesAsync(cancellationToken);
                return entity.ReviewId;
            }
        }
    }
}