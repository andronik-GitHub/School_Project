using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Queries.GetBookDetails_DataShaping
{
    public class GetBookDetailsById_DataShapingQueryHandler 
        : IRequestHandler<GetBookDetailsById_DataShapingQuery, ShapedEntity>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<BookDetails> _dataShaper;

        public GetBookDetailsById_DataShapingQueryHandler(
            ISchoolLibraryContext context, 
            IDataShaper<BookDetails> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }
        
        
        public async Task<ShapedEntity> Handle
            (GetBookDetailsById_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.BookDetails
                .AsNoTracking()
                .Include(bd => bd.Book)
                .FirstAsync(bd => bd.BookDetailId == query.Id, cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(BookDetails), query.Id) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}