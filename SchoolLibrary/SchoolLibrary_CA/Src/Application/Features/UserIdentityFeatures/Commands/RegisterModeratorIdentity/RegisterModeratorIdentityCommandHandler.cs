using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Commands.RegisterModeratorIdentity
{
    public class RegisterModeratorIdentityCommandHandler
        : IRequestHandler<RegisterModeratorIdentityCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IUserManager _userManager;

        public RegisterModeratorIdentityCommandHandler(ISchoolLibraryContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<Guid> Handle(RegisterModeratorIdentityCommand command, CancellationToken cancellationToken)
        {
            var id = await _userManager.RegisterModeratorAsync(command.UserName, command.Email, command.Password);
            await _context.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}