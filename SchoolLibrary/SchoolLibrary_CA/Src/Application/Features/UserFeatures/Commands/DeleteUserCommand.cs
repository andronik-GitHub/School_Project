using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeatures.Commands
{
    public class DeleteUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        
        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public DeleteUserCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Users
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserId == command.Id, cancellationToken);

                if (entity == null) throw new NotFoundException(nameof(User), command.Id);
                
                _context.Users.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.UserId;
            }
        }
    }
}