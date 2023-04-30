using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Mapping.Mapster;
using Application.Features.UserFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<User> _sortHelper;

        public GetAllUsersQueryHandler(ISchoolLibraryContext context, ISortHelper<User> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }
        
        
        public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var list = _context.Users.AsNoTracking();

            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);
            
            // Paging
            return await MapsterFunctions.MapListSourceToDestination<User, UserDTO>(
                list
                    //.OrderBy(u => u.UserId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize))
                .ToListAsync(cancellationToken);
        }
    }
}