using MediatR;

namespace Application.Features.UserIdentityFeatures.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand : IRequest<bool>
    {
        public string? Token { get; set; } = default!;
    }
}