using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookGenreFeatures.Commands.UpdateBookGenre
{
    public class UpdateBookGenreCommandHandler : IRequestHandler<UpdateBookGenreCommand, (Guid, Guid)>
    {
        private readonly ISchoolLibraryContext _context;

        public UpdateBookGenreCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<(Guid, Guid)> Handle(UpdateBookGenreCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.BookGenres
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    bg => bg.BookId == command.BookId && bg.GenreId == command.GenreId, 
                    cancellationToken: cancellationToken);
                
            if (entity == null) 
                throw new NotFoundException(nameof(BookGenres), new { command.BookId, command.GenreId });

            entity = MapsterFunctions.MapSourceToDestination<UpdateBookGenreCommand, BookGenres>(command);

            _context.BookGenres.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return (entity.BookId, entity.GenreId);
        }
    }
}