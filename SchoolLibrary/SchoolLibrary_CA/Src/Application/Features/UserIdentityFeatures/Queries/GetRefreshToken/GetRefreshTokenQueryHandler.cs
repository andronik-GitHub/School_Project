using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.UserIdentityFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetRefreshToken
{
    public class GetRefreshTokenQueryHandler : IRequestHandler<GetRefreshTokenQuery, AuthenticationModel>
    {
        private readonly IUserManager _userManager;
        
        public GetRefreshTokenQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }


        public async Task<AuthenticationModel> Handle(GetRefreshTokenQuery query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(query.Token)) throw new BadRequestException("Token is invalid!");
            
            return await _userManager.RefreshTokenAsync(query.Token);
        }
    }
}