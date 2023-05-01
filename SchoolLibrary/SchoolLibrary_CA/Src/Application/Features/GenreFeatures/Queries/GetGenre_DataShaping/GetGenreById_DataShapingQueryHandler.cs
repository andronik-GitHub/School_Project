using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GenreFeatures.Queries.GetGenre_DataShaping
{
    public class GetGenreById_DataShapingQueryHandler 
        : IRequestHandler<GetGenreById_DataShapingQuery, ExpandoObject>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<Genre> _dataShaper;

        public GetGenreById_DataShapingQueryHandler(
            ISchoolLibraryContext context, 
            IDataShaper<Genre> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }


        public async Task<ExpandoObject> Handle
            (GetGenreById_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Genres
                .AsNoTracking()
                .FirstAsync(bd => bd.GenreId == query.Id, cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(Genre), query.Id) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}