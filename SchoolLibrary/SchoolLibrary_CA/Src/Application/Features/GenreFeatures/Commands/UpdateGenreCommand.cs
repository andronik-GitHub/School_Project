using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.GenreFeatures.Commands
{
    public class UpdateGenreCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        
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
                    .FindAsync(new object?[] { command.Id, cancellationToken }, cancellationToken: cancellationToken);
                
                if (entity == null) throw new NotFoundException(nameof(Genre), command.Id);

                entity.Name = command.Name;

                await _context.SaveChangesAsync(cancellationToken);
                return entity.GenreId;
            }
        }
    }
}