using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.UserIdentityFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetUsersIdentity
{
    public class GetUsersIdentityQueryHandler : IRequestHandler<GetUsersIdentityQuery, IEnumerable<UserIdentityDTO>>
    {
        private readonly IUserManager _userManager;
        
        public GetUsersIdentityQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }


        public async Task<IEnumerable<UserIdentityDTO>> Handle
            (GetUsersIdentityQuery query, CancellationToken cancellationToken)
        {
            var users = await _userManager.GetUsersAsync();
            if (users is not IEnumerable<UserIdentityDTO> usersDTO) 
                throw new BadRequestException("The received data does not match the requested data type!");
            return usersDTO;
        }
    }
}