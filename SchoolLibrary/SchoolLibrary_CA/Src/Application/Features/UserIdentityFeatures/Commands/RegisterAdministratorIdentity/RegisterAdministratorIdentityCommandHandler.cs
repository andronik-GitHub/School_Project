using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Commands.RegisterAdministratorIdentity
{
    public class RegisterAdministratorIdentityCommandHandler 
        : IRequestHandler<RegisterAdministratorIdentityCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IUserManager _userManager;

        public RegisterAdministratorIdentityCommandHandler(ISchoolLibraryContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<Guid> Handle
            (RegisterAdministratorIdentityCommand command, CancellationToken cancellationToken)
        {
            var id = await _userManager.RegisterAdministratorAsync(command.UserName, command.Email, command.Password);
            await _context.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}