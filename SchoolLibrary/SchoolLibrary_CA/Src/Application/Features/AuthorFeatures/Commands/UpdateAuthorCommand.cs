using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.AuthorFeatures.Commands
{
    public class UpdateAuthorCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Birthdate { get; set; } = default!;
        public string Nationality { get; set; } = default!;
        
        public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public UpdateAuthorCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Authors
                    .FindAsync(new object?[] { command.Id, cancellationToken }, cancellationToken: cancellationToken);
                
                if (entity == null) throw new NotFoundException(nameof(Author), command.Id);
                
                entity.AuthorId = Guid.NewGuid();
                entity.FirstName = command.FirstName;
                entity.LastName = command.LastName;
                entity.Birthdate = command.Birthdate;
                entity.Nationality = command.Nationality;

                await _context.SaveChangesAsync(cancellationToken);
                return entity.AuthorId;
            }
        }
    }
}