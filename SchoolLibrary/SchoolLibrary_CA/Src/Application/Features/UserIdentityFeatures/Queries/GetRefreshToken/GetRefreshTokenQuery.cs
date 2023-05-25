using Application.Features.UserIdentityFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetRefreshToken
{
    public class GetRefreshTokenQuery : IRequest<AuthenticationModel>
    {
        public string? Token { get; set; } = default!;
    }
}