using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReviewFeatures.Queries.GetReview_DataShaping
{
    public class GetReviewById_DataShapingQueryHandler : IRequestHandler<GetReviewById_DataShapingQuery, ShapedEntity>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<Review> _dataShaper;

        public GetReviewById_DataShapingQueryHandler(
            ISchoolLibraryContext context, 
            IDataShaper<Review> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }


        public async Task<ShapedEntity> Handle
            (GetReviewById_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Reviews
                .AsNoTracking()
                .FirstAsync(bd => bd.ReviewId == query.Id, cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(Review), query.Id) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}