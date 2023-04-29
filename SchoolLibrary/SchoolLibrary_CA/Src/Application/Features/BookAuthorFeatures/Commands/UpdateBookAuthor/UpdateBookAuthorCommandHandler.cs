using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookAuthorFeatures.Commands.UpdateBookAuthor
{
    public class UpdateBookAuthorCommandHandler : IRequestHandler<UpdateBookAuthorCommand, (Guid, Guid)>
    {
        private readonly ISchoolLibraryContext _context;

        public UpdateBookAuthorCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<(Guid, Guid)> Handle(UpdateBookAuthorCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.BookAuthors
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    ba => ba.BookId == command.BookId && ba.AuthorId == command.AuthorId, 
                    cancellationToken: cancellationToken);
                
            if (entity == null) 
                throw new NotFoundException(nameof(BookAuthors), new { command.BookId, command.AuthorId });

            entity = MapsterFunctions.MapSourceToDestination<UpdateBookAuthorCommand, BookAuthors>(command);

            _context.BookAuthors.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return (entity.BookId, entity.AuthorId);
        }
    }
}