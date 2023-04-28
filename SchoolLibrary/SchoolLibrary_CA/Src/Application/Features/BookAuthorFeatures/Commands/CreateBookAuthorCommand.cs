using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.BookAuthorFeatures.Commands
{
    public class CreateBookAuthorCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid AuthorId { get; set; } = default!;
        
        public class CreateBookAuthorCommandHandler : IRequestHandler<CreateBookAuthorCommand, (Guid, Guid)>
        {
            private readonly ISchoolLibraryContext _context;

            public CreateBookAuthorCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<(Guid, Guid)> Handle(CreateBookAuthorCommand command, CancellationToken cancellationToken)
            {
                var entity = new BookAuthors
                {
                    BookId = command.BookId,
                    AuthorId = command.AuthorId
                };

                await _context.BookAuthors.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return (entity.BookId, entity.AuthorId);
            }
        }
    }
}