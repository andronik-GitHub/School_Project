using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReviewFeatures.Queries
{
    public class GetReviewByIdQuery : IRequest<Review>
    {
        public Guid Id { get; set; } = default!;
        
        public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Review>
        {
            private readonly ISchoolLibraryContext _context;
            
            public GetReviewByIdQueryHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<Review> Handle(GetReviewByIdQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.Reviews
                    .AsNoTracking()
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.ReviewId == query.Id, cancellationToken: cancellationToken);
                
                return entity ?? throw new NotFoundException(nameof(Review), query.Id);
            }
        }
    }
}