using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;

namespace Application.Features.ReviewFeatures.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;

        public CreateReviewCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<Guid> Handle(CreateReviewCommand command, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<CreateReviewCommand, Review>(command);

            await _context.Reviews.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.ReviewId;
        }
    }
}