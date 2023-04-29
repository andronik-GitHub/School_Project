using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;

namespace Application.Features.BookGenreFeatures.Commands.CreateBookGenre
{
    public class CreateBookGenreCommandHandler : IRequestHandler<CreateBookGenreCommand, (Guid, Guid)>
    {
        private readonly ISchoolLibraryContext _context;

        public CreateBookGenreCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<(Guid, Guid)> Handle(CreateBookGenreCommand command, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<CreateBookGenreCommand, BookGenres>(command);

            await _context.BookGenres.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return (entity.BookId, entity.GenreId);
        }
    }
}