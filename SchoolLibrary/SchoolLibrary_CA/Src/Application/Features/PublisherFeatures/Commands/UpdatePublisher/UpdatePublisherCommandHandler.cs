using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PublisherFeatures.Commands.UpdatePublisher
{
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

            entity = MapsterFunctions.MapSourceToDestination<UpdatePublisherCommand, Publisher>(command);

            _context.Publishers.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.PublisherId;
        }
    }
}