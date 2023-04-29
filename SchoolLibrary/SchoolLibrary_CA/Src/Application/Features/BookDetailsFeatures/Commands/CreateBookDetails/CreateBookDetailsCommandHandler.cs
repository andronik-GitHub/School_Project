using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;

namespace Application.Features.BookDetailsFeatures.Commands.CreateBookDetails
{
    public class CreateBookDetailsCommandHandler : IRequestHandler<CreateBookDetailsCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;

        public CreateBookDetailsCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<Guid> Handle(CreateBookDetailsCommand command, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<CreateBookDetailsCommand, BookDetails>(command);

            await _context.BookDetails.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.BookDetailId;
        }
    }
}