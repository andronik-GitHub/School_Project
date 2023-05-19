using Application.Features.UserIdentityFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetJwtToken
{
    public class GetJwtTokenQuery : IRequest<AuthenticationModel>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}