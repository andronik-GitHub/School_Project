using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PublisherFeatures.Commands.DeletePublisher
{
    public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand,Guid>
    {
        private readonly ISchoolLibraryContext _context;

        public DeletePublisherCommandHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }
            
            
        public async Task<Guid> Handle(DeletePublisherCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.Publishers
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PublisherId == command.Id, cancellationToken);
                
            if (entity == null) throw new NotFoundException(nameof(Publisher), command.Id);

            _context.Publishers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.PublisherId;
        }
    }
}