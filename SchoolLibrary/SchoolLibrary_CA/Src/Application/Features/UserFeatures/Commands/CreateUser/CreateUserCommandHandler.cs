using Application.Common.Interfaces;
using Application.Common.Mapping;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Features.UserFeatures.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;
            
        public CreateUserCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            User entity = MapsterFunctions.MapSourceToDestination<CreateUserCommand, User>(command);

            await _context.Users.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.UserId;
        }
    }
}