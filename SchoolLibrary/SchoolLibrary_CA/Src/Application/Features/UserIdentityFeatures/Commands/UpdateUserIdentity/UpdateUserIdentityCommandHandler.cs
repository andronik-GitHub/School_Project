using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Commands.UpdateUserIdentity
{
    public class UpdateUserIdentityCommandHandler : IRequestHandler<UpdateUserIdentityCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IUserManager _userManager;

        public UpdateUserIdentityCommandHandler(ISchoolLibraryContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<Guid> Handle(UpdateUserIdentityCommand command, CancellationToken cancellationToken)
        {
            await _userManager.UpdateUserAsync(command);
            await _context.SaveChangesAsync(cancellationToken);

            return command.Id;
        }
    }
}