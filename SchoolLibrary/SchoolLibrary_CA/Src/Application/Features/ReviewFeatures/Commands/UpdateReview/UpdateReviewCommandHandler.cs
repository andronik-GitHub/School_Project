using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReviewFeatures.Commands.UpdateReview
{
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

            entity = MapsterFunctions.MapSourceToDestination<UpdateReviewCommand, Review>(command);

            _context.Reviews.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.ReviewId;
        }
    }
}