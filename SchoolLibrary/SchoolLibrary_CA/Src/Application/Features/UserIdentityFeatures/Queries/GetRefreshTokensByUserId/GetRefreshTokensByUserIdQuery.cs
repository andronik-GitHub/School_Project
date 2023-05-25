using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.GetRefreshTokensByUserId
{
    public class GetRefreshTokensByUserIdQuery : IRequest<IEnumerable<Object>>
    {
        public Guid Id { get; set; } = default!;
    }
}