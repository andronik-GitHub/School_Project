using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.BookGenreFeatures.Commands
{
    public class CreateBookGenreCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid GenreId { get; set; } = default!;
        
        public class CreateBookGenreCommandHandler : IRequestHandler<CreateBookGenreCommand, (Guid, Guid)>
        {
            private readonly ISchoolLibraryContext _context;

            public CreateBookGenreCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<(Guid, Guid)> Handle(CreateBookGenreCommand command, CancellationToken cancellationToken)
            {
                var entity = new BookGenres
                {
                    BookId = command.BookId,
                    GenreId = command.GenreId
                };

                await _context.BookGenres.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return (entity.BookId, entity.GenreId);
            }
        }
    }
}