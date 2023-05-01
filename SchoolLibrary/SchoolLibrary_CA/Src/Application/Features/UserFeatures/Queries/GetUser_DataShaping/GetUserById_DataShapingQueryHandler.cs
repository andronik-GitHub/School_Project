using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Queries.GetUser_DataShaping
{
    public class GetUserById_DataShapingQueryHandler : IRequestHandler<GetUserById_DataShapingQuery, ExpandoObject>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<User> _dataShaper;

        public GetUserById_DataShapingQueryHandler(
            ISchoolLibraryContext context, 
            IDataShaper<User> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }


        public async Task<ExpandoObject> Handle
            (GetUserById_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                .AsNoTracking()
                .Include(u => u.UserName)
                .Include(u => u.Address)
                .FirstAsync(u => u.UserId == query.Id, cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(Review), query.Id) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}