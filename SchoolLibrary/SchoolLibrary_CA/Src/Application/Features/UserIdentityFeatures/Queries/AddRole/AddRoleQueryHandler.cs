using Application.Common.Interfaces;
using Application.Features.UserIdentityFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Queries.AddRole
{
    public class AddRoleQueryHandler : IRequestHandler<AddRoleQuery, Guid>
    {
        private readonly IUserManager _userManager;
        
        public AddRoleQueryHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }


        public async Task<Guid> Handle(AddRoleQuery query, CancellationToken cancellationToken)
        {
            var userId = await _userManager.AddRoleAsync(new AddRoleModel
            {
                Email = query.Email,
                Password = query.Password,
                Role = query.Role
            });

            return userId;
        }
    }
}