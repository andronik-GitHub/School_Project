using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.PublisherFeatures.Commands
{
    public class DeletePublisherCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        
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
                    .FindAsync(new object?[] { command.Id, cancellationToken }, cancellationToken: cancellationToken);
                
                if (entity == null) throw new NotFoundException(nameof(Publisher), command.Id);

                _context.Publishers.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.PublisherId;
            }
        }
    }
}