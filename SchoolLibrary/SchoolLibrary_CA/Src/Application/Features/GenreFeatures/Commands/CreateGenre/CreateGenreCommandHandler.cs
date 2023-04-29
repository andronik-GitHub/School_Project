using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;

namespace Application.Features.GenreFeatures.Commands.CreateGenre
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Guid> 
    {
        private readonly ISchoolLibraryContext _context;

        public CreateGenreCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
           
           
        public async Task<Guid> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
        {
            var entity = MapsterFunctions.MapSourceToDestination<CreateGenreCommand, Genre>(command);

            await _context.Genres.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.GenreId;
        }
    }
}