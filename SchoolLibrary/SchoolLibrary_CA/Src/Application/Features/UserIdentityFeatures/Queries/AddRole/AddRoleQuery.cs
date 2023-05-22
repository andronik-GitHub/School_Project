using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.AddRole
{
    public class AddRoleQuery : IRequest<Guid>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}