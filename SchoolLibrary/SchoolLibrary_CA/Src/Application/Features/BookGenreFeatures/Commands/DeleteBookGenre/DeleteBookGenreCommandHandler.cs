using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookGenreFeatures.Commands.DeleteBookGenre
{
    public class DeleteBookGenreCommandHandler : IRequestHandler<DeleteBookGenreCommand, (Guid, Guid)>
    {
        private readonly ISchoolLibraryContext _context;

        public DeleteBookGenreCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<(Guid, Guid)> Handle(DeleteBookGenreCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.BookGenres
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    bg => bg.BookId == command.BookId && bg.GenreId == command.GenreId, 
                    cancellationToken);

            if (entity == null) 
                throw new NotFoundException(nameof(BookGenres), new { command.BookId, command.GenreId });
                
            _context.BookGenres.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return (entity.BookId, entity.GenreId);
        }
    }
}