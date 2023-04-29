using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.UserFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Queries.GetUser
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly ISchoolLibraryContext _context;
        public GetUserByIdQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }

            
        public async Task<UserDTO> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<User, UserDTO>(await _context.Users
                .AsNoTracking()
                .FirstAsync(u => u.UserId == query.Id, cancellationToken));
                
            return entity ?? throw new NotFoundException(nameof(User), query.Id);
        }
    }
}