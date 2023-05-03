using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthorFeatures.Queries.GetAuthor_DataShaping
{
    public class GetAuthorById_DataShapingQueryHandler : IRequestHandler<GetAuthorById_DataShapingQuery, ShapedEntity>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<Author> _dataShaper;

        public GetAuthorById_DataShapingQueryHandler(ISchoolLibraryContext context, IDataShaper<Author> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }


        public async Task<ShapedEntity> Handle(GetAuthorById_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AuthorId == query.Id, cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(Author), query.Id) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}