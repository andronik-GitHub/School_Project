using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.BookFeatures.Commands
{
    public class CreateBookCommand : IRequest<Guid>
    {
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = default!;
        public Guid PublisherId { get; set; }
        
        public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;
            
            public CreateBookCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<Guid> Handle(CreateBookCommand command, CancellationToken cancellationToken)
            {
                var entity = new Book
                {
                    BookId = Guid.NewGuid(),
                    Title = command.Title,
                    PublishingYear = command.PublishingYear,
                    ISBN = command.ISBN,
                    PublisherId = command.PublisherId
                };

                await _context.Books.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.BookId;
            }
        }
    }
}