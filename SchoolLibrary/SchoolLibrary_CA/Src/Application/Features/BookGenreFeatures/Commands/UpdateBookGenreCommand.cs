using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookGenreFeatures.Commands
{
    public class UpdateBookGenreCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid GenreId { get; set; } = default!;
        
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

                entity.BookId = command.BookId;
                entity.GenreId = command.GenreId;

                await _context.SaveChangesAsync(cancellationToken);
                return (entity.BookId, entity.GenreId);
            }
        }
    }
}