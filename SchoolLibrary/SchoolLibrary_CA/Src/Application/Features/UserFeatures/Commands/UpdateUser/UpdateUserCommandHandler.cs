using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;
            
        public UpdateUserCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
        
        
        public async Task<Guid> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == command.Id, cancellationToken);
                
            if (entity == null) throw new NotFoundException(nameof(User), command.Id);

            entity = MapsterFunctions.MapSourceToDestination<UpdateUserCommand, User>(command);

            await _context.SaveChangesAsync(cancellationToken);
            return entity.UserId;
        }
    }
}