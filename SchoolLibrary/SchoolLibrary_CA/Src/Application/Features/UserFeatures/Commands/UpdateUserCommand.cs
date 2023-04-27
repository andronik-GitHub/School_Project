using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    public class UpdateUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Phone { get; set; } = default!;
        
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
                    .FindAsync(new object?[] { command.Id, cancellationToken }, cancellationToken: cancellationToken);
                
                if (entity == null) throw new NotFoundException(nameof(User), command.Id);

                entity.UserName = new UserName(new Name(command.FirstName), new Name(command.LastName));
                entity.Email = command.Email;
                entity.Password = command.Password;
                entity.Address = new Address
                    (new Location(command.Street), new Location(command.City), new Location(command.Country));
                entity.Phone = command.Phone;

                await _context.SaveChangesAsync(cancellationToken);
                return entity.UserId;
            }
        }
    }
}