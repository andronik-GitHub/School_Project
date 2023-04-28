using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Commands
{
    public class DeleteBookDetailsCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        
        public class DeleteBookDetailsCommandHandler : IRequestHandler<DeleteBookDetailsCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public DeleteBookDetailsCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(DeleteBookDetailsCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.BookDetails
                    .AsNoTracking()
                    .FirstOrDefaultAsync(bd => bd.BookDetailId == command.Id, cancellationToken);

                if (entity == null) throw new NotFoundException(nameof(BookDetails), command.Id);
                
                _context.BookDetails.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.BookDetailId;
            }
        }
    }
}