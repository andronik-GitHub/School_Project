using Application.Features.UserIdentityFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetUserIdentity
{
    public class GetUserIdentityQuery : IRequest<UserIdentityDTO>
    {
        public Guid Id { get; set; } = default!;
    }
}