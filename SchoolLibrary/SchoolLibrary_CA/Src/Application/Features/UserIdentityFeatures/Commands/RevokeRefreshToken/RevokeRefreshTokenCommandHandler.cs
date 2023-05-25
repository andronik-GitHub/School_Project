using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, bool>
    {
        private readonly IUserManager _userManager;

        public RevokeRefreshTokenCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }


        public async Task<bool> Handle(RevokeRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            return await _userManager.RevokeRefreshTokenAsync(command.Token);
        }
    }
}