using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PublisherFeatures.Queries.GetPublisher_DataShaping
{
    public class GetPublisherById_DataShapingQueryHandler
        : IRequestHandler<GetPublisherById_DataShapingQuery, ShapedEntity>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<Publisher> _dataShaper;

        public GetPublisherById_DataShapingQueryHandler(
            ISchoolLibraryContext context, 
            IDataShaper<Publisher> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }


        public async Task<ShapedEntity> Handle
            (GetPublisherById_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Publishers
                .AsNoTracking()
                .FirstAsync(bd => bd.PublisherId == query.Id, cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(Publisher), query.Id) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}