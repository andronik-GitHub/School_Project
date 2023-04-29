using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Commands.UpdateBook
{
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

            entity = MapsterFunctions.MapSourceToDestination<UpdateBookCommand, Book>(command);
            
            _context.Books.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.BookId;
        }
    }
}