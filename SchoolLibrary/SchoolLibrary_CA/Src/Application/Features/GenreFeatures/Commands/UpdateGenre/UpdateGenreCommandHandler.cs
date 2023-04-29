using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GenreFeatures.Commands.UpdateGenre
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;

        public UpdateGenreCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<Guid> Handle(UpdateGenreCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.GenreId == command.Id, cancellationToken);
                
            if (entity == null) throw new NotFoundException(nameof(Genre), command.Id);

            entity = MapsterFunctions.MapSourceToDestination<UpdateGenreCommand, Genre>(command);

            _context.Genres.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.GenreId;
        }
    }
}