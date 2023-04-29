using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.ReviewFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReviewFeatures.Queries.GetReview
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDTO>
    {
        private readonly ISchoolLibraryContext _context;
            
        public GetReviewByIdQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<ReviewDTO> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<Review, ReviewDTO>
                (await _context.Reviews
                    .AsNoTracking()
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .FirstAsync(r => r.ReviewId == query.Id, cancellationToken: cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(Review), query.Id);
        }
    }
}