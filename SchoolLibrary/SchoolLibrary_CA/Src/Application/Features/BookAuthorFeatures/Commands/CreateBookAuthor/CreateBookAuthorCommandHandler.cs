using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;

namespace Application.Features.BookAuthorFeatures.Commands.CreateBookAuthor
{
    public class CreateBookAuthorCommandHandler : IRequestHandler<CreateBookAuthorCommand, (Guid, Guid)>
    {
        private readonly ISchoolLibraryContext _context;

        public CreateBookAuthorCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<(Guid, Guid)> Handle(CreateBookAuthorCommand command, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<CreateBookAuthorCommand, BookAuthors>(command);

            await _context.BookAuthors.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return (entity.BookId, entity.AuthorId);
        }
    }
}