using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookAuthorFeatures.Commands
{
    public class DeleteBookAuthorCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid AuthorId { get; set; } = default!;
        
        public class DeleteBookAuthorCommandHandler : IRequestHandler<DeleteBookAuthorCommand, (Guid, Guid)>
        {
            private readonly ISchoolLibraryContext _context;

            public DeleteBookAuthorCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<(Guid, Guid)> Handle(DeleteBookAuthorCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.BookAuthors
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        ba => ba.BookId == command.BookId && ba.AuthorId == command.AuthorId, 
                        cancellationToken);

                if (entity == null) 
                    throw new NotFoundException(nameof(BookAuthors), new { command.BookId, command.AuthorId });
                
                _context.BookAuthors.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return (entity.BookId, entity.AuthorId);
            }
        }
    }
}