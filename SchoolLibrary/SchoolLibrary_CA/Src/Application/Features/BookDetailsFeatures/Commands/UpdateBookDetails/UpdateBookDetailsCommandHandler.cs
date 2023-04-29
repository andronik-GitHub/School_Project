using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Commands.UpdateBookDetails
{
    public class UpdateBookDetailsCommandHandler : IRequestHandler<UpdateBookDetailsCommand, Guid>
    {
        private readonly ISchoolLibraryContext _context;

        public UpdateBookDetailsCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<Guid> Handle(UpdateBookDetailsCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.BookDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(bd => bd.BookDetailId == command.Id, cancellationToken);
                
            if (entity == null) throw new NotFoundException(nameof(BookDetails), command.Id);
                
            entity = MapsterFunctions.MapSourceToDestination<UpdateBookDetailsCommand, BookDetails>(command);
            
            _context.BookDetails.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.BookDetailId;
        }
    }
}