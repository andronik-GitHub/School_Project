using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.GenreFeatures.Commands
{
    public class CreateGenreCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        
        public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Guid> 
        {
           private readonly ISchoolLibraryContext _context;

           public CreateGenreCommandHandler(ISchoolLibraryContext context)
           {
               _context = context;
           }
           
           
           public async Task<Guid> Handle(CreateGenreCommand command, CancellationToken cancellationToken)
           {
               var entity = new Genre
               {
                   GenreId = Guid.NewGuid(),
                   Name = command.Name
               };

               await _context.Genres.AddAsync(entity, cancellationToken);
               await _context.SaveChangesAsync(cancellationToken);
               return entity.GenreId;
           }
        }
    }
}