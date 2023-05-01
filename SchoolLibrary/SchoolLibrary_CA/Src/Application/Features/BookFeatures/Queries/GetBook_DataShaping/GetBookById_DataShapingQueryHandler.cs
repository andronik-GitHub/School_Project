using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Queries.GetBook_DataShaping
{
    public class GetBookById_DataShapingQueryHandler : IRequestHandler<GetBookById_DataShapingQuery, ExpandoObject>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<Book> _dataShaper;

        public GetBookById_DataShapingQueryHandler(
            ISchoolLibraryContext context, 
            IDataShaper<Book> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }


        public async Task<ExpandoObject> Handle
            (GetBookById_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Books
                .AsNoTracking()
                .Include(b => b.Publisher)
                .FirstAsync(bd => bd.BookId == query.Id, cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(Book), query.Id) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}