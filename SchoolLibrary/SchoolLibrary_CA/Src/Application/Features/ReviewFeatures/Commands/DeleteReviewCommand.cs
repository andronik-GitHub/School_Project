using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReviewFeatures.Commands
{
    public class DeleteReviewCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        
        public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public DeleteReviewCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(DeleteReviewCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Reviews
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.ReviewId == command.Id, cancellationToken);

                if (entity == null) throw new NotFoundException(nameof(Review), command.Id);
                
                _context.Reviews.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.ReviewId;
            }
        }
    }
}