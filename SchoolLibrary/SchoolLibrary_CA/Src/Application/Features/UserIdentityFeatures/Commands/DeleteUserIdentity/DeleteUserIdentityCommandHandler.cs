using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.UserIdentityFeatures.Commands.DeleteUserIdentity
{
    public class DeleteUserIdentityCommandHandler : IRequestHandler<DeleteUserIdentityCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IUserManager _userManager;

        public DeleteUserIdentityCommandHandler(ISchoolLibraryContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<Guid> Handle(DeleteUserIdentityCommand command, CancellationToken cancellationToken)
        {
            if (command.Id != Guid.Empty)
            {
                await _userManager.DeleteUserAsync(command.Id);
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return command.Id;
        }
    }
}