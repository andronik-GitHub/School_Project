using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Commands
{
    public class UpdateBookCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = default!;
        public Guid PublisherId { get; set; }
        
        public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;
            
            public UpdateBookCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }
            
            
            public async Task<Guid> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Books
                    .AsNoTracking()
                    .FirstOrDefaultAsync(b => b.BookId == command.Id, cancellationToken: cancellationToken);
                
                if (entity == null) throw new NotFoundException(nameof(Book), command.Id);

                entity.Title = command.Title;
                entity.PublishingYear = command.PublishingYear;
                entity.ISBN = command.ISBN;
                entity.PublisherId = command.PublisherId;

                await _context.SaveChangesAsync(cancellationToken);
                return entity.BookId;
            }
        }
    }
}