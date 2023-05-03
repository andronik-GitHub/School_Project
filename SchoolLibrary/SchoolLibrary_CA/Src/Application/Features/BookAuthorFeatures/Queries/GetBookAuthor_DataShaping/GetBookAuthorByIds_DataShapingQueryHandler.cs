using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookAuthorFeatures.Queries.GetBookAuthor_DataShaping
{
    public class GetBookAuthorByIds_DataShapingQueryHandler 
        : IRequestHandler<GetBookAuthorByIds_DataShapingQuery, ShapedEntity>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<BookAuthors> _dataShaper;

        public GetBookAuthorByIds_DataShapingQueryHandler(
            ISchoolLibraryContext context, 
            IDataShaper<BookAuthors> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }


        public async Task<ShapedEntity> Handle
            (GetBookAuthorByIds_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.BookAuthors
                .AsNoTracking()
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .FirstAsync(
                    ba => ba.BookId == query.BookId && ba.AuthorId == query.AuthorId, 
                    cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(BookAuthors), new { query.BookId, query.AuthorId }) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}