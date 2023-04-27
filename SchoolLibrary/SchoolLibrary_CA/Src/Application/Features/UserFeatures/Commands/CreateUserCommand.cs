using Application.Common.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Phone { get; set; } = default!;
        
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;
            
            public CreateUserCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
                var entity = new User
                {
                    UserId = Guid.NewGuid(),
                    UserName = new UserName(new Name(command.FirstName), new Name(command.LastName)),
                    Email = command.Email,
                    Password = command.Password,
                    Address = new Address
                        (new Location(command.Street), new Location(command.City), new Location(command.Country)),
                    Phone = command.Phone
                };

                await _context.Users.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.UserId;
            }
        }
    }
}