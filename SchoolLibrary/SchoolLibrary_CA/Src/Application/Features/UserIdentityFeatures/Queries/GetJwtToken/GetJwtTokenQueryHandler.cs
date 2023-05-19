using Application.Common.Interfaces;
using Application.Features.UserIdentityFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetJwtToken
{
    public class GetJwtTokenQueryHandler : IRequestHandler<GetJwtTokenQuery, AuthenticationModel>
    {
        private readonly IUserManager _userManager;
        
        public GetJwtTokenQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }


        public async Task<AuthenticationModel> Handle(GetJwtTokenQuery query, CancellationToken cancellationToken)
        {
            var tokenRequest = new TokenRequestModel
            {
                Email = query.Email,
                Password = query.Password
            };
            return await _userManager.GetTokenAsync(tokenRequest);
        }
    }
}