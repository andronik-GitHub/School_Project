using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.AuthorFeatures.Commands
{
    public class CreateAuthorCommand : IRequest<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Birthdate { get; set; } = default!;
        public string Nationality { get; set; } = default!;
        
        public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public CreateAuthorCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
            {
                var entity = new Author
                {
                    AuthorId = Guid.NewGuid(),
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Birthdate = command.Birthdate,
                    Nationality = command.Nationality
                };

                await _context.Authors.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.AuthorId;
            }
        }
    }
}