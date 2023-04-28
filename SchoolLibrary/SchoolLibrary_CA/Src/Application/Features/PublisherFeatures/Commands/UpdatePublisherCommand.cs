using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PublisherFeatures.Commands
{
    public class UpdatePublisherCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
        
        public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, Guid>
        {
            private readonly ISchoolLibraryContext _context;

            public UpdatePublisherCommandHandler(ISchoolLibraryContext context)
            {
                _context = context;
            }


            public async Task<Guid> Handle(UpdatePublisherCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Publishers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PublisherId == command.Id, cancellationToken);
                
                if (entity == null) throw new NotFoundException(nameof(Publisher), command.Id);

                entity.Name = command.Name;
                entity.Location = command.Location;

                await _context.SaveChangesAsync(cancellationToken);
                return entity.PublisherId;
            }
        }
    }
}