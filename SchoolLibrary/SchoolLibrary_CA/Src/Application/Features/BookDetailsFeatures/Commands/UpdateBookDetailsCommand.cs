using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Commands
{
    public class UpdateBookDetailsCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
        public Guid BookId { get; set; }
        
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
                
                entity.Pages = command.Pages;
                entity.Language = command.Language;
                entity.Format = command.Format;
                entity.BookId = command.BookId;

                await _context.SaveChangesAsync(cancellationToken);
                return entity.BookDetailId;
            }
        }
    }
}