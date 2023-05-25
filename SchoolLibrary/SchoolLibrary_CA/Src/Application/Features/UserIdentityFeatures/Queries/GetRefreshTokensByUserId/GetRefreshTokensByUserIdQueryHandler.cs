using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetRefreshTokensByUserId
{
    public class GetRefreshTokensByUserIdQueryHandler 
        : IRequestHandler<GetRefreshTokensByUserIdQuery, IEnumerable<object>>
    {
        private readonly IUserManager _userManager;
        
        public GetRefreshTokensByUserIdQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }


        public async Task<IEnumerable<object>> Handle
            (GetRefreshTokensByUserIdQuery query, CancellationToken cancellationToken)
        {
            return await _userManager.GetRefreshTokensByUserId(query.Id);
        }
    }
}