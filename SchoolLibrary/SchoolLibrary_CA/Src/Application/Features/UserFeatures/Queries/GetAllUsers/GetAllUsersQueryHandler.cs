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

        public GetAllUsersQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
        
        
        public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions.MapListSourceToDestination<User, UserDTO>(await _context.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}