using MediatR;

namespace Application.Features.UserIdentityFeatures.Commands.DeleteUserIdentity
{
    public class DeleteUserIdentityCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
    }
}