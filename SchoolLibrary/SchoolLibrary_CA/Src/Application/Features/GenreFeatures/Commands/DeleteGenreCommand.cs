using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.GenreFeatures.Commands
{
    public class DeleteGenreCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        
        public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public DeleteGenreCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }

            
            public async Task<Guid> Handle(DeleteGenreCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Genres
                    .FindAsync(new object?[] { command.Id, cancellationToken }, cancellationToken: cancellationToken);

                if (entity == null) throw new NotFoundException(nameof(Genre), command.Id);
                
                _context.Genres.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.GenreId;
            }
        }
    }
}