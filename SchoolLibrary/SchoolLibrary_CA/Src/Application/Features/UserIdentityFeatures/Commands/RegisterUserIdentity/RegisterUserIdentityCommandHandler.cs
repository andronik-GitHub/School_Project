using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Commands.RegisterUserIdentity
{
    public class RegisterUserIdentityCommandHandler : IRequestHandler<RegisterUserIdentityCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IUserManager _userManager;

        public RegisterUserIdentityCommandHandler(ISchoolLibraryContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<Guid> Handle(RegisterUserIdentityCommand command, CancellationToken cancellationToken)
        {
            var id = await _userManager.RegisterUserAsync(command.UserName, command.Email, command.Password, null);
            await _context.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}