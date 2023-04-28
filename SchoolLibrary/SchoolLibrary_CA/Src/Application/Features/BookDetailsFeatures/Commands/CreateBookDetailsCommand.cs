using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.BookDetailsFeatures.Commands
{
    public class CreateBookDetailsCommand : IRequest<Guid>
    {
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
        public Guid BookId { get; set; }
        
        public class CreateBookDetailsCommandHandler : IRequestHandler<CreateBookDetailsCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public CreateBookDetailsCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(CreateBookDetailsCommand command, CancellationToken cancellationToken)
            {
                var entity = new BookDetails
                {
                    BookDetailId = Guid.NewGuid(),
                    Pages = command.Pages,
                    Language = command.Language,
                    Format = command.Format,
                    BookId = command.BookId
                };

                await _context.BookDetails.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.BookDetailId;
            }
        }
    }
}