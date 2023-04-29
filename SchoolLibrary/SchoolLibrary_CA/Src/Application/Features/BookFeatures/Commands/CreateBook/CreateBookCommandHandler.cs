using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;

namespace Application.Features.BookFeatures.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;
            
        public CreateBookCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<Guid> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<CreateBookCommand, Book>(command);

            await _context.Books.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.BookId;
        }
    }
}