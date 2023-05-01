using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookGenreFeatures.Queries.GetBookGenre_DataShaping
{
    public class GetBookGenreByIds_DataShapingQueryHandler 
        : IRequestHandler<GetBookGenreByIds_DataShapingQuery, ExpandoObject>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<BookGenres> _dataShaper;

        public GetBookGenreByIds_DataShapingQueryHandler(
            ISchoolLibraryContext context, 
            IDataShaper<BookGenres> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }


        public async Task<ExpandoObject> Handle
            (GetBookGenreByIds_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.BookGenres
                .AsNoTracking()
                .Include(bg => bg.Book)
                .Include(bg => bg.Genre)
                .FirstAsync(
                    ba => ba.BookId == query.BookId && ba.GenreId == query.GenreId, 
                    cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(BookGenres), new { query.BookId, query.GenreId }) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}