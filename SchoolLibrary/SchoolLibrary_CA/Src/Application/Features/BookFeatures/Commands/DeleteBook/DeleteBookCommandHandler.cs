using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Commands.DeleteBook
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;

        public DeleteBookCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<Guid> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookId == command.Id, cancellationToken);

            if (entity == null) throw new NotFoundException(nameof(Book), command.Id);
                
            _context.Books.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.BookId;
        }
    }
}